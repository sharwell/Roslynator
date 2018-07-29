// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;

namespace Roslynator.Documentation
{
    public interface IDocumentationFile : IEquatable<IDocumentationFile>
    {
        ImmutableArray<string> Names { get; }

        DocumentationKind DocumentationKind { get; }
    }
}
