// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class MemberDocumentationModel : SymbolDocumentationModel, IDocumentationFile
    {
        private MemberDocumentationModel(
            ISymbol symbol,
            ImmutableArray<ISymbol> overloads,
            ImmutableArray<string> nameAndBaseNamesAndNamespaceNames,
            DocumentationModel documentationModel) : base(symbol, documentationModel)
        {
            Overloads = overloads;
            Names = nameAndBaseNamesAndNamespaceNames;
        }

        public ImmutableArray<ISymbol> Overloads { get; }

        public bool IsOverloaded
        {
            get { return Overloads.Length > 1; }
        }

        public ImmutableArray<string> Names { get; }

        public DocumentationKind Kind => DocumentationKind.Member;

        public static MemberDocumentationModel Create(ISymbol symbol, ImmutableArray<ISymbol> overloads, DocumentationModel documentationModel)
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
                overloads,
                names.ToImmutableArray(),
                documentationModel);
        }

        public bool Equals(IDocumentationFile other)
        {
            return DocumentationFileEqualityComparer.Instance.Equals(this, other);
        }
    }
}
