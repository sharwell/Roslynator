// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
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

        public IDocumentationFile ContainingFile { get; set; }

        public abstract string GetDocumentPath(DocumentationKind kind, IDocumentationFile documentationFile);

        public abstract DocumentationUrlInfo GetLocalUrl(IDocumentationFile documentationFile);

        public DocumentationUrlInfo GetExternalUrl(IDocumentationFile documentationFile)
        {
            foreach (ExternalUrlProvider provider in ExternalProviders)
            {
                DocumentationUrlInfo urlInfo = provider.CreateUrl(documentationFile);

                if (urlInfo.Url != null)
                    return urlInfo;
            }

            return default;
        }

        internal static string GetUrl(string fileName, ImmutableArray<string> names, char separator)
        {
            int capacity = fileName.Length + 1;

            foreach (string name in names)
                capacity += name.Length;

            capacity += names.Length - 1;

            StringBuilder sb = StringBuilderCache.GetInstance(capacity);

            sb.Append(names.Last());

            for (int i = names.Length - 2; i >= 0; i--)
            {
                sb.Append(separator);
                sb.Append(names[i]);
            }

            sb.Append(separator);
            sb.Append(fileName);

            return StringBuilderCache.GetStringAndFree(sb);
        }
    }
}
