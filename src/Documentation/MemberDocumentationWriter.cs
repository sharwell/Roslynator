// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal abstract class MemberDocumentationWriter
    {
        private ImmutableArray<MemberDocumentationParts> _enabledAndSortedMemberParts;

        protected MemberDocumentationWriter(DocumentationWriter writer)
        {
            Writer = writer;
        }

        public DocumentationWriter Writer { get; }

        public SymbolDisplayFormat Format => FormatProvider.SimpleDefinitionFormat;

        public SymbolDisplayFormatProvider FormatProvider => Writer.FormatProvider;

        public DocumentationModel DocumentationModel => Writer.DocumentationModel;

        public DocumentationOptions Options => Writer.Options;

        public DocumentationResources Resources => Writer.Resources;

        internal int BaseHeadingLevel
        {
            get { return Writer.BaseHeadingLevel; }
            set { Writer.BaseHeadingLevel = value; }
        }

        public virtual IComparer<MemberDocumentationParts> Comparer { get; }

        internal ImmutableArray<MemberDocumentationParts> EnabledAndSortedMemberParts
        {
            get
            {
                if (_enabledAndSortedMemberParts.IsDefault)
                {
                    _enabledAndSortedMemberParts = Enum.GetValues(typeof(MemberDocumentationParts))
                        .Cast<MemberDocumentationParts>()
                        .Where(f => f != MemberDocumentationParts.None
                            && f != MemberDocumentationParts.All
                            && Options.IsPartEnabled(f))
                        .OrderBy(f => f, Comparer)
                        .ToImmutableArray();
                }

                return _enabledAndSortedMemberParts;
            }
        }

        private SymbolXmlDocumentation GetXmlDocumentation(ISymbol symbol)
        {
            return DocumentationModel.GetXmlDocumentation(symbol, Options.PreferredCultureName);
        }

        public virtual void WriteMember(MemberDocumentationModel model)
        {
            ISymbol symbol = model.Symbol;

            foreach (MemberDocumentationParts part in EnabledAndSortedMemberParts)
            {
                switch (part)
                {
                    case MemberDocumentationParts.Namespace:
                        {
                            Writer.WriteNamespace(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Assembly:
                        {
                            Writer.WriteAssembly(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Title:
                        {
                            WriteTitle(symbol, isOverloaded: model.IsOverloaded);
                            break;
                        }
                }
            }

            if (!model.IsOverloaded)
            {
                WriteContent(symbol);
            }
            else
            {
                //TODO: create link for overloads
                Writer.WriteTable(
                    model.Overloads,
                    heading: Resources.OverloadsTitle,
                    headingLevel: 2,
                    header1: Resources.GetName(symbol),
                    header2: Resources.SummaryTitle,
                    format: FormatProvider.SimpleDefinitionFormat,
                    additionalOptions: SymbolDisplayAdditionalMemberOptions.UseItemPropertyName | SymbolDisplayAdditionalMemberOptions.UseOperatorName,
                    addLink: false);

                foreach (ISymbol symbol2 in model.Overloads)
                {
                    BaseHeadingLevel++;

                    Writer.WriteStartHeading(1);
                    Writer.WriteString(symbol2.ToDisplayString(Format, SymbolDisplayAdditionalMemberOptions.UseItemPropertyName | SymbolDisplayAdditionalMemberOptions.UseOperatorName));
                    Writer.WriteEndHeading();
                    WriteContent(symbol2);

                    BaseHeadingLevel--;
                }
            }
        }

        public virtual void WriteTitle(ISymbol symbol, bool isOverloaded)
        {
            Writer.WriteStartHeading(1);

            SymbolDisplayFormat format = (isOverloaded)
                ? FormatProvider.OverloadedMemberTitleFormat
                : FormatProvider.MemberTitleFormat;

            Writer.WriteString(symbol.ToDisplayString(format, SymbolDisplayAdditionalMemberOptions.UseItemPropertyName | SymbolDisplayAdditionalMemberOptions.UseOperatorName));
            Writer.WriteSpace();
            Writer.WriteString(Resources.GetName(symbol));
            Writer.WriteEndHeading();
        }

        public virtual void WriteImplements(ISymbol symbol)
        {
            using (IEnumerator<ISymbol> en = symbol.FindImplementedInterfaceMembers()
                .OrderBy(f => f.ToDisplayString(FormatProvider.MemberImplementsFormat, SymbolDisplayAdditionalMemberOptions.UseItemPropertyName))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    Writer.WriteHeading(3, Resources.ImplementsTitle);

                    Writer.WriteStartBulletList();

                    do
                    {
                        Writer.WriteStartBulletItem();
                        Writer.WriteLink(en.Current, FormatProvider.MemberImplementsFormat, SymbolDisplayAdditionalMemberOptions.UseItemPropertyName);
                        Writer.WriteEndBulletItem();
                    }
                    while (en.MoveNext());

                    Writer.WriteEndBulletList();
                }
            }
        }

        public virtual void WriteReturnValue(ISymbol symbol)
        {
        }

        public void WriteContent(ISymbol symbol)
        {
            foreach (MemberDocumentationParts part in EnabledAndSortedMemberParts)
            {
                switch (part)
                {
                    case MemberDocumentationParts.Obsolete:
                        {
                            if (symbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                                Writer.WriteObsolete(symbol);

                            break;
                        }
                    case MemberDocumentationParts.Summary:
                        {
                            Writer.WriteSummary(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Definition:
                        {
                            Writer.WriteDefinition(symbol);
                            break;
                        }
                    case MemberDocumentationParts.TypeParameters:
                        {
                            Writer.WriteTypeParameters(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Parameters:
                        {
                            Writer.WriteParameters(symbol);
                            break;
                        }
                    case MemberDocumentationParts.ReturnValue:
                        {
                            WriteReturnValue(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Implements:
                        {
                            WriteImplements(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Attributes:
                        {
                            Writer.WriteAttributes(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Exceptions:
                        {
                            Writer.WriteExceptions(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Examples:
                        {
                            Writer.WriteExamples(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Remarks:
                        {
                            Writer.WriteRemarks(symbol);
                            break;
                        }
                    case MemberDocumentationParts.SeeAlso:
                        {
                            Writer.WriteSeeAlso(symbol);
                            break;
                        }
                }
            }
        }

        public static MemberDocumentationWriter Create(ISymbol symbol, DocumentationWriter writer)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Event:
                    {
                        return new EventDocumentationWriter(writer);
                    }
                case SymbolKind.Field:
                    {
                        return new FieldDocumentationWriter(writer);
                    }
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        switch (methodSymbol.MethodKind)
                        {
                            case MethodKind.Constructor:
                                {
                                    return new ConstructorDocumentationWriter(writer);
                                }
                            case MethodKind.UserDefinedOperator:
                            case MethodKind.Conversion:
                                {
                                    return new OperatorDocumentationWriter(writer);
                                }
                        }

                        return new MethodDocumentationWriter(writer);
                    }
                case SymbolKind.Property:
                    {
                        return new PropertyDocumentationWriter(writer);
                    }
            }

            throw new InvalidOperationException();
        }

        private class ConstructorDocumentationWriter : MemberDocumentationWriter
        {
            public ConstructorDocumentationWriter(DocumentationWriter writer) : base(writer)
            {
            }

            public override void WriteTitle(ISymbol symbol, bool isOverloaded)
            {
                Writer.WriteStartHeading(1);

                if (!isOverloaded)
                {
                    Writer.WriteString(symbol.ToDisplayString(FormatProvider.SimpleDefinitionFormat));
                    Writer.WriteSpace();
                    Writer.WriteString(Resources.ConstructorTitle);
                }
                else
                {
                    Writer.WriteString(symbol.ToDisplayString(FormatProvider.TitleFormat));
                    Writer.WriteSpace();
                    Writer.WriteString(Resources.ConstructorsTitle);
                }

                Writer.WriteEndHeading();
            }
        }

        private class EventDocumentationWriter : MemberDocumentationWriter
        {
            public EventDocumentationWriter(DocumentationWriter writer) : base(writer)
            {
            }
        }

        private class FieldDocumentationWriter : MemberDocumentationWriter
        {
            public FieldDocumentationWriter(DocumentationWriter writer) : base(writer)
            {
            }

            public override void WriteReturnValue(ISymbol symbol)
            {
                var fieldSymbol = (IFieldSymbol)symbol;

                Writer.WriteHeading(3, Resources.FieldValueTitle);
                Writer.WriteLinkOrTypeLink(fieldSymbol.Type);
            }
        }

        private class MethodDocumentationWriter : MemberDocumentationWriter
        {
            public MethodDocumentationWriter(DocumentationWriter writer) : base(writer)
            {
            }

            public override void WriteReturnValue(ISymbol symbol)
            {
                var methodSymbol = (IMethodSymbol)symbol;

                ITypeSymbol returnType = methodSymbol.ReturnType;

                if (returnType.SpecialType == SpecialType.System_Void)
                    return;

                Writer.WriteHeading(3, Resources.ReturnsTitle);
                Writer.WriteLinkOrTypeLink(returnType);
                Writer.WriteLine();
                Writer.WriteLine();

                GetXmlDocumentation(methodSymbol)?.WriteElementContentTo(Writer, WellKnownTags.Returns);
            }
        }

        private class OperatorDocumentationWriter : MemberDocumentationWriter
        {
            public OperatorDocumentationWriter(DocumentationWriter writer) : base(writer)
            {
            }

            public override void WriteReturnValue(ISymbol symbol)
            {
                var methodSymbol = (IMethodSymbol)symbol;

                Writer.WriteHeading(3, Resources.ReturnsTitle);
                Writer.WriteLinkOrTypeLink(methodSymbol.ReturnType);
                Writer.WriteLine();
                Writer.WriteLine();

                GetXmlDocumentation(methodSymbol)?.WriteElementContentTo(Writer, WellKnownTags.Returns);
            }
        }

        private class PropertyDocumentationWriter : MemberDocumentationWriter
        {
            public PropertyDocumentationWriter(DocumentationWriter writer) : base(writer)
            {
            }

            public override void WriteReturnValue(ISymbol symbol)
            {
                var propertySymbol = (IPropertySymbol)symbol;

                Writer.WriteHeading(3, Resources.PropertyValueTitle);
                Writer.WriteLinkOrTypeLink(propertySymbol.Type);
                Writer.WriteLine();
                Writer.WriteLine();

                string elementName = (propertySymbol.IsIndexer) ? WellKnownTags.Returns : WellKnownTags.Value;

                GetXmlDocumentation(propertySymbol)?.WriteElementContentTo(Writer, elementName);
            }
        }
    }
}
