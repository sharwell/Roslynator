// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public sealed class ExtendedExternalTypesDocumentationModel
    {
        internal ExtendedExternalTypesDocumentationModel(DocumentationModel documentationModel)
        {
            DocumentationModel = documentationModel;
        }

        public DocumentationModel DocumentationModel { get; }

        public IEnumerable<NamespaceDocumentationModel> Namespaces()
        {
            return Types()
                .Select(f => f.Symbol.ContainingNamespace)
                .Distinct(MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
                .Select(f => DocumentationModel.GetNamespaceModel(f));
        }

        public IEnumerable<TypeDocumentationModel> Types()
        {
            return Iterator().Distinct().Select(f => DocumentationModel.GetTypeModel(f));

            IEnumerable<INamedTypeSymbol> Iterator()
            {
                foreach (IMethodSymbol methodSymbol in DocumentationModel.GetExtensionMethods())
                {
                    INamedTypeSymbol typeSymbol = GetExternalSymbol(methodSymbol);

                    if (typeSymbol != null)
                        yield return typeSymbol;
                }
            }

            INamedTypeSymbol GetExternalSymbol(IMethodSymbol methodSymbol)
            {
                INamedTypeSymbol type = DocumentationModel.GetExtendedTypeSymbol(methodSymbol);

                if (type == null)
                    return null;

                foreach (IAssemblySymbol assembly in DocumentationModel.Assemblies)
                {
                    if (type.ContainingAssembly == assembly)
                        return null;
                }

                return type;
            }
        }
    }
}
