// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SymbolDocumentationModel : IEquatable<SymbolDocumentationModel>
    {
        private ImmutableArray<AttributeData> _attributes;
        private string _commentId;

        public SymbolDocumentationModel(
            ISymbol symbol,
            DocumentationModel documentationModel)
        {
            Symbol = symbol;
            DocumentationModel = documentationModel;
        }

        public ISymbol Symbol { get; }

        public string CommentId
        {
            get { return _commentId ?? (_commentId = Symbol.GetDocumentationCommentId()); }
        }

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

        public bool IsExternal
        {
            get { return DocumentationModel.IsExternal(Symbol); }
        }

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
