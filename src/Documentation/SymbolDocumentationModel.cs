// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    //TODO:  abstract
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SymbolDocumentationModel : IEquatable<SymbolDocumentationModel>
    {
        internal SymbolDocumentationModel(
            ISymbol symbol,
            DocumentationModel documentationModel)
        {
            Symbol = symbol;
            DocumentationModel = documentationModel;
        }

        public ISymbol Symbol { get; }

        internal DocumentationModel DocumentationModel { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return $"{Symbol.Kind} {Symbol.ToDisplayString(Roslynator.SymbolDisplayFormats.Test)}"; }
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
