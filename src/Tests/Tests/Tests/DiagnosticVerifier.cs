﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Roslynator.Tests.Text;
using Xunit;
using static Roslynator.Tests.CompilerDiagnosticVerifier;

namespace Roslynator.Tests
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public abstract class DiagnosticVerifier : CodeVerifier
    {
        public abstract DiagnosticDescriptor Descriptor { get; }

        public abstract DiagnosticAnalyzer Analyzer { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return $"{Descriptor.Id} {Analyzer.GetType().Name}"; }
        }

        public async Task VerifyDiagnosticAsync(
            string source,
            CodeVerificationOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            SpanParserResult result = SpanParser.GetSpans(source);

            await VerifyDiagnosticAsync(
                result.Text,
                result.Spans.Select(f => CreateDiagnostic(f.Span, f.LineSpan)),
                additionalSources: null,
                options: options,
                cancellationToken).ConfigureAwait(false);
        }

        public async Task VerifyDiagnosticAsync(
            string theory,
            string fromData,
            CodeVerificationOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            (TextSpan span, string text) = SpanParser.ReplaceSpan(theory, fromData);

            SpanParserResult result = SpanParser.GetSpans(text);

            if (result.Spans.Any())
            {
                await VerifyDiagnosticAsync(result.Text, result.Spans.Select(f => f.Span), options, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await VerifyDiagnosticAsync(text, span, options, cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task VerifyDiagnosticAsync(
            string source,
            TextSpan span,
            CodeVerificationOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await VerifyDiagnosticAsync(
                source,
                CreateDiagnostic(source, span),
                options,
                cancellationToken).ConfigureAwait(false);
        }

        public async Task VerifyDiagnosticAsync(
            string source,
            IEnumerable<TextSpan> spans,
            CodeVerificationOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await VerifyDiagnosticAsync(
                source,
                spans.Select(span => CreateDiagnostic(source, span)),
                additionalSources: null,
                options: options,
                cancellationToken).ConfigureAwait(false);
        }

        public async Task VerifyDiagnosticAsync(
            string source,
            Diagnostic expectedDiagnostic,
            CodeVerificationOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await VerifyDiagnosticAsync(
                source,
                new Diagnostic[] { expectedDiagnostic },
                additionalSources: null,
                options: options,
                cancellationToken).ConfigureAwait(false);
        }

        public async Task VerifyDiagnosticAsync(
            string source,
            IEnumerable<Diagnostic> expectedDiagnostics,
            string[] additionalSources = null,
            CodeVerificationOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            Document document = CreateDocument(source, additionalSources ?? Array.Empty<string>());

            Compilation compilation = await document.Project.GetCompilationAsync(cancellationToken).ConfigureAwait(false);

            ImmutableArray<Diagnostic> compilerDiagnostics = compilation.GetDiagnostics(cancellationToken);

            if (options == null)
                options = Options;

            VerifyCompilerDiagnostics(compilerDiagnostics, options);

            if (options.EnableDiagnosticsDisabledByDefault)
                compilation = compilation.EnableDiagnosticsDisabledByDefault(Analyzer);

            ImmutableArray<Diagnostic> diagnostics = await compilation.GetAnalyzerDiagnosticsAsync(Analyzer, DiagnosticComparer.SpanStart, cancellationToken).ConfigureAwait(false);

            if (diagnostics.Length > 0
                && Analyzer.SupportedDiagnostics.Length > 1)
            {
                VerifyDiagnostics(FilterDiagnostics(), expectedDiagnostics, cancellationToken);
            }
            else
            {
                VerifyDiagnostics(diagnostics, expectedDiagnostics, cancellationToken);
            }

            IEnumerable<Diagnostic> FilterDiagnostics()
            {
                foreach (Diagnostic diagnostic in diagnostics)
                {
                    bool success = false;
                    foreach (Diagnostic expectedDiagnostic in expectedDiagnostics)
                    {
                        if (DiagnosticComparer.Id.Equals(diagnostic, expectedDiagnostic))
                        {
                            success = true;
                            break;
                        }
                    }

                    if (success)
                        yield return diagnostic;
                }
            }
        }

        public async Task VerifyNoDiagnosticAsync(
            string theory,
            string fromData,
            CodeVerificationOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            (TextSpan span, string text) = SpanParser.ReplaceSpan(theory, fromData);

            await VerifyNoDiagnosticAsync(
                source: text,
                additionalSources: null,
                options: options,
                cancellationToken).ConfigureAwait(false);
        }

        public async Task VerifyNoDiagnosticAsync(
            string source,
            string[] additionalSources = null,
            CodeVerificationOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!Analyzer.Supports(Descriptor))
                Assert.True(false, $"Diagnostic \"{Descriptor.Id}\" is not supported by analyzer \"{Analyzer.GetType().Name}\".");

            Document document = CreateDocument(source, additionalSources ?? Array.Empty<string>());

            Compilation compilation = await document.Project.GetCompilationAsync(cancellationToken).ConfigureAwait(false);

            ImmutableArray<Diagnostic> compilerDiagnostics = compilation.GetDiagnostics(cancellationToken);

            if (options == null)
                options = Options;

            VerifyCompilerDiagnostics(compilerDiagnostics, options);

            if (options.EnableDiagnosticsDisabledByDefault)
                compilation = compilation.EnableDiagnosticsDisabledByDefault(Analyzer);

            ImmutableArray<Diagnostic> analyzerDiagnostics = await compilation.GetAnalyzerDiagnosticsAsync(Analyzer, DiagnosticComparer.SpanStart, cancellationToken).ConfigureAwait(false);

            foreach (Diagnostic diagnostic in analyzerDiagnostics)
            {
                if (string.Equals(diagnostic.Id, Descriptor.Id, StringComparison.Ordinal))
                    Assert.True(false, $"No diagnostic expected{analyzerDiagnostics.Where(f => string.Equals(f.Id, Descriptor.Id, StringComparison.Ordinal)).ToDebugString()}");
            }
        }

        private void VerifyDiagnostics(
            IEnumerable<Diagnostic> actual,
            IEnumerable<Diagnostic> expected,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            VerifyDiagnostics(actual, expected, checkAdditionalLocations: false, cancellationToken: cancellationToken);
        }

        private void VerifyDiagnostics(
            IEnumerable<Diagnostic> actual,
            IEnumerable<Diagnostic> expected,
            bool checkAdditionalLocations,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            int expectedCount = 0;
            int actualCount = 0;

            using (IEnumerator<Diagnostic> expectedEnumerator = expected.GetEnumerator())
            using (IEnumerator<Diagnostic> actualEnumerator = actual.GetEnumerator())
            {
                if (!expectedEnumerator.MoveNext())
                    throw new InvalidOperationException($"'{nameof(expected)}' contains no elements.");

                do
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    expectedCount++;

                    Diagnostic expectedDiagnostic = expectedEnumerator.Current;

                    if (!Analyzer.Supports(expectedDiagnostic.Descriptor))
                        Assert.True(false, $"Diagnostic \"{expectedDiagnostic.Id}\" is not supported by analyzer \"{Analyzer.GetType().Name}\".");

                    if (actualEnumerator.MoveNext())
                    {
                        actualCount++;

                        VerifyDiagnostic(actualEnumerator.Current, expectedDiagnostic, checkAdditionalLocations: checkAdditionalLocations);
                    }
                    else
                    {
                        while (expectedEnumerator.MoveNext())
                            expectedCount++;

                        Assert.True(false, $"Mismatch between number of diagnostics returned, expected: {expectedCount} actual: {actualCount}{actual.ToDebugString()}");
                    }

                } while (expectedEnumerator.MoveNext());

                if (actualEnumerator.MoveNext())
                {
                    actualCount++;

                    while (actualEnumerator.MoveNext())
                        actualCount++;

                    Assert.True(false, $"Mismatch between number of diagnostics returned, expected: {expectedCount} actual: {actualCount}{actual.ToDebugString()}");
                }
            }
        }

        private static void VerifyDiagnostic(
            Diagnostic actualDiagnostic,
            Diagnostic expectedDiagnostic,
            bool checkAdditionalLocations = false)
        {
            if (actualDiagnostic.Id != expectedDiagnostic.Id)
                Assert.True(false, $"Diagnostic id expected to be \"{expectedDiagnostic.Id}\", actual: \"{actualDiagnostic.Id}\"{GetMessage()}");

            VerifyLocation(actualDiagnostic.Location, expectedDiagnostic.Location);

            if (checkAdditionalLocations)
                VerifyAdditionalLocations(actualDiagnostic.AdditionalLocations, expectedDiagnostic.AdditionalLocations);

            void VerifyLocation(
                Location actualLocation,
                Location expectedLocation)
            {
                VerifyFileLinePositionSpan(actualLocation.GetLineSpan(), expectedLocation.GetLineSpan());
            }

            void VerifyAdditionalLocations(
                IReadOnlyList<Location> actual,
                IReadOnlyList<Location> expected)
            {
                int actualCount = actual.Count;
                int expectedCount = expected.Count;

                if (actualCount != expectedCount)
                    Assert.True(false, $"{expectedCount} additional location(s) expected, actual: {actualCount}{GetMessage()}");

                for (int j = 0; j < actualCount; j++)
                    VerifyLocation(actual[j], expected[j]);
            }

            void VerifyFileLinePositionSpan(
                FileLinePositionSpan actual,
                FileLinePositionSpan expected)
            {
                if (actual.Path != expected.Path)
                    Assert.True(false, $"Diagnostic expected to be in file \"{expected.Path}\", actual: \"{actual.Path}\"{GetMessage()}");

                VerifyLinePosition(actual.StartLinePosition, expected.StartLinePosition, "start");

                VerifyLinePosition(actual.EndLinePosition, expected.EndLinePosition, "end");
            }

            void VerifyLinePosition(
                LinePosition actual,
                LinePosition expected,
                string startOrEnd)
            {
                int actualLine = actual.Line;
                int expectedLine = expected.Line;

                if (actualLine != expectedLine)
                    Assert.True(false, $"Diagnostic expected to {startOrEnd} on line {expectedLine}, actual: {actualLine}{GetMessage()}");

                int actualCharacter = actual.Character;
                int expectedCharacter = expected.Character;

                if (actualCharacter != expectedCharacter)
                    Assert.True(false, $"Diagnostic expected to {startOrEnd} at column {expectedCharacter}, actual: {actualCharacter}{GetMessage()}");
            }

            string GetMessage()
            {
                return $"\r\n\r\nExpected diagnostic:\r\n{expectedDiagnostic}\r\n\r\nActual diagnostic:\r\n{actualDiagnostic}\r\n";
            }
        }

        private protected Diagnostic CreateDiagnostic(string source, TextSpan span)
        {
            LinePositionSpan lineSpan = span.ToLinePositionSpan(source);

            return CreateDiagnostic(span, lineSpan);
        }

        private protected Diagnostic CreateDiagnostic(TextSpan span, LinePositionSpan lineSpan)
        {
            Location location = Location.Create(CreateFileName(), span, lineSpan);

            return Diagnostic.Create(Descriptor, location);
        }
    }
}
