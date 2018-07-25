// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal sealed class MemberDefinitionComparer : IComparer<ISymbol>
    {
        public static MemberDefinitionComparer Instance { get; } = new MemberDefinitionComparer();

        public int Compare(ISymbol x, ISymbol y)
        {
            if (object.ReferenceEquals(x, y))
                return 0;

            if (x == null)
                return -1;

            if (y == null)
                return 1;

            int result = GetRank(x).CompareTo(GetRank(y));

            if (result != 0)
                return result;

            result = string.Compare(x.Name, y.Name, StringComparison.Ordinal);

            if (result != 0)
                return result;

            return string.CompareOrdinal(
                x.ToDisplayString(SymbolDisplayFormats.SortDefinitionList),
                y.ToDisplayString(SymbolDisplayFormats.SortDefinitionList));
        }

        public static int GetRank(ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Event:
                    {
                        return 5;
                    }
                case SymbolKind.Field:
                    {
                        var fieldSymbol = (IFieldSymbol)symbol;

                        if (fieldSymbol.IsConst)
                            return 1;

                        return 2;
                    }
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        switch (methodSymbol.MethodKind)
                        {
                            case MethodKind.StaticConstructor:
                                return 3;
                            case MethodKind.Constructor:
                                return 4;
                            case MethodKind.Conversion:
                                return 9;
                            case MethodKind.UserDefinedOperator:
                                return 10;
                            case MethodKind.Ordinary:
                                return 8;
                        }

                        break;
                    }
                case SymbolKind.Property:
                    {
                        var propertySymbol = (IPropertySymbol)symbol;

                        if (propertySymbol.IsIndexer)
                            return 6;

                        return 7;
                    }
            }

            Debug.Fail(symbol.ToDisplayString(Roslynator.SymbolDisplayFormats.Test));

            return 0;
        }
    }
}
