// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class SymbolDefinitionListBuilder
    {
        private static readonly SymbolDisplayFormat _typeFormat = SymbolDisplayFormats.FullDefinition.WithTypeQualificationStyle(SymbolDisplayTypeQualificationStyle.NameOnly);

        private static readonly SymbolDisplayFormat _nameAndContainingNamesAndNameSpacesFormat = new SymbolDisplayFormat(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters);

        private bool _pendingIndentation;

        public SymbolDefinitionListBuilder(StringBuilder stringBuilder = null)
        {
            StringBuilder = stringBuilder ?? new StringBuilder();
        }

        public int IndentationLevel { get; set; }

        public int IndentationSize { get; set; } = 2;

        public char IndentationChar { get; set; } = ' ';

        public StringBuilder StringBuilder { get; }

        public int Length => StringBuilder.Length;

        private INamespaceSymbol CurrentNamespace { get; set; }

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
                    AppendTypes(typeSymbols.Where(f => f.ContainingNamespace.IsGlobalNamespace));
                }
                else
                {
                    AppendSymbol(namespaceSymbol, SymbolDisplayFormats.NamespaceDefinition);
                    AppendLine();
                    AppendLine("{");

                    CurrentNamespace = namespaceSymbol;
                    IncreaseIndentation();
                    CurrentNamespace = null;

                    AppendTypes(grouping);

                    DecreaseIndentation();

                    AppendLine("}");
                    AppendLine();
                }
            }

            StringBuilder sb = StringBuilderCache.GetInstance();
            foreach (INamespaceSymbol namespaceSymbol in Namespaces.OrderBy(f => f, NamespaceDefinitionComparer.Instance))
            {
                sb.Append("using ");
                sb.Append(namespaceSymbol.ToDisplayString(_nameAndContainingNamesAndNameSpacesFormat));
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

                        //TODO: sort interfaces
                        Append(SymbolDefinitionBuilder.GetDisplayParts(en.Current, _typeFormat, SymbolDisplayTypeDeclarationOptions.IncludeAccessibility | SymbolDisplayTypeDeclarationOptions.IncludeModifiers));
                        AppendLine();

                        switch (typeKind)
                        {
                            case TypeKind.Class:
                                {
                                    AppendLine("{");
                                    IncreaseIndentation();

                                    AppendMembers(en.Current);

                                    DecreaseIndentation();
                                    AppendLine("}");
                                    break;
                                }
                            case TypeKind.Delegate:
                                {
                                    break;
                                }
                            case TypeKind.Enum:
                                {
                                    AppendLine("{");
                                    IncreaseIndentation();

                                    foreach (ISymbol member in en.Current.GetMembers())
                                    {
                                        if (member.Kind == SymbolKind.Field
                                            && member.DeclaredAccessibility == Accessibility.Public)
                                        {
                                            AppendSymbol(member, SymbolDisplayFormats.FullDefinition);
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
                                    AppendLine("{");
                                    IncreaseIndentation();

                                    AppendMembers(en.Current);

                                    DecreaseIndentation();
                                    AppendLine("}");
                                    break;
                                }
                            case TypeKind.Struct:
                                {
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

        //TODO:  explicit implementations ?
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
                        Append(SymbolDefinitionBuilder.GetDisplayParts(en.Current, SymbolDisplayFormats.FullDefinition));

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

        //TODO: sort attributes
        private void AppendAttributes(ISymbol symbol)
        {
            foreach (AttributeData attributeData in symbol.GetAttributes())
            {
                INamedTypeSymbol attribute = attributeData.AttributeClass;

                if (!DocumentationUtility.ShouldBeHidden(attribute))
                {
                    Append("[");
                    AppendSymbol(attribute, _nameAndContainingNamesAndNameSpacesFormat);
                    Append("]");
                    AppendLine();
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

                        if (containingNamespace?.IsGlobalNamespace == false
                            && containingNamespace != CurrentNamespace)
                        {
                            Namespaces.Add(containingNamespace);
                        }
                    }
                }

                StringBuilder.Append(part);
            }
        }

        public void Append(string value)
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
            _pendingIndentation = true;
        }

        public void AppendLine(string value)
        {
            Append(value);
            AppendLine();
        }

        public void AppendIndentation()
        {
            for (int i = 0; i < IndentationLevel; i++)
            {
                Append(IndentationChar, IndentationSize);
            }
        }

        public void IncreaseIndentation()
        {
            IndentationLevel++;
        }

        public void DecreaseIndentation()
        {
            IndentationLevel--;
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
