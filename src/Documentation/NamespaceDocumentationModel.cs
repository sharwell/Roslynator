// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public sealed class NamespaceDocumentationModel : SymbolDocumentationModel
    {
        internal NamespaceDocumentationModel(
            INamespaceSymbol namespaceSymbol,
            DocumentationModel documentationModel) : base(namespaceSymbol, documentationModel)
        {
            NamespaceSymbol = namespaceSymbol;
        }

        public INamespaceSymbol NamespaceSymbol { get; }

        public IEnumerable<INamedTypeSymbol> GetTypeSymbols()
        {
            return DocumentationModel.TypeSymbols.Where(f => MetadataNameEqualityComparer<INamespaceSymbol>.Instance.Equals(f.ContainingNamespace, NamespaceSymbol));
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