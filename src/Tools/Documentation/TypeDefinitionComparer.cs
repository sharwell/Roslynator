// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal sealed class TypeDefinitionComparer : IComparer<INamedTypeSymbol>
    {
        public static TypeDefinitionComparer Instance { get; } = new TypeDefinitionComparer();

        public int Compare(INamedTypeSymbol x, INamedTypeSymbol y)
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

            return string.Compare(
                x.ToDisplayString(SymbolDisplayFormats.FullDefinition, SymbolDisplayTypeDeclarationOptions.IncludeAccessibility | SymbolDisplayTypeDeclarationOptions.IncludeModifiers),
                y.ToDisplayString(SymbolDisplayFormats.FullDefinition, SymbolDisplayTypeDeclarationOptions.IncludeAccessibility | SymbolDisplayTypeDeclarationOptions.IncludeModifiers),
                StringComparison.Ordinal);
        }

        private static int GetRank(INamedTypeSymbol symbol)
        {
            switch (symbol.TypeKind)
            {
                case TypeKind.Class:
                    return 1;
                case TypeKind.Delegate:
                    return 5;
                case TypeKind.Enum:
                    return 4;
                case TypeKind.Interface:
                    return 3;
                case TypeKind.Struct:
                    return 2;
            }

            Debug.Fail(symbol.ToDisplayString(Roslynator.SymbolDisplayFormats.Test));

            return 0;
        }
    }
}
