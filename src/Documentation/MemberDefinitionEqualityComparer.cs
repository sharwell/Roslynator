// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

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

        private static bool ParametersEqual(ImmutableArray<IParameterSymbol> parameters1, ImmutableArray<IParameterSymbol> parameters2)
        {
            int length = parameters1.Length;

            if (length != parameters2.Length)
                return false;

            for (int i = 0; i < length; i++)
            {
                if (!ParameterEqualityComparer.Instance.Equals(parameters1[i], parameters2[i]))
                    return false;
            }

            return true;
        }

        public override int GetHashCode(ISymbol obj)
        {
            SymbolKind kind = obj.Kind;

            int hashCode = Hash.Combine(StringComparer.Ordinal.GetHashCode(obj.Name), (int)kind);

            if (kind == SymbolKind.Method)
            {
                var methodSymbol = (IMethodSymbol)obj;

                return Hash.Combine((int)methodSymbol.MethodKind,
                    Hash.Combine(methodSymbol.TypeParameters.Length,
                    Hash.Combine(Hash.CombineValues(methodSymbol.Parameters, ParameterEqualityComparer.Instance), hashCode)));
            }
            else if (kind == SymbolKind.Property)
            {
                var propertySymbol = (IPropertySymbol)obj;

                return Hash.Combine(propertySymbol.IsIndexer,
                    Hash.Combine(Hash.CombineValues(propertySymbol.Parameters, ParameterEqualityComparer.Instance), hashCode));
            }

            return hashCode;
        }

        private class ParameterEqualityComparer : EqualityComparer<IParameterSymbol>
        {
            public static ParameterEqualityComparer Instance { get; } = new ParameterEqualityComparer();

            public override bool Equals(IParameterSymbol x, IParameterSymbol y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;

                if (x == null)
                    return false;

                if (y == null)
                    return false;

                return x.RefKind == y.RefKind
                    && x.Type == y.Type;
            }

            public override int GetHashCode(IParameterSymbol obj)
            {
                if (obj == null)
                    return 0;

                return Hash.Combine(obj.Type, (int)obj.RefKind);
            }
        }
    }
}
