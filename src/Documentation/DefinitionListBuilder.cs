// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal class DefinitionListBuilder
    {
        private static readonly SymbolDisplayFormat _namespaceFormat = SymbolDisplayFormats.NamespaceDefinition;

        private static readonly SymbolDisplayFormat _typeFormat = SymbolDisplayFormats.FullDefinition.Update(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly);

        private static readonly SymbolDisplayFormat _memberFormat = SymbolDisplayFormats.FullDefinition.Update(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

        private static readonly SymbolDisplayFormat _enumFieldFormat = SymbolDisplayFormats.FullDefinition;

        private bool _pendingIndentation;
        private int _indentationLevel;
        private INamespaceSymbol _currentNamespace;

        public DefinitionListBuilder(StringBuilder stringBuilder = null, DefinitionListOptions options = null)
        {
            StringBuilder = stringBuilder ?? new StringBuilder();
            Options = options ?? DefinitionListOptions.Default;
        }

        public StringBuilder StringBuilder { get; }

        public DefinitionListOptions Options { get; }

        public int Length => StringBuilder.Length;

        internal HashSet<INamespaceSymbol> Namespaces { get; } = new HashSet<INamespaceSymbol>(MetadataNameEqualityComparer<INamespaceSymbol>.Instance);

        public virtual IComparer<INamespaceSymbol> NamespaceComparer => NamespaceSymbolComparer.SystemFirst;

        public virtual IComparer<INamedTypeSymbol> TypeComparer => TypeDefinitionComparer.Instance;

        public virtual IComparer<ISymbol> MemberComparer => MemberDefinitionComparer.Instance;

        public virtual bool IsVisibleType(ISymbol symbol)
        {
            return symbol.IsPubliclyVisible();
        }

        public virtual bool IsVisibleMember(ISymbol symbol)
        {
            if (!symbol.IsPubliclyVisible())
                return false;

            switch (symbol.Kind)
            {
                case SymbolKind.Event:
                case SymbolKind.Field:
                case SymbolKind.Property:
                    {
                        return true;
                    }
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        switch (methodSymbol.MethodKind)
                        {
                            case MethodKind.Constructor:
                                {
                                    return methodSymbol.ContainingType.TypeKind != TypeKind.Struct
                                        || methodSymbol.Parameters.Any();
                                }
                            case MethodKind.Conversion:
                            case MethodKind.UserDefinedOperator:
                            case MethodKind.Ordinary:
                                return true;
                            case MethodKind.ExplicitInterfaceImplementation:
                            case MethodKind.StaticConstructor:
                            case MethodKind.Destructor:
                            case MethodKind.EventAdd:
                            case MethodKind.EventRaise:
                            case MethodKind.EventRemove:
                            case MethodKind.PropertyGet:
                            case MethodKind.PropertySet:
                                return false;
                            default:
                                {
                                    Debug.Fail(methodSymbol.MethodKind.ToString());
                                    break;
                                }
                        }

                        return true;
                    }
                case SymbolKind.NamedType:
                    {
                        return false;
                    }
                default:
                    {
                        Debug.Fail(symbol.Kind.ToString());
                        return false;
                    }
            }
        }

        public virtual bool IsVisibleAttribute(INamedTypeSymbol attributeType)
        {
            return DocumentationUtility.IsVisibleAttribute(attributeType);
        }

        public void Append(DocumentationModel documentationModel)
        {
            foreach (NamespaceDocumentationModel namespaceModel in documentationModel.NamespaceModels.OrderBy(f => f.NamespaceSymbol, NamespaceComparer))
            {
                INamespaceSymbol namespaceSymbol = namespaceModel.NamespaceSymbol;

                if (!namespaceSymbol.IsGlobalNamespace)
                {
                    Append(namespaceSymbol, _namespaceFormat);
                    BeginTypeContent();
                }

                _currentNamespace = namespaceSymbol;
                AppendTypes(namespaceModel.TypeModels.Select(f => f.TypeSymbol).Where(f => f.ContainingType == null));
                _currentNamespace = null;

                if (!namespaceSymbol.IsGlobalNamespace)
                {
                    EndTypeContent();
                    AppendLine();
                }
            }
        }

        private void AppendTypes(IEnumerable<INamedTypeSymbol> types, bool insertNewLineBeforeFirstType = false)
        {
            using (IEnumerator<INamedTypeSymbol> en = types.OrderBy(f => f, TypeComparer).GetEnumerator())
            {
                if (en.MoveNext())
                {
                    if (insertNewLineBeforeFirstType)
                        AppendLine();

                    while (true)
                    {
                        TypeKind typeKind = en.Current.TypeKind;

                        Append(SymbolDefinitionBuilder.GetDisplayParts(
                            en.Current,
                            _typeFormat,
                            SymbolDisplayTypeDeclarationOptions.IncludeAccessibility | SymbolDisplayTypeDeclarationOptions.IncludeModifiers,
                            isVisibleAttribute: IsVisibleAttribute,
                            newLineOnAttributes: Options.NewLineOnAttributes,
                            attributeArguments: Options.AttributeArguments,
                            omitIEnumerable: Options.OmitIEnumerable));

                        switch (typeKind)
                        {
                            case TypeKind.Class:
                                {
                                    BeginTypeContent();
                                    AppendMembers(en.Current);
                                    EndTypeContent();
                                    break;
                                }
                            case TypeKind.Delegate:
                                {
                                    AppendLine(";");
                                    break;
                                }
                            case TypeKind.Enum:
                                {
                                    BeginTypeContent();

                                    foreach (ISymbol member in en.Current.GetMembers())
                                    {
                                        if (member.Kind == SymbolKind.Field
                                            && member.DeclaredAccessibility == Accessibility.Public)
                                        {
                                            Append(member, _enumFieldFormat);
                                            Append(",");
                                            AppendLine();
                                        }
                                    }

                                    EndTypeContent();
                                    break;
                                }
                            case TypeKind.Interface:
                                {
                                    BeginTypeContent();
                                    AppendMembers(en.Current);
                                    EndTypeContent();
                                    break;
                                }
                            case TypeKind.Struct:
                                {
                                    BeginTypeContent();
                                    AppendMembers(en.Current);
                                    EndTypeContent();
                                    break;
                                }
                        }

                        if (en.MoveNext())
                        {
                            AppendLine();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void BeginTypeContent()
        {
            if (Options.OpenBraceOnNewLine)
            {
                AppendLine();
                AppendLine("{");
            }
            else
            {
                AppendLine(" {");
            }

            _indentationLevel++;
        }

        private void EndTypeContent()
        {
            Debug.Assert(_indentationLevel > 0, "Cannot decrease indentation.");

            _indentationLevel--;

            AppendLine("}");
        }

        private void AppendMembers(INamedTypeSymbol typeModel)
        {
            bool isAny = false;

            using (IEnumerator<ISymbol> en = typeModel.GetMembers().Where(f => IsVisibleMember(f))
                .OrderBy(f => f, MemberComparer)
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    MemberDefinitionKind kind = MemberDefinitionComparer.GetKind(en.Current);

                    while (true)
                    {
                        Append(SymbolDefinitionBuilder.GetAttributesParts(
                            en.Current.GetAttributes(),
                            predicate: IsVisibleAttribute,
                            newLineOnAttributes: Options.NewLineOnAttributes,
                            attributeArguments: Options.AttributeArguments));

                        Append(en.Current.ToDisplayParts(_memberFormat));

                        if (en.Current.Kind != SymbolKind.Property)
                            Append(";");

                        AppendLine();

                        isAny = true;

                        if (en.MoveNext())
                        {
                            MemberDefinitionKind kind2 = MemberDefinitionComparer.GetKind(en.Current);

                            if (kind != kind2)
                            {
                                AppendLine();
                            }
                            else if (Options.EmptyLineBetweenMembers)
                            {
                                AppendLine();
                            }

                            kind = kind2;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            AppendTypes(typeModel.GetTypeMembers().Where(f => IsVisibleType(f)), insertNewLineBeforeFirstType: isAny);
        }

        public void Append(ISymbol symbol, SymbolDisplayFormat format)
        {
            Append(symbol.ToDisplayParts(format));
        }

        public void Append(INamedTypeSymbol symbol, SymbolDisplayFormat format, SymbolDisplayTypeDeclarationOptions typeDeclarationOptions = SymbolDisplayTypeDeclarationOptions.None)
        {
            Append(symbol.ToDisplayParts(format, typeDeclarationOptions));
        }

        private void Append(ImmutableArray<SymbolDisplayPart> parts)
        {
            foreach (SymbolDisplayPart part in parts)
            {
                CheckPendingIndentation();

                if (part.IsTypeName())
                {
                    ISymbol symbol = part.Symbol;

                    if (symbol != null)
                    {
                        INamespaceSymbol containingNamespace = symbol.ContainingNamespace;

                        if (!containingNamespace.IsGlobalNamespace
                            && ShouldAddNamespace(containingNamespace))
                        {
                            Namespaces.Add(containingNamespace);
                        }
                    }
                }

                StringBuilder.Append(part);

                if (part.Kind == SymbolDisplayPartKind.LineBreak
                    && Options.Indent)
                {
                    _pendingIndentation = true;
                }
            }

            bool ShouldAddNamespace(INamespaceSymbol containingNamespace)
            {
                INamespaceSymbol n = _currentNamespace;

                do
                {
                    if (MetadataNameEqualityComparer<INamespaceSymbol>.Instance.Equals(containingNamespace, n))
                        return false;

                    n = n.ContainingNamespace;
                }
                while (n?.IsGlobalNamespace == false);

                return true;
            }
        }

        public void Append(string value)
        {
            CheckPendingIndentation();
            StringBuilder.Append(value);
        }

        public void Append(char value)
        {
            CheckPendingIndentation();
            StringBuilder.Append(value);
        }

        public void Append(object value)
        {
            CheckPendingIndentation();
            StringBuilder.Append(value);
        }

        public void Append(char value, int repeatCount)
        {
            CheckPendingIndentation();
            StringBuilder.Append(value, repeatCount);
        }

        public void AppendLine()
        {
            StringBuilder.AppendLine();

            if (Options.Indent)
                _pendingIndentation = true;
        }

        public void AppendLine(string value)
        {
            Append(value);
            AppendLine();
        }

        public void AppendIndentation()
        {
            for (int i = 0; i < _indentationLevel; i++)
            {
                Append(Options.IndentChars);
            }
        }

        private void CheckPendingIndentation()
        {
            if (_pendingIndentation)
            {
                _pendingIndentation = false;
                AppendIndentation();
            }
        }

        public override string ToString()
        {
            return StringBuilder.ToString();
        }
    }
}
