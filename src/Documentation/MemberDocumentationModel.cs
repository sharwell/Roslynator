// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class MemberDocumentationModel : SymbolDocumentationModel
    {
        private MemberDocumentationModel(
            ISymbol symbol,
            ImmutableArray<ISymbol> symbolAndBaseTypesAndNamespaces,
            ImmutableArray<string> nameAndBaseNamesAndNamespaceNames,
            DocumentationModel documentationModel) : base(symbol, symbolAndBaseTypesAndNamespaces, nameAndBaseNamesAndNamespaceNames, documentationModel)
        {
        }

        public static MemberDocumentationModel Create2(ISymbol symbol, DocumentationModel documentationModel)
        {
            ImmutableArray<ISymbol>.Builder symbols = ImmutableArray.CreateBuilder<ISymbol>();
            ImmutableArray<string>.Builder names = ImmutableArray.CreateBuilder<string>();

            if (symbol.Kind == SymbolKind.Method
                && ((IMethodSymbol)symbol).MethodKind == MethodKind.Constructor)
            {
                names.Add(WellKnownNames.ConstructorName);
            }
            else if (symbol.Kind == SymbolKind.Property
                && ((IPropertySymbol)symbol).IsIndexer)
            {
                //TODO: explicitly implemented indexer
                names.Add("Item");
            }
            else
            {
                ISymbol explicitImplementation = symbol.GetFirstExplicitInterfaceImplementation();

                if (explicitImplementation != null)
                {
                    string name = explicitImplementation
                        .ToDisplayParts(SymbolDisplayFormats.ExplicitImplementationFullName, SymbolDisplayAdditionalMemberOptions.UseItemPropertyName)
                        .Where(part => part.Kind != SymbolDisplayPartKind.Space)
                        .Select(part => (part.IsPunctuation()) ? part.WithText("-") : part)
                        .ToImmutableArray()
                        .ToDisplayString();

                    names.Add(name);
                }
                else
                {
                    int arity = symbol.GetArity();

                    if (arity > 0)
                    {
                        names.Add(symbol.Name + "-" + arity.ToString(CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        names.Add(symbol.Name);
                    }
                }
            }

            symbols.Add(symbol);

            INamedTypeSymbol containingType = symbol.ContainingType;

            while (containingType != null)
            {
                int arity = containingType.Arity;

                names.Add((arity > 0) ? containingType.Name + "-" + arity.ToString(CultureInfo.InvariantCulture) : containingType.Name);

                symbols.Add(containingType);

                containingType = containingType.ContainingType;
            }

            INamespaceSymbol containingNamespace = symbol.ContainingNamespace;

            if (containingNamespace != null)
            {
                if (containingNamespace.IsGlobalNamespace)
                {
                    names.Add(WellKnownNames.GlobalNamespaceName);
                    symbols.Add(containingNamespace);
                }
                else
                {
                    do
                    {
                        names.Add(containingNamespace.Name);

                        symbols.Add(containingNamespace);

                        containingNamespace = containingNamespace.ContainingNamespace;
                    }
                    while (containingNamespace?.IsGlobalNamespace == false);
                }
            }

            return new MemberDocumentationModel(
                symbol,
                symbols.ToImmutableArray(),
                names.ToImmutableArray(),
                documentationModel);
        }
    }
}
