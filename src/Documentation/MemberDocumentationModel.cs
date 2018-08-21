// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class MemberDocumentationModel : IEquatable<MemberDocumentationModel>
    {
        internal MemberDocumentationModel(
            ISymbol symbol,
            ImmutableArray<ISymbol> overloads,
            DocumentationModel documentationModel)
        {
            Symbol = symbol;
            Overloads = overloads;
            DocumentationModel = documentationModel;
        }

        public ISymbol Symbol { get; }

        internal DocumentationModel DocumentationModel { get; }

        public ImmutableArray<ISymbol> Overloads { get; }

        public bool IsOverloaded
        {
            get { return Overloads.Length > 1; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return $"{Symbol.Name} Overloads = {Overloads.Length}"; }
        }

        public bool Equals(MemberDocumentationModel other)
        {
            return other.Symbol.Name == Symbol.Name
                && other.Symbol.ContainingType.Equals(Symbol.ContainingType);
        }

        public override bool Equals(object obj)
        {
            return obj is MemberDocumentationModel other
                && Equals(other);
        }

        public override int GetHashCode()
        {
            return Hash.Combine(Symbol.ContainingType, Hash.Create(Symbol.Name));
        }
    }
}
