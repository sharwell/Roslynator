// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    //TODO: SymbolDisplayFormatProvider ?
    public abstract class SymbolDisplayFormatProvider
    {
        public static SymbolDisplayFormatProvider Default { get; } = new DefaultSymbolDisplayFormatProvider();

        public abstract SymbolDisplayFormat TitleFormat { get; }

        public abstract SymbolDisplayFormat MemberTitleFormat { get; }

        public abstract SymbolDisplayFormat OverloadedMemberTitleFormat { get; }

        public abstract SymbolDisplayFormat TypeFormat { get; }

        public abstract SymbolDisplayFormat FullDefinitionFormat { get; }

        public abstract SymbolDisplayFormat SimpleDefinitionFormat { get; }

        public abstract SymbolDisplayFormat InheritanceFormat { get; }

        public abstract SymbolDisplayFormat DerivedFormat { get; }

        public abstract SymbolDisplayFormat MemberImplementsFormat { get; }

        public abstract SymbolDisplayFormat AttributeFormat { get; }

        public abstract SymbolDisplayFormat CrefFormat { get; }

        private class DefaultSymbolDisplayFormatProvider : SymbolDisplayFormatProvider
        {
            public override SymbolDisplayFormat TitleFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypesAndTypeParameters; }
            }

            public override SymbolDisplayFormat MemberTitleFormat
            {
                get
                {
                    return SymbolDisplayFormats.Default.Update(
                        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
                        memberOptions: SymbolDisplayMemberOptions.IncludeExplicitInterface
                            | SymbolDisplayMemberOptions.IncludeParameters
                            | SymbolDisplayMemberOptions.IncludeContainingType,
                        delegateStyle: SymbolDisplayDelegateStyle.NameAndParameters,
                        parameterOptions: SymbolDisplayParameterOptions.IncludeType);
                }
            }

            public override SymbolDisplayFormat OverloadedMemberTitleFormat
            {
                get
                {
                    return SymbolDisplayFormats.Default.Update(
                        genericsOptions: SymbolDisplayGenericsOptions.None,
                        memberOptions: SymbolDisplayMemberOptions.IncludeExplicitInterface
                            | SymbolDisplayMemberOptions.IncludeContainingType);
                }
            }

            public override SymbolDisplayFormat TypeFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypesAndTypeParameters; }
            }

            public override SymbolDisplayFormat FullDefinitionFormat
            {
                get { return SymbolDisplayFormats.FullDefinition; }
            }

            public override SymbolDisplayFormat SimpleDefinitionFormat
            {
                get
                {
                    return SymbolDisplayFormats.Default.Update(
                        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
                        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
                        memberOptions: SymbolDisplayMemberOptions.IncludeExplicitInterface
                            | SymbolDisplayMemberOptions.IncludeParameters,
                        delegateStyle: SymbolDisplayDelegateStyle.NameAndParameters,
                        parameterOptions: SymbolDisplayParameterOptions.IncludeType);
                }
            }

            public override SymbolDisplayFormat InheritanceFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypesAndTypeParameters; }
            }

            public override SymbolDisplayFormat DerivedFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespacesAndTypeParameters; }
            }

            public override SymbolDisplayFormat MemberImplementsFormat
            {
                get
                {
                    return SymbolDisplayFormats.Default.Update(
                        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
                        memberOptions: SymbolDisplayMemberOptions.IncludeExplicitInterface
                            | SymbolDisplayMemberOptions.IncludeContainingType);
                }
            }

            public override SymbolDisplayFormat AttributeFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypesAndTypeParameters; }
            }

            public override SymbolDisplayFormat CrefFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypesAndTypeParameters; }
            }
        }
    }
}
