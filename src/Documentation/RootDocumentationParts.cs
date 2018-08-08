// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Roslynator.Documentation
{
    [Flags]
    public enum RootDocumentationParts
    {
        None = 0,
        Content = 1,
        ExtendedExternalTypesLink = 2,
        Namespaces = 4,
        Classes = 8,
        StaticClasses = 16,
        Structs = 32,
        Interfaces = 64,
        Enums = 128,
        Delegates = 256,
        Types = Classes | StaticClasses | Structs | Interfaces | Enums | Delegates,
        All = Content | ExtendedExternalTypesLink | Namespaces | Types
    }
}
