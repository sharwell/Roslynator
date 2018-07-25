// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Roslynator.Documentation
{
    public abstract class DocumentationUriProvider
    {
        protected DocumentationUriProvider(IEnumerable<ExternalUriProvider> externalProviders = null)
        {
            ExternalProviders = (externalProviders != null)
                ? ImmutableArray.CreateRange(externalProviders)
                : ImmutableArray<ExternalUriProvider>.Empty;
        }

        public static DocumentationUriProvider GitHubProvider { get; } = new GitHubDocumentationUriProvider(ImmutableArray.Create(ExternalUriProvider.MicrosoftDocs));

        public ImmutableArray<ExternalUriProvider> ExternalProviders { get; }

        //TODO: rename DirectoryModel
        public SymbolDocumentationModel DirectoryModel { get; set; }

        //TODO: getdocumenturl
        public abstract string GetFilePath(DocumentationKind kind, SymbolDocumentationModel symbolModel);

        public abstract DocumentationUrlInfo GetLocalUrl(SymbolDocumentationModel symbolModel);

        public DocumentationUrlInfo GetExternalUrl(SymbolDocumentationModel symbolModel)
        {
            foreach (ExternalUriProvider provider in ExternalProviders)
            {
                DocumentationUrlInfo urlInfo = provider.CreateUrl(symbolModel);

                if (urlInfo.Url != null)
                    return urlInfo;
            }

            return default;
        }

        internal static string GetFullUri(string fileName, ImmutableArray<string> names, char separator)
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
