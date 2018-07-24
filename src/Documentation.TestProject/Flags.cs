// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

#pragma warning disable CS1591, RCS1191

namespace Roslynator.Documentation.Test
{
    [Flags]
    public enum Flags
    {
        None = 0,
        A = 1,
        B = 2,
        AB = 3,
        C = 4,
        D = 8,
    }
}
