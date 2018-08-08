// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Roslynator.Documentation
{
    [Flags]
    public enum RootDocumentationParts
    {
        None = 0,
        ExtendedExternalTypesLink = 2,
        Namespaces = 4,
        Types = 8,
        All = ExtendedExternalTypesLink | Namespaces | Types,
    }
}
