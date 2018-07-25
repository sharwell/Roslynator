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

        private static readonly SymbolDisplayFormat _typeFormat = SymbolDisplayFormats.FullDefinition
            .WithTypeQualificationStyle(SymbolDisplayTypeQualificationStyle.NameOnly);

        private static readonly SymbolDisplayFormat _memberFormat = SymbolDisplayFormats.FullDefinition
            .WithTypeQualificationStyle(SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

        private static readonly SymbolDisplayFormat _enumFieldFormat = SymbolDisplayFormats.FullDefinition;

        private bool _pendingIndent;
        private int _indentationLevel;
        private INamespaceSymbol _currentNamespace;

        public DefinitionListBuilder(DefinitionListOptions options = null, StringBuilder stringBuilder = null)
        {
            Options = options ?? DefinitionListOptions.Default;
            StringBuilder = stringBuilder ?? new StringBuilder();
        }

        public DefinitionListOptions Options { get; }

        public StringBuilder StringBuilder { get; }

        public int Length => StringBuilder.Length;

        public virtual IComparer<INamespaceSymbol> NamespaceComparer { get; }

        private HashSet<INamespaceSymbol> Namespaces { get; } = new HashSet<INamespaceSymbol>(MetadataNameEqualityComparer<INamespaceSymbol>.Instance);

        public void AppendSymbols(IEnumerable<INamedTypeSymbol> typeSymbols)
        {
            foreach (IGrouping<INamespaceSymbol, INamedTypeSymbol> grouping in typeSymbols
               .GroupBy(f => f.ContainingNamespace, MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
               .OrderBy(f => f.Key, NamespaceDefinitionComparer.Instance))
            {
                INamespaceSymbol namespaceSymbol = grouping.Key;

                if (namespaceSymbol.IsGlobalNamespace)
                {
                    _currentNamespace = namespaceSymbol;
                    AppendTypes(typeSymbols.Where(f => f.ContainingNamespace.IsGlobalNamespace));
                    _currentNamespace = null;
                }
                else
                {
                    AppendSymbol(namespaceSymbol, _namespaceFormat);
                    AppendLine();
                    AppendLine("{");

                    IncreaseIndentation();

                    _currentNamespace = namespaceSymbol;
                    AppendTypes(grouping);
                    _currentNamespace = null;

                    DecreaseIndentation();

                    AppendLine("}");
                    AppendLine();
                }
            }

            StringBuilder sb = StringBuilderCache.GetInstance();
            foreach (INamespaceSymbol namespaceSymbol in Namespaces.OrderBy(f => f, NamespaceDefinitionComparer.Instance))
            {
                sb.Append("using ");
                sb.Append(namespaceSymbol.ToDisplayString(SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespacesAndTypeParameters));
                sb.AppendLine(";");
            }

            sb.AppendLine();

            StringBuilder.Insert(0, StringBuilderCache.GetStringAndFree(sb));

            Namespaces.Clear();
        }

        private void AppendTypes(IEnumerable<INamedTypeSymbol> typeSymbols, bool insertNewLineBeforeFirstType = false)
        {
            using (IEnumerator<INamedTypeSymbol> en = typeSymbols.OrderBy(f => f, TypeDefinitionComparer.Instance).GetEnumerator())
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
                                    AppendLine();
                                    AppendLine("{");
                                    IncreaseIndentation();

                                    AppendMembers(en.Current);

                                    DecreaseIndentation();
                                    AppendLine("}");
                                    break;
                                }
                            case TypeKind.Delegate:
                                {
                                    AppendLine(";");
                                    break;
                                }
                            case TypeKind.Enum:
                                {
                                    AppendLine();
                                    AppendLine("{");
                                    IncreaseIndentation();

                                    foreach (ISymbol member in en.Current.GetMembers())
                                    {
                                        if (member.Kind == SymbolKind.Field
                                            && member.DeclaredAccessibility == Accessibility.Public)
                                        {
                                            AppendSymbol(member, _enumFieldFormat);
                                            Append(",");
                                            AppendLine();
                                        }
                                    }

                                    DecreaseIndentation();
                                    AppendLine("}");
                                    break;
                                }
                            case TypeKind.Interface:
                                {
                                    AppendLine();
                                    AppendLine("{");
                                    IncreaseIndentation();

                                    AppendMembers(en.Current);

                                    DecreaseIndentation();
                                    AppendLine("}");
                                    break;
                                }
                            case TypeKind.Struct:
                                {
                                    AppendLine();
                                    AppendLine("{");
                                    IncreaseIndentation();

                                    AppendMembers(en.Current);

                                    DecreaseIndentation();
                                    AppendLine("}");
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

        private void AppendMembers(INamedTypeSymbol typeSymbol)
        {
            bool isAny = false;

            using (IEnumerator<ISymbol> en = typeSymbol.GetMembers(Predicate)
                .OrderBy(f => f, MemberDefinitionComparer.Instance).GetEnumerator())
            {
                if (en.MoveNext())
                {
                    int rank = MemberDefinitionComparer.GetRank(en.Current);

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
                            int rank2 = MemberDefinitionComparer.GetRank(en.Current);

                            if (rank != rank2)
                                AppendLine();

                            rank = rank2;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            AppendTypes(typeSymbol.GetTypeMembers().Where(f => f.IsPubliclyVisible()), insertNewLineBeforeFirstType: isAny);

            bool Predicate(ISymbol symbol)
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
                                case MethodKind.ExplicitInterfaceImplementation:
                                case MethodKind.UserDefinedOperator:
                                case MethodKind.Ordinary:
                                case MethodKind.StaticConstructor:
                                    return true;
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
        }

        private void AppendAttributes(ISymbol symbol)
        {
            foreach (AttributeData attributeData in GetAttributes()
                .OrderBy(f => f.AttributeClass, TypeDefinitionComparer.Instance))
            {
                Append("[");
                AppendSymbol(attributeData.AttributeClass, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespacesAndTypeParameters);

                ImmutableArray<TypedConstant> constructorArguments = attributeData.ConstructorArguments;

                ImmutableArray<KeyValuePair<string, TypedConstant>> namedArguments = attributeData.NamedArguments;

                bool hasConstructorArgument = false;
                bool hasNamedArgument = false;

                ImmutableArray<TypedConstant>.Enumerator en = constructorArguments.GetEnumerator();

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

                ImmutableArray<KeyValuePair<string, TypedConstant>>.Enumerator en2 = namedArguments.GetEnumerator();

                if (en2.MoveNext())
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
                        AppendNamedArgument(en2.Current);

                        if (en2.MoveNext())
                        {
                            Append(", ");
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (hasConstructorArgument || hasNamedArgument)
                {
                    Append(")");
                }

                Append("]");
                AppendLine();
            }

            IEnumerable<AttributeData> GetAttributes()
            {
                foreach (AttributeData attributeData in symbol.GetAttributes())
                {
                    INamedTypeSymbol attribute = attributeData.AttributeClass;

                    if (!DocumentationUtility.ShouldBeHidden(attribute))
                        yield return attributeData;
                }
            }

            void AppendNamedArgument(KeyValuePair<string, TypedConstant> kvp)
            {
                Append(kvp.Key);
                Append(" = ");
                AppendConstantValue(kvp.Value);
            }

            void AppendConstantValue(TypedConstant typedConstant)
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
                                    AppendSymbol(en.Current.Symbol, SymbolDisplayFormats.EnumFieldFullName);

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
                                AppendSymbol((INamedTypeSymbol)typedConstant.Type, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespacesAndTypeParameters);
                                Append(")");
                                Append(typedConstant.Value);
                            }

                            break;
                        }
                    case TypedConstantKind.Type:
                        {
                            Append("typeof(");
                            AppendSymbol((ISymbol)typedConstant.Value, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespacesAndTypeParameters);
                            Append(")");

                            break;
                        }
                    case TypedConstantKind.Array:
                        {
                            var arrayType = (IArrayTypeSymbol)typedConstant.Type;
                            Append("new ");
                            AppendSymbol(arrayType.ElementType, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespacesAndTypeParameters);
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
        }

        public void AppendSymbol(ISymbol symbol, SymbolDisplayFormat format)
        {
            Append(symbol.ToDisplayParts(format));
        }

        public void AppendSymbol(INamedTypeSymbol symbol, SymbolDisplayFormat format, SymbolDisplayTypeDeclarationOptions typeDeclarationOptions = SymbolDisplayTypeDeclarationOptions.None)
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
                _pendingIndent = true;
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

        public void IncreaseIndentation()
        {
            _indentationLevel++;
        }

        public void DecreaseIndentation()
        {
            Debug.Assert(_indentationLevel  > 0, "Cannot decrease indentation.");
            _indentationLevel--;
        }

        private void CheckPendingIndentation()
        {
            if (_pendingIndent)
            {
                _pendingIndent = false;
                AppendIndentation();
            }
        }

        public override string ToString()
        {
            return StringBuilder.ToString();
        }
    }
}
