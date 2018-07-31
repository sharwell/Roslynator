// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.CodeAnalysis;
using Roslynator.CSharp;

namespace Roslynator.Documentation
{
    public abstract class DocumentationWriter : IDisposable
    {
        private bool _disposed;

        protected DocumentationWriter(
            DocumentationModel documentationModel,
            DocumentationUrlProvider urlProvider,
            DocumentationOptions options = null,
            DocumentationResources resources = null)
        {
            DocumentationModel = documentationModel;
            UrlProvider = urlProvider;
            Options = options ?? DocumentationOptions.Default;
            Resources = resources ?? DocumentationResources.Default;
        }

        public DocumentationModel DocumentationModel { get; }

        internal bool CanCreateTypeLocalUrl { get; set; } = true;

        internal bool CanCreateMemberLocalUrl { get; set; } = true;

        protected internal int BaseHeadingLevel { get; set; }

        public SymbolDisplayFormatProvider FormatProvider => Options.FormatProvider;

        public DocumentationOptions Options { get; }

        public DocumentationResources Resources { get; }

        public DocumentationUrlProvider UrlProvider { get; }

        private SymbolXmlDocumentation GetSymbolDocumentation(ISymbol symbol)
        {
            return DocumentationModel.GetXmlDocumentation(symbol);
        }

        public abstract void WriteStartDocument();

        public abstract void WriteEndDocument();

        public abstract void WriteStartBold();

        public abstract void WriteEndBold();

        public virtual void WriteBold(string text)
        {
            WriteStartBold();
            WriteString(text);
            WriteEndBold();
        }

        public abstract void WriteStartItalic();

        public abstract void WriteEndItalic();

        public virtual void WriteItalic(string text)
        {
            WriteStartItalic();
            WriteString(text);
            WriteEndItalic();
        }

        public abstract void WriteStartStrikethrough();

        public abstract void WriteEndStrikethrough();

        public virtual void WriteStrikethrough(string text)
        {
            WriteStartStrikethrough();
            WriteString(text);
            WriteEndStrikethrough();
        }

        public abstract void WriteInlineCode(string text);

        public abstract void WriteStartHeading(int level);

        public abstract void WriteEndHeading();

        public virtual void WriteHeading1(string text)
        {
            WriteHeading(1, text);
        }

        public virtual void WriteHeading2(string text)
        {
            WriteHeading(2, text);
        }

        public virtual void WriteHeading3(string text)
        {
            WriteHeading(3, text);
        }

        public virtual void WriteHeading4(string text)
        {
            WriteHeading(4, text);
        }

        public virtual void WriteHeading5(string text)
        {
            WriteHeading(5, text);
        }

        public virtual void WriteHeading6(string text)
        {
            WriteHeading(6, text);
        }

        public virtual void WriteHeading(int level, string text)
        {
            WriteStartHeading(level);
            WriteString(text);
            WriteEndHeading();
        }

        public abstract void WriteStartBulletList();

        public abstract void WriteEndBulletList();

        public abstract void WriteStartBulletItem();

        public abstract void WriteEndBulletItem();

        public virtual void WriteBulletItem(string text)
        {
            WriteStartBulletItem();
            WriteString(text);
            WriteEndBulletItem();
        }

        public abstract void WriteStartOrderedList();

        public abstract void WriteEndOrderedList();

        public abstract void WriteStartOrderedItem(int number);

        public abstract void WriteEndOrderedItem();

        public virtual void WriteOrderedItem(int number, string text)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException(nameof(number), number, "Item number must be greater than or equal to 0.");

            WriteStartOrderedItem(number);
            WriteString(text);
            WriteEndOrderedItem();
        }

        public abstract void WriteImage(string text, string url, string title = null);

        public abstract void WriteLink(string text, string url, string title = null);

        public void WriteLinkOrText(string text, string url = null, string title = null)
        {
            if (!string.IsNullOrEmpty(url))
            {
                WriteLink(text, url, title);
            }
            else
            {
                WriteString(text);
            }
        }

        public abstract void WriteCodeBlock(string text, string language = null);

        public abstract void WriteStartBlockQuote();

        public abstract void WriteEndBlockQuote();

