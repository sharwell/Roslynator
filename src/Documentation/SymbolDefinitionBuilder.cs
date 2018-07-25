// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal static class SymbolDefinitionBuilder
    {
        public static ImmutableArray<SymbolDisplayPart> GetDisplayParts(
            ISymbol symbol,
            SymbolDisplayFormat format,
            SymbolDisplayTypeDeclarationOptions typeDeclarationOptions = SymbolDisplayTypeDeclarationOptions.None,
            Func<INamedTypeSymbol, bool> attributePredicate = null,
            bool formatBaseList = false,
            bool formatConstraints = false,
            bool tryUseNameOnly = false)
        {
            ImmutableArray<SymbolDisplayPart> parts;

            if (symbol is INamedTypeSymbol typeSymbol)
            {
                parts = typeSymbol.ToDisplayParts(format, typeDeclarationOptions);
            }
            else
            {
                parts = symbol.ToDisplayParts(format);
                typeSymbol = null;
            }

            ImmutableArray<AttributeData> attributes = ImmutableArray<AttributeData>.Empty;
            bool hasAttributes = false;

            if (attributePredicate != null)
            {
                attributes = symbol.GetAttributes();

                hasAttributes = attributes.Any(f => attributePredicate(f.AttributeClass));
            }

            int baseListCount = 0;
            INamedTypeSymbol baseType = null;
            ImmutableArray<INamedTypeSymbol> interfaces = default;

            if (typeSymbol != null)
            {
                if (typeSymbol.TypeKind.Is(TypeKind.Class, TypeKind.Interface))
                {
                    baseType = typeSymbol.BaseType;

                    if (baseType?.SpecialType == SpecialType.System_Object)
                        baseType = null;
                }

                interfaces = typeSymbol.Interfaces;

                if (interfaces.Any(f => f.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T))
                    interfaces = interfaces.RemoveAll(f => f.SpecialType == SpecialType.System_Collections_IEnumerable);

                baseListCount = interfaces.Length;

                if (baseType != null)
                    baseListCount++;
            }

            int constraintCount = 0;
            int whereIndex = -1;

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].IsKeyword("where"))
                {
                    if (whereIndex == -1)
                        whereIndex = i;

                    constraintCount++;
                }
            }

            if (!hasAttributes
                && baseListCount == 0
                && constraintCount == 0)
            {
                return parts;
            }

            INamespaceSymbol containingNamespace = symbol.ContainingNamespace;

            ImmutableArray<SymbolDisplayPart>.Builder builder = ImmutableArray.CreateBuilder<SymbolDisplayPart>(parts.Length);

            foreach (AttributeData attributeData in attributes
                .Where(f => attributePredicate(f.AttributeClass))
                .OrderBy(f => ToDisplayString(f.AttributeClass, containingNamespace, tryUseNameOnly)))
            {
                builder.AddPunctuation("[");
                builder.AddDisplayParts(attributeData.AttributeClass, containingNamespace, tryUseNameOnly);
                builder.AddPunctuation("]");
                builder.AddLineBreak();
            }

            if (baseListCount == 0
                && constraintCount == 0)
            {
                builder.AddRange(parts);
                return builder.ToImmutableArray();
            }

            if (baseListCount > 0)
            {
                if (whereIndex != -1)
                {
                    builder.AddRange(parts, whereIndex);
                }
                else
                {
                    builder.AddRange(parts);
                    builder.AddSpace();
                }

                builder.AddPunctuation(":");
                builder.AddSpace();

                if (baseType != null)
                {
                    builder.AddDisplayParts(baseType, containingNamespace, tryUseNameOnly);

                    if (interfaces.Any())
                    {
                        builder.AddPunctuation(",");

                        if (formatBaseList)
                        {
                            builder.AddLineBreak();
                            builder.AddIndentation();
                        }
                        else
                        {
                            builder.AddSpace();
                        }
                    }
                }

                interfaces = interfaces.Sort((x, y) =>
                {
                    INamespaceSymbol n1 = x.ContainingNamespace;
                    INamespaceSymbol n2 = y.ContainingNamespace;

                    if (!MetadataNameEqualityComparer<INamespaceSymbol>.Instance.Equals(n1, n2))
                    {
                        return string.CompareOrdinal(
                            n1.ToDisplayString(SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces),
                            n2.ToDisplayString(SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces));
                    }

                    return string.CompareOrdinal(
                        ToDisplayString(x, containingNamespace, tryUseNameOnly),
                        ToDisplayString(y, containingNamespace, tryUseNameOnly));
                });

                ImmutableArray<INamedTypeSymbol>.Enumerator en = interfaces.GetEnumerator();

                if (en.MoveNext())
                {
                    while (true)
                    {
                        builder.AddDisplayParts(en.Current, containingNamespace, tryUseNameOnly);

                        if (en.MoveNext())
                        {
                            builder.AddPunctuation(",");

                            if (formatBaseList)
                            {
                                builder.AddLineBreak();
                                builder.AddIndentation();
                            }
                            else
                            {
                                builder.AddSpace();
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (whereIndex != -1)
                {
                    if (!formatConstraints
                        || (baseListCount == 1 && constraintCount == 1))
                    {
                        builder.AddSpace();
                    }
                }
            }
            else if (whereIndex != -1)
            {
                builder.AddRange(parts, whereIndex);
            }
            else
            {
                builder.AddRange(parts);
            }

            if (whereIndex != -1)
            {
                for (int i = whereIndex; i < parts.Length; i++)
                {
                    if (parts[i].IsKeyword("where"))
                    {
                        if (formatConstraints
                            && (baseListCount > 1 || constraintCount > 1))
                        {
                            builder.AddLineBreak();
                            builder.AddIndentation();
                        }

                        builder.Add(parts[i]);
                    }
                    else if (parts[i].IsTypeName()
                        && parts[i].Symbol is INamedTypeSymbol namedTypeSymbol)
                    {
                        builder.AddDisplayParts(namedTypeSymbol, containingNamespace, tryUseNameOnly);
                    }
                    else
                    {
                        builder.Add(parts[i]);
                    }
                }
            }

            return builder.ToImmutableArray();
        }

        private static string ToDisplayString(INamedTypeSymbol symbol, INamespaceSymbol containingNamespace, bool useNameOnlyIfPossible)
        {
            ImmutableArray<SymbolDisplayPart>.Builder builder = ImmutableArray.CreateBuilder<SymbolDisplayPart>();

            builder.AddDisplayParts(symbol, containingNamespace, useNameOnlyIfPossible);

            return builder.ToImmutableArray().ToDisplayString();
        }

        private static void AddDisplayParts(this ImmutableArray<SymbolDisplayPart>.Builder builder, INamedTypeSymbol symbol, INamespaceSymbol containingNamespace, bool useNameOnlyIfPossible)
        {
            if (useNameOnlyIfPossible
                && symbol.ContainingNamespace == containingNamespace)
            {
                builder.AddRange(symbol.ToDisplayParts(SymbolDisplayFormats.TypeNameAndContainingTypes));
            }
            else
            {
                builder.AddRange(symbol.ToDisplayParts(SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces));
            }

            ImmutableArray<ITypeSymbol> typeArguments = symbol.TypeArguments;

            ImmutableArray<ITypeSymbol>.Enumerator en = typeArguments.GetEnumerator();

            if (en.MoveNext())
            {
                builder.AddPunctuation("<");

                while (true)
                {
                    if (en.Current.Kind == SymbolKind.NamedType)
                    {
                        builder.AddDisplayParts((INamedTypeSymbol)en.Current, containingNamespace, useNameOnlyIfPossible);
                    }
                    else
                    {
                        Debug.Assert(en.Current.Kind == SymbolKind.TypeParameter, en.Current.Kind.ToString());

                        builder.Add(new SymbolDisplayPart(SymbolDisplayPartKind.TypeParameterName, en.Current, en.Current.Name));
                    }

                    if (en.MoveNext())
                    {
                        builder.AddPunctuation(",");
                        builder.AddSpace();
                    }
                    else
                    {
                        break;
                    }
                }

                builder.AddPunctuation(">");
            }
        }

        private static void AddSpace(this ImmutableArray<SymbolDisplayPart>.Builder builder)
        {
            builder.Add(SymbolDisplayPartFactory.Space());
        }

        private static void AddIndentation(this ImmutableArray<SymbolDisplayPart>.Builder builder)
        {
            builder.Add(SymbolDisplayPartFactory.Indentation());
        }

        private static void AddLineBreak(this ImmutableArray<SymbolDisplayPart>.Builder builder)
        {
            builder.Add(SymbolDisplayPartFactory.LineBreak());
        }

        private static void AddPunctuation(this ImmutableArray<SymbolDisplayPart>.Builder builder, string text)
        {
            builder.Add(SymbolDisplayPartFactory.Punctuation(text));
        }
    }
}
