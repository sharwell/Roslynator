// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Roslynator.Documentation
{
    internal class MemberDefinitionEqualityComparer : EqualityComparer<ISymbol>
    {
        public static MemberDefinitionEqualityComparer Instance { get; } = new MemberDefinitionEqualityComparer();

        public override bool Equals(ISymbol x, ISymbol y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (x == null)
                return false;

            if (y == null)
                return false;

            if (x.Kind != y.Kind)
                return false;

            if (!string.Equals(x.Name, y.Name, StringComparison.Ordinal))
                return false;

            switch (x.Kind)
            {
                case SymbolKind.Event:
                case SymbolKind.Field:
                    {
                        return true;
                    }
                case SymbolKind.Method:
                    {
                        var a = (IMethodSymbol)x;
                        var b = (IMethodSymbol)y;

                        //TODO: constraints?
                        return a.MethodKind == MethodKind.Ordinary
                            && b.MethodKind == MethodKind.Ordinary
                            && a.TypeParameters.Length == b.TypeParameters.Length
                            && ParametersEqual(a.Parameters, b.Parameters);
                    }
                case SymbolKind.Property:
                    {
                        var a = (IPropertySymbol)x;
                        var b = (IPropertySymbol)y;

                        return a.IsIndexer == b.IsIndexer
                            && ParametersEqual(a.Parameters, b.Parameters);
                    }
                default:
                    {
                        throw new InvalidOperationException($"Unknown symbol kind '{x.Kind.ToString()}'.");
                    }
            }
        }

        //TODO: custom modifiers
        private static bool ParametersEqual(ImmutableArray<IParameterSymbol> parameters1, ImmutableArray<IParameterSymbol> parameters2)
        {
            int length = parameters1.Length;

            if (length != parameters2.Length)
                return false;

            for (int i = 0; i < length; i++)
            {
                IParameterSymbol parameter1 = parameters1[i];
                IParameterSymbol parameter2 = parameters2[i];

                if (parameter1.RefKind != parameter2.RefKind)
                    return false;

                if (parameter1.Type != parameter2.Type)
                    return false;
            }

            return true;
        }

        public override int GetHashCode(ISymbol obj)
        {
            //TODO: gethashcode
            return 0;
        }
    }
}
