// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Roslynator.Documentation
{
    public abstract class DocumentationUrlProvider
    {
        protected DocumentationUrlProvider(IEnumerable<ExternalUrlProvider> externalProviders = null)
        {
            ExternalProviders = (externalProviders != null)
                ? ImmutableArray.CreateRange(externalProviders)
                : ImmutableArray<ExternalUrlProvider>.Empty;
        }

        public static DocumentationUrlProvider GitHubProvider { get; } = new GitHubDocumentationUrlProvider(ImmutableArray.Create(ExternalUrlProvider.MicrosoftDocs));

        public ImmutableArray<ExternalUrlProvider> ExternalProviders { get; }

        public ImmutableArray<string> CurrentFolders { get; set; }

        public abstract string GetDocumentPath(DocumentationKind kind, ImmutableArray<string> folders);

        public abstract DocumentationUrlInfo GetLocalUrl(ImmutableArray<string> folders);

        public DocumentationUrlInfo GetExternalUrl(ImmutableArray<string> folders)
        {
            foreach (ExternalUrlProvider provider in ExternalProviders)
            {
                DocumentationUrlInfo urlInfo = provider.CreateUrl(folders);

                if (urlInfo.Url != null)
                    return urlInfo;
            }

            return default;
        }

        internal static string GetUrl(string fileName, ImmutableArray<string> folders, char separator)
        {
            int capacity = fileName.Length + 1;

            foreach (string name in folders)
                capacity += name.Length;

            capacity += folders.Length - 1;

            StringBuilder sb = StringBuilderCache.GetInstance(capacity);

            sb.Append(folders[0]);

            for (int i = 1; i < folders.Length ; i++)
            {
                sb.Append(separator);
                sb.Append(folders[i]);
            }

            sb.Append(separator);
            sb.Append(fileName);

            return StringBuilderCache.GetStringAndFree(sb);
        }
    }
}
