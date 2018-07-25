// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class GitHubDocumentationUriProvider : DocumentationUriProvider
    {
        public const string ReadMeFileName = "README.md";

        public GitHubDocumentationUriProvider(IEnumerable<ExternalDocumentationUrlProvider> externalProviders = null)
            : base(externalProviders)
        {
        }

        public override string GetFilePath(DocumentationKind kind, SymbolDocumentationModel symbolModel)
        {
            switch (kind)
            {
                case DocumentationKind.Root:
                    return ReadMeFileName;
                case DocumentationKind.Namespace:
                case DocumentationKind.Type:
                case DocumentationKind.Member:
                    return GetFullUri(ReadMeFileName, symbolModel.NameAndBaseNamesAndNamespaceNames, '\\');
                case DocumentationKind.ObjectModel:
                    return WellKnownNames.ObjectModelFileName;
                case DocumentationKind.ExtendedExternalTypes:
                    return WellKnownNames.ExtendedExternalTypesFileName;
                default:
                    throw new ArgumentException("", nameof(kind));
            }
        }

        public override DocumentationUrlInfo GetLocalUrl(SymbolDocumentationModel symbolModel, SymbolDocumentationModel directoryModel)
        {
            string url = CreateLocalUrl();

            return new DocumentationUrlInfo(url, DocumentationUrlKind.Local);

            string CreateLocalUrl()
            {
                if (directoryModel == null)
                    return GetFullUri(ReadMeFileName, symbolModel.NameAndBaseNamesAndNamespaceNames, '/');

                if (symbolModel == directoryModel)
                    return "./" + ReadMeFileName;

                int count = 0;

                ImmutableArray<ISymbol> symbols = symbolModel.SymbolAndBaseTypesAndNamespaces;

                int i = symbols.Length - 1;
                int j = directoryModel.SymbolAndBaseTypesAndNamespaces.Length - 1;

                while (i >= 0
                    && j >= 0
                    && symbols[i] == directoryModel.SymbolAndBaseTypesAndNamespaces[j])
                {
                    count++;
                    i--;
                    j--;
                }

                int diff = directoryModel.SymbolAndBaseTypesAndNamespaces.Length - count;

                StringBuilder sb = StringBuilderCache.GetInstance();

                if (diff > 0)
                {
                    sb.Append("..");
                    diff--;

                    while (diff > 0)
                    {
                        sb.Append("/..");
                        diff--;
                    }
                }

                ImmutableArray<string> names = symbolModel.NameAndBaseNamesAndNamespaceNames;

                i = names.Length - 1 - count;

                if (i >= 0)
                {
                    if (sb.Length > 0)
                        sb.Append("/");

                    sb.Append(names[i]);
                    i--;

                    while (i >= 0)
                    {
                        sb.Append("/");
                        sb.Append(names[i]);
                        i--;
                    }
                }

                sb.Append("/");
                sb.Append(ReadMeFileName);

                return StringBuilderCache.GetStringAndFree(sb);
            }
        }
    }
}