        public virtual void WriteBlockQuote(string text)
        {
            WriteStartBlockQuote();
            WriteString(text);
            WriteEndBlockQuote();
        }

        public abstract void WriteHorizontalRule();

        public abstract void WriteStartTable(int columnCount);

        public abstract void WriteEndTable();

        public abstract void WriteStartTableRow();

        public abstract void WriteEndTableRow();

        public abstract void WriteStartTableCell();

        public abstract void WriteEndTableCell();

        public abstract void WriteTableHeaderSeparator();

        public abstract void WriteCharEntity(char value);

        public abstract void WriteEntityRef(string name);

        public abstract void WriteComment(string text);

        public abstract void Flush();

        public abstract void WriteString(string text);

        public abstract void WriteRaw(string data);

        public abstract void WriteLine();

        public virtual void WriteValue(bool value)
        {
            WriteString((value) ? Resources.TrueValue : Resources.FalseValue);
        }

        public virtual void WriteValue(int value)
        {
            WriteString(value.ToString(null, CultureInfo.InvariantCulture));
        }

        public virtual void WriteValue(long value)
        {
            WriteString(value.ToString(null, CultureInfo.InvariantCulture));
        }

        public virtual void WriteValue(float value)
        {
            WriteString(value.ToString(null, CultureInfo.InvariantCulture));
        }

        public virtual void WriteValue(double value)
        {
            WriteString(value.ToString(null, CultureInfo.InvariantCulture));
        }

        public virtual void WriteValue(decimal value)
        {
            WriteString(value.ToString(null, CultureInfo.InvariantCulture));
        }

        public void WriteSpace()
        {
            WriteString(" ");
        }

        public void WriteSymbol(ISymbol symbol, SymbolDisplayFormat format = null, SymbolDisplayAdditionalMemberOptions additionalOptions = SymbolDisplayAdditionalMemberOptions.None)
        {
            WriteString(symbol.ToDisplayString(format, additionalOptions));
        }

        public void WriteTableCell(string text)
        {
            WriteStartTableCell();
            WriteString(text);
            WriteEndTableCell();
        }

        public virtual void WriteTitle(ISymbol symbol)
        {
            WriteHeading(1, symbol, FormatProvider.TitleFormat, SymbolDisplayAdditionalMemberOptions.UseItemPropertyName | SymbolDisplayAdditionalMemberOptions.UseOperatorName, addLink: false);
        }

        public virtual void WriteNamespace(ISymbol symbol)
        {
            WriteString(Resources.NamespaceTitle);
            WriteString(Resources.Colon);
            WriteSpace();
            WriteLink(symbol.ContainingNamespace, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces);
            WriteLine();
            WriteLine();
        }

        public virtual void WriteAssembly(ISymbol symbol)
        {
            WriteString(Resources.AssemblyTitle);
            WriteString(Resources.Colon);
            WriteSpace();
            WriteString(symbol.ContainingAssembly.Name);
            WriteString(Resources.Dot);
            WriteString(Resources.DllExtension);
            WriteLine();
            WriteLine();
        }

        public virtual void WriteObsolete(ISymbol symbol)
        {
            WriteBold(Resources.ObsoleteWarning);
            WriteLine();
            WriteLine();

            TypedConstant typedConstant = symbol.GetAttribute(MetadataNames.System_ObsoleteAttribute).ConstructorArguments.FirstOrDefault();

            if (typedConstant.Type?.SpecialType == SpecialType.System_String)
            {
                string message = typedConstant.Value?.ToString();

                if (!string.IsNullOrEmpty(message))
                    WriteString(message);

                WriteLine();
            }

            WriteLine();
        }

        public virtual void WriteSummary(ISymbol symbol)
        {
            WriteSection(symbol, heading: Resources.SummaryTitle, "summary");
        }

        public virtual void WriteDefinition(ISymbol symbol)
        {
            ImmutableArray<SymbolDisplayPart> parts = SymbolDefinitionBuilder.GetDisplayParts(
                symbol,
                FormatProvider.FullDefinitionFormat,
                typeDeclarationOptions: SymbolDisplayTypeDeclarationOptions.IncludeAccessibility | SymbolDisplayTypeDeclarationOptions.IncludeModifiers,
                attributePredicate: f => DocumentationUtility.IsVisibleAttribute(f),
                formatBaseList: Options.FormatBaseList,
                formatConstraints: Options.FormatConstraints,
                tryUseNameOnly: true);

            WriteCodeBlock(parts.ToDisplayString(), symbol.Language);
        }

