// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal static class SymbolExtensions
    {
        //XTODO: move to core
        public static ImmutableArray<INamedTypeSymbol> GetTypes(this IAssemblySymbol assemblySymbol, Func<INamedTypeSymbol, bool> predicate = null)
        {
            ImmutableArray<INamedTypeSymbol>.Builder builder = ImmutableArray.CreateBuilder<INamedTypeSymbol>();

            GetTypes(assemblySymbol.GlobalNamespace);

            return builder.ToImmutableArray();

            void GetTypes(INamespaceOrTypeSymbol namespaceOrTypeSymbol)
            {
                if (namespaceOrTypeSymbol is INamedTypeSymbol namedTypeSymbol
                    && (predicate == null || predicate(namedTypeSymbol)))
                {
                    builder.Add(namedTypeSymbol);
                }

                foreach (ISymbol memberSymbol in namespaceOrTypeSymbol.GetMembers())
                {
                    if (memberSymbol is INamespaceOrTypeSymbol namespaceOrTypeSymbol2)
                    {
                        GetTypes(namespaceOrTypeSymbol2);
                    }
                }
            }
        }

        public static ImmutableArray<ISymbol> GetMembers(this INamedTypeSymbol typeSymbol, Func<ISymbol, bool> predicate, bool includeInherited = false)
        {
            if (includeInherited)
            {
                if (typeSymbol.TypeKind == TypeKind.Interface)
                {
                    return GetInterfaceMembersIncludingInherited();
                }
                else
                {
                    return GetMembersIncludingInherited();
                }
            }
            else if (predicate != null)
            {
                return typeSymbol
                    .GetMembers()
                    .Where(predicate)
                    .ToImmutableArray();
            }

            return typeSymbol.GetMembers();

            ImmutableArray<ISymbol> GetMembersIncludingInherited()
            {
                var symbols = new HashSet<ISymbol>(MemberDefinitionEqualityComparer.Instance);

                HashSet<ISymbol> overriddenSymbols = null;

                foreach (ISymbol symbol in GetMembers(typeSymbol, predicate: predicate))
                {
                    ISymbol overriddenSymbol = symbol.OverriddenSymbol();

                    if (overriddenSymbol != null)
                    {
                        (overriddenSymbols ?? (overriddenSymbols = new HashSet<ISymbol>())).Add(overriddenSymbol);
                    }

                    symbols.Add(symbol);
                }

                INamedTypeSymbol baseType = typeSymbol.BaseType;

                while (baseType != null)
                {
                    bool areInternalsVisible = typeSymbol.ContainingAssembly == baseType.ContainingAssembly
                        || baseType.ContainingAssembly.GivesAccessTo(typeSymbol.ContainingAssembly);

                    foreach (ISymbol symbol in baseType.GetMembers())
                    {
                        if (!symbol.IsStatic
                            && symbol.DeclaredAccessibility != Accessibility.Private
                            && (predicate == null || predicate(symbol))
                            && (symbol.DeclaredAccessibility != Accessibility.Internal || areInternalsVisible))
                        {
                            if (overriddenSymbols?.Remove(symbol) != true)
                                symbols.Add(symbol);

                            ISymbol overriddenSymbol = symbol.OverriddenSymbol();

                            if (overriddenSymbol != null)
                            {
                                (overriddenSymbols ?? (overriddenSymbols = new HashSet<ISymbol>())).Add(overriddenSymbol);
                            }
                        }
                    }

                    baseType = baseType.BaseType;
                }

                return symbols.ToImmutableArray();
            }

            ImmutableArray<ISymbol> GetInterfaceMembersIncludingInherited()
            {
                var symbols = new HashSet<ISymbol>(MemberDefinitionEqualityComparer.Instance);

                foreach (ISymbol symbol in GetMembers(typeSymbol, predicate: predicate))
                    symbols.Add(symbol);

                foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.AllInterfaces)
                {
                    foreach (ISymbol symbol in GetMembers(interfaceSymbol, predicate: predicate))
                        symbols.Add(symbol);
                }

                return symbols.ToImmutableArray();
            }
        }

        public static int GetArity(this ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Method:
                    return ((IMethodSymbol)symbol).Arity;
                case SymbolKind.NamedType:
                    return ((INamedTypeSymbol)symbol).Arity;
            }

            return 0;
        }

        public static ImmutableArray<ITypeParameterSymbol> GetTypeParameters(this ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Method:
                    return ((IMethodSymbol)symbol).TypeParameters;
                case SymbolKind.NamedType:
                    return ((INamedTypeSymbol)symbol).TypeParameters;
            }

            return ImmutableArray<ITypeParameterSymbol>.Empty;
        }

        public static ISymbol GetFirstExplicitInterfaceImplementation(this ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Event:
                    return ((IEventSymbol)symbol).ExplicitInterfaceImplementations.FirstOrDefault();
                case SymbolKind.Method:
                    return ((IMethodSymbol)symbol).ExplicitInterfaceImplementations.FirstOrDefault();
                case SymbolKind.Property:
                    return ((IPropertySymbol)symbol).ExplicitInterfaceImplementations.FirstOrDefault();
            }

            return null;
        }

        public static ISymbol OverriddenSymbol(this ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Method:
                    return ((IMethodSymbol)symbol).OverriddenMethod;
                case SymbolKind.Property:
                    return ((IPropertySymbol)symbol).OverriddenProperty;
                case SymbolKind.Event:
                    return ((IEventSymbol)symbol).OverriddenEvent;
            }

            return null;
        }
    }
}
