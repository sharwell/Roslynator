// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Roslynator.Documentation
{
    internal sealed class DocumentationFileEqualityComparer : EqualityComparer<IDocumentationFile>
    {
        private DocumentationFileEqualityComparer()
        {
        }

        public static DocumentationFileEqualityComparer Instance { get; } = new DocumentationFileEqualityComparer();

        public override bool Equals(IDocumentationFile x, IDocumentationFile y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (x == null)
                return false;

            if (y == null)
                return false;

            if (x.Kind != y.Kind)
                return false;

            ImmutableArray<string> n1 = x.Names;
            ImmutableArray<string> n2 = y.Names;

            if (n1.Length != n2.Length)
                return false;

            for (int i = n1.Length - 1; i >= 0; i--)
            {
                if (!string.Equals(n1[i], n2[i], StringComparison.Ordinal))
                    return false;
            }

            return true;
        }

        public override int GetHashCode(IDocumentationFile obj)
        {
            if (obj == null)
                return 0;

            return Hash.CombineValues(obj.Names, StringComparer.Ordinal);
        }
    }
}
