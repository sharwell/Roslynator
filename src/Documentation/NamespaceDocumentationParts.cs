// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Roslynator.Documentation
{
    [Flags]
    public enum NamespaceDocumentationParts
    {
        None = 0,
        Content = 1,
        Summary = 2,
        Examples = 4,
        Remarks = 8,
        Classes = 16,
        Structs = 32,
        Interfaces = 64,
        Enums = 128,
        Delegates = 256,
        SeeAlso = 512,
        All = Content | Summary | Examples | Remarks | Classes | Structs | Interfaces | Enums | Delegates | SeeAlso
    }
}
