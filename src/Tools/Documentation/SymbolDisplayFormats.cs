﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal static class SymbolDisplayFormats
    {
        public static SymbolDisplayFormat Default { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: DefaultGlobalNamespaceStyle,
             typeQualificationStyle: DefaultTypeQualificationStyle,
             genericsOptions: DefaultGenericsOptions,
             memberOptions: DefaultMemberOptions,
             delegateStyle: DefaultDelegateStyle,
             extensionMethodStyle: DefaultExtensionMethodStyle,
             parameterOptions: DefaultParameterOptions,
             propertyStyle: DefaultPropertyStyle,
             localOptions: DefaultLocalOptions,
             kindOptions: DefaultKindOptions,
             miscellaneousOptions: DefaultMiscellaneousOptions);

        public static SymbolDisplayFormat TypeName { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: DefaultGlobalNamespaceStyle,
             typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
             genericsOptions: SymbolDisplayGenericsOptions.None,
             memberOptions: DefaultMemberOptions,
             delegateStyle: DefaultDelegateStyle,
             extensionMethodStyle: DefaultExtensionMethodStyle,
             parameterOptions: DefaultParameterOptions,
             propertyStyle: DefaultPropertyStyle,
             localOptions: DefaultLocalOptions,
             kindOptions: DefaultKindOptions,
             miscellaneousOptions: DefaultMiscellaneousOptions);

        public static SymbolDisplayFormat TypeNameAndTypeParameters { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: DefaultGlobalNamespaceStyle,
             typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
             genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
             memberOptions: DefaultMemberOptions,
             delegateStyle: DefaultDelegateStyle,
             extensionMethodStyle: DefaultExtensionMethodStyle,
             parameterOptions: DefaultParameterOptions,
             propertyStyle: DefaultPropertyStyle,
             localOptions: DefaultLocalOptions,
             kindOptions: DefaultKindOptions,
             miscellaneousOptions: DefaultMiscellaneousOptions);

        public static SymbolDisplayFormat TypeNameAndContainingTypes { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: DefaultGlobalNamespaceStyle,
             typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes,
             genericsOptions: SymbolDisplayGenericsOptions.None,
             memberOptions: DefaultMemberOptions,
             delegateStyle: DefaultDelegateStyle,
             extensionMethodStyle: DefaultExtensionMethodStyle,
             parameterOptions: DefaultParameterOptions,
             propertyStyle: DefaultPropertyStyle,
             localOptions: DefaultLocalOptions,
             kindOptions: DefaultKindOptions,
             miscellaneousOptions: DefaultMiscellaneousOptions);

        public static SymbolDisplayFormat TypeNameAndContainingTypesAndTypeParameters { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: DefaultGlobalNamespaceStyle,
             typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes,
             genericsOptions: DefaultGenericsOptions
                | SymbolDisplayGenericsOptions.IncludeTypeParameters,
             memberOptions: DefaultMemberOptions,
             delegateStyle: DefaultDelegateStyle,
             extensionMethodStyle: DefaultExtensionMethodStyle,
             parameterOptions: DefaultParameterOptions,
             propertyStyle: DefaultPropertyStyle,
             localOptions: DefaultLocalOptions,
             kindOptions: DefaultKindOptions,
             miscellaneousOptions: DefaultMiscellaneousOptions);

        public static SymbolDisplayFormat TypeNameAndContainingTypesAndNamespaces { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining,
             typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
             genericsOptions: SymbolDisplayGenericsOptions.None,
             memberOptions: DefaultMemberOptions,
             delegateStyle: DefaultDelegateStyle,
             extensionMethodStyle: DefaultExtensionMethodStyle,
             parameterOptions: DefaultParameterOptions,
             propertyStyle: DefaultPropertyStyle,
             localOptions: DefaultLocalOptions,
             kindOptions: DefaultKindOptions,
             miscellaneousOptions: DefaultMiscellaneousOptions);

        public static SymbolDisplayFormat TypeNameAndContainingTypesAndNamespacesAndTypeParameters { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining,
             typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
             genericsOptions: DefaultGenericsOptions
                | SymbolDisplayGenericsOptions.IncludeTypeParameters,
             memberOptions: DefaultMemberOptions,
             delegateStyle: DefaultDelegateStyle,
             extensionMethodStyle: DefaultExtensionMethodStyle,
             parameterOptions: DefaultParameterOptions,
             propertyStyle: DefaultPropertyStyle,
             localOptions: DefaultLocalOptions,
             kindOptions: DefaultKindOptions,
             miscellaneousOptions: DefaultMiscellaneousOptions);

        public static SymbolDisplayFormat OverloadedMemberTitle { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: DefaultGlobalNamespaceStyle,
             typeQualificationStyle: DefaultTypeQualificationStyle,
             genericsOptions: SymbolDisplayGenericsOptions.None,
             memberOptions: DefaultMemberOptions
                | SymbolDisplayMemberOptions.IncludeExplicitInterface
                | SymbolDisplayMemberOptions.IncludeContainingType,
             delegateStyle: DefaultDelegateStyle,
             extensionMethodStyle: DefaultExtensionMethodStyle,
             parameterOptions: DefaultParameterOptions,
             propertyStyle: DefaultPropertyStyle,
             localOptions: DefaultLocalOptions,
             kindOptions: DefaultKindOptions,
             miscellaneousOptions: DefaultMiscellaneousOptions);

        public static SymbolDisplayFormat MemberTitle { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: DefaultGlobalNamespaceStyle,
             typeQualificationStyle: DefaultTypeQualificationStyle,
             genericsOptions: DefaultGenericsOptions
                | SymbolDisplayGenericsOptions.IncludeTypeParameters,
             memberOptions: DefaultMemberOptions
                | SymbolDisplayMemberOptions.IncludeExplicitInterface
                | SymbolDisplayMemberOptions.IncludeParameters
                | SymbolDisplayMemberOptions.IncludeContainingType,
             delegateStyle: SymbolDisplayDelegateStyle.NameAndParameters,
             extensionMethodStyle: DefaultExtensionMethodStyle,
             parameterOptions: DefaultParameterOptions
                | SymbolDisplayParameterOptions.IncludeType,
             propertyStyle: DefaultPropertyStyle,
             localOptions: DefaultLocalOptions,
             kindOptions: DefaultKindOptions,
             miscellaneousOptions: DefaultMiscellaneousOptions);

        public static SymbolDisplayFormat MemberImplements { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: DefaultGlobalNamespaceStyle,
             typeQualificationStyle: DefaultTypeQualificationStyle,
             genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
             memberOptions: DefaultMemberOptions
                | SymbolDisplayMemberOptions.IncludeExplicitInterface
                | SymbolDisplayMemberOptions.IncludeContainingType,
             delegateStyle: DefaultDelegateStyle,
             extensionMethodStyle: DefaultExtensionMethodStyle,
             parameterOptions: DefaultParameterOptions,
             propertyStyle: DefaultPropertyStyle,
             localOptions: DefaultLocalOptions,
             kindOptions: DefaultKindOptions,
             miscellaneousOptions: DefaultMiscellaneousOptions);

        public static SymbolDisplayFormat SimpleDefinition { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: DefaultGlobalNamespaceStyle,
             typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
             genericsOptions: DefaultGenericsOptions
                | SymbolDisplayGenericsOptions.IncludeTypeParameters,
             memberOptions: DefaultMemberOptions
                | SymbolDisplayMemberOptions.IncludeExplicitInterface
                | SymbolDisplayMemberOptions.IncludeParameters,
             delegateStyle: SymbolDisplayDelegateStyle.NameAndParameters,
             extensionMethodStyle: DefaultExtensionMethodStyle,
             parameterOptions: DefaultParameterOptions
                | SymbolDisplayParameterOptions.IncludeType,
             propertyStyle: DefaultPropertyStyle,
             localOptions: DefaultLocalOptions,
             kindOptions: DefaultKindOptions,
             miscellaneousOptions: DefaultMiscellaneousOptions);

        public static SymbolDisplayFormat FullDefinition { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: DefaultGlobalNamespaceStyle,
             typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes,
             genericsOptions: DefaultGenericsOptions
                | SymbolDisplayGenericsOptions.IncludeTypeParameters
                | SymbolDisplayGenericsOptions.IncludeTypeConstraints
                | SymbolDisplayGenericsOptions.IncludeVariance,
             memberOptions: DefaultMemberOptions
                | SymbolDisplayMemberOptions.IncludeType
                | SymbolDisplayMemberOptions.IncludeModifiers
                | SymbolDisplayMemberOptions.IncludeAccessibility
                | SymbolDisplayMemberOptions.IncludeExplicitInterface
                | SymbolDisplayMemberOptions.IncludeParameters
                | SymbolDisplayMemberOptions.IncludeConstantValue
                | SymbolDisplayMemberOptions.IncludeRef,
             delegateStyle: SymbolDisplayDelegateStyle.NameAndSignature,
             extensionMethodStyle: DefaultExtensionMethodStyle,
             parameterOptions: DefaultParameterOptions
                | SymbolDisplayParameterOptions.IncludeExtensionThis
                | SymbolDisplayParameterOptions.IncludeParamsRefOut
                | SymbolDisplayParameterOptions.IncludeType
                | SymbolDisplayParameterOptions.IncludeName
                | SymbolDisplayParameterOptions.IncludeDefaultValue,
             propertyStyle: SymbolDisplayPropertyStyle.ShowReadWriteDescriptor,
             localOptions: DefaultLocalOptions,
             kindOptions: DefaultKindOptions
                | SymbolDisplayKindOptions.IncludeNamespaceKeyword
                | SymbolDisplayKindOptions.IncludeTypeKeyword
                | SymbolDisplayKindOptions.IncludeMemberKeyword,
             miscellaneousOptions: DefaultMiscellaneousOptions
                | SymbolDisplayMiscellaneousOptions.UseSpecialTypes
                | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers
            );

        public static SymbolDisplayFormat SortDefinitionList { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
             typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes,
             genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters
                | SymbolDisplayGenericsOptions.IncludeTypeConstraints
                | SymbolDisplayGenericsOptions.IncludeVariance,
             memberOptions: SymbolDisplayMemberOptions.IncludeParameters
                | SymbolDisplayMemberOptions.IncludeConstantValue
                | SymbolDisplayMemberOptions.IncludeRef,
             delegateStyle: SymbolDisplayDelegateStyle.NameAndParameters,
             extensionMethodStyle: SymbolDisplayExtensionMethodStyle.StaticMethod,
             parameterOptions: SymbolDisplayParameterOptions.IncludeExtensionThis
                | SymbolDisplayParameterOptions.IncludeParamsRefOut
                | SymbolDisplayParameterOptions.IncludeType
                | SymbolDisplayParameterOptions.IncludeName
                | SymbolDisplayParameterOptions.IncludeDefaultValue,
             propertyStyle: SymbolDisplayPropertyStyle.NameOnly,
             localOptions: SymbolDisplayLocalOptions.None,
             kindOptions: SymbolDisplayKindOptions.None,
             miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes
        );

        public static SymbolDisplayFormat NamespaceDefinition { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: DefaultGlobalNamespaceStyle,
             typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
             genericsOptions: DefaultGenericsOptions,
             memberOptions: DefaultMemberOptions,
             delegateStyle: DefaultDelegateStyle,
             extensionMethodStyle: DefaultExtensionMethodStyle,
             parameterOptions: DefaultParameterOptions,
             propertyStyle: DefaultPropertyStyle,
             localOptions: DefaultLocalOptions,
             kindOptions: DefaultKindOptions
                | SymbolDisplayKindOptions.IncludeNamespaceKeyword,
             miscellaneousOptions: DefaultMiscellaneousOptions
        );

        public static SymbolDisplayFormat ExplicitImplementationFullName { get; } = new SymbolDisplayFormat(
             globalNamespaceStyle: DefaultGlobalNamespaceStyle,
             typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
             genericsOptions: DefaultGenericsOptions
                | SymbolDisplayGenericsOptions.IncludeTypeParameters,
             memberOptions: DefaultMemberOptions
                | SymbolDisplayMemberOptions.IncludeExplicitInterface
                | SymbolDisplayMemberOptions.IncludeContainingType,
             delegateStyle: DefaultDelegateStyle,
             extensionMethodStyle: DefaultExtensionMethodStyle,
             parameterOptions: DefaultParameterOptions,
             propertyStyle: DefaultPropertyStyle,
             localOptions: DefaultLocalOptions,
             kindOptions: DefaultKindOptions,
             miscellaneousOptions: DefaultMiscellaneousOptions);

        internal const SymbolDisplayGlobalNamespaceStyle DefaultGlobalNamespaceStyle
            = SymbolDisplayGlobalNamespaceStyle.Omitted;
            //= SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining;
            //= SymbolDisplayGlobalNamespaceStyle.Included;

        internal const SymbolDisplayTypeQualificationStyle DefaultTypeQualificationStyle
            = SymbolDisplayTypeQualificationStyle.NameOnly;
            //= SymbolDisplayTypeQualificationStyle.NameAndContainingTypes;
            //= SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces;

        internal const SymbolDisplayGenericsOptions DefaultGenericsOptions = SymbolDisplayGenericsOptions.None
            | SymbolDisplayGenericsOptions.IncludeTypeParameters;
            //| SymbolDisplayGenericsOptions.IncludeTypeConstraints
            //| SymbolDisplayGenericsOptions.IncludeVariance

        internal const SymbolDisplayMemberOptions DefaultMemberOptions = SymbolDisplayMemberOptions.None;
            //| SymbolDisplayMemberOptions.IncludeType
            //| SymbolDisplayMemberOptions.IncludeModifiers
            //| SymbolDisplayMemberOptions.IncludeAccessibility
            //| SymbolDisplayMemberOptions.IncludeExplicitInterface
            //| SymbolDisplayMemberOptions.IncludeParameters
            //| SymbolDisplayMemberOptions.IncludeContainingType
            //| SymbolDisplayMemberOptions.IncludeConstantValue
            //| SymbolDisplayMemberOptions.IncludeRef

        internal const SymbolDisplayDelegateStyle DefaultDelegateStyle
            = SymbolDisplayDelegateStyle.NameOnly;
            //= SymbolDisplayDelegateStyle.NameAndParameters;
            //= SymbolDisplayDelegateStyle.NameAndSignature;

        internal const SymbolDisplayExtensionMethodStyle DefaultExtensionMethodStyle
            = SymbolDisplayExtensionMethodStyle.Default;
            //= SymbolDisplayExtensionMethodStyle.InstanceMethod;
            //= SymbolDisplayExtensionMethodStyle.StaticMethod;

        internal const SymbolDisplayParameterOptions DefaultParameterOptions = SymbolDisplayParameterOptions.None;
            //| SymbolDisplayParameterOptions.IncludeExtensionThis
            //| SymbolDisplayParameterOptions.IncludeParamsRefOut
            //| SymbolDisplayParameterOptions.IncludeType
            //| SymbolDisplayParameterOptions.IncludeName
            //| SymbolDisplayParameterOptions.IncludeDefaultValue
            //| SymbolDisplayParameterOptions.IncludeOptionalBrackets

        internal const SymbolDisplayPropertyStyle DefaultPropertyStyle
            = SymbolDisplayPropertyStyle.NameOnly;
            //= SymbolDisplayPropertyStyle.ShowReadWriteDescriptor;

        internal const SymbolDisplayLocalOptions DefaultLocalOptions = SymbolDisplayLocalOptions.None;
            //| SymbolDisplayLocalOptions.IncludeType
            //| SymbolDisplayLocalOptions.IncludeConstantValue
            //| SymbolDisplayLocalOptions.IncludeRef

        internal const SymbolDisplayKindOptions DefaultKindOptions = SymbolDisplayKindOptions.None;
            //| SymbolDisplayKindOptions.IncludeNamespaceKeyword
            //| SymbolDisplayKindOptions.IncludeTypeKeyword
            //| SymbolDisplayKindOptions.IncludeMemberKeyword

        internal const SymbolDisplayMiscellaneousOptions DefaultMiscellaneousOptions = SymbolDisplayMiscellaneousOptions.None;
            //| SymbolDisplayMiscellaneousOptions.UseSpecialTypes
            //| SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers
            //| SymbolDisplayMiscellaneousOptions.UseAsterisksInMultiDimensionalArrays
            //| SymbolDisplayMiscellaneousOptions.UseErrorTypeSymbolName
            //| SymbolDisplayMiscellaneousOptions.RemoveAttributeSuffix
            //| SymbolDisplayMiscellaneousOptions.ExpandNullable
    }
}
