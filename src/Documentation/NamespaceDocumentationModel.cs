// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    //TODO: zrušit
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class NamespaceDocumentationModel
    {
        internal NamespaceDocumentationModel(
            INamespaceSymbol namespaceSymbol,
            DocumentationModel documentationModel)
        {
            Symbol = namespaceSymbol;
            DocumentationModel = documentationModel;
        }

        public INamespaceSymbol Symbol { get; }

        internal DocumentationModel DocumentationModel { get; }

        //TODO: GetTypes
        public IEnumerable<INamedTypeSymbol> GetTypeSymbols()
        {
            return DocumentationModel.Types.Where(f => MetadataNameEqualityComparer<INamespaceSymbol>.Instance.Equals(f.ContainingNamespace, Symbol));
        }

        public bool Equals(NamespaceDocumentationModel other)
        {
            return MetadataNameEqualityComparer<INamespaceSymbol>.Instance.Equals(Symbol, other.Symbol);
        }

        public override bool Equals(object obj)
        {
            return obj is NamespaceDocumentationModel other
                && Equals(other);
        }

        public override int GetHashCode()
        {
            return MetadataNameEqualityComparer<INamespaceSymbol>.Instance.GetHashCode(Symbol);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return $"{Symbol.Kind} {Symbol.ToDisplayString(Roslynator.SymbolDisplayFormats.Test)}"; }
        }
    }
}