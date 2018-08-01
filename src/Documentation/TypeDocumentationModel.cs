// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public sealed class TypeDocumentationModel : SymbolDocumentationModel
    {
        private ImmutableArray<ISymbol> _members;
        private ImmutableArray<ISymbol> _membersIncludingInherited;

        internal TypeDocumentationModel(
            INamedTypeSymbol typeSymbol,
            DocumentationModel documentationModel) : base(typeSymbol, documentationModel)
        {
            TypeSymbol = typeSymbol;
        }

        public INamedTypeSymbol TypeSymbol { get; }

        public TypeKind TypeKind => TypeSymbol.TypeKind;

        public IEnumerable<TypeDocumentationModel> TypeModels
        {
            get
            {
                return TypeSymbol
                    .GetTypeMembers()
                    .Where(f => DocumentationModel.IsVisible(f))
                    .Select(f => DocumentationModel.GetTypeModel(f));
            }
        }

        internal ImmutableArray<ISymbol> Members
        {
            get
            {
                if (_members.IsDefault)
                {
                    _members = TypeSymbol.GetMembers(f => DocumentationModel.IsVisible(f));
                }

                return _members;
            }
        }

        internal ImmutableArray<ISymbol> MembersIncludingInherited
        {
            get
            {
                if (_membersIncludingInherited.IsDefault)
                {
                    if (Symbol.IsStatic)
                    {
                        _membersIncludingInherited = Members;
                    }
                    else
                    {
                        _membersIncludingInherited = TypeSymbol.GetMembers(f => DocumentationModel.IsVisible(f), includeInherited: true);
                    }
                }

                return _membersIncludingInherited;
            }
        }

        private ImmutableArray<ISymbol> GetMembers(bool includeInherited)
        {
            return (includeInherited) ? MembersIncludingInherited : Members;
        }

        public IEnumerable<IFieldSymbol> GetFields(bool includeInherited = false)
        {
            if (TypeKind != TypeKind.Delegate)
            {
                foreach (ISymbol member in (GetMembers(includeInherited)))
                {
                    if (member.Kind == SymbolKind.Field)
                        yield return (IFieldSymbol)member;
                }
            }
        }

        public IEnumerable<IMethodSymbol> GetConstructors()
        {
            if (!TypeKind.Is(TypeKind.Delegate, TypeKind.Enum))
            {
                foreach (ISymbol member in Members)
                {
                    if (member.Kind == SymbolKind.Method)
                    {
                        var methodSymbol = (IMethodSymbol)member;

                        if (methodSymbol.MethodKind == MethodKind.Constructor)
                        {
                            if (methodSymbol.ContainingType.TypeKind != TypeKind.Struct
                                || methodSymbol.Parameters.Any())
                            {
                                yield return methodSymbol;
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<IPropertySymbol> GetProperties(bool includeInherited = false)
        {
            if (!TypeKind.Is(TypeKind.Delegate, TypeKind.Enum))
            {
                foreach (ISymbol member in (GetMembers(includeInherited)))
                {
                    if (member.Kind == SymbolKind.Property)
                        yield return (IPropertySymbol)member;
                }
            }
        }

        public IEnumerable<IMethodSymbol> GetMethods(bool includeInherited = false)
        {
            if (!TypeKind.Is(TypeKind.Delegate, TypeKind.Enum))
            {
                foreach (ISymbol member in (GetMembers(includeInherited)))
                {
                    if (member.Kind == SymbolKind.Method)
                    {
                        var methodSymbol = (IMethodSymbol)member;

                        if (methodSymbol.MethodKind == MethodKind.Ordinary)
                            yield return methodSymbol;
                    }
                }
            }
        }

        public IEnumerable<IMethodSymbol> GetOperators(bool includeInherited = false)
        {
            if (!TypeKind.Is(TypeKind.Delegate, TypeKind.Enum))
            {
                foreach (ISymbol member in (GetMembers(includeInherited)))
                {
                    if (member.Kind == SymbolKind.Method)
                    {
                        var methodSymbol = (IMethodSymbol)member;

                        if (methodSymbol.MethodKind.Is(
                            MethodKind.UserDefinedOperator,
                            MethodKind.Conversion))
                        {
                            yield return methodSymbol;
                        }
                    }
                }
            }
        }

        public IEnumerable<IEventSymbol> GetEvents(bool includeInherited = false)
        {
            if (!TypeKind.Is(TypeKind.Delegate, TypeKind.Enum))
            {
                foreach (ISymbol member in (GetMembers(includeInherited)))
                {
                    if (member.Kind == SymbolKind.Event)
                        yield return (IEventSymbol)member;
                }
            }
        }

        public IEnumerable<ISymbol> GetExplicitInterfaceImplementations()
        {
            if (TypeKind.Is(TypeKind.Delegate, TypeKind.Enum))
                yield break;

            foreach (ISymbol member in TypeSymbol.GetMembers())
            {
                switch (member.Kind)
                {
                    case SymbolKind.Event:
                        {
                            var eventSymbol = (IEventSymbol)member;

                            if (!eventSymbol.ExplicitInterfaceImplementations.IsDefaultOrEmpty)
                                yield return eventSymbol;

                            break;
                        }
                    case SymbolKind.Method:
                        {
                            var methodSymbol = (IMethodSymbol)member;

                            if (methodSymbol.MethodKind != MethodKind.ExplicitInterfaceImplementation)
                                break;

                            ImmutableArray<IMethodSymbol> explicitInterfaceImplementations = methodSymbol.ExplicitInterfaceImplementations;

                            if (explicitInterfaceImplementations.IsDefaultOrEmpty)
                                break;

                            if (methodSymbol.MetadataName.EndsWith(".get_Item", StringComparison.Ordinal))
                            {
                                if (explicitInterfaceImplementations[0].MethodKind == MethodKind.PropertyGet)
                                    break;
                            }
                            else if (methodSymbol.MetadataName.EndsWith(".set_Item", StringComparison.Ordinal))
                            {
                                if (explicitInterfaceImplementations[0].MethodKind == MethodKind.PropertySet)
                                    break;
                            }

                            yield return methodSymbol;
                            break;
                        }
                    case SymbolKind.Property:
                        {
                            var propertySymbol = (IPropertySymbol)member;

                            if (!propertySymbol.ExplicitInterfaceImplementations.IsDefaultOrEmpty)
                                yield return propertySymbol;

                            break;
                        }
                }
            }
        }

        public IEnumerable<IMethodSymbol> GetExtensionMethods()
        {
            return DocumentationModel.GetExtensionMethods(TypeSymbol);
        }

        public IEnumerable<INamedTypeSymbol> GetDerivedTypes()
        {
            if (TypeKind.Is(TypeKind.Class, TypeKind.Interface)
                && !TypeSymbol.IsStatic)
            {
                foreach (TypeDocumentationModel typeModel in DocumentationModel.TypeModels)
                {
                    if (typeModel.TypeSymbol.InheritsFrom(TypeSymbol, includeInterfaces: true))
                        yield return typeModel.TypeSymbol;
                }
            }
        }

        public IEnumerable<INamedTypeSymbol> GetImplementedInterfaces(bool omitIEnumerable = false)
        {
            if (!TypeSymbol.IsStatic
                && !TypeSymbol.TypeKind.Is(TypeKind.Enum, TypeKind.Delegate))
            {
                ImmutableArray<INamedTypeSymbol> allInterfaces = TypeSymbol.AllInterfaces;

                if (omitIEnumerable
                    && allInterfaces.Any(f => f.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T))
                {
                    foreach (INamedTypeSymbol interfaceType in allInterfaces)
                    {
                        if (interfaceType.SpecialType != SpecialType.System_Collections_IEnumerable)
                            yield return interfaceType;
                    }
                }
                else
                {
                    foreach (INamedTypeSymbol interfaceType in allInterfaces)
                        yield return interfaceType;
                }
            }
        }

        public IEnumerable<MemberDocumentationModel> GetMemberModels(TypeDocumentationParts parts = TypeDocumentationParts.All)
        {
            if (TypeKind.Is(TypeKind.Enum, TypeKind.Delegate))
                yield break;

            if (IsEnabled(TypeDocumentationParts.Constructors))
            {
                foreach (MemberDocumentationModel model in GetMembers(GetConstructors()))
                    yield return model;
            }

            if (IsEnabled(TypeDocumentationParts.Fields))
            {
                foreach (MemberDocumentationModel model in GetMembers(GetFields()))
                    yield return model;
            }

            if (IsEnabled(TypeDocumentationParts.Properties))
            {
                foreach (MemberDocumentationModel model in GetMembers(GetProperties()))
                    yield return model;
            }

            if (IsEnabled(TypeDocumentationParts.Methods))
            {
                foreach (MemberDocumentationModel model in GetMembers(GetMethods()))
                    yield return model;
            }

            if (IsEnabled(TypeDocumentationParts.Operators))
            {
                foreach (MemberDocumentationModel model in GetMembers(GetOperators()))
                    yield return model;
            }

            if (IsEnabled(TypeDocumentationParts.Events))
            {
                foreach (MemberDocumentationModel model in GetMembers(GetEvents()))
                    yield return model;
            }

            if (IsEnabled(TypeDocumentationParts.ExplicitInterfaceImplementations))
            {
                foreach (MemberDocumentationModel model in GetMembers(GetExplicitInterfaceImplementations()))
                    yield return model;
            }

            bool IsEnabled(TypeDocumentationParts part)
            {
                return (parts & part) != 0;
            }

            IEnumerable<MemberDocumentationModel> GetMembers(IEnumerable<ISymbol> symbols)
            {
                foreach (IGrouping<string, ISymbol> grouping in symbols.GroupBy(f => f.Name))
                {
                    ImmutableArray<ISymbol> symbolsWithName = grouping.ToImmutableArray();

                    ISymbol symbol = symbolsWithName[0];

                    yield return new MemberDocumentationModel(symbol, symbolsWithName, DocumentationModel);
                }
            }
        }
    }
}
