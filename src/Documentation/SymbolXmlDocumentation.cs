﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Roslynator.CSharp;

namespace Roslynator.Documentation
{
    public class SymbolXmlDocumentation
    {
        internal static SymbolXmlDocumentation Default { get; } = new SymbolXmlDocumentation(null, null, null);

        private readonly XElement _element;

        internal SymbolXmlDocumentation(ISymbol symbol, string commentId, XElement element)
        {
            Symbol = symbol;
            CommentId = commentId;
            _element = element;
        }

        public ISymbol Symbol { get; }

        public string CommentId { get; }

        public bool HasElement(string name)
        {
            return _element.Element(name) != null;
        }

        public void WriteContentTo(DocumentationWriter writer, string elementName, bool inlineOnly = false)
        {
            XElement element = _element.Element(elementName);

            if (element != null)
                WriteContent(writer, element, inlineOnly);
        }

        private void WriteContent(DocumentationWriter writer, XElement element, bool inlineOnly = false)
        {
            using (IEnumerator<XNode> en = element.Nodes().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    XNode node = null;

                    bool isFirst = true;
                    bool isLast = false;

                    do
                    {
                        node = en.Current;

                        isLast = !en.MoveNext();

                        if (node is XText t)
                        {
                            string value = t.Value;
                            value = TextUtility.RemoveLeadingTrailingNewLine(value, isFirst, isLast);

                            if (inlineOnly)
                                value = TextUtility.ToSingleLine(value);

                            writer.WriteString(value);
                        }
                        else if (node is XElement e)
                        {
                            switch (XmlElementNameKindMapper.GetKindOrDefault(e.Name.LocalName))
                            {
                                case XmlElementKind.C:
                                    {
                                        string value = e.Value;
                                        value = TextUtility.ToSingleLine(value);
                                        writer.WriteInlineCode(value);
                                        break;
                                    }
                                case XmlElementKind.Code:
                                    {
                                        if (inlineOnly)
                                            break;

                                        string value = e.Value;
                                        value = TextUtility.RemoveLeadingTrailingNewLine(value);

                                        DocumentationModel documentationModel = writer.DocumentationModel;

                                        string language = Symbol.Language;

                                        writer.WriteCodeBlock(value, language ?? documentationModel.Language);

                                        break;
                                    }
                                case XmlElementKind.List:
                                    {
                                        if (inlineOnly)
                                            break;

                                        string type = e.Attribute("type")?.Value;

                                        if (!string.IsNullOrEmpty(type))
                                        {
                                            switch (type)
                                            {
                                                case "bullet":
                                                    {
                                                        WriteList(writer, e.Elements());
                                                        break;
                                                    }
                                                case "number":
                                                    {
                                                        WriteList(writer, e.Elements(), isOrdered: true);
                                                        break;
                                                    }
                                                case "table":
                                                    {
                                                        WriteTable(writer, e.Elements());
                                                        break;
                                                    }
                                                default:
                                                    {
                                                        Debug.Fail(type);
                                                        break;
                                                    }
                                            }
                                        }

                                        break;
                                    }
                                case XmlElementKind.Para:
                                    {
                                        writer.WriteLine();
                                        writer.WriteLine();
                                        WriteContent(writer, e);
                                        writer.WriteLine();
                                        writer.WriteLine();
                                        break;
                                    }
                                case XmlElementKind.ParamRef:
                                    {
                                        string parameterName = e.Attribute("name")?.Value;

                                        if (parameterName != null)
                                            writer.WriteBold(parameterName);

                                        break;
                                    }
                                case XmlElementKind.See:
                                    {
                                        string commentId = e.Attribute("cref")?.Value;

                                        if (commentId != null)
                                        {
                                            ISymbol symbol = writer.DocumentationModel.GetFirstSymbolForDeclarationId(commentId);

                                            //XTODO: repair roslyn documentation
                                            Debug.Assert(symbol != null
                                                || commentId == "T:Microsoft.CodeAnalysis.CSharp.SyntaxNode"
                                                || commentId == "T:Microsoft.CodeAnalysis.CSharp.SyntaxToken"
                                                || commentId == "T:Microsoft.CodeAnalysis.CSharp.SyntaxTrivia"
                                                || commentId == "T:Microsoft.CodeAnalysis.VisualBasic.SyntaxNode"
                                                || commentId == "T:Microsoft.CodeAnalysis.VisualBasic.SyntaxToken"
                                                || commentId == "T:Microsoft.CodeAnalysis.VisualBasic.SyntaxTrivia", commentId);

                                            if (symbol != null)
                                            {
                                                writer.WriteLink(symbol, SymbolDisplayFormats.TypeNameAndContainingTypesAndTypeParameters, SymbolDisplayAdditionalMemberOptions.UseItemPropertyName | SymbolDisplayAdditionalMemberOptions.UseOperatorName);
                                            }
                                            else
                                            {
                                                writer.WriteBold(TextUtility.RemovePrefixFromDocumentationCommentId(commentId));
                                            }
                                        }

                                        break;
                                    }
                                case XmlElementKind.TypeParamRef:
                                    {
                                        string typeParameterName = e.Attribute("name")?.Value;

                                        if (typeParameterName != null)
                                            writer.WriteBold(typeParameterName);

                                        break;
                                    }
                                case XmlElementKind.Example:
                                case XmlElementKind.Exception:
                                case XmlElementKind.Exclude:
                                case XmlElementKind.Include:
                                case XmlElementKind.InheritDoc:
                                case XmlElementKind.Param:
                                case XmlElementKind.Permission:
                                case XmlElementKind.Remarks:
                                case XmlElementKind.Returns:
                                case XmlElementKind.SeeAlso:
                                case XmlElementKind.Summary:
                                case XmlElementKind.TypeParam:
                                case XmlElementKind.Value:
                                    {
                                        break;
                                    }
                                default:
                                    {
                                        Debug.Fail(e.Name.LocalName);
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            Debug.Fail(node.NodeType.ToString());
                        }

                        isFirst = false;
                    }
                    while (!isLast);
                }
            }
        }

        private void WriteList(DocumentationWriter writer, IEnumerable<XElement> elements, bool isOrdered = false)
        {
            int number = 1;

            using (IEnumerator<XElement> en = Iterator().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    if (isOrdered)
                    {
                        writer.WriteStartOrderedList();
                    }
                    else
                    {
                        writer.WriteStartBulletList();
                    }

                    do
                    {
                        WriteStartItem();
                        WriteContent(writer, en.Current, inlineOnly: true);
                        WriteEndItem();
                    }
                    while (en.MoveNext());

                    if (isOrdered)
                    {
                        writer.WriteEndOrderedList();
                    }
                    else
                    {
                        writer.WriteEndBulletList();
                    }
                }
            }

            IEnumerable<XElement> Iterator()
            {
                foreach (XElement element in elements)
                {
                    if (element.Name.LocalName == "item")
                    {
                        using (IEnumerator<XElement> en = element.Elements().GetEnumerator())
                        {
                            if (en.MoveNext())
                            {
                                XElement element2 = en.Current;

                                if (element2.Name.LocalName == "description")
                                {
                                    yield return element2;
                                }
                            }
                            else
                            {
                                yield return element;
                            }
                        }
                    }
                }
            }

            void WriteStartItem()
            {
                if (isOrdered)
                {
                    writer.WriteStartOrderedItem(number);
                    number++;
                }
                else
                {
                    writer.WriteStartBulletItem();
                }
            }

            void WriteEndItem()
            {
                if (isOrdered)
                {
                    writer.WriteEndOrderedItem();
                }
                else
                {
                    writer.WriteEndBulletItem();
                }
            }
        }

