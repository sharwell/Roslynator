// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Roslynator.Documentation
{
    [Flags]
    public enum TypeDocumentationParts
    {
        None = 0,
        Content = 1,
        Namespace = 2,
        Assembly = 4,
        Obsolete = 8,
        Summary = 16,
        Definition = 32,
        TypeParameters = 64,
        Parameters = 128,
        ReturnValue = 256,
        Inheritance = 512,
        Attributes = 1024,
        Derived = 2048,
        Implements = 4096,
        Examples = 8192,
        Remarks = 16384,
        Constructors = 32768,
        Fields = 65536,
        Properties = 131072,
        Methods = 262144,
        Operators = 524288,
        Events = 1048576,
        ExplicitInterfaceImplementations = 2097152,
        ExtensionMethods = 4194304,
        Classes = 8388608,
        Structs = 16777216,
        Interfaces = 33554432,
        Enums = 67108864,
        Delegates = 134217728,
        NestedTypes = Classes | Structs | Interfaces | Enums | Delegates,
        SeeAlso = 268435456,
        AllExceptNestedTypes = All & ~NestedTypes,
        All = Content | Namespace | Assembly | Obsolete | Summary | Definition | TypeParameters | Parameters | ReturnValue | Inheritance | Attributes | Derived | Implements | Examples | Remarks | Constructors | Fields | Properties | Methods | Operators | Events | ExplicitInterfaceImplementations | ExtensionMethods | Classes | Structs | Interfaces | Enums | Delegates | SeeAlso,
    }
}
