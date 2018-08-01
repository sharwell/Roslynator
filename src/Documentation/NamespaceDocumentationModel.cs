// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public sealed class NamespaceDocumentationModel : SymbolDocumentationModel
    {
        private ImmutableArray<TypeDocumentationModel> _types;

        internal NamespaceDocumentationModel(
            INamespaceSymbol namespaceSymbol,
            DocumentationModel documentationModel) : base(namespaceSymbol, documentationModel)
        {
            NamespaceSymbol = namespaceSymbol;
        }

        public INamespaceSymbol NamespaceSymbol { get; }

        public ImmutableArray<TypeDocumentationModel> TypeModels
        {
            get
            {
                if (_types.IsDefault)
                {
                    _types = DocumentationModel.TypeModels
                        .Where(f => MetadataNameEqualityComparer<INamespaceSymbol>.Instance.Equals(f.Symbol.ContainingNamespace, NamespaceSymbol))
                        .ToImmutableArray();
                }

                return _types;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is NamespaceDocumentationModel other
                && MetadataNameEqualityComparer<INamespaceSymbol>.Instance.Equals(NamespaceSymbol, other.NamespaceSymbol);
        }

        public override int GetHashCode()
        {
            return MetadataNameEqualityComparer<INamespaceSymbol>.Instance.GetHashCode(NamespaceSymbol);
        }
    }
}