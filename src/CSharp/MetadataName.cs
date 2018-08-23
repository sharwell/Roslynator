// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    internal readonly struct MetadataName : IEquatable<MetadataName>
    {
        public MetadataName(IEnumerable<string> containingNamespaces, string name)
            : this(containingNamespaces, Array.Empty<string>(), name)
        {
        }

        public MetadataName(IEnumerable<string> containingNamespaces, IEnumerable<string> containingTypes, string name)
            : this(containingNamespaces.ToImmutableArray(), containingTypes.ToImmutableArray(), name)
        {
        }

        public MetadataName(ImmutableArray<string> containingNamespaces, string name)
            : this(containingNamespaces, ImmutableArray<string>.Empty, name)
        {
        }

        public MetadataName(ImmutableArray<string> containingNamespaces, ImmutableArray<string> containingTypes, string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ContainingTypes = containingTypes;
            ContainingNamespaces = containingNamespaces;
        }

        public ImmutableArray<string> ContainingNamespaces { get; }

        public ImmutableArray<string> ContainingTypes { get; }

        public string Name { get; }

        public bool IsDefault
        {
            get { return Name == null; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            return ToString(SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);
        }

        internal string ToString(SymbolDisplayTypeQualificationStyle typeQualificationStyle)
        {
            if (IsDefault)
                return "";

            switch (typeQualificationStyle)
            {
                case SymbolDisplayTypeQualificationStyle.NameOnly:
                    {
                        return Name;
                    }
                case SymbolDisplayTypeQualificationStyle.NameAndContainingTypes:
                    {
                        if (ContainingTypes.Any())
                            return string.Join("+", ContainingTypes) + "+" + Name;

                        return Name;
                    }
                case SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces:
                    {
                        if (ContainingNamespaces.Any())
                        {
                            string @namespace = string.Join(".", ContainingNamespaces);

                            if (ContainingTypes.Any())
                            {
                                return @namespace + "." + string.Join("+", ContainingTypes) + "+" + Name;
                            }
                            else
                            {
                                return @namespace + "." + Name;
                            }
                        }
                        else if (ContainingTypes.Any())
                        {
                            return string.Join("+", ContainingTypes) + "+" + Name;
                        }

                        return Name;
                    }
            }

            throw new ArgumentException($"Unknown enum value '{typeQualificationStyle}'.", nameof(typeQualificationStyle));
        }

        public override bool Equals(object obj)
        {
            return obj is MetadataName other
                && Equals(other);
        }

        internal bool Equals(ISymbol symbol)
        {
            if (symbol == null)
                return false;

            if (!string.Equals(Name, symbol.MetadataName, StringComparison.Ordinal))
                return false;

            INamedTypeSymbol containingType = symbol.ContainingType;

            for (int i = ContainingTypes.Length - 1; i >= 0; i--)
            {
                if (containingType == null)
                    return false;

                if (!string.Equals(containingType.MetadataName, ContainingTypes[i], StringComparison.Ordinal))
                    return false;

                containingType = containingType.ContainingType;
            }

            if (containingType != null)
                return false;

            INamespaceSymbol containingNamespace = symbol.ContainingNamespace;

            for (int i = ContainingNamespaces.Length - 1; i >= 0; i--)
            {
                if (containingNamespace?.IsGlobalNamespace != false)
                    return false;

                if (!string.Equals(containingNamespace.Name, ContainingNamespaces[i], StringComparison.Ordinal))
                    return false;

                containingNamespace = containingNamespace.ContainingNamespace;
            }

            return containingNamespace?.IsGlobalNamespace == true;
        }

        public bool Equals(MetadataName other)
        {
            if (IsDefault)
                return other.IsDefault;

            if (other.IsDefault)
                return false;

            if (!string.Equals(Name, other.Name, StringComparison.Ordinal))
                return false;

            if (!ContainingTypes.SequenceEqual(other.ContainingTypes, StringComparer.Ordinal))
                return false;

            if (!ContainingNamespaces.SequenceEqual(other.ContainingNamespaces, StringComparer.Ordinal))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            if (IsDefault)
                return 0;

            return Hash.Combine(Hash.CombineValues(ContainingNamespaces, StringComparer.Ordinal),
                Hash.Combine(Hash.CombineValues(ContainingTypes, StringComparer.Ordinal),
                Hash.Create(Name)));
        }

        public static MetadataName ParseNamespaceName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            int length = name.Length;

            if (length == 0)
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (name[0] == '.')
                throw new ArgumentException("Name is invalid.", nameof(name));

            if (length > 1
                && name[length - 1] == '.')
            {
                throw new ArgumentException("Name is invalid.", nameof(name));
            }

            ImmutableArray<string>.Builder builder = null;

            int prev = 0;

            for (int i = 1; i < length - 1; i++)
            {
                if (name[i] == '.')
                {
                    string n = name.Substring(prev, i - prev);

                    (builder ?? (builder = ImmutableArray.CreateBuilder<string>())).Add(n);

                    prev = i + 1;
                }
            }

            if (builder == null)
                return new MetadataName(ImmutableArray<string>.Empty, name);

            return new MetadataName(builder.ToImmutableArray(), name.Substring(prev, length - prev));
        }

        public static MetadataName ParseTypeName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            int length = name.Length;

            if (length == 0)
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (name[0] == '.')
                throw new ArgumentException("Name is invalid.", nameof(name));

            if (name[0] == '+')
                throw new ArgumentException("Name is invalid.", nameof(name));

            if (length > 1)
            {
                if (name[length - 1] == '.')
                    throw new ArgumentException("Name is invalid.", nameof(name));

                if (name[length - 1] == '+')
                    throw new ArgumentException("Name is invalid.", nameof(name));
            }

            ImmutableArray<string>.Builder containingNamespaces = null;
            ImmutableArray<string>.Builder containingTypes = null;
            string containingType = null;

            int prevIndex = 0;

            for (int i = 1; i < length - 1; i++)
            {
                if (name[i] == '.')
                {
                    if (containingType != null
                        || containingTypes != null)
                    {
                        throw new ArgumentException("Name is invalid.", nameof(name));
                    }

                    string n = name.Substring(prevIndex, i - prevIndex);

                    (containingNamespaces ?? (containingNamespaces = ImmutableArray.CreateBuilder<string>())).Add(n);

                    prevIndex = i + 1;
                }
                else if (name[i] == '+')
                {
                    string n = name.Substring(prevIndex, i - prevIndex);

                    if (containingType == null)
                    {
                        containingType = n;
                    }
                    else
                    {
                        (containingTypes ?? (containingTypes = ImmutableArray.CreateBuilder<string>())).Add(containingType);

                        containingType = null;

                        containingTypes.Add(n);
                    }

                    prevIndex = i + 1;
                }
            }

            return new MetadataName(
                containingNamespaces?.ToImmutableArray() ?? ImmutableArray<string>.Empty,
                (containingType != null)
                    ? ImmutableArray.Create(containingType)
                    : containingTypes?.ToImmutableArray() ?? ImmutableArray<string>.Empty,
                name.Substring(prevIndex, length - prevIndex));
        }

        public static bool operator ==(in MetadataName info1, in MetadataName info2)
        {
            return info1.Equals(info2);
        }

        public static bool operator !=(in MetadataName info1, in MetadataName info2)
        {
            return !(info1 == info2);
        }
    }
}