        private void WriteTable(DocumentationWriter writer, IEnumerable<XElement> elements)
        {
            using (IEnumerator<XElement> en = elements.GetEnumerator())
            {
                if (en.MoveNext())
                {
                    XElement element = en.Current;

                    string name = element.Name.LocalName;

                    if (name == "listheader"
                        && en.MoveNext())
                    {
                        int columnCount = element.Elements().Count();

                        writer.WriteStartTable(columnCount);
                        writer.WriteStartTableRow();

                        foreach (XElement element2 in element.Elements())
                        {
                            writer.WriteStartTableCell();
                            WriteContent(writer, element2, inlineOnly: true);
                            writer.WriteEndTableCell();
                        }

                        writer.WriteEndTableRow();
                        writer.WriteTableHeaderSeparator();

                        do
                        {
                            element = en.Current;

                            writer.WriteStartTableRow();

                            int count = 0;
                            foreach (XElement element2 in element.Elements())
                            {
                                writer.WriteStartTableCell();
                                WriteContent(writer, element2, inlineOnly: true);
                                writer.WriteEndTableCell();
                                count++;

                                if (count == columnCount)
                                    break;
                            }

                            while (count < columnCount)
                            {
                                writer.WriteTableCell(null);
                                count++;
                            }

                            writer.WriteEndTableRow();
                        }
                        while (en.MoveNext());

                        writer.WriteEndTable();
                    }
                }
            }
        }

        public void WriteSectionTo(DocumentationWriter writer, string heading, string elementName)
        {
            XElement element = _element.Element(elementName);

            if (element == null)
                return;

            if (heading != null)
            {
                writer.WriteHeading(2, heading);
            }
            else
            {
                writer.WriteLine();
            }

            WriteContentTo(writer, elementName);
        }

        public void WriteExceptionsTo(DocumentationWriter writer)
        {
            using (IEnumerator<(XElement element, ISymbol exceptionSymbol)> en = GetExceptions().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    writer.WriteHeading(3, writer.Resources.ExceptionsTitle);

                    do
                    {
                        XElement element = en.Current.element;
                        ISymbol exceptionSymbol = en.Current.exceptionSymbol;

                        writer.WriteLink(exceptionSymbol, SymbolDisplayFormats.TypeNameAndContainingTypesAndTypeParameters);
                        writer.WriteLine();
                        writer.WriteLine();
                        WriteContent(writer, element);
                        writer.WriteLine();
                        writer.WriteLine();
                    }
                    while (en.MoveNext());
                }
            }

            IEnumerable<(XElement element, ISymbol exceptionSymbol)> GetExceptions()
            {
                foreach (XElement e in _element.Elements(WellKnownTags.Exception))
                {
                    string commentId = e.Attribute("cref")?.Value;

                    if (commentId != null)
                    {
                        ISymbol exceptionSymbol = writer.DocumentationModel.GetFirstSymbolForReferenceId(commentId);

                        if (exceptionSymbol != null)
                            yield return (e, exceptionSymbol);
                    }
                }
            }
        }

        public IEnumerable<string> SeeAlsoCommentIds()
        {
            foreach (XElement element in _element.Elements(WellKnownTags.SeeAlso))
            {
                string commentId = element.Attribute("cref")?.Value;

                if (commentId != null)
                    yield return commentId;
            }
        }

        public void WriteParamContentTo(DocumentationWriter writer, string name)
        {
            foreach (XElement e in _element.Elements(WellKnownTags.Param))
            {
                if (e.Attribute("name")?.Value == name)
                {
                    WriteContent(writer, e, inlineOnly: true);
                    return;
                }
            }
        }
    }
}
