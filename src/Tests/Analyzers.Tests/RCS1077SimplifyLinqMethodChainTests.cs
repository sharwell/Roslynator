﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslynator.CSharp;
using Roslynator.CSharp.Analysis;
using Roslynator.CSharp.CodeFixes;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpDiagnosticVerifier;

namespace Roslynator.Analyzers.Tests
{
    public static class RCS1077SimplifyLinqMethodChainTests
    {
        private static DiagnosticDescriptor Descriptor { get; } = DiagnosticDescriptors.SimplifyLinqMethodChain;

        private static DiagnosticAnalyzer Analyzer { get; } = new InvocationExpressionAnalyzer();

        private static CodeFixProvider CodeFixProvider { get; } = new SimplifyLinqMethodChainCodeFixProvider();

        [Theory]
        [InlineData("Where(_ => true).Any()", "Any(_ => true)")]
        [InlineData("Where(_ => true).Count()", "Count(_ => true)")]
        [InlineData("Where(_ => true).First()", "First(_ => true)")]
        [InlineData("Where(_ => true).FirstOrDefault()", "FirstOrDefault(_ => true)")]
        [InlineData("Where(_ => true).Last()", "Last(_ => true)")]
        [InlineData("Where(_ => true).LastOrDefault()", "LastOrDefault(_ => true)")]
        [InlineData("Where(_ => true).LongCount()", "LongCount(_ => true)")]
        [InlineData("Where(_ => true).Single()", "Single(_ => true)")]
        public static void TestDiagnosticWithCodeFix(string fixableCode, string fixedCode)
        {
            VerifyDiagnosticAndCodeFix(
@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var items = new List<string>();

        var x = items.<<<>>>;
    }
}
",
                fixableCode,
                fixedCode,
                descriptor: Descriptor,
                analyzer: Analyzer,
                codeFixProvider: CodeFixProvider);
        }

        [Fact]
        public static void TestDiagnosticWithCodeFix_Multiline()
        {
            VerifyDiagnosticAndCodeFix(
@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var items = new List<string>();

        var x = items
            .Where(_ => true)
            .Any();
    }
}
",
@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var items = new List<string>();

        var x = items
            .Any(_ => true);
    }
}
",
                descriptor: Descriptor,
                analyzer: Analyzer,
                codeFixProvider: CodeFixProvider);
        }

        [Theory]
        [InlineData("Where(_ => true).Any()", "Any(_ => true)")]
        [InlineData("Where(_ => true).Count()", "Count(_ => true)")]
        [InlineData("Where(_ => true).First()", "First(_ => true)")]
        [InlineData("Where(_ => true).FirstOrDefault()", "FirstOrDefault(_ => true)")]
        [InlineData("Where(_ => true).Last()", "Last(_ => true)")]
        [InlineData("Where(_ => true).LastOrDefault()", "LastOrDefault(_ => true)")]
        [InlineData("Where(_ => true).LongCount()", "LongCount(_ => true)")]
        [InlineData("Where(_ => true).Single()", "Single(_ => true)")]
        public static void TestDiagnosticWithCodeFix_ImmutableArray(string fixableCode, string fixedCode)
        {
            VerifyDiagnosticAndCodeFix(
@"
using System.Collections.Immutable;
using System.Linq;

class C
{
    void M()
    {
        ImmutableArray<string> items = ImmutableArray.Create<string>();

        var x = items.<<<>>>;
    }
}
",
            fixableCode,
            fixedCode,
            descriptor: Descriptor,
            analyzer: Analyzer,
            codeFixProvider: CodeFixProvider);
        }

        [Theory]
        [InlineData("Where(f => f is object).Cast<object>()", "OfType<object>()")]
        [InlineData("Where((f) => f is object).Cast<object>()", "OfType<object>()")]
        [InlineData(@"Where(f =>
        {
            return f is object;
        }).Cast<object>()", "OfType<object>()")]
        public static void TestCallOfTypeInsteadOfWhereAndCast(string fixableCode, string fixedCode)
        {
            VerifyDiagnosticAndCodeFix(
@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var items = new List<string>();

        IEnumerable<object> q = items.<<<>>>;
    }
}
",
                fixableCode,
                fixedCode,
                descriptor: Descriptor,
                analyzer: Analyzer,
                codeFixProvider: CodeFixProvider);
        }

        [Theory]
        [InlineData(@"Where(f => f.StartsWith(""a"")).Any(f => f.StartsWith(""b""))", @"Any(f => f.StartsWith(""a"") && f.StartsWith(""b""))")]
        [InlineData(@"Where((f) => f.StartsWith(""a"")).Any(f => f.StartsWith(""b""))", @"Any((f) => f.StartsWith(""a"") && f.StartsWith(""b""))")]
        public static void TestCombineWhereAndAny(string fixableCode, string fixedCode)
        {
            VerifyDiagnosticAndCodeFix(
@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var items = new List<string>();

        if (items.<<<>>>) { }
    }
}
",
                fixableCode,
                fixedCode,
                descriptor: Descriptor,
                analyzer: Analyzer,
                codeFixProvider: CodeFixProvider);
        }

        [Theory]
        [InlineData(@"Where(f => f.StartsWith(""a"")).Any(f => f.StartsWith(""b""))", @"Any(f => f.StartsWith(""a"") && f.StartsWith(""b""))")]
        [InlineData(@"Where((f) => f.StartsWith(""a"")).Any(f => f.StartsWith(""b""))", @"Any((f) => f.StartsWith(""a"") && f.StartsWith(""b""))")]
        public static void TestCombineWhereAndAny_ImmutableArray(string fixableCode, string fixedCode)
        {
            VerifyDiagnosticAndCodeFix(
@"
using System.Collections.Immutable;
using System.Linq;

class C
{
    void M()
    {
        ImmutableArray<string> items = ImmutableArray<string>.Empty;

        if (items.<<<>>>) { }
    }
}
",
                fixableCode,
                fixedCode,
                descriptor: Descriptor,
                analyzer: Analyzer,
                codeFixProvider: CodeFixProvider);
        }

        [Theory]
        [InlineData("items.FirstOrDefault(_ => true) != null", "items.Any(_ => true)")]
        [InlineData("items.FirstOrDefault(_ => true) == null", "!items.Any(_ => true)")]
        [InlineData("items.FirstOrDefault(_ => true) is null", "!items.Any(_ => true)")]
        public static void TestNullCheckWithFirstOrDefault_IEnumerable(string fixableCode, string fixedCode)
        {
            VerifyDiagnosticAndCodeFix(
@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var items = new List<string>();

        if (<<<>>>) { }
    }
}
",
                fixableCode,
                fixedCode,
                descriptor: Descriptor,
                analyzer: Analyzer,
                codeFixProvider: CodeFixProvider);
        }

        [Theory]
        [InlineData("items.FirstOrDefault(_ => true) != null", "items.Any(_ => true)")]
        [InlineData("items.FirstOrDefault(_ => true) == null", "!items.Any(_ => true)")]
        [InlineData("items.FirstOrDefault(_ => true) is null", "!items.Any(_ => true)")]
        public static void TestNullCheckWithFirstOrDefault_ImmutableArray(string fixableCode, string fixedCode)
        {
            VerifyDiagnosticAndCodeFix(
@"
using System.Collections.Immutable;
using System.Linq;

class C
{
    void M()
    {
        ImmutableArray<string> items = ImmutableArray<string>.Empty;

        if (<<<>>>) { }
    }
}
",
                fixableCode,
                fixedCode,
                descriptor: Descriptor,
                analyzer: Analyzer,
                codeFixProvider: CodeFixProvider);
        }

        [Fact]
        public static void TestNoDiagnostic()
        {
            VerifyNoDiagnostic(
@"
",
                descriptor: Descriptor,
                analyzer: Analyzer);
        }

        [Fact]
        public static void TestNoDiagnostic_CallOfTypeInsteadOfWhereAndCast()
        {
            VerifyNoDiagnostic(
@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var items = new List<string>();

        IEnumerable<object> q = items.Where(f => f is string).Cast<object>();
    }
}
",
                descriptor: Descriptor,
                analyzer: Analyzer);
        }

        [Fact]
        public static void TestNoDiagnostic_CombineWhereAndAny()
        {
            VerifyNoDiagnostic(
@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var items = new List<string>();

        if (items.Where(f => f.StartsWith(""a"")).Any(g => g.StartsWith(""b""))) { }
    }
}
",
                descriptor: Descriptor,
                analyzer: Analyzer);
        }
    }
}