        public virtual void WriteTypeParameters(ISymbol symbol)
        {
            ImmutableArray<ITypeParameterSymbol> typeParameters = symbol.GetTypeParameters();

            if (typeParameters.Any())
                WriteTable(typeParameters, Resources.TypeParametersTitle, 3, Resources.NameTitle, Resources.SummaryTitle, SymbolDisplayFormats.TypeName);
        }

        public virtual void WriteParameters(ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        WriteTable(
                            methodSymbol.Parameters,
                            Resources.ParametersTitle,
                            3,
                            Resources.NameTitle,
                            Resources.SummaryTitle,
                            SymbolDisplayFormats.TypeName);

                        break;
                    }
                case SymbolKind.NamedType:
                    {
                        var namedTypeSymbol = (INamedTypeSymbol)symbol;

                        IMethodSymbol methodSymbol = namedTypeSymbol.DelegateInvokeMethod;

                        if (methodSymbol != null)
                        {
                            WriteTable(
                                methodSymbol.Parameters,
                                Resources.ParametersTitle,
                                3,
                                Resources.NameTitle,
                                Resources.SummaryTitle,
                                SymbolDisplayFormats.TypeName);
                        }

                        break;
                    }
                case SymbolKind.Property:
                    {
                        var propertySymbol = (IPropertySymbol)symbol;

                        WriteTable(
                            propertySymbol.Parameters,
                            Resources.ParametersTitle,
                            3,
                            Resources.NameTitle,
                            Resources.SummaryTitle,
                            SymbolDisplayFormats.TypeName);

                        break;
                    }
            }
        }

        public virtual void WriteReturnValue(ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.NamedType:
                    {
                        var namedTypeSymbol = (INamedTypeSymbol)symbol;

                        IMethodSymbol methodSymbol = namedTypeSymbol.DelegateInvokeMethod;

                        if (methodSymbol != null)
                        {
                            ITypeSymbol returnType = methodSymbol.ReturnType;

                            if (returnType.SpecialType == SpecialType.System_Void)
                                return;

                            WriteHeading(3, Resources.ReturnValueTitle);
                            WriteLinkOrTypeLink(returnType);
                            WriteLine();

                            GetSymbolDocumentation(symbol)?.WriteElementContentTo(this, "returns");
                        }

                        break;
                    }
            }
        }

        public virtual void WriteInheritance(ITypeSymbol typeSymbol)
        {
            TypeKind typeKind = typeSymbol.TypeKind;

            if (typeKind == TypeKind.Interface)
                return;

            if (typeKind == TypeKind.Class
                && typeSymbol.IsStatic)
            {
                return;
            }

            WriteHeading(3, Resources.InheritanceTitle);

            using (IEnumerator<ITypeSymbol> en = typeSymbol.BaseTypesAndSelf().Reverse().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    ITypeSymbol symbol = en.Current;

                    bool isLast = !en.MoveNext();

                    WriterLinkOrText(symbol, isLast);

                    do
                    {
                        WriteSpace();
                        WriteCharEntity(Resources.InheritanceChar);
                        WriteSpace();

                        symbol = en.Current;
                        isLast = !en.MoveNext();

                        WriterLinkOrText(symbol.OriginalDefinition, isLast);
                    }
                    while (!isLast);
                }
            }

            WriteLine();

            void WriterLinkOrText(ITypeSymbol symbol, bool isLast)
            {
                if (isLast)
                {
                    WriteSymbol(symbol, FormatProvider.InheritanceFormat);
                }
                else
                {
                    WriteLink(symbol, FormatProvider.InheritanceFormat);
                }
            }
        }

        public virtual void WriteAttributes(ISymbol symbol)
        {
            ImmutableArray<AttributeData> attributes = symbol.GetAttributes();

            if (!attributes.Any())
                return;

            IEnumerable<(ISymbol symbol, INamedTypeSymbol attributeSymbol)> symbolsAttributes = attributes
                .Where(f => DocumentationUtility.IsVisibleAttribute(f.AttributeClass))
                .Select(f => ((symbol, attributeClass: f.AttributeClass)));

            if (symbol is ITypeSymbol typeSymbol)
            {
                List<(ISymbol symbol, INamedTypeSymbol attributeSymbol)> inheritedAttributes = GetInheritedAttributes();

                if (inheritedAttributes != null)
                    symbolsAttributes = symbolsAttributes.Concat(inheritedAttributes);
            }

            using (IEnumerator<(ISymbol symbol, INamedTypeSymbol attributeSymbol)> en = symbolsAttributes
                .OrderBy(f => f.attributeSymbol.ToDisplayString(FormatProvider.TypeFormat))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    WriteHeading(3, Resources.AttributesTitle);

                    while (true)
                    {
                        WriteLink(en.Current.attributeSymbol, FormatProvider.TypeFormat);

                        if (symbol != en.Current.symbol)
                        {
                            WriteInheritedFrom(en.Current.symbol.OriginalDefinition, FormatProvider.TypeFormat);
                        }

                        if (en.MoveNext())
                        {
                            WriteString(Resources.Comma);
                            WriteSpace();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            WriteLine();

            List<(ISymbol symbol, INamedTypeSymbol attributeSymbol)> GetInheritedAttributes()
            {
                if (typeSymbol == null)
                    return null;

                List<(ISymbol typeSymbol, INamedTypeSymbol attributeSymbol)> inheritedAttributes = null;

                INamedTypeSymbol baseType = typeSymbol.BaseType;

                while (baseType != null
                    && baseType.SpecialType != SpecialType.System_Object)
                {
                    foreach (AttributeData attribute in baseType.GetAttributes())
                    {
                        AttributeData attributeUsage = attribute.AttributeClass.GetAttribute(MetadataNames.System_AttributeUsageAttribute);

                        if (attributeUsage != null)
                        {
                            TypedConstant typedConstant = attributeUsage.NamedArguments.FirstOrDefault(f => f.Key == "Inherited").Value;

                            if (typedConstant.Type?.SpecialType == SpecialType.System_Boolean
                                && (!(bool)typedConstant.Value))
                            {
                                continue;
                            }
                        }

                        if (!DocumentationUtility.IsVisibleAttribute(attribute.AttributeClass))
                            continue;

                        if (AttributesContains(attribute))
                            continue;

                        if (inheritedAttributes == null)
                        {
                            inheritedAttributes = new List<(ISymbol typeSymbol, INamedTypeSymbol attributeSymbol)>();
                        }
                        else if (InheritedAttributesContains(attribute))
                        {
                            continue;
                        }

                        inheritedAttributes.Add((baseType, attribute.AttributeClass));
                    }

                    baseType = baseType.BaseType;
                }

                return inheritedAttributes;

                bool AttributesContains(AttributeData attribute)
                {
                    foreach (AttributeData f in attributes)
                    {
                        if (f.AttributeClass == attribute.AttributeClass)
                            return true;
                    }

                    return false;
                }

                bool InheritedAttributesContains(AttributeData attribute)
                {
                    foreach ((ISymbol typeSymbol, INamedTypeSymbol attributeSymbol) f in inheritedAttributes)
                    {
                        if (f.attributeSymbol == attribute.AttributeClass)
                            return true;
                    }

                    return false;
                }
            }
        }

        public virtual void WriteDerived(IEnumerable<INamedTypeSymbol> derivedTypes)
        {
            using (IEnumerator<INamedTypeSymbol> en = derivedTypes
                .OrderBy(f => f.ToDisplayString(FormatProvider.DerivedFormat))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    WriteHeading(3, Resources.DerivedTitle);

                    int count = 0;

                    WriteStartBulletList();

                    do
                    {
                        WriteBulletItemLink(en.Current, FormatProvider.DerivedFormat);

                        count++;

                        if (count == Options.MaxDerivedItems)
                        {
                            if (en.MoveNext())
                                WriteBulletItem(Resources.Ellipsis);

                            break;
                        }
                    }
                    while (en.MoveNext());

                    WriteEndBulletList();
                }
            }
        }

        public virtual void WriteImplements(IEnumerable<INamedTypeSymbol> implementedTypes)
        {
            using (IEnumerator<INamedTypeSymbol> en = implementedTypes
                .OrderBy(f => f.ToDisplayString(FormatProvider.TypeFormat))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    WriteHeading(3, Resources.ImplementsTitle);

                    WriteStartBulletList();

                    do
                    {
                        WriteStartBulletItem();
                        WriteTypeLink(en.Current);
                        WriteEndBulletItem();
                    }
                    while (en.MoveNext());

                    WriteEndBulletList();
                }
            }
        }

        public virtual void WriteExceptions(ISymbol symbol)
        {
            GetSymbolDocumentation(symbol)?.WriteExceptionsTo(this);
        }

        public virtual void WriteExamples(ISymbol symbol)
        {
            WriteSection(symbol, heading: Resources.ExamplesTitle, "examples");
        }

        public virtual void WriteRemarks(ISymbol symbol)
        {
            WriteSection(symbol, heading: Resources.RemarksTitle, "remarks");
        }

        public virtual void WriteEnumFields(IEnumerable<IFieldSymbol> fields)
        {
            using (IEnumerator<IFieldSymbol> en = fields.GetEnumerator())
            {
                if (en.MoveNext())
                {
                    INamedTypeSymbol enumType = en.Current.ContainingType;

                    bool hasCombinedValue = false;

                    ImmutableArray<EnumFieldInfo> fieldInfos = default;

                    if (enumType.HasAttribute(MetadataNames.System_FlagsAttribute))
                    {
                        fieldInfos = EnumUtility.GetFields(enumType);

                        foreach (IFieldSymbol field in fields)
                        {
                            if (!EnumUtility.GetMinimalConstituentFields(field, fieldInfos).IsDefault)
                            {
                                hasCombinedValue = true;
                                break;
                            }
                        }
                    }

                    WriteHeading(2, Resources.FieldsTitle);

                    WriteStartTable((hasCombinedValue) ? 4 : 3);
                    WriteStartTableRow();
                    WriteTableCell(Resources.NameTitle);
                    WriteTableCell(Resources.ValueTitle);

                    if (hasCombinedValue)
                        WriteTableCell(Resources.CombinationOfTitle);

                    WriteTableCell(Resources.SummaryTitle);
                    WriteEndTableRow();
                    WriteTableHeaderSeparator();

                    do
                    {
                        IFieldSymbol fieldSymbol = en.Current;

                        WriteStartTableRow();
                        WriteTableCell(fieldSymbol.ToDisplayString(FormatProvider.SimpleDefinitionFormat));
                        WriteTableCell(fieldSymbol.ConstantValue.ToString());

                        if (hasCombinedValue)
                        {
                            WriteStartTableCell();

                            ImmutableArray<EnumFieldInfo> constitiuentFields = EnumUtility.GetMinimalConstituentFields(en.Current, fieldInfos);

                            if (!constitiuentFields.IsDefault)
                            {
                                WriteString(constitiuentFields[0].Name);

                                for (int i = 1; i < constitiuentFields.Length; i++)
                                {
                                    WriteString(" | ");
                                    WriteString(constitiuentFields[i].Name);
                                }
                            }

                            WriteEndTableCell();
                        }

                        SymbolXmlDocumentation xmlDocumentation = GetSymbolDocumentation(fieldSymbol);

                        if (xmlDocumentation != null)
                        {
                            WriteStartTableCell();
                            xmlDocumentation.WriteElementContentTo(this, "summary", inlineOnly: true);
                            WriteEndTableCell();
                        }

                        WriteEndTableRow();
                    }
                    while (en.MoveNext());

                    WriteEndTable();
                }
            }
        }

        public virtual void WriteConstructors(IEnumerable<IMethodSymbol> constructors)
        {
            WriteTable(constructors, Resources.ConstructorsTitle, 2, Resources.ConstructorTitle, Resources.SummaryTitle, FormatProvider.SimpleDefinitionFormat);
        }

        public virtual void WriteFields(IEnumerable<IFieldSymbol> fields, INamedTypeSymbol containingType)
        {
            WriteTable(fields, Resources.FieldsTitle, 2, Resources.FieldTitle, Resources.SummaryTitle, FormatProvider.SimpleDefinitionFormat, containingType: containingType);
        }

        public virtual void WriteProperties(IEnumerable<IPropertySymbol> properties, INamedTypeSymbol containingType)
        {
            WriteTable(properties, Resources.PropertiesTitle, 2, Resources.PropertyTitle, Resources.SummaryTitle, FormatProvider.SimpleDefinitionFormat, SymbolDisplayAdditionalMemberOptions.UseItemPropertyName, containingType: containingType);
        }

        public virtual void WriteMethods(IEnumerable<IMethodSymbol> methods, INamedTypeSymbol containingType)
        {
            WriteTable(methods, Resources.MethodsTitle, 2, Resources.MethodTitle, Resources.SummaryTitle, FormatProvider.SimpleDefinitionFormat, containingType: containingType);
        }

        public virtual void WriteOperators(IEnumerable<IMethodSymbol> operators, INamedTypeSymbol containingType)
        {
            WriteTable(operators, Resources.OperatorsTitle, 2, Resources.OperatorTitle, Resources.SummaryTitle, FormatProvider.SimpleDefinitionFormat, SymbolDisplayAdditionalMemberOptions.UseOperatorName, containingType: containingType);
        }

        public virtual void WriteEvents(IEnumerable<IEventSymbol> events, INamedTypeSymbol containingType)
        {
            WriteTable(events, Resources.EventsTitle, 2, Resources.EventTitle, Resources.SummaryTitle, FormatProvider.SimpleDefinitionFormat, containingType: containingType);
        }

        public virtual void WriteExplicitInterfaceImplementations(IEnumerable<ISymbol> explicitInterfaceImplementations)
        {
            WriteTable(explicitInterfaceImplementations, Resources.ExplicitInterfaceImplementationsTitle, 2, Resources.MemberTitle, Resources.SummaryTitle, FormatProvider.SimpleDefinitionFormat, SymbolDisplayAdditionalMemberOptions.UseItemPropertyName);
        }

        public virtual void WriteExtensionMethods(IEnumerable<IMethodSymbol> methods)
        {
            WriteTable(
                methods,
                Resources.ExtensionMethodsTitle,
                2,
                Resources.MethodTitle,
                Resources.SummaryTitle,
                FormatProvider.SimpleDefinitionFormat);
        }

        public virtual void WriteSeeAlso(ISymbol symbol)
        {
            GetSymbolDocumentation(symbol)?.WriteSeeAlsoTo(this);
        }

        private void WriteSection(ISymbol symbol, string heading, string elementName)
        {
            GetSymbolDocumentation(symbol)?.WriteSectionTo(this, heading, elementName);
        }

        internal void WriteTable(
            IEnumerable<ISymbol> symbols,
            string heading,
            int headingLevel,
            string header1,
            string header2,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalMemberOptions additionalOptions = SymbolDisplayAdditionalMemberOptions.None,
            bool addLink = true,
            INamedTypeSymbol containingType = null)
        {
            using (IEnumerator<ISymbol> en = symbols
                .OrderBy(f => f.ToDisplayString(format, additionalOptions))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    if (heading != null)
                        WriteHeading(headingLevel, heading);

                    WriteStartTable(2);
                    WriteStartTableRow();
                    WriteTableCell(header1);
                    WriteTableCell(header2);
                    WriteEndTableRow();
                    WriteTableHeaderSeparator();

                    do
                    {
                        ISymbol symbol = en.Current;

                        WriteStartTableRow();
                        WriteStartTableCell();

                        if (symbol.IsKind(SymbolKind.Parameter, SymbolKind.TypeParameter))
                        {
                            WriteString(symbol.Name);
                        }
                        else if (addLink)
                        {
                            WriteLink(symbol, format, additionalOptions);
                        }
                        else
                        {
                            WriteString(symbol.ToDisplayString(format, additionalOptions));
                        }

                        WriteEndTableCell();
                        WriteStartTableCell();

                        bool isInherited = containingType != null
                            && symbol.ContainingType != containingType;

                        if (symbol.IsKind(SymbolKind.Parameter, SymbolKind.TypeParameter))
                        {
                            GetSymbolDocumentation(symbol.ContainingSymbol)?.WriteParamContentTo(this, symbol.Name);
                        }
                        else
                        {
                            ISymbol symbol2 = (isInherited) ? symbol.OriginalDefinition : symbol;
                            GetSymbolDocumentation(symbol2)?.WriteElementContentTo(this, "summary", inlineOnly: true);
                        }

                        if (isInherited)
                        {
                            WriteInheritedFrom(symbol.ContainingType.OriginalDefinition, FormatProvider.TypeFormat, additionalOptions);
                        }
                        else
                        {
                            if (Options.IndicateOverride
                                && symbol.IsOverride)
                            {
                                ISymbol overriddenSymbol = symbol.OverriddenSymbol();

                                if (overriddenSymbol != null)
                                {
                                    WriteSpace();
                                    WriteString(Resources.OpenParenthesis);
                                    WriteString(Resources.OverridesTitle);
                                    WriteSpace();
                                    WriteLink(overriddenSymbol, SymbolDisplayFormats.MemberNameAndContainingTypeName, additionalOptions);
                                    WriteString(Resources.CloseParenthesis);
                                }
                            }

                            if (Options.IndicateInterfaceImplementation)
                            {
                                using (IEnumerator<ISymbol> en2 = symbol.FindImplementedInterfaceMembers().GetEnumerator())
                                {
                                    if (en2.MoveNext())
                                    {
                                        WriteSpace();
                                        WriteString(Resources.OpenParenthesis);
                                        WriteString(Resources.ImplementsTitle);

                                        while (true)
                                        {
                                            WriteSpace();
                                            WriteLink(en2.Current, SymbolDisplayFormats.MemberNameAndContainingTypeName, additionalOptions);

                                            if (en2.MoveNext())
                                            {
                                                WriteString(Resources.Comma);
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }

                                        WriteString(Resources.CloseParenthesis);
                                    }
                                }
                            }
                        }

                        WriteEndTableCell();
                        WriteEndTableRow();
                    }
                    while (en.MoveNext());

                    WriteEndTable();
                }
            }
        }

        private void WriteInheritedFrom(ISymbol symbol, SymbolDisplayFormat format, SymbolDisplayAdditionalMemberOptions additionalOptions = SymbolDisplayAdditionalMemberOptions.None)
        {
            WriteSpace();
            WriteString(Resources.OpenParenthesis);
            WriteString(Resources.InheritedFrom);
            WriteSpace();
            WriteLink(symbol, format, additionalOptions);
            WriteString(Resources.CloseParenthesis);
        }

        internal void WriteList(
            IEnumerable<ISymbol> symbols,
            string heading,
            int headingLevel,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalMemberOptions additionalOptions = SymbolDisplayAdditionalMemberOptions.None,
            bool canCreateExternalUrl = true)
        {
            using (IEnumerator<ISymbol> en = symbols
                .OrderBy(f => f.ToDisplayString(format, additionalOptions))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    if (heading != null)
                        WriteHeading(headingLevel, heading);

                    WriteStartBulletList();

                    do
                    {
                        WriteBulletItemLink(en.Current, format, canCreateExternalUrl: canCreateExternalUrl);
                    }
                    while (en.MoveNext());

                    WriteEndBulletList();
                }
            }
        }

        internal void WriteHeading(
            int level,
            ISymbol symbol,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalMemberOptions additionalOptions = SymbolDisplayAdditionalMemberOptions.None,
            bool addLink = true)
        {
            WriteStartHeading(level);

            if (addLink)
            {
                WriteLink(symbol, format, additionalOptions);
            }
            else
            {
                WriteSymbol(symbol, format, additionalOptions);
            }

            if (symbol.Kind != SymbolKind.Namespace
                || !((INamespaceSymbol)symbol).IsGlobalNamespace)
            {
                WriteSpace();
                WriteString(Resources.GetName(symbol));
            }

            WriteEndHeading();
        }

        internal void WriteBulletItemLink(
            ISymbol symbol,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalMemberOptions additionalOptions = SymbolDisplayAdditionalMemberOptions.None,
            bool canCreateExternalUrl = true)
        {
            WriteStartBulletItem();
            WriteLink(symbol, format, additionalOptions: additionalOptions, canCreateExternalUrl: canCreateExternalUrl);
            WriteEndBulletItem();
        }

        public void WriteLink(
            ISymbol symbol,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalMemberOptions additionalOptions = SymbolDisplayAdditionalMemberOptions.None,
            bool canCreateExternalUrl = true)
        {
            string url = GetUrl(symbol, canCreateExternalUrl);

            WriteLinkOrText(symbol.ToDisplayString(format, additionalOptions), url);
        }

        internal void WriteLinkOrTypeLink(
            ITypeSymbol typeSymbol,
            bool containingTypes = true,
            bool canCreateExternalUrl = true)
        {
            if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
            {
                WriteTypeLink(namedTypeSymbol, containingTypes: containingTypes, canCreateExternalUrl: canCreateExternalUrl);
            }
            else
            {
                SymbolDisplayFormat format = (containingTypes)
                    ? SymbolDisplayFormats.TypeNameAndContainingTypesAndTypeParameters
                    : SymbolDisplayFormats.TypeNameAndTypeParameters;

                WriteLink(typeSymbol, format, canCreateExternalUrl: canCreateExternalUrl);
            }
        }

        public void WriteTypeLink(
            INamedTypeSymbol typeSymbol,
            bool containingTypes = true,
            bool canCreateExternalUrl = true)
        {
            ImmutableArray<ITypeSymbol> typeArguments = typeSymbol.TypeArguments;

            if (typeSymbol.IsNullableType())
            {
                ITypeSymbol typeArgument = typeSymbol.TypeArguments[0];

                WriteLinkOrTypeLink(typeArgument, containingTypes: containingTypes, canCreateExternalUrl: canCreateExternalUrl);
                WriteString("?");
            }
            else if (typeArguments.Any(f => f.Kind != SymbolKind.TypeParameter))
            {
                SymbolDisplayFormat format = (containingTypes)
                    ? SymbolDisplayFormats.TypeNameAndContainingTypes
                    : SymbolDisplayFormats.TypeName;

                string url = GetUrl(typeSymbol, canCreateExternalUrl);

                WriteLinkOrText(typeSymbol.ToDisplayString(format), url);

                ImmutableArray<ITypeSymbol>.Enumerator en = typeArguments.GetEnumerator();

                if (en.MoveNext())
                {
                    WriteString("<");

                    while (true)
                    {
                        if (en.Current.Kind == SymbolKind.NamedType)
                        {
                            WriteTypeLink((INamedTypeSymbol)en.Current, containingTypes: containingTypes, canCreateExternalUrl: canCreateExternalUrl);
                        }
                        else
                        {
                            Debug.Assert(en.Current.Kind == SymbolKind.TypeParameter, en.Current.Kind.ToString());

                            WriteString(en.Current.Name);
                        }

                        if (en.MoveNext())
                        {
                            WriteString(", ");
                        }
                        else
                        {
                            break;
                        }
                    }

                    WriteString(">");
                }
            }
            else
            {
                SymbolDisplayFormat format = (containingTypes)
                    ? SymbolDisplayFormats.TypeNameAndContainingTypesAndTypeParameters
                    : SymbolDisplayFormats.TypeNameAndTypeParameters;

                string url = GetUrl(typeSymbol, canCreateExternalUrl);

                WriteLinkOrText(typeSymbol.ToDisplayString(format), url);
            }
        }

        private string GetUrl(
            ISymbol symbol,
            bool canCreateExternalUrl = true)
        {
            ImmutableArray<string> folders = DocumentationModel.GetFolders(symbol);

            if (folders.IsDefault)
                return null;

            switch (symbol.Kind)
            {
                case SymbolKind.NamedType:
                    {
                        if (!CanCreateTypeLocalUrl)
                            return null;

                        break;
                    }
                case SymbolKind.Event:
                case SymbolKind.Field:
                case SymbolKind.Method:
                case SymbolKind.Property:
                    {
                        if (!CanCreateMemberLocalUrl)
                            return null;

                        break;
                    }
                case SymbolKind.Parameter:
                case SymbolKind.TypeParameter:
                    {
                        return null;
                    }
            }

            if (DocumentationModel.IsExternal(symbol)
                && canCreateExternalUrl)
            {
                return UrlProvider.GetExternalUrl(folders).Url;
            }

            return UrlProvider.GetLocalUrl(folders).Url;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    Close();

                _disposed = true;
            }
        }

        public virtual void Close()
        {
        }
    }
}
