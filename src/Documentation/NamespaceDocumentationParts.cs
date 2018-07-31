// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Roslynator.Documentation
{
    [Flags]
    public enum NamespaceDocumentationParts
    {
        None = 0,
        Heading = 1,
        Examples = 2,
        Remarks = 4,
        Classes = 8,
        Structs = 16,
        Interfaces = 32,
        Enums = 64,
        Delegates = 128,
        SeeAlso = 256,
        All = None | Heading | Examples | Remarks | Classes | Structs | Interfaces | Enums | Delegates | SeeAlso
    }
}
