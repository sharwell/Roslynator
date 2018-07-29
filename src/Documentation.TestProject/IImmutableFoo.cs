// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

#pragma warning disable CS1591

namespace Roslynator.Documentation.Test
{
    public interface IImmutableFoo<T>
        : IList<T>,
        IEquatable<IImmutableFoo<T>>,
        IList,
        IStructuralComparable,
        IStructuralEquatable,
        IImmutableList<T>
    {
        new T this[int index] { get; set; }
    }
}
