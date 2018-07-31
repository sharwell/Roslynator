// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

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

        public virtual IComparer<INamespaceSymbol> NamespaceComparer => NamespaceDefinitionComparer.Instance;

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
                AppendTypes(namespaceModel.Types.Select(f => f.TypeSymbol).Where(f => f.ContainingType == null));
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

                        AppendAttributes(en.Current);

                        Append(SymbolDefinitionBuilder.GetDisplayParts(
                            en.Current,
                            _typeFormat,
                            SymbolDisplayTypeDeclarationOptions.IncludeAccessibility | SymbolDisplayTypeDeclarationOptions.IncludeModifiers,
                            attributePredicate: _ => false));

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
                        AppendAttributes(en.Current);
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

        private void AppendAttributes(ISymbol symbol)
        {
            using (IEnumerator<AttributeData> en = symbol.GetAttributes()
                .Where(f => IsVisibleAttribute(f.AttributeClass))
                .OrderBy(f => f.AttributeClass, TypeComparer).GetEnumerator())
            {
                if (en.MoveNext())
                {
                    Append("[");

                    while (true)
                    {
                        Append(en.Current.AttributeClass, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespacesAndTypeParameters);

                        if (Options.AttributeArguments)
                            AppendAttributeArguments(en.Current);

                        if (en.MoveNext())
                        {
                            if (Options.NewLineOnAttributes)
                            {
                                AppendLine("]");
                                Append("[");
                            }
                            else
                            {
                                Append(", ");
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    AppendLine("]");
                }
            }
        }

        private void AppendAttributeArguments(AttributeData attributeData)
        {
            bool hasConstructorArgument = false;
            bool hasNamedArgument = false;

            AppendConstructorArguments();
            AppendNamedArguments();

            if (hasConstructorArgument || hasNamedArgument)
            {
                Append(")");
            }

            void AppendConstructorArguments()
            {
                ImmutableArray<TypedConstant>.Enumerator en = attributeData.ConstructorArguments.GetEnumerator();

                if (en.MoveNext())
                {
                    hasConstructorArgument = true;
                    Append("(");

                    while (true)
                    {
                        AppendConstantValue(en.Current);

                        if (en.MoveNext())
                        {
                            Append(", ");
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            void AppendNamedArguments()
            {
                ImmutableArray<KeyValuePair<string, TypedConstant>>.Enumerator en = attributeData.NamedArguments.GetEnumerator();

                if (en.MoveNext())
                {
                    hasNamedArgument = true;

                    if (hasConstructorArgument)
                    {
                        Append(", ");
                    }
                    else
                    {
                        Append("(");
                    }

                    while (true)
                    {
                        Append(en.Current.Key);
                        Append(" = ");
                        AppendConstantValue(en.Current.Value);

                        if (en.MoveNext())
                        {
                            Append(", ");
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void AppendConstantValue(TypedConstant typedConstant)
        {
            switch (typedConstant.Kind)
            {
                case TypedConstantKind.Primitive:
                    {
                        Append(SymbolDisplay.FormatPrimitive(typedConstant.Value, quoteStrings: true, useHexadecimalNumbers: false));
                        break;
                    }
                case TypedConstantKind.Enum:
                    {
                        OneOrMany<EnumFieldInfo> oneOrMany = EnumUtility.GetConstituentFields(typedConstant.Value, (INamedTypeSymbol)typedConstant.Type);

                        OneOrMany<EnumFieldInfo>.Enumerator en = oneOrMany.GetEnumerator();

                        if (en.MoveNext())
                        {
                            while (true)
                            {
                                Append(en.Current.Symbol, SymbolDisplayFormats.EnumFieldFullName);

                                if (en.MoveNext())
                                {
                                    Append(" | ");
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Append("(");
                            Append((INamedTypeSymbol)typedConstant.Type, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespacesAndTypeParameters);
                            Append(")");
                            Append(typedConstant.Value);
                        }

                        break;
                    }
                case TypedConstantKind.Type:
                    {
                        Append("typeof(");
                        Append((ISymbol)typedConstant.Value, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespacesAndTypeParameters);
                        Append(")");

                        break;
                    }
                case TypedConstantKind.Array:
                    {
                        var arrayType = (IArrayTypeSymbol)typedConstant.Type;
                        Append("new ");
                        Append(arrayType.ElementType, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespacesAndTypeParameters);
                        Append("[] { ");

                        ImmutableArray<TypedConstant>.Enumerator en = typedConstant.Values.GetEnumerator();

                        if (en.MoveNext())
                        {
                            while (true)
                            {
                                AppendConstantValue(en.Current);

                                if (en.MoveNext())
                                {
                                    Append(", ");
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }

                        Append(" }");
                        break;
                    }
                default:
                    {
                        throw new InvalidOperationException();
                    }
            }
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
            CheckPendingIndentation();

            foreach (SymbolDisplayPart part in parts)
            {
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
