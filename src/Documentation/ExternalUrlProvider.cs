// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Roslynator.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public abstract class ExternalUrlProvider
    {
        public static ExternalUrlProvider MicrosoftDocs { get; } = new MicrosoftDocsUrlProvider();

        public abstract string Name { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay => Name;

        public abstract DocumentationUrlInfo CreateUrl(IDocumentationFile documentationFile);

        private class MicrosoftDocsUrlProvider : ExternalUrlProvider
        {
            public override string Name => "Microsoft Docs";

            public override DocumentationUrlInfo CreateUrl(IDocumentationFile documentationFile)
            {
                ImmutableArray<string> names = documentationFile.Names;

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

                return default;
            }
        }
    }
}
