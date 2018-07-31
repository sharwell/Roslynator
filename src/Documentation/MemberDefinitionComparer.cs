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

            int result = ((int)GetKind(x)).CompareTo((int)GetKind(y));

            if (result != 0)
                return result;

            result = string.Compare(x.Name, y.Name, StringComparison.Ordinal);

            if (result != 0)
                return result;

            return string.CompareOrdinal(
                x.ToDisplayString(SymbolDisplayFormats.SortDefinitionList),
                y.ToDisplayString(SymbolDisplayFormats.SortDefinitionList));
        }

        public static MemberDefinitionKind GetKind(ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Event:
                    {
                        return MemberDefinitionKind.Event;
                    }
                case SymbolKind.Field:
                    {
                        var fieldSymbol = (IFieldSymbol)symbol;

                        if (fieldSymbol.IsConst)
                            return MemberDefinitionKind.Const;

                        return MemberDefinitionKind.Field;
                    }
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        switch (methodSymbol.MethodKind)
                        {
                            case MethodKind.Constructor:
                                return MemberDefinitionKind.Constructor;
                            case MethodKind.Conversion:
                                return MemberDefinitionKind.ConversionOperator;
                            case MethodKind.UserDefinedOperator:
                                return MemberDefinitionKind.Operator;
                            case MethodKind.Ordinary:
                                return MemberDefinitionKind.Method;
                        }

                        break;
                    }
                case SymbolKind.Property:
                    {
                        var propertySymbol = (IPropertySymbol)symbol;

                        if (propertySymbol.IsIndexer)
                            return MemberDefinitionKind.Indexer;

                        return MemberDefinitionKind.Property;
                    }
            }

            Debug.Fail(symbol.ToDisplayString(Roslynator.SymbolDisplayFormats.Test));

            return MemberDefinitionKind.None;
        }
    }
}
