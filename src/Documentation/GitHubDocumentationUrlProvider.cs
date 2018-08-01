// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal class GitHubDocumentationUrlProvider : DocumentationUrlProvider
    {
        public const string ReadMeFileName = "README.md";

        private Dictionary<ISymbol, ImmutableArray<string>> _symbolToFoldersMap;

        public GitHubDocumentationUrlProvider(IEnumerable<ExternalUrlProvider> externalProviders = null)
            : base(externalProviders)
        {
        }

        public override ImmutableArray<string> GetFolders(ISymbol symbol)
        {
            if (_symbolToFoldersMap == null)
                _symbolToFoldersMap = new Dictionary<ISymbol, ImmutableArray<string>>();

            if (_symbolToFoldersMap.TryGetValue(symbol, out ImmutableArray<string> folders))
                return folders;

            folders = base.GetFolders(symbol);

            _symbolToFoldersMap[symbol] = folders;

            return folders;
        }

        public override string GetDocumentPath(DocumentationKind kind, ImmutableArray<string> folders)
        {
            switch (kind)
            {
                case DocumentationKind.Root:
                    return ReadMeFileName;
                case DocumentationKind.Namespace:
                case DocumentationKind.Type:
                case DocumentationKind.Member:
                    return GetUrl(ReadMeFileName, folders, Path.DirectorySeparatorChar);
                case DocumentationKind.ObjectModel:
                    return WellKnownNames.ObjectModelFileName;
                case DocumentationKind.ExtendedExternalTypes:
                    return WellKnownNames.ExtendedExternalTypesFileName;
                default:
                    throw new ArgumentException("", nameof(kind));
            }
        }

        public override DocumentationUrlInfo GetLocalUrl(ImmutableArray<string> folders)
        {
            string url = CreateLocalUrl();

            return new DocumentationUrlInfo(url, DocumentationUrlKind.Local);

            string CreateLocalUrl()
            {
                ImmutableArray<string> currentFolders = (CurrentSymbol != null)
                    ? GetFolders(CurrentSymbol)
                    : default;

                if (currentFolders.IsDefault)
                    return GetUrl(ReadMeFileName, folders, '/');

                if (FoldersEqual(currentFolders, folders))
                    return "./" + ReadMeFileName;

                int count = 0;

                int i = 0;
                int j = 0;

                while (i < folders.Length
                    && j < currentFolders.Length
                    && string.Equals(folders[i], currentFolders[j], StringComparison.Ordinal))
                {
                    count++;
                    i++;
                    j++;
                }

                int diff = currentFolders.Length - count;

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

                i = count;

                //i = folders.Length - 1 - count;

                if (i < folders.Length)
                {
                    if (sb.Length > 0)
                        sb.Append("/");

                    sb.Append(folders[i]);
                    i++;

                    while (i < folders.Length)
                    {
                        sb.Append("/");
                        sb.Append(folders[i]);
                        i++;
                    }
                }

                sb.Append("/");
                sb.Append(ReadMeFileName);

                Debug.WriteLine(sb.ToString());

                return StringBuilderCache.GetStringAndFree(sb);
            }

            bool FoldersEqual(ImmutableArray<string> folders1, ImmutableArray<string> folders2)
            {
                int length = folders1.Length;

                if (length != folders2.Length)
                    return false;

                for (int i = 0; i < length; i++)
                {
                    if (folders1[i] != folders2[i])
                        return false;
                }

                return true;
            }
        }
    }
}
