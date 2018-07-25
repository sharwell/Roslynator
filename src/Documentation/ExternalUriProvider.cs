// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public abstract class ExternalUriProvider
    {
        public static ExternalUriProvider MicrosoftDocs { get; } = new MicrosoftDocsUriProvider();

        public abstract string Name { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay => Name;

        public abstract DocumentationUrlInfo CreateUrl(SymbolDocumentationModel symbolModel);

        private class MicrosoftDocsUriProvider : ExternalUriProvider
        {
            public override string Name => "Microsoft Docs";

            public override DocumentationUrlInfo CreateUrl(SymbolDocumentationModel symbolModel)
            {
                if (symbolModel.SymbolAndBaseTypesAndNamespaces.LastOrDefault()?.Kind == SymbolKind.Namespace)
                {
                    ImmutableArray<string> names = symbolModel.NameAndBaseNamesAndNamespaceNames;

                    switch (names.Last())
                    {
                        case "System":
                        case "Microsoft":
                            {
                                const string baseUrl = "https://docs.microsoft.com/en-us/dotnet/api/";

                                int capacity = baseUrl.Length;

                                foreach (string name in names)
                                    capacity += name.Length;

                                capacity += names.Length - 1;

                                StringBuilder sb = StringBuilderCache.GetInstance(capacity);

                                sb.Append(baseUrl);

                                sb.Append(names.Last().ToLowerInvariant());

                                for (int i = names.Length - 2; i >= 0; i--)
                                {
                                    sb.Append(".");
                                    sb.Append(names[i].ToLowerInvariant());
                                }

                                return new DocumentationUrlInfo(StringBuilderCache.GetStringAndFree(sb), DocumentationUrlKind.External);
                            }
                    }
                }

                return default;
            }
        }
    }
}
