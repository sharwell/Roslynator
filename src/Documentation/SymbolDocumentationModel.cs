// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SymbolDocumentationModel : IEquatable<SymbolDocumentationModel>
    {
        private ImmutableArray<AttributeData> _attributes;
        private string _commentId;

        protected SymbolDocumentationModel(
            ISymbol symbol,
            ImmutableArray<ISymbol> symbolAndBaseTypesAndNamespaces,
            ImmutableArray<string> nameAndBaseNamesAndNamespaceNames,
            DocumentationModel documentationModel)
        {
            Symbol = symbol;
            SymbolAndBaseTypesAndNamespaces = symbolAndBaseTypesAndNamespaces;
            NameAndBaseNamesAndNamespaceNames = nameAndBaseNamesAndNamespaceNames;
            DocumentationModel = documentationModel;
        }

        public ISymbol Symbol { get; }

        public string CommentId
        {
            get { return _commentId ?? (_commentId = Symbol.GetDocumentationCommentId()); }
        }

        internal ImmutableArray<ISymbol> SymbolAndBaseTypesAndNamespaces { get; }

        internal ImmutableArray<string> NameAndBaseNamesAndNamespaceNames { get; }

        internal DocumentationModel DocumentationModel { get; }

        public ImmutableArray<AttributeData> Attributes
        {
            get
            {
                if (_attributes.IsDefault)
                    _attributes = Symbol.GetAttributes();

                return _attributes;
            }
        }

        public bool IsObsolete
        {
            get { return Attributes.Any(f => f.AttributeClass.HasMetadataName(MetadataNames.System_ObsoleteAttribute)); }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return $"{Symbol.Kind} {Symbol.ToDisplayString(Roslynator.SymbolDisplayFormats.Test)}"; }
        }

        internal static SymbolDocumentationModel Create(DocumentationModel documentationModel)
        {
            return new SymbolDocumentationModel(
                symbol: null,
                symbolAndBaseTypesAndNamespaces: ImmutableArray<ISymbol>.Empty,
                nameAndBaseNamesAndNamespaceNames: ImmutableArray<string>.Empty,
                documentationModel: documentationModel);
        }

        public static SymbolDocumentationModel Create(ISymbol symbol, DocumentationModel documentationModel)
        {
            ImmutableArray<ISymbol>.Builder symbols = ImmutableArray.CreateBuilder<ISymbol>();
            ImmutableArray<string>.Builder names = ImmutableArray.CreateBuilder<string>();

            ISymbol explicitImplementation = symbol.GetFirstExplicitInterfaceImplementation();

            names.Add(symbol.Name);

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

            return new SymbolDocumentationModel(
                symbol,
                symbols.ToImmutableArray(),
                names.ToImmutableArray(),
                documentationModel);
        }

        public override bool Equals(object obj)
        {
            return (object)this == obj;
        }

        public bool Equals(SymbolDocumentationModel other)
        {
            return Equals((object)other);
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode();
        }
    }
}
