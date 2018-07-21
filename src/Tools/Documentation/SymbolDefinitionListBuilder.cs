// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class SymbolDefinitionListBuilder
    {
        private bool _pendingIndentation;

        public SymbolDefinitionListBuilder(StringBuilder stringBuilder = null)
        {
            StringBuilder = stringBuilder ?? new StringBuilder();
        }

        public int IndentationLevel { get; set; }

        public int IndentationSize { get; set; } = 2;

        public char IndentationChar { get; set; } = ' ';

        public HashSet<INamespaceSymbol> Namespaces { get; }

        public StringBuilder StringBuilder { get; }

        public int Length => StringBuilder.Length;

        public void AppendSymbols(IEnumerable<INamedTypeSymbol> typeSymbols)
        {
            foreach (IGrouping<INamespaceSymbol, INamedTypeSymbol> grouping in typeSymbols
               .Where(f => !f.ContainingNamespace.IsGlobalNamespace)
               .GroupBy(f => f.ContainingNamespace, MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
               .OrderBy(f => f.Key.ToDisplayString(SymbolDisplayFormats.NamespaceDefinition)))
            {
                AppendSymbol(grouping.Key, SymbolDisplayFormats.NamespaceDefinition);
                AppendLine();
                AppendLine("{");

                IncreaseIndentation();

                AppendTypes(grouping);

                DecreaseIndentation();

                AppendLine("}");
                AppendLine();
            }

            AppendTypes(typeSymbols.Where(f => f.ContainingNamespace.IsGlobalNamespace));
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

                        AppendSymbol(en.Current, SymbolDisplayFormats.FullDefinition, SymbolDisplayTypeDeclarationOptions.IncludeAccessibility | SymbolDisplayTypeDeclarationOptions.IncludeModifiers);
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

        private void AppendMembers(INamedTypeSymbol typeSymbol)
        {
            bool isAny = false;

            foreach (ISymbol member in typeSymbol.GetMembers(f =>
            {
                if (!f.IsPubliclyVisible())
                    return false;

                switch (f.Kind)
                {
                    case SymbolKind.Event:
                    case SymbolKind.Field:
                    case SymbolKind.Property:
                        {
                            return true;
                        }
                    case SymbolKind.Method:
                        {
                            var methodSymbol = (IMethodSymbol)f;

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
                            Debug.Fail(f.Kind.ToString());
                            return false;
                        }
                }
            })
            .OrderBy(f => f, MemberDefinitionComparer.Instance))
            {
                AppendSymbol(member, SymbolDisplayFormats.FullDefinition);

                if (member.Kind != SymbolKind.Property)
                    Append(";");

                AppendLine();

                isAny = true;
            }

            AppendTypes(typeSymbol.GetTypeMembers().Where(f => f.IsPubliclyVisible()), insertNewLineBeforeFirstType: isAny);
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

        public void AppendSymbol(ISymbol symbol, SymbolDisplayFormat format)
        {
            CheckPendingIndentation();
            StringBuilder.Append(symbol.ToDisplayString(format));
        }

        public void AppendSymbol(INamedTypeSymbol symbol, SymbolDisplayFormat format, SymbolDisplayTypeDeclarationOptions typeDeclarationOptions = SymbolDisplayTypeDeclarationOptions.None)
        {
            CheckPendingIndentation();
            StringBuilder.Append(symbol.ToDisplayString(format, typeDeclarationOptions));
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
