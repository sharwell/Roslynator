// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class SymbolXmlDocumentation
    {
        private readonly XElement _element;

        internal static SymbolXmlDocumentation Default { get; } = new SymbolXmlDocumentation(null, null, null);

        internal SymbolXmlDocumentation(ISymbol symbol, string commentId, XElement element)
        {
            Symbol = symbol;
            CommentId = commentId;
            _element = element;
        }

        public ISymbol Symbol { get; }

        public string CommentId { get; }

        public XElement Element(string name)
        {
            return _element.Element(name);
        }

        internal XElement Element(string name, string attributeName, string attributeValue)
        {
            foreach (XElement element in _element.Elements(name))
            {
                if (element.Attribute(attributeName)?.Value == attributeValue)
                    return element;
            }

            return default;
        }

        public IEnumerable<XElement> Elements(string name)
        {
            return _element.Elements(name);
        }

        public bool HasElement(string name)
        {
            return Element(name) != null;
        }
    }
}
