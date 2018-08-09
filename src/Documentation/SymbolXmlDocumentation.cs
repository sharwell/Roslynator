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

        public IEnumerable<XElement> Elements(string name)
        {
            return _element.Elements(name);
        }

        public bool HasElement(string name)
        {
            return Element(name) != null;
        }

        public void WriteContentTo(DocumentationWriter writer, string elementName, bool inlineOnly = false)
        {
            XElement element = _element.Element(elementName);

            if (element != null)
                element?.WriteContentTo(writer, inlineOnly);
        }

        public IEnumerable<(XElement element, ISymbol exceptionSymbol)> GetExceptions(Compilation compilation)
        {
            foreach (XElement element in _element.Elements(WellKnownTags.Exception))
            {
                string commentId = element.Attribute("cref")?.Value;

                if (commentId != null)
                {
                    ISymbol exceptionSymbol = DocumentationCommentId.GetFirstSymbolForReferenceId(commentId, compilation);

                    if (exceptionSymbol != null)
                        yield return (element, exceptionSymbol);
                }
            }
        }

        public XElement ParamElement(string name)
        {
            foreach (XElement element in _element.Elements(WellKnownTags.Param))
            {
                if (element.Attribute("name")?.Value == name)
                    return element;
            }

            return default;
        }

        public XElement TypeParamElement(string name)
        {
            foreach (XElement element in _element.Elements(WellKnownTags.TypeParam))
            {
                if (element.Attribute("name")?.Value == name)
                    return element;
            }

            return default;
        }
    }
}
