// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

#pragma warning disable RCS1085

namespace Roslynator.Documentation
{
    public class SymbolDefinitionListBuilder
    {
        private bool _pendingIndentation;
        private readonly StringBuilder _sb;

        public SymbolDefinitionListBuilder(StringBuilder stringBuilder = null)
        {
            _sb = stringBuilder ?? new StringBuilder();
        }

        public int IndentationLevel { get; set; }

        public int IndentationSize { get; set; } = 2;

        public char IndentationChar { get; set; } = ' ';

        public HashSet<INamespaceSymbol> Namespaces { get; }

        public StringBuilder StringBuilder
        {
            get { return _sb; }
        }

        public void AppendSymbols(IEnumerable<INamedTypeSymbol> typeSymbols)
        {
            foreach (IGrouping<INamespaceSymbol, INamedTypeSymbol> grouping in typeSymbols
               .GroupBy(f => f.ContainingNamespace, MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
               .OrderBy(f => f.Key.ToDisplayString(SymbolDisplayFormats.NamespaceDefinition)))
            {
                INamespaceSymbol namespaceSymbol = grouping.Key;

                AppendSymbol(namespaceSymbol, SymbolDisplayFormats.NamespaceDefinition);
                AppendLine();
                AppendLine("{");

                IncreaseIndentation();

                foreach (IGrouping<TypeKind, INamedTypeSymbol> grouping2 in grouping
                    .GroupBy(f => f.TypeKind)
                    .OrderBy(f => f.Key.ToNamespaceDocumentationPart(), DocumentationOptions.Default.NamespacePartComparer))
                {
                    TypeKind typeKind = grouping2.Key;

                    foreach (INamedTypeSymbol typeSymbol in grouping2
                        .OrderBy(f => f.ToDisplayString(SymbolDisplayFormats.FullDefinition)))
                    {
                        AppendSymbol(typeSymbol, SymbolDisplayFormats.FullDefinition, SymbolDisplayTypeDeclarationOptions.IncludeAccessibility | SymbolDisplayTypeDeclarationOptions.IncludeModifiers);
                        AppendLine();

                        switch (typeKind)
                        {
                            case TypeKind.Class:
                                {
                                    AppendLine("{");
                                    IncreaseIndentation();
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

                                    foreach (ISymbol member in typeSymbol.GetMembers())
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
                                    DecreaseIndentation();
                                    AppendLine("}");
                                    break;
                                }
                            case TypeKind.Struct:
                                {
                                    AppendLine("{");
                                    IncreaseIndentation();
                                    DecreaseIndentation();
                                    AppendLine("}");
                                    break;
                                }
                        }
                    }
                }

                DecreaseIndentation();

                AppendLine("}");
                AppendLine();
            }
        }

        public void Append(string value)
        {
            CheckPendingIndentation();
            _sb.Append(value);
        }

        public void Append(object value)
        {
            CheckPendingIndentation();
            _sb.Append(value);
        }

        public void Append(char value, int repeatCount)
        {
            CheckPendingIndentation();
            _sb.Append(value, repeatCount);
        }

        public void AppendSymbol(ISymbol symbol, SymbolDisplayFormat format)
        {
            CheckPendingIndentation();
            _sb.Append(symbol.ToDisplayString(format));
        }

        public void AppendSymbol(INamedTypeSymbol symbol, SymbolDisplayFormat format, SymbolDisplayTypeDeclarationOptions typeDeclarationOptions = SymbolDisplayTypeDeclarationOptions.None)
        {
            CheckPendingIndentation();
            _sb.Append(symbol.ToDisplayString(format, typeDeclarationOptions));
        }

        public void AppendLine()
        {
            _sb.AppendLine();
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
            return _sb.ToString();
        }
    }
}
