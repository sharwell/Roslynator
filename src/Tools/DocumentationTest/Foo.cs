﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

#pragma warning disable CS1591, CA1051, CA1822, RCS1101, RCS1163

namespace Roslynator.Documentation.Test
{
    /// <summary>
    /// a &#x2192; &gt; b <see cref="Foo"/>
    /// </summary>
    /// <remarks>
    /// bla <c>null</c>
    /// <code>
    /// string s = null;
    /// </code>
    /// bla2
    /// <para>
    /// paratext
    /// </para>
    /// <list type="bullet">
    /// <item><description>This is the first bullet</description></item>
    /// <item>
    /// <description>
    /// This is
    /// the second bullet
    /// </description>
    /// </item>
    /// </list>
    /// <list type="table">
    /// <listheader>
    /// <term>h1</term>
    /// <description>h2</description>
    /// </listheader>
    /// <item>
    /// <term><see cref="int"/></term>
    /// <description>int</description>
    /// </item>
    /// <item>
    /// <term><see cref="int"/>2</term>
    /// <description>int2</description>
    /// </item>
    /// </list>
    /// <list type="table">
    /// <listheader>
    /// <term>h1</term>
    /// <term>h2</term>
    /// <term>h3</term>
    /// </listheader>
    /// <item>
    /// <term><see cref="int"/>1</term>
    /// <term><see cref="int"/>2</term>
    /// <term><see cref="int"/>3</term>
    /// </item>
    /// </list>
    /// </remarks>
    /// <seealso cref="object"/>
    /// <seealso cref="Foo"/>
    /// <exception cref="ArgumentException">a</exception>
    /// <exception cref="ArgumentNullException">b</exception>
    [Obsolete("Foo is obsolete.")]
    public class Foo : Bla
    {
        /// <summary>
        /// f
        /// </summary>
        public readonly string Field = "";

        /// <summary>
        /// aaa
        /// </summary>
        /// <param name="s">qqq</param>
        /// <exception cref="ArgumentException">a</exception>
        /// <exception cref="ArgumentNullException">b</exception>
        /// <seealso cref="object"/>
        public Foo(string s)
        {
        }

        public Foo(string s, string s2)
        {
        }

        /// <summary>
        /// s <paramref name="value"/> <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">p</param>
        /// <PARAM name="value2">p</PARAM>
        /// <permission cref="Foo">ppp</permission>
        public void Bar<T>(string value, string value2)
        {
            Bar<T>(value, value2);
        }

        /// <summary>abc</summary>
        public void Bar()
        {
        }

        /// <summary>
        ///     1
        /// 2
        /// </summary>
        public void Bar2()
        {
        }

        /// <summary>
        /// s
        /// </summary>
        /// <param name="index">p</param>
        /// <returns>v</returns>
        public Foo this[int index]
        {
            get { return null; }
            set { }
        }

        /// <summary>
        /// s
        /// </summary>
        /// <param name="index">p</param>
        /// <param name="index2">p</param>
        /// <returns>v</returns>
        public Foo this[int index, int index2]
        {
            get { return null; }
            set { }
        }
    }
}
