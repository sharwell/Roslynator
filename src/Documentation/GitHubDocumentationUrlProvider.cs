// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Text;

namespace Roslynator.Documentation
{
    internal class GitHubDocumentationUrlProvider : DocumentationUrlProvider
    {
        public const string ReadMeFileName = "README.md";

        public GitHubDocumentationUrlProvider(IEnumerable<ExternalUrlProvider> externalProviders = null)
            : base(externalProviders)
        {
        }

        public override string GetDocumentPath(DocumentationKind kind, IDocumentationFile documentationFile)
        {
            switch (kind)
            {
                case DocumentationKind.Root:
                    return ReadMeFileName;
                case DocumentationKind.Namespace:
                case DocumentationKind.Type:
                case DocumentationKind.Member:
                    return GetUrl(ReadMeFileName, documentationFile.Names, Path.DirectorySeparatorChar);
                case DocumentationKind.ObjectModel:
                    return WellKnownNames.ObjectModelFileName;
                case DocumentationKind.ExtendedExternalTypes:
                    return WellKnownNames.ExtendedExternalTypesFileName;
                default:
                    throw new ArgumentException("", nameof(kind));
            }
        }

        public override DocumentationUrlInfo GetLocalUrl(IDocumentationFile documentationFile)
        {
            string url = CreateLocalUrl();

            return new DocumentationUrlInfo(url, DocumentationUrlKind.Local);

            string CreateLocalUrl()
            {
                if (ContainingFile == null)
                    return GetUrl(ReadMeFileName, documentationFile.Names, '/');

                if (ContainingFile == documentationFile)
                    return "./" + ReadMeFileName;

                int count = 0;

                ImmutableArray<string> names = documentationFile.Names;

                int i = names.Length - 1;
                int j = ContainingFile.Names.Length - 1;

                while (i >= 0
                    && j >= 0
                    && string.Equals(names[i], ContainingFile.Names[j], StringComparison.Ordinal))
                {
                    count++;
                    i--;
                    j--;
                }

                int diff = ContainingFile.Names.Length - count;

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
