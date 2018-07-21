namespace Roslynator
{
  public abstract class NameGenerator
  {
    protected NameGenerator();
    public static NameGenerator Default { get; }
    public abstract string EnsureUniqueName(string baseName, IEnumerable<string> reservedNames, bool isCaseSensitive = true);
    public abstract string EnsureUniqueName(string baseName, ImmutableArray<ISymbol> symbols, bool isCaseSensitive = true);
    public static bool IsUniqueName(string name, IEnumerable<string> reservedNames, bool isCaseSensitive = true);
    public static bool IsUniqueName(string name, ImmutableArray<ISymbol> symbols, bool isCaseSensitive = true);
    public static string CreateName(ITypeSymbol typeSymbol, bool firstCharToLower = false);
    public string EnsureUniqueLocalName(string baseName, SemanticModel semanticModel, int position, bool isCaseSensitive = true, CancellationToken cancellationToken = default(CancellationToken));
    public string EnsureUniqueMemberName(string baseName, INamedTypeSymbol typeSymbol, bool isCaseSensitive = true);
    public string EnsureUniqueMemberName(string baseName, SemanticModel semanticModel, int position, bool isCaseSensitive = true, CancellationToken cancellationToken = default(CancellationToken));
  }

  public class SeparatedSyntaxListSelection<TNode> where TNode : SyntaxNode
  {
    protected SeparatedSyntaxListSelection(SeparatedSyntaxList<TNode> list, TextSpan span, int firstIndex, int lastIndex);
    public TNode this[int index] { get; }
    public SeparatedSyntaxList<TNode> UnderlyingList { get; }
    public TextSpan OriginalSpan { get; }
    public int Count { get; }
    public int FirstIndex { get; }
    public int LastIndex { get; }
    public SeparatedSyntaxListSelection<TNode>.Enumerator GetEnumerator();
    public TNode First();
    public TNode Last();
    public static SeparatedSyntaxListSelection<TNode> Create(SeparatedSyntaxList<TNode> list, TextSpan span);
    public static bool TryCreate(SeparatedSyntaxList<TNode> list, TextSpan span, out SeparatedSyntaxListSelection<TNode> selection);

    public struct SeparatedSyntaxListSelection<TNode>.Enumerator
    {
      public TNode Current { get; }
      public bool MoveNext();
      public override bool Equals(object obj);
      public override int GetHashCode();
      public void Reset();
    }
  }

  public class SyntaxListSelection<TNode> where TNode : SyntaxNode
  {
    protected SyntaxListSelection(SyntaxList<TNode> list, TextSpan span, int firstIndex, int lastIndex);
    public TNode this[int index] { get; }
    public SyntaxList<TNode> UnderlyingList { get; }
    public TextSpan OriginalSpan { get; }
    public int Count { get; }
    public int FirstIndex { get; }
    public int LastIndex { get; }
    public SyntaxListSelection<TNode>.Enumerator GetEnumerator();
    public TNode First();
    public TNode Last();
    public static SyntaxListSelection<TNode> Create(SyntaxList<TNode> list, TextSpan span);
    public static bool TryCreate(SyntaxList<TNode> list, TextSpan span, out SyntaxListSelection<TNode> selection);

    public struct SyntaxListSelection<TNode>.Enumerator
    {
      public TNode Current { get; }
      public bool MoveNext();
      public override bool Equals(object obj);
      public override int GetHashCode();
      public void Reset();
    }
  }

  public static class DiagnosticsExtensions
  {
    public static void ReportDiagnostic(this SymbolAnalysisContext context, DiagnosticDescriptor descriptor, Location location, IEnumerable<Location> additionalLocations, ImmutableDictionary<string, string> properties, params object[] messageArgs);
    public static void ReportDiagnostic(this SymbolAnalysisContext context, DiagnosticDescriptor descriptor, Location location, IEnumerable<Location> additionalLocations, params object[] messageArgs);
    public static void ReportDiagnostic(this SymbolAnalysisContext context, DiagnosticDescriptor descriptor, Location location, ImmutableDictionary<string, string> properties, params object[] messageArgs);
    public static void ReportDiagnostic(this SymbolAnalysisContext context, DiagnosticDescriptor descriptor, Location location, params object[] messageArgs);
    public static void ReportDiagnostic(this SymbolAnalysisContext context, DiagnosticDescriptor descriptor, SyntaxNode node, params object[] messageArgs);
    public static void ReportDiagnostic(this SymbolAnalysisContext context, DiagnosticDescriptor descriptor, SyntaxToken token, params object[] messageArgs);
    public static void ReportDiagnostic(this SymbolAnalysisContext context, DiagnosticDescriptor descriptor, SyntaxTrivia trivia, params object[] messageArgs);
    public static void ReportDiagnostic(this SyntaxNodeAnalysisContext context, DiagnosticDescriptor descriptor, Location location, IEnumerable<Location> additionalLocations, ImmutableDictionary<string, string> properties, params object[] messageArgs);
    public static void ReportDiagnostic(this SyntaxNodeAnalysisContext context, DiagnosticDescriptor descriptor, Location location, IEnumerable<Location> additionalLocations, params object[] messageArgs);
    public static void ReportDiagnostic(this SyntaxNodeAnalysisContext context, DiagnosticDescriptor descriptor, Location location, ImmutableDictionary<string, string> properties, params object[] messageArgs);
    public static void ReportDiagnostic(this SyntaxNodeAnalysisContext context, DiagnosticDescriptor descriptor, Location location, params object[] messageArgs);
    public static void ReportDiagnostic(this SyntaxNodeAnalysisContext context, DiagnosticDescriptor descriptor, SyntaxNode node, params object[] messageArgs);
    public static void ReportDiagnostic(this SyntaxNodeAnalysisContext context, DiagnosticDescriptor descriptor, SyntaxToken token, params object[] messageArgs);
    public static void ReportDiagnostic(this SyntaxNodeAnalysisContext context, DiagnosticDescriptor descriptor, SyntaxTrivia trivia, params object[] messageArgs);
    public static void ReportDiagnostic(this SyntaxTreeAnalysisContext context, DiagnosticDescriptor descriptor, Location location, IEnumerable<Location> additionalLocations, ImmutableDictionary<string, string> properties, params object[] messageArgs);
    public static void ReportDiagnostic(this SyntaxTreeAnalysisContext context, DiagnosticDescriptor descriptor, Location location, IEnumerable<Location> additionalLocations, params object[] messageArgs);
    public static void ReportDiagnostic(this SyntaxTreeAnalysisContext context, DiagnosticDescriptor descriptor, Location location, ImmutableDictionary<string, string> properties, params object[] messageArgs);
    public static void ReportDiagnostic(this SyntaxTreeAnalysisContext context, DiagnosticDescriptor descriptor, Location location, params object[] messageArgs);
    public static void ReportDiagnostic(this SyntaxTreeAnalysisContext context, DiagnosticDescriptor descriptor, SyntaxNode node, params object[] messageArgs);
    public static void ReportDiagnostic(this SyntaxTreeAnalysisContext context, DiagnosticDescriptor descriptor, SyntaxToken token, params object[] messageArgs);
    public static void ReportDiagnostic(this SyntaxTreeAnalysisContext context, DiagnosticDescriptor descriptor, SyntaxTrivia trivia, params object[] messageArgs);
  }

  public static class EnumExtensions
  {
    public static bool Is(this Accessibility accessibility, Accessibility accessibility1, Accessibility accessibility2);
    public static bool Is(this Accessibility accessibility, Accessibility accessibility1, Accessibility accessibility2, Accessibility accessibility3);
    public static bool Is(this Accessibility accessibility, Accessibility accessibility1, Accessibility accessibility2, Accessibility accessibility3, Accessibility accessibility4);
    public static bool Is(this Accessibility accessibility, Accessibility accessibility1, Accessibility accessibility2, Accessibility accessibility3, Accessibility accessibility4, Accessibility accessibility5);
    public static bool Is(this MethodKind methodKind, MethodKind methodKind1, MethodKind methodKind2);
    public static bool Is(this MethodKind methodKind, MethodKind methodKind1, MethodKind methodKind2, MethodKind methodKind3);
    public static bool Is(this MethodKind methodKind, MethodKind methodKind1, MethodKind methodKind2, MethodKind methodKind3, MethodKind methodKind4);
    public static bool Is(this MethodKind methodKind, MethodKind methodKind1, MethodKind methodKind2, MethodKind methodKind3, MethodKind methodKind4, MethodKind methodKind5);
    public static bool Is(this SpecialType specialType, SpecialType specialType1, SpecialType specialType2);
    public static bool Is(this SpecialType specialType, SpecialType specialType1, SpecialType specialType2, SpecialType specialType3);
    public static bool Is(this SpecialType specialType, SpecialType specialType1, SpecialType specialType2, SpecialType specialType3, SpecialType specialType4);
    public static bool Is(this SpecialType specialType, SpecialType specialType1, SpecialType specialType2, SpecialType specialType3, SpecialType specialType4, SpecialType specialType5);
    public static bool Is(this TypeKind typeKind, TypeKind typeKind1, TypeKind typeKind2);
    public static bool Is(this TypeKind typeKind, TypeKind typeKind1, TypeKind typeKind2, TypeKind typeKind3);
    public static bool Is(this TypeKind typeKind, TypeKind typeKind1, TypeKind typeKind2, TypeKind typeKind3, TypeKind typeKind4);
    public static bool Is(this TypeKind typeKind, TypeKind typeKind1, TypeKind typeKind2, TypeKind typeKind3, TypeKind typeKind4, TypeKind typeKind5);
    public static bool IsMoreRestrictiveThan(this Accessibility accessibility, Accessibility other);
  }

  public static class FileLinePositionSpanExtensions
  {
    public static bool IsMultiLine(this FileLinePositionSpan fileLinePositionSpan);
    public static bool IsSingleLine(this FileLinePositionSpan fileLinePositionSpan);
    public static int EndLine(this FileLinePositionSpan fileLinePositionSpan);
    public static int StartLine(this FileLinePositionSpan fileLinePositionSpan);
  }

  public static class SemanticModelExtensions
  {
    public static INamedTypeSymbol GetEnclosingNamedType(this SemanticModel semanticModel, int position, CancellationToken cancellationToken = default(CancellationToken));
    public static INamedTypeSymbol GetTypeByMetadataName(this SemanticModel semanticModel, string fullyQualifiedMetadataName);
    public static ISymbol GetSymbol(this SemanticModel semanticModel, SyntaxNode node, CancellationToken cancellationToken = default(CancellationToken));
    public static ITypeSymbol GetTypeSymbol(this SemanticModel semanticModel, SyntaxNode node, CancellationToken cancellationToken = default(CancellationToken));
    public static TSymbol GetEnclosingSymbol<TSymbol>(this SemanticModel semanticModel, int position, CancellationToken cancellationToken = default(CancellationToken)) where TSymbol : ISymbol;
  }

  public static class SymbolExtensions
  {
    public static AttributeData GetAttribute(this ISymbol symbol, INamedTypeSymbol attributeClass);
    public static IEnumerable<INamedTypeSymbol> BaseTypes(this ITypeSymbol type);
    public static IEnumerable<ITypeSymbol> BaseTypesAndSelf(this ITypeSymbol typeSymbol);
    public static IMethodSymbol ReducedFromOrSelf(this IMethodSymbol methodSymbol);
    public static TSymbol FindMember<TSymbol>(this ITypeSymbol typeSymbol, Func<TSymbol, bool> predicate = null) where TSymbol : ISymbol;
    public static TSymbol FindMember<TSymbol>(this ITypeSymbol typeSymbol, string name, Func<TSymbol, bool> predicate = null) where TSymbol : ISymbol;
    public static bool ContainsMember<TSymbol>(this ITypeSymbol typeSymbol, Func<TSymbol, bool> predicate = null) where TSymbol : ISymbol;
    public static bool ContainsMember<TSymbol>(this ITypeSymbol typeSymbol, string name, Func<TSymbol, bool> predicate = null) where TSymbol : ISymbol;
    public static bool EqualsOrInheritsFrom(this ITypeSymbol type, ITypeSymbol baseType, bool includeInterfaces = false);
    public static bool HasAttribute(this ISymbol symbol, INamedTypeSymbol attributeClass);
    public static bool HasAttribute(this ITypeSymbol typeSymbol, INamedTypeSymbol attributeClass, bool includeBaseTypes);
    public static bool HasConstantValue(this IFieldSymbol fieldSymbol, bool value);
    public static bool HasConstantValue(this IFieldSymbol fieldSymbol, byte value);
    public static bool HasConstantValue(this IFieldSymbol fieldSymbol, char value);
    public static bool HasConstantValue(this IFieldSymbol fieldSymbol, decimal value);
    public static bool HasConstantValue(this IFieldSymbol fieldSymbol, double value);
    public static bool HasConstantValue(this IFieldSymbol fieldSymbol, float value);
    public static bool HasConstantValue(this IFieldSymbol fieldSymbol, int value);
    public static bool HasConstantValue(this IFieldSymbol fieldSymbol, long value);
    public static bool HasConstantValue(this IFieldSymbol fieldSymbol, sbyte value);
    public static bool HasConstantValue(this IFieldSymbol fieldSymbol, short value);
    public static bool HasConstantValue(this IFieldSymbol fieldSymbol, string value);
    public static bool HasConstantValue(this IFieldSymbol fieldSymbol, uint value);
    public static bool HasConstantValue(this IFieldSymbol fieldSymbol, ulong value);
    public static bool HasConstantValue(this IFieldSymbol fieldSymbol, ushort value);
    public static bool Implements(this ITypeSymbol typeSymbol, INamedTypeSymbol interfaceSymbol, bool allInterfaces = false);
    public static bool Implements(this ITypeSymbol typeSymbol, SpecialType interfaceType, bool allInterfaces = false);
    public static bool ImplementsAny(this ITypeSymbol typeSymbol, SpecialType interfaceType1, SpecialType interfaceType2, SpecialType interfaceType3, bool allInterfaces = false);
    public static bool ImplementsAny(this ITypeSymbol typeSymbol, SpecialType interfaceType1, SpecialType interfaceType2, bool allInterfaces = false);
    public static bool ImplementsInterfaceMember(this ISymbol symbol, INamedTypeSymbol interfaceSymbol, bool allInterfaces = false);
    public static bool ImplementsInterfaceMember(this ISymbol symbol, bool allInterfaces = false);
    public static bool ImplementsInterfaceMember<TSymbol>(this ISymbol symbol, INamedTypeSymbol interfaceSymbol, bool allInterfaces = false) where TSymbol : ISymbol;
    public static bool ImplementsInterfaceMember<TSymbol>(this ISymbol symbol, bool allInterfaces = false) where TSymbol : ISymbol;
    public static bool InheritsFrom(this ITypeSymbol type, ITypeSymbol baseType, bool includeInterfaces = false);
    public static bool IsAsyncMethod(this ISymbol symbol);
    public static bool IsErrorType(this ISymbol symbol);
    public static bool IsIEnumerableOfT(this ITypeSymbol typeSymbol);
    public static bool IsIEnumerableOrIEnumerableOfT(this ITypeSymbol typeSymbol);
    public static bool IsKind(this ISymbol symbol, SymbolKind kind1, SymbolKind kind2);
    public static bool IsKind(this ISymbol symbol, SymbolKind kind1, SymbolKind kind2, SymbolKind kind3);
    public static bool IsKind(this ISymbol symbol, SymbolKind kind1, SymbolKind kind2, SymbolKind kind3, SymbolKind kind4);
    public static bool IsKind(this ISymbol symbol, SymbolKind kind1, SymbolKind kind2, SymbolKind kind3, SymbolKind kind4, SymbolKind kind5);
    public static bool IsNullableOf(this INamedTypeSymbol namedTypeSymbol, ITypeSymbol typeArgument);
    public static bool IsNullableOf(this INamedTypeSymbol namedTypeSymbol, SpecialType specialType);
    public static bool IsNullableOf(this ITypeSymbol typeSymbol, ITypeSymbol typeArgument);
    public static bool IsNullableOf(this ITypeSymbol typeSymbol, SpecialType specialType);
    public static bool IsNullableType(this ITypeSymbol typeSymbol);
    public static bool IsObject(this ITypeSymbol typeSymbol);
    public static bool IsOrdinaryExtensionMethod(this IMethodSymbol methodSymbol);
    public static bool IsParameterArrayOf(this IParameterSymbol parameterSymbol, SpecialType elementType);
    public static bool IsParameterArrayOf(this IParameterSymbol parameterSymbol, SpecialType elementType1, SpecialType elementType2);
    public static bool IsParameterArrayOf(this IParameterSymbol parameterSymbol, SpecialType elementType1, SpecialType elementType2, SpecialType elementType3);
    public static bool IsPubliclyVisible(this ISymbol symbol);
    public static bool IsReducedExtensionMethod(this IMethodSymbol methodSymbol);
    public static bool IsRefOrOut(this IParameterSymbol parameterSymbol);
    public static bool IsReferenceTypeOrNullableType(this ITypeSymbol typeSymbol);
    public static bool IsString(this ITypeSymbol typeSymbol);
    public static bool IsVoid(this ITypeSymbol typeSymbol);
    public static bool SupportsExplicitDeclaration(this ITypeSymbol typeSymbol);
  }

  public static class SyntaxExtensions
  {
    public static IEnumerable<SyntaxTrivia> DescendantTrivia<TNode>(this SyntaxList<TNode> list, Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false) where TNode : SyntaxNode;
    public static IEnumerable<SyntaxTrivia> DescendantTrivia<TNode>(this SyntaxList<TNode> list, TextSpan span, Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false) where TNode : SyntaxNode;
    public static SeparatedSyntaxList<TNode> ReplaceAt<TNode>(this SeparatedSyntaxList<TNode> list, int index, TNode newNode) where TNode : SyntaxNode;
    public static SeparatedSyntaxList<TNode> WithTriviaFrom<TNode>(this SeparatedSyntaxList<TNode> list, SyntaxNode node) where TNode : SyntaxNode;
    public static SyntaxList<TNode> ReplaceAt<TNode>(this SyntaxList<TNode> list, int index, TNode newNode) where TNode : SyntaxNode;
    public static SyntaxList<TNode> WithTriviaFrom<TNode>(this SyntaxList<TNode> list, SyntaxNode node) where TNode : SyntaxNode;
    public static SyntaxNodeOrToken WithoutLeadingTrivia(this SyntaxNodeOrToken nodeOrToken);
    public static SyntaxNodeOrToken WithoutTrailingTrivia(this SyntaxNodeOrToken nodeOrToken);
    public static SyntaxNodeOrToken WithoutTrivia(this SyntaxNodeOrToken nodeOrToken);
    public static SyntaxToken AppendToLeadingTrivia(this SyntaxToken token, IEnumerable<SyntaxTrivia> trivia);
    public static SyntaxToken AppendToLeadingTrivia(this SyntaxToken token, SyntaxTrivia trivia);
    public static SyntaxToken AppendToTrailingTrivia(this SyntaxToken token, IEnumerable<SyntaxTrivia> trivia);
    public static SyntaxToken AppendToTrailingTrivia(this SyntaxToken token, SyntaxTrivia trivia);
    public static SyntaxToken PrependToLeadingTrivia(this SyntaxToken token, IEnumerable<SyntaxTrivia> trivia);
    public static SyntaxToken PrependToLeadingTrivia(this SyntaxToken token, SyntaxTrivia trivia);
    public static SyntaxToken PrependToTrailingTrivia(this SyntaxToken token, IEnumerable<SyntaxTrivia> trivia);
    public static SyntaxToken PrependToTrailingTrivia(this SyntaxToken token, SyntaxTrivia trivia);
    public static SyntaxToken WithTriviaFrom(this SyntaxToken token, SyntaxNode node);
    public static SyntaxToken WithoutLeadingTrivia(this SyntaxToken token);
    public static SyntaxToken WithoutTrailingTrivia(this SyntaxToken token);
    public static SyntaxTokenList ReplaceAt(this SyntaxTokenList tokenList, int index, SyntaxToken newToken);
    public static SyntaxTriviaList GetLeadingAndTrailingTrivia(this SyntaxNode node);
    public static SyntaxTriviaList LeadingAndTrailingTrivia(this SyntaxToken token);
    public static SyntaxTriviaList ReplaceAt(this SyntaxTriviaList triviaList, int index, SyntaxTrivia newTrivia);
    public static TNode AppendToLeadingTrivia<TNode>(this TNode node, IEnumerable<SyntaxTrivia> trivia) where TNode : SyntaxNode;
    public static TNode AppendToLeadingTrivia<TNode>(this TNode node, SyntaxTrivia trivia) where TNode : SyntaxNode;
    public static TNode AppendToTrailingTrivia<TNode>(this TNode node, IEnumerable<SyntaxTrivia> trivia) where TNode : SyntaxNode;
    public static TNode AppendToTrailingTrivia<TNode>(this TNode node, SyntaxTrivia trivia) where TNode : SyntaxNode;
    public static TNode FirstAncestor<TNode>(this SyntaxNode node, Func<TNode, bool> predicate = null, bool ascendOutOfTrivia = true) where TNode : SyntaxNode;
    public static TNode FirstDescendant<TNode>(this SyntaxNode node, Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false) where TNode : SyntaxNode;
    public static TNode FirstDescendant<TNode>(this SyntaxNode node, TextSpan span, Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false) where TNode : SyntaxNode;
    public static TNode FirstDescendantOrSelf<TNode>(this SyntaxNode node, Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false) where TNode : SyntaxNode;
    public static TNode FirstDescendantOrSelf<TNode>(this SyntaxNode node, TextSpan span, Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false) where TNode : SyntaxNode;
    public static TNode PrependToLeadingTrivia<TNode>(this TNode node, IEnumerable<SyntaxTrivia> trivia) where TNode : SyntaxNode;
    public static TNode PrependToLeadingTrivia<TNode>(this TNode node, SyntaxTrivia trivia) where TNode : SyntaxNode;
    public static TNode PrependToTrailingTrivia<TNode>(this TNode node, IEnumerable<SyntaxTrivia> trivia) where TNode : SyntaxNode;
    public static TNode PrependToTrailingTrivia<TNode>(this TNode node, SyntaxTrivia trivia) where TNode : SyntaxNode;
    public static TNode WithTriviaFrom<TNode>(this TNode node, SyntaxToken token) where TNode : SyntaxNode;
    public static bool All(this SyntaxTokenList list, Func<SyntaxToken, bool> predicate);
    public static bool All(this SyntaxTriviaList list, Func<SyntaxTrivia, bool> predicate);
    public static bool All<TNode>(this SeparatedSyntaxList<TNode> list, Func<TNode, bool> predicate) where TNode : SyntaxNode;
    public static bool All<TNode>(this SyntaxList<TNode> list, Func<TNode, bool> predicate) where TNode : SyntaxNode;
    public static bool Any(this SyntaxTokenList list, Func<SyntaxToken, bool> predicate);
    public static bool Any(this SyntaxTriviaList list, Func<SyntaxTrivia, bool> predicate);
    public static bool Any<TNode>(this SeparatedSyntaxList<TNode> list, Func<TNode, bool> predicate) where TNode : SyntaxNode;
    public static bool Any<TNode>(this SyntaxList<TNode> list, Func<TNode, bool> predicate) where TNode : SyntaxNode;
    public static bool Contains(this SyntaxTokenList tokens, SyntaxToken token);
    public static bool Contains<TNode>(this SeparatedSyntaxList<TNode> list, TNode node) where TNode : SyntaxNode;
    public static bool Contains<TNode>(this SyntaxList<TNode> list, TNode node) where TNode : SyntaxNode;
    public static bool ContainsDirectives(this SyntaxNode node, TextSpan span);
    public static bool IsFirst<TNode>(this SeparatedSyntaxList<TNode> list, TNode node) where TNode : SyntaxNode;
    public static bool IsFirst<TNode>(this SyntaxList<TNode> list, TNode node) where TNode : SyntaxNode;
    public static bool IsLast<TNode>(this SeparatedSyntaxList<TNode> list, TNode node) where TNode : SyntaxNode;
    public static bool IsLast<TNode>(this SyntaxList<TNode> list, TNode node) where TNode : SyntaxNode;
    public static bool SpanContainsDirectives(this SyntaxNode node);
    public static bool TryGetContainingList(this SyntaxTrivia trivia, out SyntaxTriviaList triviaList, bool allowLeading = true, bool allowTrailing = true);
    public static int IndexOf(this SyntaxTokenList tokens, Func<SyntaxToken, bool> predicate);
    public static int IndexOf(this SyntaxTriviaList triviaList, Func<SyntaxTrivia, bool> predicate);
  }

  public static class SyntaxTreeExtensions
  {
    public static bool IsMultiLineSpan(this SyntaxTree syntaxTree, TextSpan span, CancellationToken cancellationToken = default(CancellationToken));
    public static bool IsSingleLineSpan(this SyntaxTree syntaxTree, TextSpan span, CancellationToken cancellationToken = default(CancellationToken));
    public static int GetEndLine(this SyntaxTree syntaxTree, TextSpan span, CancellationToken cancellationToken = default(CancellationToken));
    public static int GetStartLine(this SyntaxTree syntaxTree, TextSpan span, CancellationToken cancellationToken = default(CancellationToken));
  }

  public static class WorkspaceExtensions
  {
    public static Task<Document> InsertNodeAfterAsync(this Document document, SyntaxNode nodeInList, SyntaxNode newNode, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> InsertNodeBeforeAsync(this Document document, SyntaxNode nodeInList, SyntaxNode newNode, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> InsertNodesAfterAsync(this Document document, SyntaxNode nodeInList, IEnumerable<SyntaxNode> newNodes, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> InsertNodesBeforeAsync(this Document document, SyntaxNode nodeInList, IEnumerable<SyntaxNode> newNodes, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> RemoveNodeAsync(this Document document, SyntaxNode node, SyntaxRemoveOptions options, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> RemoveNodesAsync(this Document document, IEnumerable<SyntaxNode> nodes, SyntaxRemoveOptions options, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> ReplaceNodeAsync(this Document document, SyntaxNode oldNode, IEnumerable<SyntaxNode> newNodes, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> ReplaceNodeAsync(this Document document, SyntaxNode oldNode, SyntaxNode newNode, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> ReplaceNodesAsync<TNode>(this Document document, IEnumerable<TNode> nodes, Func<TNode, TNode, SyntaxNode> computeReplacementNode, CancellationToken cancellationToken = default(CancellationToken)) where TNode : SyntaxNode;
    public static Task<Document> ReplaceTokenAsync(this Document document, SyntaxToken oldToken, IEnumerable<SyntaxToken> newTokens, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> ReplaceTokenAsync(this Document document, SyntaxToken oldToken, SyntaxToken newToken, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> ReplaceTriviaAsync(this Document document, SyntaxTrivia oldTrivia, IEnumerable<SyntaxTrivia> newTrivia, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> ReplaceTriviaAsync(this Document document, SyntaxTrivia oldTrivia, SyntaxTrivia newTrivia, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> WithTextChangeAsync(this Document document, TextChange textChange, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> WithTextChangesAsync(this Document document, IEnumerable<TextChange> textChanges, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> WithTextChangesAsync(this Document document, TextChange[] textChanges, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Solution> ReplaceNodeAsync<TNode>(this Solution solution, TNode oldNode, TNode newNode, CancellationToken cancellationToken = default(CancellationToken)) where TNode : SyntaxNode;
    public static Task<Solution> ReplaceNodesAsync<TNode>(this Solution solution, IEnumerable<TNode> nodes, Func<TNode, TNode, SyntaxNode> computeReplacementNodes, CancellationToken cancellationToken = default(CancellationToken)) where TNode : SyntaxNode;
  }

  public readonly struct ExtensionMethodSymbolInfo
  {
    public IMethodSymbol ReducedSymbol { get; }
    public IMethodSymbol ReducedSymbolOrSymbol { get; }
    public IMethodSymbol Symbol { get; }
    public bool IsReduced { get; }
    public bool Equals(ExtensionMethodSymbolInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public static bool operator !=(in ExtensionMethodSymbolInfo info1, in ExtensionMethodSymbolInfo info2);
    public static bool operator ==(in ExtensionMethodSymbolInfo info1, in ExtensionMethodSymbolInfo info2);
  }

  public interface ISelection<T>
  {
    int FirstIndex { get; }
    int LastIndex { get; }
    T First();
    T Last();
  }
}

namespace Roslynator.CSharp
{
  public abstract class ModifierList<TNode> where TNode : SyntaxNode
  {
    public static ModifierList<TNode> Instance { get; }
    public TNode Insert(TNode node, SyntaxKind kind, IComparer<SyntaxKind> comparer = null);
    public TNode Insert(TNode node, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null);
    public TNode Remove(TNode node, SyntaxKind kind);
    public TNode Remove(TNode node, SyntaxToken modifier);
    public TNode RemoveAll(TNode node);
    public TNode RemoveAll(TNode node, Func<SyntaxToken, bool> predicate);
    public TNode RemoveAt(TNode node, int index);
  }

  public sealed class MemberDeclarationListSelection
  {
    public SyntaxNode Parent { get; }
    public static MemberDeclarationListSelection Create(CompilationUnitSyntax compilationUnit, TextSpan span);
    public static MemberDeclarationListSelection Create(NamespaceDeclarationSyntax namespaceDeclaration, TextSpan span);
    public static MemberDeclarationListSelection Create(TypeDeclarationSyntax typeDeclaration, TextSpan span);
    public static bool TryCreate(NamespaceDeclarationSyntax namespaceDeclaration, TextSpan span, out MemberDeclarationListSelection selectedMembers);
    public static bool TryCreate(TypeDeclarationSyntax typeDeclaration, TextSpan span, out MemberDeclarationListSelection selectedMembers);
  }

  public sealed class StatementListSelection
  {
    public static StatementListSelection Create(BlockSyntax block, TextSpan span);
    public static StatementListSelection Create(SwitchSectionSyntax switchSection, TextSpan span);
    public static StatementListSelection Create(in StatementListInfo statementsInfo, TextSpan span);
    public static bool TryCreate(BlockSyntax block, TextSpan span, out StatementListSelection selectedStatements);
    public static bool TryCreate(SwitchSectionSyntax switchSection, TextSpan span, out StatementListSelection selectedStatements);
  }

  public static class CSharpExtensions
  {
    public static ExtensionMethodSymbolInfo GetExtensionMethodInfo(this SemanticModel semanticModel, ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken));
    public static ExtensionMethodSymbolInfo GetReducedExtensionMethodInfo(this SemanticModel semanticModel, ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken));
    public static IMethodSymbol GetMethodSymbol(this SemanticModel semanticModel, ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken));
    public static IParameterSymbol DetermineParameter(this SemanticModel semanticModel, ArgumentSyntax argument, bool allowParams = false, bool allowCandidate = false, CancellationToken cancellationToken = default(CancellationToken));
    public static IParameterSymbol DetermineParameter(this SemanticModel semanticModel, AttributeArgumentSyntax attributeArgument, bool allowParams = false, bool allowCandidate = false, CancellationToken cancellationToken = default(CancellationToken));
    public static ISymbol GetSymbol(this SemanticModel semanticModel, AttributeSyntax attribute, CancellationToken cancellationToken = default(CancellationToken));
    public static ISymbol GetSymbol(this SemanticModel semanticModel, ConstructorInitializerSyntax constructorInitializer, CancellationToken cancellationToken = default(CancellationToken));
    public static ISymbol GetSymbol(this SemanticModel semanticModel, CrefSyntax cref, CancellationToken cancellationToken = default(CancellationToken));
    public static ISymbol GetSymbol(this SemanticModel semanticModel, ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken));
    public static ISymbol GetSymbol(this SemanticModel semanticModel, OrderingSyntax ordering, CancellationToken cancellationToken = default(CancellationToken));
    public static ISymbol GetSymbol(this SemanticModel semanticModel, SelectOrGroupClauseSyntax selectOrGroupClause, CancellationToken cancellationToken = default(CancellationToken));
    public static ITypeSymbol GetTypeSymbol(this SemanticModel semanticModel, AttributeSyntax attribute, CancellationToken cancellationToken = default(CancellationToken));
    public static ITypeSymbol GetTypeSymbol(this SemanticModel semanticModel, ConstructorInitializerSyntax constructorInitializer, CancellationToken cancellationToken = default(CancellationToken));
    public static ITypeSymbol GetTypeSymbol(this SemanticModel semanticModel, ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken));
    public static ITypeSymbol GetTypeSymbol(this SemanticModel semanticModel, SelectOrGroupClauseSyntax selectOrGroupClause, CancellationToken cancellationToken = default(CancellationToken));
    public static bool HasConstantValue(this SemanticModel semanticModel, ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken));
    public static bool IsDefaultValue(this SemanticModel semanticModel, ITypeSymbol typeSymbol, ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken));
  }

  public static class CSharpFactory
  {
    public static AccessorDeclarationSyntax AddAccessorDeclaration(ArrowExpressionClauseSyntax expressionBody);
    public static AccessorDeclarationSyntax AddAccessorDeclaration(BlockSyntax body);
    public static AccessorDeclarationSyntax AddAccessorDeclaration(SyntaxTokenList modifiers, ArrowExpressionClauseSyntax expressionBody);
    public static AccessorDeclarationSyntax AddAccessorDeclaration(SyntaxTokenList modifiers, BlockSyntax body);
    public static AccessorDeclarationSyntax AutoGetAccessorDeclaration(SyntaxTokenList modifiers = default(SyntaxTokenList));
    public static AccessorDeclarationSyntax AutoSetAccessorDeclaration(SyntaxTokenList modifiers = default(SyntaxTokenList));
    public static AccessorDeclarationSyntax GetAccessorDeclaration(ArrowExpressionClauseSyntax expressionBody);
    public static AccessorDeclarationSyntax GetAccessorDeclaration(BlockSyntax body);
    public static AccessorDeclarationSyntax GetAccessorDeclaration(SyntaxTokenList modifiers, ArrowExpressionClauseSyntax expressionBody);
    public static AccessorDeclarationSyntax GetAccessorDeclaration(SyntaxTokenList modifiers, BlockSyntax body);
    public static AccessorDeclarationSyntax RemoveAccessorDeclaration(ArrowExpressionClauseSyntax expressionBody);
    public static AccessorDeclarationSyntax RemoveAccessorDeclaration(BlockSyntax body);
    public static AccessorDeclarationSyntax RemoveAccessorDeclaration(SyntaxTokenList modifiers, ArrowExpressionClauseSyntax expressionBody);
    public static AccessorDeclarationSyntax RemoveAccessorDeclaration(SyntaxTokenList modifiers, BlockSyntax body);
    public static AccessorDeclarationSyntax SetAccessorDeclaration(ArrowExpressionClauseSyntax expressionBody);
    public static AccessorDeclarationSyntax SetAccessorDeclaration(BlockSyntax body);
    public static AccessorDeclarationSyntax SetAccessorDeclaration(SyntaxTokenList modifiers, ArrowExpressionClauseSyntax expressionBody);
    public static AccessorDeclarationSyntax SetAccessorDeclaration(SyntaxTokenList modifiers, BlockSyntax body);
    public static AccessorListSyntax AccessorList(AccessorDeclarationSyntax accessor);
    public static AccessorListSyntax AccessorList(params AccessorDeclarationSyntax[] accessors);
    public static ArgumentListSyntax ArgumentList(ArgumentSyntax argument);
    public static ArgumentListSyntax ArgumentList(params ArgumentSyntax[] arguments);
    public static ArgumentSyntax Argument(NameColonSyntax nameColon, ExpressionSyntax expression);
    public static AssignmentExpressionSyntax AddAssignmentExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static AssignmentExpressionSyntax AddAssignmentExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static AssignmentExpressionSyntax AndAssignmentExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static AssignmentExpressionSyntax AndAssignmentExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static AssignmentExpressionSyntax DivideAssignmentExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static AssignmentExpressionSyntax DivideAssignmentExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static AssignmentExpressionSyntax ExclusiveOrAssignmentExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static AssignmentExpressionSyntax ExclusiveOrAssignmentExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static AssignmentExpressionSyntax LeftShiftAssignmentExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static AssignmentExpressionSyntax LeftShiftAssignmentExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static AssignmentExpressionSyntax ModuloAssignmentExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static AssignmentExpressionSyntax ModuloAssignmentExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static AssignmentExpressionSyntax MultiplyAssignmentExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static AssignmentExpressionSyntax MultiplyAssignmentExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static AssignmentExpressionSyntax OrAssignmentExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static AssignmentExpressionSyntax OrAssignmentExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static AssignmentExpressionSyntax RightShiftAssignmentExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static AssignmentExpressionSyntax RightShiftAssignmentExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static AssignmentExpressionSyntax SimpleAssignmentExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static AssignmentExpressionSyntax SimpleAssignmentExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static AssignmentExpressionSyntax SubtractAssignmentExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static AssignmentExpressionSyntax SubtractAssignmentExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static AttributeArgumentListSyntax AttributeArgumentList(AttributeArgumentSyntax attributeArgument);
    public static AttributeArgumentListSyntax AttributeArgumentList(params AttributeArgumentSyntax[] attributeArguments);
    public static AttributeArgumentSyntax AttributeArgument(NameColonSyntax nameColon, ExpressionSyntax expression);
    public static AttributeArgumentSyntax AttributeArgument(NameEqualsSyntax nameEquals, ExpressionSyntax expression);
    public static AttributeListSyntax AttributeList(AttributeSyntax attribute);
    public static AttributeListSyntax AttributeList(params AttributeSyntax[] attributes);
    public static AttributeSyntax Attribute(NameSyntax name, AttributeArgumentSyntax argument);
    public static BaseListSyntax BaseList(BaseTypeSyntax type);
    public static BaseListSyntax BaseList(SyntaxToken colonToken, BaseTypeSyntax baseType);
    public static BaseListSyntax BaseList(SyntaxToken colonToken, params BaseTypeSyntax[] types);
    public static BaseListSyntax BaseList(params BaseTypeSyntax[] types);
    public static BinaryExpressionSyntax AddExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax AddExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax AsExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax AsExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax BitwiseAndExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax BitwiseAndExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax BitwiseOrExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax BitwiseOrExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax CoalesceExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax CoalesceExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax DivideExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax DivideExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax EqualsExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax EqualsExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax ExclusiveOrExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax ExclusiveOrExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax GreaterThanExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax GreaterThanExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax GreaterThanOrEqualExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax GreaterThanOrEqualExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax IsExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax IsExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax LeftShiftExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax LeftShiftExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax LessThanExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax LessThanExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax LessThanOrEqualExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax LessThanOrEqualExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax LogicalAndExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax LogicalAndExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax LogicalOrExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax LogicalOrExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax ModuloExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax ModuloExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax MultiplyExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax MultiplyExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax NotEqualsExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax NotEqualsExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax RightShiftExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax RightShiftExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BinaryExpressionSyntax SubtractExpression(ExpressionSyntax left, ExpressionSyntax right);
    public static BinaryExpressionSyntax SubtractExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static BlockSyntax Block(StatementSyntax statement);
    public static BlockSyntax Block(SyntaxToken openBrace, StatementSyntax statement, SyntaxToken closeBrace);
    public static BracketedArgumentListSyntax BracketedArgumentList(ArgumentSyntax argument);
    public static BracketedArgumentListSyntax BracketedArgumentList(params ArgumentSyntax[] arguments);
    public static BracketedParameterListSyntax BracketedParameterList(ParameterSyntax parameter);
    public static BracketedParameterListSyntax BracketedParameterList(params ParameterSyntax[] parameters);
    public static CheckedExpressionSyntax CheckedExpression(ExpressionSyntax expression);
    public static CheckedExpressionSyntax CheckedExpression(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken);
    public static CheckedExpressionSyntax UncheckedExpression(ExpressionSyntax expression);
    public static CheckedExpressionSyntax UncheckedExpression(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken);
    public static ClassDeclarationSyntax ClassDeclaration(SyntaxTokenList modifiers, SyntaxToken identifier, SyntaxList<MemberDeclarationSyntax> members = default(SyntaxList<MemberDeclarationSyntax>));
    public static ClassDeclarationSyntax ClassDeclaration(SyntaxTokenList modifiers, string identifier, SyntaxList<MemberDeclarationSyntax> members = default(SyntaxList<MemberDeclarationSyntax>));
    public static ClassOrStructConstraintSyntax ClassConstraint();
    public static ClassOrStructConstraintSyntax StructConstraint();
    public static CompilationUnitSyntax CompilationUnit(MemberDeclarationSyntax member);
    public static CompilationUnitSyntax CompilationUnit(SyntaxList<UsingDirectiveSyntax> usings, MemberDeclarationSyntax member);
    public static CompilationUnitSyntax CompilationUnit(SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members);
    public static ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ArrowExpressionClauseSyntax expressionBody);
    public static ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, BlockSyntax body);
    public static ConstructorInitializerSyntax BaseConstructorInitializer(ArgumentListSyntax argumentList = null);
    public static ConstructorInitializerSyntax BaseConstructorInitializer(SyntaxToken semicolonToken, ArgumentListSyntax argumentList);
    public static ConstructorInitializerSyntax ThisConstructorInitializer(ArgumentListSyntax argumentList = null);
    public static ConstructorInitializerSyntax ThisConstructorInitializer(SyntaxToken semicolonToken, ArgumentListSyntax argumentList);
    public static ConversionOperatorDeclarationSyntax ExplicitConversionOperatorDeclaration(SyntaxTokenList modifiers, TypeSyntax type, ParameterListSyntax parameterList, ArrowExpressionClauseSyntax expressionBody);
    public static ConversionOperatorDeclarationSyntax ExplicitConversionOperatorDeclaration(SyntaxTokenList modifiers, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body);
    public static ConversionOperatorDeclarationSyntax ImplicitConversionOperatorDeclaration(SyntaxTokenList modifiers, TypeSyntax type, ParameterListSyntax parameterList, ArrowExpressionClauseSyntax expressionBody);
    public static ConversionOperatorDeclarationSyntax ImplicitConversionOperatorDeclaration(SyntaxTokenList modifiers, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body);
    public static DelegateDeclarationSyntax DelegateDeclaration(SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, ParameterListSyntax parameterList);
    public static DelegateDeclarationSyntax DelegateDeclaration(SyntaxTokenList modifiers, TypeSyntax returnType, string identifier, ParameterListSyntax parameterList);
    public static EnumDeclarationSyntax EnumDeclaration(SyntaxTokenList modifiers, SyntaxToken identifier, SeparatedSyntaxList<EnumMemberDeclarationSyntax> members);
    public static EnumMemberDeclarationSyntax EnumMemberDeclaration(SyntaxToken identifier, EqualsValueClauseSyntax value);
    public static EnumMemberDeclarationSyntax EnumMemberDeclaration(SyntaxToken identifier, ExpressionSyntax value);
    public static EnumMemberDeclarationSyntax EnumMemberDeclaration(string name, EqualsValueClauseSyntax value);
    public static EnumMemberDeclarationSyntax EnumMemberDeclaration(string name, ExpressionSyntax value);
    public static EventDeclarationSyntax EventDeclaration(SyntaxTokenList modifiers, TypeSyntax type, SyntaxToken identifier, AccessorListSyntax accessorList);
    public static EventDeclarationSyntax EventDeclaration(SyntaxTokenList modifiers, TypeSyntax type, string identifier, AccessorListSyntax accessorList);
    public static EventFieldDeclarationSyntax EventFieldDeclaration(SyntaxTokenList modifiers, TypeSyntax type, SyntaxToken identifier);
    public static EventFieldDeclarationSyntax EventFieldDeclaration(SyntaxTokenList modifiers, TypeSyntax type, string identifier);
    public static ExpressionStatementSyntax SimpleAssignmentStatement(ExpressionSyntax left, ExpressionSyntax right);
    public static ExpressionStatementSyntax SimpleAssignmentStatement(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right);
    public static FieldDeclarationSyntax FieldDeclaration(SyntaxTokenList modifiers, TypeSyntax type, SyntaxToken identifier, EqualsValueClauseSyntax initializer);
    public static FieldDeclarationSyntax FieldDeclaration(SyntaxTokenList modifiers, TypeSyntax type, SyntaxToken identifier, ExpressionSyntax value = null);
    public static FieldDeclarationSyntax FieldDeclaration(SyntaxTokenList modifiers, TypeSyntax type, string identifier, EqualsValueClauseSyntax initializer);
    public static FieldDeclarationSyntax FieldDeclaration(SyntaxTokenList modifiers, TypeSyntax type, string identifier, ExpressionSyntax value = null);
    public static GenericNameSyntax GenericName(SyntaxToken identifier, TypeSyntax typeArgument);
    public static GenericNameSyntax GenericName(string identifier, TypeSyntax typeArgument);
    public static IdentifierNameSyntax VarType();
    public static IndexerDeclarationSyntax IndexerDeclaration(SyntaxTokenList modifiers, TypeSyntax type, BracketedParameterListSyntax parameterList, AccessorListSyntax accessorList);
    public static IndexerDeclarationSyntax IndexerDeclaration(SyntaxTokenList modifiers, TypeSyntax type, BracketedParameterListSyntax parameterList, ArrowExpressionClauseSyntax expressionBody);
    public static InitializerExpressionSyntax ArrayInitializerExpression(SeparatedSyntaxList<ExpressionSyntax> expressions = default(SeparatedSyntaxList<ExpressionSyntax>));
    public static InitializerExpressionSyntax ArrayInitializerExpression(SyntaxToken openBraceToken, SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken);
    public static InitializerExpressionSyntax CollectionInitializerExpression(SeparatedSyntaxList<ExpressionSyntax> expressions = default(SeparatedSyntaxList<ExpressionSyntax>));
    public static InitializerExpressionSyntax CollectionInitializerExpression(SyntaxToken openBraceToken, SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken);
    public static InitializerExpressionSyntax ComplexElementInitializerExpression(SeparatedSyntaxList<ExpressionSyntax> expressions = default(SeparatedSyntaxList<ExpressionSyntax>));
    public static InitializerExpressionSyntax ComplexElementInitializerExpression(SyntaxToken openBraceToken, SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken);
    public static InitializerExpressionSyntax ObjectInitializerExpression(SeparatedSyntaxList<ExpressionSyntax> expressions = default(SeparatedSyntaxList<ExpressionSyntax>));
    public static InitializerExpressionSyntax ObjectInitializerExpression(SyntaxToken openBraceToken, SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken);
    public static InterfaceDeclarationSyntax InterfaceDeclaration(SyntaxTokenList modifiers, SyntaxToken identifier, SyntaxList<MemberDeclarationSyntax> members = default(SyntaxList<MemberDeclarationSyntax>));
    public static InterfaceDeclarationSyntax InterfaceDeclaration(SyntaxTokenList modifiers, string identifier, SyntaxList<MemberDeclarationSyntax> members = default(SyntaxList<MemberDeclarationSyntax>));
    public static InvocationExpressionSyntax NameOfExpression(ExpressionSyntax expression);
    public static InvocationExpressionSyntax NameOfExpression(string identifier);
    public static InvocationExpressionSyntax SimpleMemberInvocationExpression(ExpressionSyntax expression, SimpleNameSyntax name);
    public static InvocationExpressionSyntax SimpleMemberInvocationExpression(ExpressionSyntax expression, SimpleNameSyntax name, ArgumentListSyntax argumentList);
    public static InvocationExpressionSyntax SimpleMemberInvocationExpression(ExpressionSyntax expression, SimpleNameSyntax name, ArgumentSyntax argument);
    public static LiteralExpressionSyntax BooleanLiteralExpression(bool value);
    public static LiteralExpressionSyntax CharacterLiteralExpression(char value);
    public static LiteralExpressionSyntax DefaultLiteralExpression();
    public static LiteralExpressionSyntax FalseLiteralExpression();
    public static LiteralExpressionSyntax LiteralExpression(object value);
    public static LiteralExpressionSyntax NullLiteralExpression();
    public static LiteralExpressionSyntax NumericLiteralExpression(decimal value);
    public static LiteralExpressionSyntax NumericLiteralExpression(double value);
    public static LiteralExpressionSyntax NumericLiteralExpression(float value);
    public static LiteralExpressionSyntax NumericLiteralExpression(int value);
    public static LiteralExpressionSyntax NumericLiteralExpression(long value);
    public static LiteralExpressionSyntax NumericLiteralExpression(sbyte value);
    public static LiteralExpressionSyntax NumericLiteralExpression(uint value);
    public static LiteralExpressionSyntax NumericLiteralExpression(ulong value);
    public static LiteralExpressionSyntax StringLiteralExpression(string value);
    public static LiteralExpressionSyntax TrueLiteralExpression();
    public static LocalDeclarationStatementSyntax LocalDeclarationStatement(TypeSyntax type, SyntaxToken identifier, EqualsValueClauseSyntax initializer);
    public static LocalDeclarationStatementSyntax LocalDeclarationStatement(TypeSyntax type, SyntaxToken identifier, ExpressionSyntax value = null);
    public static LocalDeclarationStatementSyntax LocalDeclarationStatement(TypeSyntax type, string identifier, EqualsValueClauseSyntax initializer);
    public static LocalDeclarationStatementSyntax LocalDeclarationStatement(TypeSyntax type, string identifier, ExpressionSyntax value = null);
    public static LocalFunctionStatementSyntax LocalFunctionStatement(SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, ParameterListSyntax parameterList, ArrowExpressionClauseSyntax expressionBody);
    public static LocalFunctionStatementSyntax LocalFunctionStatement(SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, ParameterListSyntax parameterList, BlockSyntax body);
    public static MemberAccessExpressionSyntax SimpleMemberAccessExpression(ExpressionSyntax expression, SimpleNameSyntax name);
    public static MemberAccessExpressionSyntax SimpleMemberAccessExpression(ExpressionSyntax expression, SyntaxToken operatorToken, SimpleNameSyntax name);
    public static MethodDeclarationSyntax MethodDeclaration(SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, ParameterListSyntax parameterList, ArrowExpressionClauseSyntax expressionBody);
    public static MethodDeclarationSyntax MethodDeclaration(SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, ParameterListSyntax parameterList, BlockSyntax body);
    public static NamespaceDeclarationSyntax NamespaceDeclaration(NameSyntax name, MemberDeclarationSyntax member);
    public static NamespaceDeclarationSyntax NamespaceDeclaration(NameSyntax name, SyntaxList<MemberDeclarationSyntax> members);
    public static ObjectCreationExpressionSyntax ObjectCreationExpression(TypeSyntax type, ArgumentListSyntax argumentList);
    public static OperatorDeclarationSyntax OperatorDeclaration(SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken operatorToken, ParameterListSyntax parameterList, ArrowExpressionClauseSyntax expressionBody);
    public static OperatorDeclarationSyntax OperatorDeclaration(SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax body);
    public static ParameterListSyntax ParameterList(ParameterSyntax parameter);
    public static ParameterListSyntax ParameterList(params ParameterSyntax[] parameters);
    public static ParameterSyntax Parameter(TypeSyntax type, SyntaxToken identifier, EqualsValueClauseSyntax @default);
    public static ParameterSyntax Parameter(TypeSyntax type, SyntaxToken identifier, ExpressionSyntax @default = null);
    public static ParameterSyntax Parameter(TypeSyntax type, string identifier, ExpressionSyntax @default = null);
    public static PostfixUnaryExpressionSyntax PostDecrementExpression(ExpressionSyntax operand);
    public static PostfixUnaryExpressionSyntax PostDecrementExpression(ExpressionSyntax operand, SyntaxToken operatorToken);
    public static PostfixUnaryExpressionSyntax PostIncrementExpression(ExpressionSyntax operand);
    public static PostfixUnaryExpressionSyntax PostIncrementExpression(ExpressionSyntax operand, SyntaxToken operatorToken);
    public static PredefinedTypeSyntax PredefinedBoolType();
    public static PredefinedTypeSyntax PredefinedByteType();
    public static PredefinedTypeSyntax PredefinedCharType();
    public static PredefinedTypeSyntax PredefinedDecimalType();
    public static PredefinedTypeSyntax PredefinedDoubleType();
    public static PredefinedTypeSyntax PredefinedFloatType();
    public static PredefinedTypeSyntax PredefinedIntType();
    public static PredefinedTypeSyntax PredefinedLongType();
    public static PredefinedTypeSyntax PredefinedObjectType();
    public static PredefinedTypeSyntax PredefinedSByteType();
    public static PredefinedTypeSyntax PredefinedShortType();
    public static PredefinedTypeSyntax PredefinedStringType();
    public static PredefinedTypeSyntax PredefinedUIntType();
    public static PredefinedTypeSyntax PredefinedULongType();
    public static PredefinedTypeSyntax PredefinedUShortType();
    public static PredefinedTypeSyntax VoidType();
    public static PrefixUnaryExpressionSyntax AddressOfExpression(ExpressionSyntax operand);
    public static PrefixUnaryExpressionSyntax AddressOfExpression(ExpressionSyntax operand, SyntaxToken operatorToken);
    public static PrefixUnaryExpressionSyntax BitwiseNotExpression(ExpressionSyntax operand);
    public static PrefixUnaryExpressionSyntax BitwiseNotExpression(ExpressionSyntax operand, SyntaxToken operatorToken);
    public static PrefixUnaryExpressionSyntax LogicalNotExpression(ExpressionSyntax operand);
    public static PrefixUnaryExpressionSyntax LogicalNotExpression(ExpressionSyntax operand, SyntaxToken operatorToken);
    public static PrefixUnaryExpressionSyntax PointerIndirectionExpression(ExpressionSyntax operand);
    public static PrefixUnaryExpressionSyntax PointerIndirectionExpression(ExpressionSyntax operand, SyntaxToken operatorToken);
    public static PrefixUnaryExpressionSyntax PreDecrementExpression(ExpressionSyntax operand);
    public static PrefixUnaryExpressionSyntax PreDecrementExpression(ExpressionSyntax operand, SyntaxToken operatorToken);
    public static PrefixUnaryExpressionSyntax PreIncrementExpression(ExpressionSyntax operand);
    public static PrefixUnaryExpressionSyntax PreIncrementExpression(ExpressionSyntax operand, SyntaxToken operatorToken);
    public static PrefixUnaryExpressionSyntax UnaryMinusExpression(ExpressionSyntax operand);
    public static PrefixUnaryExpressionSyntax UnaryMinusExpression(ExpressionSyntax operand, SyntaxToken operatorToken);
    public static PrefixUnaryExpressionSyntax UnaryPlusExpression(ExpressionSyntax operand);
    public static PrefixUnaryExpressionSyntax UnaryPlusExpression(ExpressionSyntax operand, SyntaxToken operatorToken);
    public static PropertyDeclarationSyntax PropertyDeclaration(SyntaxTokenList modifiers, TypeSyntax type, SyntaxToken identifier, AccessorListSyntax accessorList, ExpressionSyntax value = null);
    public static PropertyDeclarationSyntax PropertyDeclaration(SyntaxTokenList modifiers, TypeSyntax type, SyntaxToken identifier, ArrowExpressionClauseSyntax expressionBody);
    public static StructDeclarationSyntax StructDeclaration(SyntaxTokenList modifiers, SyntaxToken identifier, SyntaxList<MemberDeclarationSyntax> members = default(SyntaxList<MemberDeclarationSyntax>));
    public static StructDeclarationSyntax StructDeclaration(SyntaxTokenList modifiers, string identifier, SyntaxList<MemberDeclarationSyntax> members = default(SyntaxList<MemberDeclarationSyntax>));
    public static SwitchSectionSyntax DefaultSwitchSection(StatementSyntax statement);
    public static SwitchSectionSyntax DefaultSwitchSection(SyntaxList<StatementSyntax> statements);
    public static SwitchSectionSyntax SwitchSection(SwitchLabelSyntax switchLabel, StatementSyntax statement);
    public static SwitchSectionSyntax SwitchSection(SwitchLabelSyntax switchLabel, SyntaxList<StatementSyntax> statements);
    public static SwitchSectionSyntax SwitchSection(SyntaxList<SwitchLabelSyntax> switchLabels, StatementSyntax statement);
    public static SyntaxToken AbstractKeyword();
    public static SyntaxToken AddKeyword();
    public static SyntaxToken AliasKeyword();
    public static SyntaxToken AmpersandAmpersandToken();
    public static SyntaxToken AmpersandEqualsToken();
    public static SyntaxToken AmpersandToken();
    public static SyntaxToken ArgListKeyword();
    public static SyntaxToken AsKeyword();
    public static SyntaxToken AscendingKeyword();
    public static SyntaxToken AssemblyKeyword();
    public static SyntaxToken AsteriskEqualsToken();
    public static SyntaxToken AsteriskToken();
    public static SyntaxToken AsyncKeyword();
    public static SyntaxToken AwaitKeyword();
    public static SyntaxToken BackslashToken();
    public static SyntaxToken BarBarToken();
    public static SyntaxToken BarEqualsToken();
    public static SyntaxToken BarToken();
    public static SyntaxToken BaseKeyword();
    public static SyntaxToken BoolKeyword();
    public static SyntaxToken BreakKeyword();
    public static SyntaxToken ByKeyword();
    public static SyntaxToken ByteKeyword();
    public static SyntaxToken CaretEqualsToken();
    public static SyntaxToken CaretToken();
    public static SyntaxToken CaseKeyword();
    public static SyntaxToken CatchKeyword();
    public static SyntaxToken CharKeyword();
    public static SyntaxToken CheckedKeyword();
    public static SyntaxToken ChecksumKeyword();
    public static SyntaxToken ClassKeyword();
    public static SyntaxToken CloseBraceToken();
    public static SyntaxToken CloseBracketToken();
    public static SyntaxToken CloseParenToken();
    public static SyntaxToken ColonColonToken();
    public static SyntaxToken ColonToken();
    public static SyntaxToken CommaToken();
    public static SyntaxToken ConstKeyword();
    public static SyntaxToken ContinueKeyword();
    public static SyntaxToken DecimalKeyword();
    public static SyntaxToken DefaultKeyword();
    public static SyntaxToken DefineKeyword();
    public static SyntaxToken DelegateKeyword();
    public static SyntaxToken DescendingKeyword();
    public static SyntaxToken DisableKeyword();
    public static SyntaxToken DoKeyword();
    public static SyntaxToken DollarToken();
    public static SyntaxToken DotToken();
    public static SyntaxToken DoubleKeyword();
    public static SyntaxToken DoubleQuoteToken();
    public static SyntaxToken ElifKeyword();
    public static SyntaxToken ElseKeyword();
    public static SyntaxToken EndIfKeyword();
    public static SyntaxToken EndOfDirectiveToken();
    public static SyntaxToken EndOfDocumentationCommentToken();
    public static SyntaxToken EndOfFileToken();
    public static SyntaxToken EndRegionKeyword();
    public static SyntaxToken EnumKeyword();
    public static SyntaxToken EqualsEqualsToken();
    public static SyntaxToken EqualsGreaterThanToken();
    public static SyntaxToken EqualsKeyword();
    public static SyntaxToken EqualsToken();
    public static SyntaxToken ErrorKeyword();
    public static SyntaxToken EventKeyword();
    public static SyntaxToken ExclamationEqualsToken();
    public static SyntaxToken ExclamationToken();
    public static SyntaxToken ExplicitKeyword();
    public static SyntaxToken ExternKeyword();
    public static SyntaxToken FalseKeyword();
    public static SyntaxToken FieldKeyword();
    public static SyntaxToken FinallyKeyword();
    public static SyntaxToken FixedKeyword();
    public static SyntaxToken FloatKeyword();
    public static SyntaxToken ForEachKeyword();
    public static SyntaxToken ForKeyword();
    public static SyntaxToken FromKeyword();
    public static SyntaxToken GetKeyword();
    public static SyntaxToken GlobalKeyword();
    public static SyntaxToken GotoKeyword();
    public static SyntaxToken GreaterThanEqualsToken();
    public static SyntaxToken GreaterThanGreaterThanEqualsToken();
    public static SyntaxToken GreaterThanGreaterThanToken();
    public static SyntaxToken GreaterThanToken();
    public static SyntaxToken GroupKeyword();
    public static SyntaxToken HashToken();
    public static SyntaxToken HiddenKeyword();
    public static SyntaxToken IfKeyword();
    public static SyntaxToken ImplicitKeyword();
    public static SyntaxToken InKeyword();
    public static SyntaxToken IntKeyword();
    public static SyntaxToken InterfaceKeyword();
    public static SyntaxToken InternalKeyword();
    public static SyntaxToken InterpolatedStringEndToken();
    public static SyntaxToken InterpolatedStringStartToken();
    public static SyntaxToken InterpolatedVerbatimStringStartToken();
    public static SyntaxToken IntoKeyword();
    public static SyntaxToken IsKeyword();
    public static SyntaxToken JoinKeyword();
    public static SyntaxToken LessThanEqualsToken();
    public static SyntaxToken LessThanLessThanEqualsToken();
    public static SyntaxToken LessThanLessThanToken();
    public static SyntaxToken LessThanSlashToken();
    public static SyntaxToken LessThanToken();
    public static SyntaxToken LetKeyword();
    public static SyntaxToken LineKeyword();
    public static SyntaxToken LoadKeyword();
    public static SyntaxToken LockKeyword();
    public static SyntaxToken LongKeyword();
    public static SyntaxToken MakeRefKeyword();
    public static SyntaxToken MethodKeyword();
    public static SyntaxToken MinusEqualsToken();
    public static SyntaxToken MinusGreaterThanToken();
    public static SyntaxToken MinusMinusToken();
    public static SyntaxToken MinusToken();
    public static SyntaxToken ModuleKeyword();
    public static SyntaxToken NameOfKeyword();
    public static SyntaxToken NamespaceKeyword();
    public static SyntaxToken NewKeyword();
    public static SyntaxToken NullKeyword();
    public static SyntaxToken ObjectKeyword();
    public static SyntaxToken OmittedArraySizeExpressionToken();
    public static SyntaxToken OmittedTypeArgumentToken();
    public static SyntaxToken OnKeyword();
    public static SyntaxToken OpenBraceToken();
    public static SyntaxToken OpenBracketToken();
    public static SyntaxToken OpenParenToken();
    public static SyntaxToken OperatorKeyword();
    public static SyntaxToken OrderByKeyword();
    public static SyntaxToken OutKeyword();
    public static SyntaxToken OverrideKeyword();
    public static SyntaxToken ParamKeyword();
    public static SyntaxToken ParamsKeyword();
    public static SyntaxToken PartialKeyword();
    public static SyntaxToken PercentEqualsToken();
    public static SyntaxToken PercentToken();
    public static SyntaxToken PlusEqualsToken();
    public static SyntaxToken PlusPlusToken();
    public static SyntaxToken PlusToken();
    public static SyntaxToken PragmaKeyword();
    public static SyntaxToken PrivateKeyword();
    public static SyntaxToken PropertyKeyword();
    public static SyntaxToken ProtectedKeyword();
    public static SyntaxToken PublicKeyword();
    public static SyntaxToken QuestionQuestionToken();
    public static SyntaxToken QuestionToken();
    public static SyntaxToken ReadOnlyKeyword();
    public static SyntaxToken RefKeyword();
    public static SyntaxToken RefTypeKeyword();
    public static SyntaxToken RefValueKeyword();
    public static SyntaxToken ReferenceKeyword();
    public static SyntaxToken RegionKeyword();
    public static SyntaxToken RemoveKeyword();
    public static SyntaxToken RestoreKeyword();
    public static SyntaxToken ReturnKeyword();
    public static SyntaxToken SByteKeyword();
    public static SyntaxToken SealedKeyword();
    public static SyntaxToken SelectKeyword();
    public static SyntaxToken SemicolonToken();
    public static SyntaxToken SetKeyword();
    public static SyntaxToken ShortKeyword();
    public static SyntaxToken SingleQuoteToken();
    public static SyntaxToken SizeOfKeyword();
    public static SyntaxToken SlashEqualsToken();
    public static SyntaxToken SlashGreaterThanToken();
    public static SyntaxToken SlashToken();
    public static SyntaxToken StackAllocKeyword();
    public static SyntaxToken StaticKeyword();
    public static SyntaxToken StringKeyword();
    public static SyntaxToken StructKeyword();
    public static SyntaxToken SwitchKeyword();
    public static SyntaxToken ThisKeyword();
    public static SyntaxToken ThrowKeyword();
    public static SyntaxToken TildeToken();
    public static SyntaxToken TrueKeyword();
    public static SyntaxToken TryKeyword();
    public static SyntaxToken TypeKeyword();
    public static SyntaxToken TypeOfKeyword();
    public static SyntaxToken TypeVarKeyword();
    public static SyntaxToken UIntKeyword();
    public static SyntaxToken ULongKeyword();
    public static SyntaxToken UShortKeyword();
    public static SyntaxToken UncheckedKeyword();
    public static SyntaxToken UndefKeyword();
    public static SyntaxToken UnderscoreToken();
    public static SyntaxToken UnsafeKeyword();
    public static SyntaxToken UsingKeyword();
    public static SyntaxToken VirtualKeyword();
    public static SyntaxToken VoidKeyword();
    public static SyntaxToken VolatileKeyword();
    public static SyntaxToken WarningKeyword();
    public static SyntaxToken WhenKeyword();
    public static SyntaxToken WhereKeyword();
    public static SyntaxToken WhileKeyword();
    public static SyntaxToken XmlCDataEndToken();
    public static SyntaxToken XmlCDataStartToken();
    public static SyntaxToken XmlCommentEndToken();
    public static SyntaxToken XmlCommentStartToken();
    public static SyntaxToken XmlProcessingInstructionEndToken();
    public static SyntaxToken XmlProcessingInstructionStartToken();
    public static SyntaxToken YieldKeyword();
    public static SyntaxTrivia EmptyWhitespace();
    public static SyntaxTrivia NewLine();
    public static TryStatementSyntax TryStatement(BlockSyntax block, CatchClauseSyntax @catch, FinallyClauseSyntax @finally = null);
    public static TypeArgumentListSyntax TypeArgumentList(TypeSyntax argument);
    public static TypeArgumentListSyntax TypeArgumentList(params TypeSyntax[] arguments);
    public static TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(IdentifierNameSyntax identifierName, TypeParameterConstraintSyntax typeParameterConstraint);
    public static TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(string name, TypeParameterConstraintSyntax typeParameterConstraint);
    public static TypeParameterListSyntax TypeParameterList(TypeParameterSyntax parameter);
    public static TypeParameterListSyntax TypeParameterList(params TypeParameterSyntax[] parameters);
    public static UsingDirectiveSyntax UsingStaticDirective(NameSyntax name);
    public static UsingDirectiveSyntax UsingStaticDirective(SyntaxToken usingKeyword, SyntaxToken staticKeyword, NameSyntax name, SyntaxToken semicolonToken);
    public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, SyntaxToken identifier, EqualsValueClauseSyntax initializer);
    public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, SyntaxToken identifier, ExpressionSyntax value = null);
    public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, VariableDeclaratorSyntax variable);
    public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, string identifier, ExpressionSyntax value = null);
    public static VariableDeclaratorSyntax VariableDeclarator(SyntaxToken identifier, EqualsValueClauseSyntax initializer);
    public static VariableDeclaratorSyntax VariableDeclarator(string identifier, EqualsValueClauseSyntax initializer);
    public static YieldStatementSyntax YieldBreakStatement();
    public static YieldStatementSyntax YieldReturnStatement(ExpressionSyntax expression);
  }

  public static class CSharpFacts
  {
    public static bool CanBeEmbeddedStatement(SyntaxKind kind);
    public static bool CanHaveEmbeddedStatement(SyntaxKind kind);
    public static bool CanHaveExpressionBody(SyntaxKind kind);
    public static bool CanHaveMembers(SyntaxKind kind);
    public static bool CanHaveModifiers(SyntaxKind kind);
    public static bool CanHaveStatements(SyntaxKind kind);
    public static bool IsAnonymousFunctionExpression(SyntaxKind kind);
    public static bool IsBooleanExpression(SyntaxKind kind);
    public static bool IsBooleanLiteralExpression(SyntaxKind kind);
    public static bool IsCommentTrivia(SyntaxKind kind);
    public static bool IsCompoundAssignmentExpression(SyntaxKind kind);
    public static bool IsConstraint(SyntaxKind kind);
    public static bool IsFunction(SyntaxKind kind);
    public static bool IsIncrementOrDecrementExpression(SyntaxKind kind);
    public static bool IsIterationStatement(SyntaxKind kind);
    public static bool IsJumpStatement(SyntaxKind kind);
    public static bool IsLambdaExpression(SyntaxKind kind);
    public static bool IsPredefinedType(SpecialType specialType);
    public static bool IsSimpleType(SpecialType specialType);
    public static bool IsSwitchLabel(SyntaxKind kind);
    public static bool SupportsPrefixOrPostfixUnaryOperator(SpecialType specialType);
  }

  public static class EnumExtensions
  {
    public static bool Is(this SyntaxKind kind, SyntaxKind kind1, SyntaxKind kind2);
    public static bool Is(this SyntaxKind kind, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3);
    public static bool Is(this SyntaxKind kind, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4);
    public static bool Is(this SyntaxKind kind, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5);
    public static bool Is(this SyntaxKind kind, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5, SyntaxKind kind6);
  }

  public static class ModifierList
  {
    public static SyntaxTokenList Insert(SyntaxTokenList modifiers, SyntaxKind kind, IComparer<SyntaxKind> comparer = null);
    public static SyntaxTokenList Insert(SyntaxTokenList modifiers, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null);
    public static TNode Insert<TNode>(TNode node, SyntaxKind kind, IComparer<SyntaxKind> comparer = null) where TNode : SyntaxNode;
    public static TNode Insert<TNode>(TNode node, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null) where TNode : SyntaxNode;
    public static TNode Remove<TNode>(TNode node, SyntaxKind kind) where TNode : SyntaxNode;
    public static TNode Remove<TNode>(TNode node, SyntaxToken modifier) where TNode : SyntaxNode;
    public static TNode RemoveAll<TNode>(TNode node) where TNode : SyntaxNode;
    public static TNode RemoveAll<TNode>(TNode node, Func<SyntaxToken, bool> predicate) where TNode : SyntaxNode;
    public static TNode RemoveAt<TNode>(TNode node, int index) where TNode : SyntaxNode;
    public static int GetInsertIndex(SyntaxTokenList tokens, SyntaxKind kind, IComparer<SyntaxKind> comparer = null);
    public static int GetInsertIndex(SyntaxTokenList tokens, SyntaxToken token, IComparer<SyntaxToken> comparer = null);
  }

  public static class Modifiers
  {
    public static SyntaxTokenList Const();
    public static SyntaxTokenList FromAccessibility(Accessibility accessibility);
    public static SyntaxTokenList In();
    public static SyntaxTokenList Internal();
    public static SyntaxTokenList InternalAbstract();
    public static SyntaxTokenList InternalConst();
    public static SyntaxTokenList InternalOverride();
    public static SyntaxTokenList InternalPartial();
    public static SyntaxTokenList InternalReadOnly();
    public static SyntaxTokenList InternalStatic();
    public static SyntaxTokenList InternalStaticPartial();
    public static SyntaxTokenList InternalStaticReadOnly();
    public static SyntaxTokenList InternalVirtual();
    public static SyntaxTokenList Out();
    public static SyntaxTokenList Params();
    public static SyntaxTokenList Partial();
    public static SyntaxTokenList Private();
    public static SyntaxTokenList PrivateConst();
    public static SyntaxTokenList PrivatePartial();
    public static SyntaxTokenList PrivateProtected();
    public static SyntaxTokenList PrivateReadOnly();
    public static SyntaxTokenList PrivateStatic();
    public static SyntaxTokenList PrivateStaticPartial();
    public static SyntaxTokenList PrivateStaticReadOnly();
    public static SyntaxTokenList Protected();
    public static SyntaxTokenList ProtectedAbstract();
    public static SyntaxTokenList ProtectedConst();
    public static SyntaxTokenList ProtectedInternal();
    public static SyntaxTokenList ProtectedOverride();
    public static SyntaxTokenList ProtectedReadOnly();
    public static SyntaxTokenList ProtectedStatic();
    public static SyntaxTokenList ProtectedStaticReadOnly();
    public static SyntaxTokenList ProtectedVirtual();
    public static SyntaxTokenList Public();
    public static SyntaxTokenList PublicAbstract();
    public static SyntaxTokenList PublicConst();
    public static SyntaxTokenList PublicOverride();
    public static SyntaxTokenList PublicPartial();
    public static SyntaxTokenList PublicReadOnly();
    public static SyntaxTokenList PublicStatic();
    public static SyntaxTokenList PublicStaticPartial();
    public static SyntaxTokenList PublicStaticReadOnly();
    public static SyntaxTokenList PublicVirtual();
    public static SyntaxTokenList ReadOnly();
    public static SyntaxTokenList Ref();
    public static SyntaxTokenList RefReadOnly();
    public static SyntaxTokenList Static();
    public static SyntaxTokenList StaticReadOnly();
    public static SyntaxTokenList Virtual();
  }

  public static class SymbolExtensions
  {
    public static ExpressionSyntax GetDefaultValueSyntax(this ITypeSymbol typeSymbol, SemanticModel semanticModel, int position, SymbolDisplayFormat format = null);
    public static ExpressionSyntax GetDefaultValueSyntax(this ITypeSymbol typeSymbol, TypeSyntax type);
    public static TypeSyntax ToMinimalTypeSyntax(this INamespaceOrTypeSymbol namespaceOrTypeSymbol, SemanticModel semanticModel, int position, SymbolDisplayFormat format = null);
    public static TypeSyntax ToMinimalTypeSyntax(this INamespaceSymbol namespaceSymbol, SemanticModel semanticModel, int position, SymbolDisplayFormat format = null);
    public static TypeSyntax ToMinimalTypeSyntax(this ITypeSymbol typeSymbol, SemanticModel semanticModel, int position, SymbolDisplayFormat format = null);
    public static TypeSyntax ToTypeSyntax(this INamespaceOrTypeSymbol namespaceOrTypeSymbol, SymbolDisplayFormat format = null);
    public static TypeSyntax ToTypeSyntax(this INamespaceSymbol namespaceSymbol, SymbolDisplayFormat format = null);
    public static TypeSyntax ToTypeSyntax(this ITypeSymbol typeSymbol, SymbolDisplayFormat format = null);
    public static bool SupportsConstantValue(this ITypeSymbol typeSymbol);
  }

  public static class SyntaxAccessibility
  {
    public static Accessibility GetAccessibility(AccessorDeclarationSyntax accessorDeclaration);
    public static Accessibility GetAccessibility(ClassDeclarationSyntax classDeclaration);
    public static Accessibility GetAccessibility(ConstructorDeclarationSyntax constructorDeclaration);
    public static Accessibility GetAccessibility(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration);
    public static Accessibility GetAccessibility(DelegateDeclarationSyntax delegateDeclaration);
    public static Accessibility GetAccessibility(DestructorDeclarationSyntax destructorDeclaration);
    public static Accessibility GetAccessibility(EnumDeclarationSyntax enumDeclaration);
    public static Accessibility GetAccessibility(EnumMemberDeclarationSyntax enumMemberDeclaration);
    public static Accessibility GetAccessibility(EventDeclarationSyntax eventDeclaration);
    public static Accessibility GetAccessibility(EventFieldDeclarationSyntax eventFieldDeclaration);
    public static Accessibility GetAccessibility(FieldDeclarationSyntax fieldDeclaration);
    public static Accessibility GetAccessibility(IndexerDeclarationSyntax indexerDeclaration);
    public static Accessibility GetAccessibility(InterfaceDeclarationSyntax interfaceDeclaration);
    public static Accessibility GetAccessibility(MemberDeclarationSyntax declaration);
    public static Accessibility GetAccessibility(MethodDeclarationSyntax methodDeclaration);
    public static Accessibility GetAccessibility(NamespaceDeclarationSyntax namespaceDeclaration);
    public static Accessibility GetAccessibility(OperatorDeclarationSyntax operatorDeclaration);
    public static Accessibility GetAccessibility(PropertyDeclarationSyntax propertyDeclaration);
    public static Accessibility GetAccessibility(StructDeclarationSyntax structDeclaration);
    public static Accessibility GetDefaultAccessibility(AccessorDeclarationSyntax accessorDeclaration);
    public static Accessibility GetDefaultAccessibility(BaseTypeDeclarationSyntax baseTypeDeclaration);
    public static Accessibility GetDefaultAccessibility(ClassDeclarationSyntax classDeclaration);
    public static Accessibility GetDefaultAccessibility(ConstructorDeclarationSyntax constructorDeclaration);
    public static Accessibility GetDefaultAccessibility(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration);
    public static Accessibility GetDefaultAccessibility(DelegateDeclarationSyntax delegateDeclaration);
    public static Accessibility GetDefaultAccessibility(DestructorDeclarationSyntax destructorDeclaration);
    public static Accessibility GetDefaultAccessibility(EnumDeclarationSyntax enumDeclaration);
    public static Accessibility GetDefaultAccessibility(EnumMemberDeclarationSyntax enumMemberDeclaration);
    public static Accessibility GetDefaultAccessibility(EventDeclarationSyntax eventDeclaration);
    public static Accessibility GetDefaultAccessibility(EventFieldDeclarationSyntax eventFieldDeclaration);
    public static Accessibility GetDefaultAccessibility(FieldDeclarationSyntax fieldDeclaration);
    public static Accessibility GetDefaultAccessibility(IndexerDeclarationSyntax indexerDeclaration);
    public static Accessibility GetDefaultAccessibility(InterfaceDeclarationSyntax interfaceDeclaration);
    public static Accessibility GetDefaultAccessibility(MemberDeclarationSyntax declaration);
    public static Accessibility GetDefaultAccessibility(MethodDeclarationSyntax methodDeclaration);
    public static Accessibility GetDefaultAccessibility(NamespaceDeclarationSyntax namespaceDeclaration);
    public static Accessibility GetDefaultAccessibility(OperatorDeclarationSyntax operatorDeclaration);
    public static Accessibility GetDefaultAccessibility(PropertyDeclarationSyntax propertyDeclaration);
    public static Accessibility GetDefaultAccessibility(StructDeclarationSyntax structDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(BaseTypeDeclarationSyntax baseTypeDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(ClassDeclarationSyntax classDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(ConstructorDeclarationSyntax constructorDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(DelegateDeclarationSyntax delegateDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(DestructorDeclarationSyntax destructorDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(EnumDeclarationSyntax enumDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(EnumMemberDeclarationSyntax enumMemberDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(EventDeclarationSyntax eventDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(EventFieldDeclarationSyntax eventFieldDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(FieldDeclarationSyntax fieldDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(IndexerDeclarationSyntax indexerDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(InterfaceDeclarationSyntax interfaceDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(MemberDeclarationSyntax declaration);
    public static Accessibility GetDefaultExplicitAccessibility(MethodDeclarationSyntax methodDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(NamespaceDeclarationSyntax namespaceDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(OperatorDeclarationSyntax operatorDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(PropertyDeclarationSyntax propertyDeclaration);
    public static Accessibility GetDefaultExplicitAccessibility(StructDeclarationSyntax structDeclaration);
    public static Accessibility GetExplicitAccessibility(AccessorDeclarationSyntax accessorDeclaration);
    public static Accessibility GetExplicitAccessibility(ClassDeclarationSyntax classDeclaration);
    public static Accessibility GetExplicitAccessibility(ConstructorDeclarationSyntax constructorDeclaration);
    public static Accessibility GetExplicitAccessibility(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration);
    public static Accessibility GetExplicitAccessibility(DelegateDeclarationSyntax delegateDeclaration);
    public static Accessibility GetExplicitAccessibility(DestructorDeclarationSyntax destructorDeclaration);
    public static Accessibility GetExplicitAccessibility(EnumDeclarationSyntax enumDeclaration);
    public static Accessibility GetExplicitAccessibility(EventDeclarationSyntax eventDeclaration);
    public static Accessibility GetExplicitAccessibility(EventFieldDeclarationSyntax eventFieldDeclaration);
    public static Accessibility GetExplicitAccessibility(FieldDeclarationSyntax fieldDeclaration);
    public static Accessibility GetExplicitAccessibility(IncompleteMemberSyntax incompleteMember);
    public static Accessibility GetExplicitAccessibility(IndexerDeclarationSyntax indexerDeclaration);
    public static Accessibility GetExplicitAccessibility(InterfaceDeclarationSyntax interfaceDeclaration);
    public static Accessibility GetExplicitAccessibility(MethodDeclarationSyntax methodDeclaration);
    public static Accessibility GetExplicitAccessibility(OperatorDeclarationSyntax operatorDeclaration);
    public static Accessibility GetExplicitAccessibility(PropertyDeclarationSyntax propertyDeclaration);
    public static Accessibility GetExplicitAccessibility(StructDeclarationSyntax structDeclaration);
    public static Accessibility GetExplicitAccessibility(SyntaxNode declaration);
    public static TNode WithExplicitAccessibility<TNode>(TNode node, Accessibility newAccessibility, IComparer<SyntaxKind> comparer = null) where TNode : SyntaxNode;
    public static TNode WithoutExplicitAccessibility<TNode>(TNode node) where TNode : SyntaxNode;
    public static bool IsPubliclyVisible(MemberDeclarationSyntax declaration);
    public static bool IsValidAccessibility(SyntaxNode node, Accessibility accessibility, bool ignoreOverride = false);
  }

  public static class SyntaxExtensions
  {
    public static AccessorDeclarationSyntax Getter(this AccessorListSyntax accessorList);
    public static AccessorDeclarationSyntax Getter(this IndexerDeclarationSyntax indexerDeclaration);
    public static AccessorDeclarationSyntax Getter(this PropertyDeclarationSyntax propertyDeclaration);
    public static AccessorDeclarationSyntax Setter(this AccessorListSyntax accessorList);
    public static AccessorDeclarationSyntax Setter(this IndexerDeclarationSyntax indexerDeclaration);
    public static AccessorDeclarationSyntax Setter(this PropertyDeclarationSyntax propertyDeclaration);
    public static CSharpSyntaxNode BodyOrExpressionBody(this AccessorDeclarationSyntax accessorDeclaration);
    public static CSharpSyntaxNode BodyOrExpressionBody(this ConstructorDeclarationSyntax constructorDeclaration);
    public static CSharpSyntaxNode BodyOrExpressionBody(this ConversionOperatorDeclarationSyntax conversionOperatorDeclaration);
    public static CSharpSyntaxNode BodyOrExpressionBody(this DestructorDeclarationSyntax destructorDeclaration);
    public static CSharpSyntaxNode BodyOrExpressionBody(this LocalFunctionStatementSyntax localFunctionStatement);
    public static CSharpSyntaxNode BodyOrExpressionBody(this MethodDeclarationSyntax methodDeclaration);
    public static CSharpSyntaxNode BodyOrExpressionBody(this OperatorDeclarationSyntax operatorDeclaration);
    public static CSharpSyntaxNode DeclarationOrExpression(this UsingStatementSyntax usingStatement);
    public static ClassDeclarationSyntax AddAttributeLists(this ClassDeclarationSyntax classDeclaration, bool keepDocumentationCommentOnTop, params AttributeListSyntax[] attributeLists);
    public static ClassDeclarationSyntax WithMembers(this ClassDeclarationSyntax classDeclaration, IEnumerable<MemberDeclarationSyntax> members);
    public static ClassDeclarationSyntax WithMembers(this ClassDeclarationSyntax classDeclaration, MemberDeclarationSyntax member);
    public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit, bool keepSingleLineCommentsOnTop, params UsingDirectiveSyntax[] usings);
    public static CompilationUnitSyntax WithMembers(this CompilationUnitSyntax compilationUnit, IEnumerable<MemberDeclarationSyntax> members);
    public static CompilationUnitSyntax WithMembers(this CompilationUnitSyntax compilationUnit, MemberDeclarationSyntax member);
    public static DocumentationCommentTriviaSyntax GetDocumentationComment(this MemberDeclarationSyntax member);
    public static DocumentationCommentTriviaSyntax GetSingleLineDocumentationComment(this MemberDeclarationSyntax member);
    public static EndRegionDirectiveTriviaSyntax GetEndRegionDirective(this RegionDirectiveTriviaSyntax regionDirective);
    public static ExpressionChain AsChain(this BinaryExpressionSyntax binaryExpression, TextSpan? span = null);
    public static ExpressionSyntax WalkDownParentheses(this ExpressionSyntax expression);
    public static ExpressionSyntax WalkUpParentheses(this ExpressionSyntax expression);
    public static IEnumerable<XmlElementSyntax> Elements(this DocumentationCommentTriviaSyntax documentationComment, string localName);
    public static IfStatementCascade AsCascade(this IfStatementSyntax ifStatement);
    public static IfStatementSyntax GetTopmostIf(this ElseClauseSyntax elseClause);
    public static IfStatementSyntax GetTopmostIf(this IfStatementSyntax ifStatement);
    public static InterfaceDeclarationSyntax AddAttributeLists(this InterfaceDeclarationSyntax interfaceDeclaration, bool keepDocumentationCommentOnTop, params AttributeListSyntax[] attributeLists);
    public static InterfaceDeclarationSyntax WithMembers(this InterfaceDeclarationSyntax interfaceDeclaration, IEnumerable<MemberDeclarationSyntax> members);
    public static InterfaceDeclarationSyntax WithMembers(this InterfaceDeclarationSyntax interfaceDeclaration, MemberDeclarationSyntax member);
    public static NamespaceDeclarationSyntax WithMembers(this NamespaceDeclarationSyntax namespaceDeclaration, IEnumerable<MemberDeclarationSyntax> members);
    public static NamespaceDeclarationSyntax WithMembers(this NamespaceDeclarationSyntax namespaceDeclaration, MemberDeclarationSyntax member);
    public static RegionDirectiveTriviaSyntax GetRegionDirective(this EndRegionDirectiveTriviaSyntax endRegionDirective);
    public static SeparatedSyntaxList<TNode> RemoveRange<TNode>(this SeparatedSyntaxList<TNode> list, int index, int count) where TNode : SyntaxNode;
    public static SeparatedSyntaxList<TNode> ReplaceRange<TNode>(this SeparatedSyntaxList<TNode> list, int index, int count, IEnumerable<TNode> newNodes) where TNode : SyntaxNode;
    public static SeparatedSyntaxList<TNode> ToSeparatedSyntaxList<TNode>(this IEnumerable<SyntaxNodeOrToken> nodesAndTokens) where TNode : SyntaxNode;
    public static SeparatedSyntaxList<TNode> ToSeparatedSyntaxList<TNode>(this IEnumerable<TNode> nodes) where TNode : SyntaxNode;
    public static StatementSyntax NextStatement(this StatementSyntax statement);
    public static StatementSyntax PreviousStatement(this StatementSyntax statement);
    public static StructDeclarationSyntax AddAttributeLists(this StructDeclarationSyntax structDeclaration, bool keepDocumentationCommentOnTop, params AttributeListSyntax[] attributeLists);
    public static StructDeclarationSyntax WithMembers(this StructDeclarationSyntax structDeclaration, IEnumerable<MemberDeclarationSyntax> members);
    public static StructDeclarationSyntax WithMembers(this StructDeclarationSyntax structDeclaration, MemberDeclarationSyntax member);
    public static SwitchSectionSyntax DefaultSection(this SwitchStatementSyntax switchStatement);
    public static SyntaxList<StatementSyntax> Add(this SyntaxList<StatementSyntax> statements, StatementSyntax statement, bool ignoreLocalFunctions);
    public static SyntaxList<TNode> RemoveRange<TNode>(this SyntaxList<TNode> list, int index, int count) where TNode : SyntaxNode;
    public static SyntaxList<TNode> ReplaceRange<TNode>(this SyntaxList<TNode> list, int index, int count, IEnumerable<TNode> newNodes) where TNode : SyntaxNode;
    public static SyntaxList<TNode> ToSyntaxList<TNode>(this IEnumerable<TNode> nodes) where TNode : SyntaxNode;
    public static SyntaxNode FirstAncestor(this SyntaxNode node, Func<SyntaxNode, bool> predicate, bool ascendOutOfTrivia = true);
    public static SyntaxNode FirstAncestor(this SyntaxNode node, SyntaxKind kind, bool ascendOutOfTrivia = true);
    public static SyntaxNode FirstAncestor(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, bool ascendOutOfTrivia = true);
    public static SyntaxNode FirstAncestor(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, bool ascendOutOfTrivia = true);
    public static SyntaxNode FirstAncestorOrSelf(this SyntaxNode node, Func<SyntaxNode, bool> predicate, bool ascendOutOfTrivia = true);
    public static SyntaxNode FirstAncestorOrSelf(this SyntaxNode node, SyntaxKind kind, bool ascendOutOfTrivia = true);
    public static SyntaxNode FirstAncestorOrSelf(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, bool ascendOutOfTrivia = true);
    public static SyntaxNode FirstAncestorOrSelf(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, bool ascendOutOfTrivia = true);
    public static SyntaxToken Find(this SyntaxTokenList tokenList, SyntaxKind kind);
    public static SyntaxToken TrimLeadingTrivia(this SyntaxToken token);
    public static SyntaxToken TrimTrailingTrivia(this SyntaxToken token);
    public static SyntaxToken TrimTrivia(this SyntaxToken token);
    public static SyntaxTokenList RemoveRange(this SyntaxTokenList list, int index, int count);
    public static SyntaxTokenList ReplaceRange(this SyntaxTokenList list, int index, int count, IEnumerable<SyntaxToken> newTokens);
    public static SyntaxTokenList ToSyntaxTokenList(this IEnumerable<SyntaxToken> tokens);
    public static SyntaxTrivia Find(this SyntaxTriviaList triviaList, SyntaxKind kind);
    public static SyntaxTrivia GetDocumentationCommentTrivia(this MemberDeclarationSyntax member);
    public static SyntaxTrivia GetPreprocessingMessageTrivia(this EndRegionDirectiveTriviaSyntax endRegionDirective);
    public static SyntaxTrivia GetPreprocessingMessageTrivia(this RegionDirectiveTriviaSyntax regionDirective);
    public static SyntaxTrivia GetSingleLineDocumentationCommentTrivia(this MemberDeclarationSyntax member);
    public static SyntaxTriviaList RemoveRange(this SyntaxTriviaList list, int index, int count);
    public static SyntaxTriviaList ReplaceRange(this SyntaxTriviaList list, int index, int count, IEnumerable<SyntaxTrivia> newTrivia);
    public static TNode Find<TNode>(this SeparatedSyntaxList<TNode> list, SyntaxKind kind) where TNode : SyntaxNode;
    public static TNode Find<TNode>(this SyntaxList<TNode> list, SyntaxKind kind) where TNode : SyntaxNode;
    public static TNode RemoveTrivia<TNode>(this TNode node, TextSpan? span = null) where TNode : SyntaxNode;
    public static TNode RemoveWhitespace<TNode>(this TNode node, TextSpan? span = null) where TNode : SyntaxNode;
    public static TNode ReplaceWhitespace<TNode>(this TNode node, SyntaxTrivia replacement, TextSpan? span = null) where TNode : SyntaxNode;
    public static TNode TrimLeadingTrivia<TNode>(this TNode node) where TNode : SyntaxNode;
    public static TNode TrimTrailingTrivia<TNode>(this TNode node) where TNode : SyntaxNode;
    public static TNode TrimTrivia<TNode>(this TNode node) where TNode : SyntaxNode;
    public static TextSpan BracesSpan(this ClassDeclarationSyntax classDeclaration);
    public static TextSpan BracesSpan(this EnumDeclarationSyntax enumDeclaration);
    public static TextSpan BracesSpan(this InterfaceDeclarationSyntax interfaceDeclaration);
    public static TextSpan BracesSpan(this NamespaceDeclarationSyntax namespaceDeclaration);
    public static TextSpan BracesSpan(this StructDeclarationSyntax structDeclaration);
    public static TextSpan ParenthesesSpan(this CastExpressionSyntax castExpression);
    public static TextSpan ParenthesesSpan(this CommonForEachStatementSyntax forEachStatement);
    public static TextSpan ParenthesesSpan(this ForStatementSyntax forStatement);
    public static bool Contains(this SyntaxTokenList tokenList, SyntaxKind kind);
    public static bool Contains(this SyntaxTriviaList triviaList, SyntaxKind kind);
    public static bool Contains<TNode>(this SeparatedSyntaxList<TNode> list, SyntaxKind kind) where TNode : SyntaxNode;
    public static bool Contains<TNode>(this SyntaxList<TNode> list, SyntaxKind kind) where TNode : SyntaxNode;
    public static bool ContainsAny(this SyntaxTokenList tokenList, SyntaxKind kind1, SyntaxKind kind2);
    public static bool ContainsAny(this SyntaxTokenList tokenList, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3);
    public static bool ContainsAny(this SyntaxTokenList tokenList, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4);
    public static bool ContainsAny(this SyntaxTokenList tokenList, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5);
    public static bool ContainsDefaultLabel(this SwitchSectionSyntax switchSection);
    public static bool ContainsYield(this LocalFunctionStatementSyntax localFunctionStatement);
    public static bool ContainsYield(this MethodDeclarationSyntax methodDeclaration);
    public static bool HasDocumentationComment(this MemberDeclarationSyntax member);
    public static bool HasSingleLineDocumentationComment(this MemberDeclarationSyntax member);
    public static bool IsAutoImplemented(this AccessorDeclarationSyntax accessorDeclaration);
    public static bool IsDescendantOf(this SyntaxNode node, SyntaxKind kind, bool ascendOutOfTrivia = true);
    public static bool IsEmbedded(this StatementSyntax statement, bool canBeBlock = false, bool canBeIfInsideElse = true, bool canBeUsingInsideUsing = true);
    public static bool IsEmptyOrWhitespace(this SyntaxTriviaList triviaList);
    public static bool IsEndOfLineTrivia(this SyntaxTrivia trivia);
    public static bool IsHexNumericLiteral(this LiteralExpressionSyntax literalExpression);
    public static bool IsKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2);
    public static bool IsKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3);
    public static bool IsKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4);
    public static bool IsKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5);
    public static bool IsKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5, SyntaxKind kind6);
    public static bool IsKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2);
    public static bool IsKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3);
    public static bool IsKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4);
    public static bool IsKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5);
    public static bool IsKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5, SyntaxKind kind6);
    public static bool IsKind(this SyntaxTrivia trivia, SyntaxKind kind1, SyntaxKind kind2);
    public static bool IsKind(this SyntaxTrivia trivia, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3);
    public static bool IsKind(this SyntaxTrivia trivia, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4);
    public static bool IsKind(this SyntaxTrivia trivia, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5);
    public static bool IsKind(this SyntaxTrivia trivia, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5, SyntaxKind kind6);
    public static bool IsLast(this SyntaxList<StatementSyntax> statements, StatementSyntax statement, bool ignoreLocalFunctions);
    public static bool IsParams(this ParameterSyntax parameter);
    public static bool IsParentKind(this SyntaxNode node, SyntaxKind kind);
    public static bool IsParentKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2);
    public static bool IsParentKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3);
    public static bool IsParentKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4);
    public static bool IsParentKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5);
    public static bool IsParentKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5, SyntaxKind kind6);
    public static bool IsParentKind(this SyntaxToken token, SyntaxKind kind);
    public static bool IsParentKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2);
    public static bool IsParentKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3);
    public static bool IsParentKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4);
    public static bool IsParentKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5);
    public static bool IsParentKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5, SyntaxKind kind6);
    public static bool IsSimpleIf(this IfStatementSyntax ifStatement);
    public static bool IsTopmostIf(this IfStatementSyntax ifStatement);
    public static bool IsVerbatim(this InterpolatedStringExpressionSyntax interpolatedString);
    public static bool IsVoid(this TypeSyntax type);
    public static bool IsWhitespaceOrEndOfLineTrivia(this SyntaxTrivia trivia);
    public static bool IsWhitespaceTrivia(this SyntaxTrivia trivia);
    public static bool IsYieldBreak(this YieldStatementSyntax yieldStatement);
    public static bool IsYieldReturn(this YieldStatementSyntax yieldStatement);
    public static bool ReturnsVoid(this DelegateDeclarationSyntax delegateDeclaration);
    public static bool ReturnsVoid(this LocalFunctionStatementSyntax localFunctionStatement);
    public static bool ReturnsVoid(this MethodDeclarationSyntax methodDeclaration);
    public static bool TryGetContainingList(this StatementSyntax statement, out SyntaxList<StatementSyntax> statements);
    public static int LastIndexOf(this SyntaxTriviaList triviaList, SyntaxKind kind);
    public static int LastIndexOf<TNode>(this SeparatedSyntaxList<TNode> list, SyntaxKind kind) where TNode : SyntaxNode;
    public static int LastIndexOf<TNode>(this SyntaxList<TNode> list, SyntaxKind kind) where TNode : SyntaxNode;
  }

  public static class SyntaxInfo
  {
    public static AsExpressionInfo AsExpressionInfo(BinaryExpressionSyntax binaryExpression, bool walkDownParentheses = true, bool allowMissing = false);
    public static AsExpressionInfo AsExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false);
    public static AssignmentExpressionInfo AssignmentExpressionInfo(AssignmentExpressionSyntax assignmentExpression, bool walkDownParentheses = true, bool allowMissing = false);
    public static AssignmentExpressionInfo AssignmentExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false);
    public static BinaryExpressionInfo BinaryExpressionInfo(BinaryExpressionSyntax binaryExpression, bool walkDownParentheses = true, bool allowMissing = false);
    public static BinaryExpressionInfo BinaryExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false);
    public static ConditionalExpressionInfo ConditionalExpressionInfo(ConditionalExpressionSyntax conditionalExpression, bool walkDownParentheses = true, bool allowMissing = false);
    public static ConditionalExpressionInfo ConditionalExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false);
    public static GenericInfo GenericInfo(DelegateDeclarationSyntax delegateDeclaration);
    public static GenericInfo GenericInfo(LocalFunctionStatementSyntax localFunctionStatement);
    public static GenericInfo GenericInfo(MethodDeclarationSyntax methodDeclaration);
    public static GenericInfo GenericInfo(SyntaxNode node);
    public static GenericInfo GenericInfo(TypeDeclarationSyntax typeDeclaration);
    public static GenericInfo GenericInfo(TypeParameterConstraintClauseSyntax constraintClause);
    public static GenericInfo GenericInfo(TypeParameterConstraintSyntax typeParameterConstraint);
    public static GenericInfo GenericInfo(TypeParameterListSyntax typeParameterList);
    public static GenericInfo GenericInfo(TypeParameterSyntax typeParameter);
    public static IsExpressionInfo IsExpressionInfo(BinaryExpressionSyntax binaryExpression, bool walkDownParentheses = true, bool allowMissing = false);
    public static IsExpressionInfo IsExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false);
    public static LocalDeclarationStatementInfo LocalDeclarationStatementInfo(ExpressionSyntax value, bool allowMissing = false);
    public static LocalDeclarationStatementInfo LocalDeclarationStatementInfo(LocalDeclarationStatementSyntax localDeclarationStatement, bool allowMissing = false);
    public static MemberDeclarationListInfo MemberDeclarationListInfo(ClassDeclarationSyntax declaration);
    public static MemberDeclarationListInfo MemberDeclarationListInfo(CompilationUnitSyntax compilationUnit);
    public static MemberDeclarationListInfo MemberDeclarationListInfo(InterfaceDeclarationSyntax declaration);
    public static MemberDeclarationListInfo MemberDeclarationListInfo(NamespaceDeclarationSyntax declaration);
    public static MemberDeclarationListInfo MemberDeclarationListInfo(StructDeclarationSyntax declaration);
    public static MemberDeclarationListInfo MemberDeclarationListInfo(SyntaxNode node);
    public static ModifierListInfo ModifierListInfo(AccessorDeclarationSyntax accessorDeclaration);
    public static ModifierListInfo ModifierListInfo(ClassDeclarationSyntax classDeclaration);
    public static ModifierListInfo ModifierListInfo(ConstructorDeclarationSyntax constructorDeclaration);
    public static ModifierListInfo ModifierListInfo(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration);
    public static ModifierListInfo ModifierListInfo(DelegateDeclarationSyntax delegateDeclaration);
    public static ModifierListInfo ModifierListInfo(DestructorDeclarationSyntax destructorDeclaration);
    public static ModifierListInfo ModifierListInfo(EnumDeclarationSyntax enumDeclaration);
    public static ModifierListInfo ModifierListInfo(EventDeclarationSyntax eventDeclaration);
    public static ModifierListInfo ModifierListInfo(EventFieldDeclarationSyntax eventFieldDeclaration);
    public static ModifierListInfo ModifierListInfo(FieldDeclarationSyntax fieldDeclaration);
    public static ModifierListInfo ModifierListInfo(IncompleteMemberSyntax incompleteMember);
    public static ModifierListInfo ModifierListInfo(IndexerDeclarationSyntax indexerDeclaration);
    public static ModifierListInfo ModifierListInfo(InterfaceDeclarationSyntax interfaceDeclaration);
    public static ModifierListInfo ModifierListInfo(LocalDeclarationStatementSyntax localDeclarationStatement);
    public static ModifierListInfo ModifierListInfo(LocalFunctionStatementSyntax localFunctionStatement);
    public static ModifierListInfo ModifierListInfo(MethodDeclarationSyntax methodDeclaration);
    public static ModifierListInfo ModifierListInfo(OperatorDeclarationSyntax operatorDeclaration);
    public static ModifierListInfo ModifierListInfo(ParameterSyntax parameter);
    public static ModifierListInfo ModifierListInfo(PropertyDeclarationSyntax propertyDeclaration);
    public static ModifierListInfo ModifierListInfo(StructDeclarationSyntax structDeclaration);
    public static ModifierListInfo ModifierListInfo(SyntaxNode node);
    public static NullCheckExpressionInfo NullCheckExpressionInfo(SyntaxNode node, NullCheckStyles allowedStyles = ComparisonToNull | IsPattern, bool walkDownParentheses = true, bool allowMissing = false);
    public static NullCheckExpressionInfo NullCheckExpressionInfo(SyntaxNode node, SemanticModel semanticModel, NullCheckStyles allowedStyles = All, bool walkDownParentheses = true, bool allowMissing = false, CancellationToken cancellationToken = default(CancellationToken));
    public static RegionInfo RegionInfo(EndRegionDirectiveTriviaSyntax endRegionDirective);
    public static RegionInfo RegionInfo(RegionDirectiveTriviaSyntax regionDirective);
    public static SimpleAssignmentExpressionInfo SimpleAssignmentExpressionInfo(AssignmentExpressionSyntax assignmentExpression, bool walkDownParentheses = true, bool allowMissing = false);
    public static SimpleAssignmentExpressionInfo SimpleAssignmentExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false);
    public static SimpleAssignmentStatementInfo SimpleAssignmentStatementInfo(AssignmentExpressionSyntax assignmentExpression, bool walkDownParentheses = true, bool allowMissing = false);
    public static SimpleAssignmentStatementInfo SimpleAssignmentStatementInfo(ExpressionStatementSyntax expressionStatement, bool walkDownParentheses = true, bool allowMissing = false);
    public static SimpleAssignmentStatementInfo SimpleAssignmentStatementInfo(StatementSyntax statement, bool walkDownParentheses = true, bool allowMissing = false);
    public static SimpleIfElseInfo SimpleIfElseInfo(IfStatementSyntax ifStatement, bool walkDownParentheses = true, bool allowMissing = false);
    public static SimpleIfStatementInfo SimpleIfStatementInfo(IfStatementSyntax ifStatement, bool walkDownParentheses = true, bool allowMissing = false);
    public static SimpleIfStatementInfo SimpleIfStatementInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false);
    public static SimpleMemberInvocationExpressionInfo SimpleMemberInvocationExpressionInfo(InvocationExpressionSyntax invocationExpression, bool allowMissing = false);
    public static SimpleMemberInvocationExpressionInfo SimpleMemberInvocationExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false);
    public static SimpleMemberInvocationStatementInfo SimpleMemberInvocationStatementInfo(ExpressionStatementSyntax expressionStatement, bool allowMissing = false);
    public static SimpleMemberInvocationStatementInfo SimpleMemberInvocationStatementInfo(InvocationExpressionSyntax invocationExpression, bool allowMissing = false);
    public static SimpleMemberInvocationStatementInfo SimpleMemberInvocationStatementInfo(SyntaxNode node, bool allowMissing = false);
    public static SingleLocalDeclarationStatementInfo SingleLocalDeclarationStatementInfo(ExpressionSyntax value);
    public static SingleLocalDeclarationStatementInfo SingleLocalDeclarationStatementInfo(LocalDeclarationStatementSyntax localDeclarationStatement, bool allowMissing = false);
    public static SingleLocalDeclarationStatementInfo SingleLocalDeclarationStatementInfo(VariableDeclarationSyntax variableDeclaration, bool allowMissing = false);
    public static SingleParameterLambdaExpressionInfo SingleParameterLambdaExpressionInfo(LambdaExpressionSyntax lambdaExpression, bool allowMissing = false);
    public static SingleParameterLambdaExpressionInfo SingleParameterLambdaExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false);
    public static StatementListInfo StatementListInfo(StatementSyntax statement);
    public static StringConcatenationExpressionInfo StringConcatenationExpressionInfo(BinaryExpressionSyntax binaryExpression, SemanticModel semanticModel, CancellationToken cancellationToken = default(CancellationToken));
    public static StringConcatenationExpressionInfo StringConcatenationExpressionInfo(SyntaxNode node, SemanticModel semanticModel, bool walkDownParentheses = true, CancellationToken cancellationToken = default(CancellationToken));
    public static StringConcatenationExpressionInfo StringConcatenationExpressionInfo(in ExpressionChain expressionChain, SemanticModel semanticModel, CancellationToken cancellationToken = default(CancellationToken));
    public static StringLiteralExpressionInfo StringLiteralExpressionInfo(LiteralExpressionSyntax literalExpression);
    public static StringLiteralExpressionInfo StringLiteralExpressionInfo(SyntaxNode node, bool walkDownParentheses = true);
    public static UsingDirectiveListInfo UsingDirectiveListInfo(CompilationUnitSyntax compilationUnit);
    public static UsingDirectiveListInfo UsingDirectiveListInfo(NamespaceDeclarationSyntax declaration);
    public static UsingDirectiveListInfo UsingDirectiveListInfo(SyntaxNode node);
    public static XmlElementInfo XmlElementInfo(XmlNodeSyntax xmlNode);
  }

  public static class WorkspaceExtensions
  {
    public static Task<Document> RemoveCommentsAsync(this Document document, CommentKinds kinds, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> RemoveCommentsAsync(this Document document, TextSpan span, CommentKinds kinds, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> RemovePreprocessorDirectivesAsync(this Document document, PreprocessorDirectiveKinds directiveKinds, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> RemovePreprocessorDirectivesAsync(this Document document, TextSpan span, PreprocessorDirectiveKinds directiveKinds, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> RemoveRegionAsync(this Document document, RegionInfo region, CancellationToken cancellationToken = default(CancellationToken));
    public static Task<Document> RemoveTriviaAsync(this Document document, TextSpan span, CancellationToken cancellationToken = default(CancellationToken));
  }

  public static class WorkspaceSyntaxExtensions
  {
    public static ParenthesizedExpressionSyntax Parenthesize(this ExpressionSyntax expression, bool includeElasticTrivia = true, bool simplifiable = true);
    public static SyntaxToken WithFormatterAnnotation(this SyntaxToken token);
    public static SyntaxToken WithRenameAnnotation(this SyntaxToken token);
    public static SyntaxToken WithSimplifierAnnotation(this SyntaxToken token);
    public static TNode WithFormatterAnnotation<TNode>(this TNode node) where TNode : SyntaxNode;
    public static TNode WithSimplifierAnnotation<TNode>(this TNode node) where TNode : SyntaxNode;
  }

  public readonly struct ExpressionChain
  {
    public BinaryExpressionSyntax BinaryExpression { get; }
    public TextSpan? Span { get; }
    public ExpressionChain.Enumerator GetEnumerator();
    public ExpressionChain.Reversed Reverse();
    public bool Equals(ExpressionChain other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in ExpressionChain info1, in ExpressionChain info2);
    public static bool operator ==(in ExpressionChain info1, in ExpressionChain info2);

    public readonly struct ExpressionChain.Reversed
    {
      public Reversed(in ExpressionChain chain);
      public ExpressionChain.Reversed.Enumerator GetEnumerator();
      public bool Equals(ExpressionChain.Reversed other);
      public override bool Equals(object obj);
      public override int GetHashCode();
      public override string ToString();
      public static bool operator !=(in ExpressionChain.Reversed reversed1, in ExpressionChain.Reversed reversed2);
      public static bool operator ==(in ExpressionChain.Reversed reversed1, in ExpressionChain.Reversed reversed2);

      public struct ExpressionChain.Reversed.Enumerator
      {
        public ExpressionSyntax Current { get; }
        public bool MoveNext();
        public override bool Equals(object obj);
        public override int GetHashCode();
        public void Reset();
      }
    }

    public struct ExpressionChain.Enumerator
    {
      public ExpressionSyntax Current { get; }
      public bool MoveNext();
      public override bool Equals(object obj);
      public override int GetHashCode();
      public void Reset();
    }
  }

  public readonly struct IfStatementCascade
  {
    public IfStatementSyntax IfStatement { get; }
    public IfStatementCascade.Enumerator GetEnumerator();
    public bool Equals(IfStatementCascade other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in IfStatementCascade info1, in IfStatementCascade info2);
    public static bool operator ==(in IfStatementCascade info1, in IfStatementCascade info2);

    public struct IfStatementCascade.Enumerator
    {
      public IfStatementOrElseClause Current { get; }
      public bool MoveNext();
      public override bool Equals(object obj);
      public override int GetHashCode();
      public void Reset();
    }
  }

  public readonly struct IfStatementOrElseClause
  {
    public StatementSyntax Statement { get; }
    public SyntaxKind Kind { get; }
    public SyntaxNode Parent { get; }
    public TextSpan FullSpan { get; }
    public TextSpan Span { get; }
    public bool IsElse { get; }
    public bool IsIf { get; }
    public ElseClauseSyntax AsElse();
    public IfStatementSyntax AsIf();
    public bool Equals(IfStatementOrElseClause other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static implicit operator ElseClauseSyntax(in IfStatementOrElseClause ifOrElse);
    public static implicit operator IfStatementOrElseClause(ElseClauseSyntax elseClause);
    public static implicit operator IfStatementOrElseClause(IfStatementSyntax ifStatement);
    public static implicit operator IfStatementSyntax(in IfStatementOrElseClause ifOrElse);
    public static bool operator !=(in IfStatementOrElseClause left, in IfStatementOrElseClause right);
    public static bool operator ==(in IfStatementOrElseClause left, in IfStatementOrElseClause right);
  }

  public enum CommentKinds
  {
    None = 0,
    SingleLine = 1,
    MultiLine = 2,
    NonDocumentation = SingleLine | MultiLine,
    SingleLineDocumentation = 4,
    MultiLineDocumentation = 8,
    Documentation = SingleLineDocumentation | MultiLineDocumentation,
    All = NonDocumentation | Documentation,
  }

  public enum ModifierKinds
  {
    None = 0,
    New = 1,
    Public = 2,
    Private = 4,
    Protected = 8,
    Internal = 16,
    Accessibility = Public | Private | Protected | Internal,
    Const = 32,
    Static = 64,
    Virtual = 128,
    Sealed = 256,
    Override = 512,
    Abstract = 1024,
    AbstractVirtualOverride = Virtual | Override | Abstract,
    ReadOnly = 2048,
    Extern = 4096,
    Unsafe = 8192,
    Volatile = 16384,
    Async = 32768,
    Partial = 65536,
    Ref = 131072,
    Out = 262144,
    In = 524288,
    Params = 1048576,
    This = 2097152,
  }

  public enum NullCheckStyles
  {
    None = 0,
    EqualsToNull = 1,
    NotEqualsToNull = 2,
    ComparisonToNull = EqualsToNull | NotEqualsToNull,
    IsNull = 4,
    NotIsNull = 8,
    IsPattern = IsNull | NotIsNull,
    NotHasValue = 16,
    CheckingNull = EqualsToNull | IsNull | NotHasValue,
    HasValue = 32,
    CheckingNotNull = NotEqualsToNull | NotIsNull | HasValue,
    HasValueProperty = NotHasValue | HasValue,
    All = ComparisonToNull | IsPattern | HasValueProperty,
  }

  public enum PreprocessorDirectiveKinds
  {
    None = 0,
    If = 1,
    Elif = 2,
    Else = 4,
    EndIf = 8,
    Region = 16,
    EndRegion = 32,
    Define = 64,
    Undef = 128,
    Error = 256,
    Warning = 512,
    Line = 1024,
    PragmaWarning = 2048,
    PragmaChecksum = 4096,
    Pragma = PragmaWarning | PragmaChecksum,
    Reference = 8192,
    Load = 16384,
    Bad = 32768,
    Shebang = 65536,
    All = If | Elif | Else | EndIf | Region | EndRegion | Define | Undef | Error | Warning | Line | Pragma | Reference | Load | Bad | Shebang,
  }
}

namespace Roslynator.CSharp.Syntax
{
  public readonly struct AsExpressionInfo
  {
    public BinaryExpressionSyntax AsExpression { get; }
    public ExpressionSyntax Expression { get; }
    public SyntaxToken OperatorToken { get; }
    public TypeSyntax Type { get; }
    public bool Success { get; }
    public bool Equals(AsExpressionInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in AsExpressionInfo info1, in AsExpressionInfo info2);
    public static bool operator ==(in AsExpressionInfo info1, in AsExpressionInfo info2);
  }

  public readonly struct AssignmentExpressionInfo
  {
    public AssignmentExpressionSyntax AssignmentExpression { get; }
    public ExpressionSyntax Left { get; }
    public ExpressionSyntax Right { get; }
    public SyntaxKind Kind { get; }
    public SyntaxToken OperatorToken { get; }
    public bool Success { get; }
    public bool Equals(AssignmentExpressionInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in AssignmentExpressionInfo info1, in AssignmentExpressionInfo info2);
    public static bool operator ==(in AssignmentExpressionInfo info1, in AssignmentExpressionInfo info2);
  }

  public readonly struct BinaryExpressionInfo
  {
    public BinaryExpressionSyntax BinaryExpression { get; }
    public ExpressionSyntax Left { get; }
    public ExpressionSyntax Right { get; }
    public SyntaxKind Kind { get; }
    public bool Success { get; }
    public ExpressionChain AsChain();
    public IEnumerable<ExpressionSyntax> Expressions(bool leftToRight = false);
    public bool Equals(BinaryExpressionInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in BinaryExpressionInfo info1, in BinaryExpressionInfo info2);
    public static bool operator ==(in BinaryExpressionInfo info1, in BinaryExpressionInfo info2);
  }

  public readonly struct ConditionalExpressionInfo
  {
    public ConditionalExpressionSyntax ConditionalExpression { get; }
    public ExpressionSyntax Condition { get; }
    public ExpressionSyntax WhenFalse { get; }
    public ExpressionSyntax WhenTrue { get; }
    public SyntaxToken ColonToken { get; }
    public SyntaxToken QuestionToken { get; }
    public bool Success { get; }
    public bool Equals(ConditionalExpressionInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in ConditionalExpressionInfo info1, in ConditionalExpressionInfo info2);
    public static bool operator ==(in ConditionalExpressionInfo info1, in ConditionalExpressionInfo info2);
  }

  public readonly struct GenericInfo
  {
    public SeparatedSyntaxList<TypeParameterSyntax> TypeParameters { get; }
    public SyntaxKind Kind { get; }
    public SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses { get; }
    public SyntaxNode Node { get; }
    public TypeParameterListSyntax TypeParameterList { get; }
    public bool Success { get; }
    public GenericInfo RemoveAllConstraintClauses();
    public GenericInfo RemoveConstraintClause(TypeParameterConstraintClauseSyntax constraintClause);
    public GenericInfo RemoveTypeParameter(TypeParameterSyntax typeParameter);
    public GenericInfo WithConstraintClauses(SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses);
    public GenericInfo WithTypeParameterList(TypeParameterListSyntax typeParameterList);
    public TypeParameterConstraintClauseSyntax FindConstraintClause(string typeParameterName);
    public TypeParameterSyntax FindTypeParameter(string name);
    public bool Equals(GenericInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in GenericInfo info1, in GenericInfo info2);
    public static bool operator ==(in GenericInfo info1, in GenericInfo info2);
  }

  public readonly struct IsExpressionInfo
  {
    public BinaryExpressionSyntax IsExpression { get; }
    public ExpressionSyntax Expression { get; }
    public SyntaxToken OperatorToken { get; }
    public TypeSyntax Type { get; }
    public bool Success { get; }
    public bool Equals(IsExpressionInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in IsExpressionInfo info1, in IsExpressionInfo info2);
    public static bool operator ==(in IsExpressionInfo info1, in IsExpressionInfo info2);
  }

  public readonly struct LocalDeclarationStatementInfo
  {
    public LocalDeclarationStatementSyntax Statement { get; }
    public SeparatedSyntaxList<VariableDeclaratorSyntax> Variables { get; }
    public SyntaxToken SemicolonToken { get; }
    public SyntaxTokenList Modifiers { get; }
    public TypeSyntax Type { get; }
    public VariableDeclarationSyntax Declaration { get; }
    public bool Success { get; }
    public bool Equals(LocalDeclarationStatementInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in LocalDeclarationStatementInfo info1, in LocalDeclarationStatementInfo info2);
    public static bool operator ==(in LocalDeclarationStatementInfo info1, in LocalDeclarationStatementInfo info2);
  }

  public readonly struct MemberDeclarationListInfo
  {
    public MemberDeclarationSyntax this[int index] { get; }
    public SyntaxList<MemberDeclarationSyntax> Members { get; }
    public SyntaxNode Parent { get; }
    public bool Success { get; }
    public int Count { get; }
    public MemberDeclarationListInfo Add(MemberDeclarationSyntax member);
    public MemberDeclarationListInfo AddRange(IEnumerable<MemberDeclarationSyntax> members);
    public MemberDeclarationListInfo Insert(int index, MemberDeclarationSyntax member);
    public MemberDeclarationListInfo InsertRange(int index, IEnumerable<MemberDeclarationSyntax> members);
    public MemberDeclarationListInfo Remove(MemberDeclarationSyntax member);
    public MemberDeclarationListInfo RemoveAt(int index);
    public MemberDeclarationListInfo RemoveNode(SyntaxNode node, SyntaxRemoveOptions options);
    public MemberDeclarationListInfo Replace(MemberDeclarationSyntax memberInList, MemberDeclarationSyntax newMember);
    public MemberDeclarationListInfo ReplaceAt(int index, MemberDeclarationSyntax newMember);
    public MemberDeclarationListInfo ReplaceNode(SyntaxNode oldNode, SyntaxNode newNode);
    public MemberDeclarationListInfo ReplaceRange(MemberDeclarationSyntax memberInList, IEnumerable<MemberDeclarationSyntax> newMembers);
    public MemberDeclarationListInfo WithMembers(IEnumerable<MemberDeclarationSyntax> members);
    public MemberDeclarationListInfo WithMembers(SyntaxList<MemberDeclarationSyntax> members);
    public MemberDeclarationSyntax First();
    public MemberDeclarationSyntax FirstOrDefault();
    public MemberDeclarationSyntax Last();
    public MemberDeclarationSyntax LastOrDefault();
    public SyntaxList<MemberDeclarationSyntax>.Enumerator GetEnumerator();
    public bool Any();
    public bool Equals(MemberDeclarationListInfo other);
    public int IndexOf(Func<MemberDeclarationSyntax, bool> predicate);
    public int IndexOf(MemberDeclarationSyntax member);
    public int LastIndexOf(Func<MemberDeclarationSyntax, bool> predicate);
    public int LastIndexOf(MemberDeclarationSyntax member);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in MemberDeclarationListInfo info1, in MemberDeclarationListInfo info2);
    public static bool operator ==(in MemberDeclarationListInfo info1, in MemberDeclarationListInfo info2);
  }

  public readonly struct ModifierListInfo
  {
    public Accessibility ExplicitAccessibility { get; }
    public SyntaxNode Parent { get; }
    public SyntaxTokenList Modifiers { get; }
    public bool IsAbstract { get; }
    public bool IsAsync { get; }
    public bool IsConst { get; }
    public bool IsExtern { get; }
    public bool IsIn { get; }
    public bool IsNew { get; }
    public bool IsOut { get; }
    public bool IsOverride { get; }
    public bool IsParams { get; }
    public bool IsPartial { get; }
    public bool IsReadOnly { get; }
    public bool IsRef { get; }
    public bool IsSealed { get; }
    public bool IsStatic { get; }
    public bool IsUnsafe { get; }
    public bool IsVirtual { get; }
    public bool IsVolatile { get; }
    public bool Success { get; }
    public ModifierKinds GetKinds();
    public ModifierListInfo WithExplicitAccessibility(Accessibility newAccessibility, IComparer<SyntaxKind> comparer = null);
    public ModifierListInfo WithModifiers(SyntaxTokenList modifiers);
    public ModifierListInfo WithoutExplicitAccessibility();
    public bool Equals(ModifierListInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in ModifierListInfo info1, in ModifierListInfo info2);
    public static bool operator ==(in ModifierListInfo info1, in ModifierListInfo info2);
  }

  public readonly struct NullCheckExpressionInfo
  {
    public ExpressionSyntax Expression { get; }
    public ExpressionSyntax NullCheckExpression { get; }
    public NullCheckStyles Style { get; }
    public bool IsCheckingNotNull { get; }
    public bool IsCheckingNull { get; }
    public bool Success { get; }
    public bool Equals(NullCheckExpressionInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in NullCheckExpressionInfo info1, in NullCheckExpressionInfo info2);
    public static bool operator ==(in NullCheckExpressionInfo info1, in NullCheckExpressionInfo info2);
  }

  public readonly struct RegionInfo
  {
    public EndRegionDirectiveTriviaSyntax EndDirective { get; }
    public RegionDirectiveTriviaSyntax Directive { get; }
    public TextSpan FullSpan { get; }
    public TextSpan Span { get; }
    public bool IsEmpty { get; }
    public bool Success { get; }
    public bool Equals(RegionInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in RegionInfo info1, in RegionInfo info2);
    public static bool operator ==(in RegionInfo info1, in RegionInfo info2);
  }

  public readonly struct SimpleAssignmentExpressionInfo
  {
    public AssignmentExpressionSyntax AssignmentExpression { get; }
    public ExpressionSyntax Left { get; }
    public ExpressionSyntax Right { get; }
    public SyntaxToken OperatorToken { get; }
    public bool Success { get; }
    public bool Equals(SimpleAssignmentExpressionInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in SimpleAssignmentExpressionInfo info1, in SimpleAssignmentExpressionInfo info2);
    public static bool operator ==(in SimpleAssignmentExpressionInfo info1, in SimpleAssignmentExpressionInfo info2);
  }

  public readonly struct SimpleAssignmentStatementInfo
  {
    public AssignmentExpressionSyntax AssignmentExpression { get; }
    public ExpressionStatementSyntax Statement { get; }
    public ExpressionSyntax Left { get; }
    public ExpressionSyntax Right { get; }
    public SyntaxToken OperatorToken { get; }
    public bool Success { get; }
    public bool Equals(SimpleAssignmentStatementInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in SimpleAssignmentStatementInfo info1, in SimpleAssignmentStatementInfo info2);
    public static bool operator ==(in SimpleAssignmentStatementInfo info1, in SimpleAssignmentStatementInfo info2);
  }

  public readonly struct SimpleIfElseInfo
  {
    public ElseClauseSyntax Else { get; }
    public ExpressionSyntax Condition { get; }
    public IfStatementSyntax IfStatement { get; }
    public StatementSyntax WhenFalse { get; }
    public StatementSyntax WhenTrue { get; }
    public bool Success { get; }
    public bool Equals(SimpleIfElseInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in SimpleIfElseInfo info1, in SimpleIfElseInfo info2);
    public static bool operator ==(in SimpleIfElseInfo info1, in SimpleIfElseInfo info2);
  }

  public readonly struct SimpleIfStatementInfo
  {
    public ExpressionSyntax Condition { get; }
    public IfStatementSyntax IfStatement { get; }
    public StatementSyntax Statement { get; }
    public bool Success { get; }
    public bool Equals(SimpleIfStatementInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in SimpleIfStatementInfo info1, in SimpleIfStatementInfo info2);
    public static bool operator ==(in SimpleIfStatementInfo info1, in SimpleIfStatementInfo info2);
  }

  public readonly struct SimpleMemberInvocationExpressionInfo
  {
    public ArgumentListSyntax ArgumentList { get; }
    public ExpressionSyntax Expression { get; }
    public InvocationExpressionSyntax InvocationExpression { get; }
    public MemberAccessExpressionSyntax MemberAccessExpression { get; }
    public SeparatedSyntaxList<ArgumentSyntax> Arguments { get; }
    public SimpleNameSyntax Name { get; }
    public SyntaxToken OperatorToken { get; }
    public bool Success { get; }
    public string NameText { get; }
    public bool Equals(SimpleMemberInvocationExpressionInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in SimpleMemberInvocationExpressionInfo info1, in SimpleMemberInvocationExpressionInfo info2);
    public static bool operator ==(in SimpleMemberInvocationExpressionInfo info1, in SimpleMemberInvocationExpressionInfo info2);
  }

  public readonly struct SimpleMemberInvocationStatementInfo
  {
    public ArgumentListSyntax ArgumentList { get; }
    public ExpressionStatementSyntax Statement { get; }
    public ExpressionSyntax Expression { get; }
    public InvocationExpressionSyntax InvocationExpression { get; }
    public MemberAccessExpressionSyntax MemberAccessExpression { get; }
    public SeparatedSyntaxList<ArgumentSyntax> Arguments { get; }
    public SimpleNameSyntax Name { get; }
    public bool Success { get; }
    public string NameText { get; }
    public bool Equals(SimpleMemberInvocationStatementInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in SimpleMemberInvocationStatementInfo info1, in SimpleMemberInvocationStatementInfo info2);
    public static bool operator ==(in SimpleMemberInvocationStatementInfo info1, in SimpleMemberInvocationStatementInfo info2);
  }

  public readonly struct SingleLocalDeclarationStatementInfo
  {
    public EqualsValueClauseSyntax Initializer { get; }
    public ExpressionSyntax Value { get; }
    public LocalDeclarationStatementSyntax Statement { get; }
    public SyntaxToken EqualsToken { get; }
    public SyntaxToken Identifier { get; }
    public SyntaxToken SemicolonToken { get; }
    public SyntaxTokenList Modifiers { get; }
    public TypeSyntax Type { get; }
    public VariableDeclarationSyntax Declaration { get; }
    public VariableDeclaratorSyntax Declarator { get; }
    public bool Success { get; }
    public string IdentifierText { get; }
    public bool Equals(SingleLocalDeclarationStatementInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in SingleLocalDeclarationStatementInfo info1, in SingleLocalDeclarationStatementInfo info2);
    public static bool operator ==(in SingleLocalDeclarationStatementInfo info1, in SingleLocalDeclarationStatementInfo info2);
  }

  public readonly struct SingleParameterLambdaExpressionInfo
  {
    public CSharpSyntaxNode Body { get; }
    public LambdaExpressionSyntax LambdaExpression { get; }
    public ParameterListSyntax ParameterList { get; }
    public ParameterSyntax Parameter { get; }
    public bool IsParenthesizedLambda { get; }
    public bool IsSimpleLambda { get; }
    public bool Success { get; }
    public bool Equals(SingleParameterLambdaExpressionInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in SingleParameterLambdaExpressionInfo info1, in SingleParameterLambdaExpressionInfo info2);
    public static bool operator ==(in SingleParameterLambdaExpressionInfo info1, in SingleParameterLambdaExpressionInfo info2);
  }

  public readonly struct StatementListInfo
  {
    public StatementSyntax this[int index] { get; }
    public BlockSyntax ParentAsBlock { get; }
    public SwitchSectionSyntax ParentAsSwitchSection { get; }
    public SyntaxList<StatementSyntax> Statements { get; }
    public SyntaxNode Parent { get; }
    public bool IsParentBlock { get; }
    public bool IsParentSwitchSection { get; }
    public bool Success { get; }
    public int Count { get; }
    public StatementListInfo Add(StatementSyntax statement);
    public StatementListInfo AddRange(IEnumerable<StatementSyntax> statements);
    public StatementListInfo Insert(int index, StatementSyntax statement);
    public StatementListInfo InsertRange(int index, IEnumerable<StatementSyntax> statements);
    public StatementListInfo Remove(StatementSyntax statement);
    public StatementListInfo RemoveAt(int index);
    public StatementListInfo RemoveNode(SyntaxNode node, SyntaxRemoveOptions options);
    public StatementListInfo Replace(StatementSyntax statementInList, StatementSyntax newStatement);
    public StatementListInfo ReplaceAt(int index, StatementSyntax newStatement);
    public StatementListInfo ReplaceNode(SyntaxNode oldNode, SyntaxNode newNode);
    public StatementListInfo ReplaceRange(StatementSyntax statementInList, IEnumerable<StatementSyntax> newStatements);
    public StatementListInfo WithStatements(IEnumerable<StatementSyntax> statements);
    public StatementListInfo WithStatements(SyntaxList<StatementSyntax> statements);
    public StatementSyntax First();
    public StatementSyntax FirstOrDefault();
    public StatementSyntax Last();
    public StatementSyntax LastOrDefault();
    public SyntaxList<StatementSyntax>.Enumerator GetEnumerator();
    public bool Any();
    public bool Equals(StatementListInfo other);
    public int IndexOf(Func<StatementSyntax, bool> predicate);
    public int IndexOf(StatementSyntax statement);
    public int LastIndexOf(Func<StatementSyntax, bool> predicate);
    public int LastIndexOf(StatementSyntax statement);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in StatementListInfo info1, in StatementListInfo info2);
    public static bool operator ==(in StatementListInfo info1, in StatementListInfo info2);
  }

  public readonly struct StringConcatenationExpressionInfo
  {
    public BinaryExpressionSyntax BinaryExpression { get; }
    public bool Success { get; }
    public ExpressionChain AsChain();
    public IEnumerable<ExpressionSyntax> Expressions(bool leftToRight = false);
    public bool Equals(StringConcatenationExpressionInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in StringConcatenationExpressionInfo info1, in StringConcatenationExpressionInfo info2);
    public static bool operator ==(in StringConcatenationExpressionInfo info1, in StringConcatenationExpressionInfo info2);
  }

  public readonly struct StringLiteralExpressionInfo
  {
    public LiteralExpressionSyntax Expression { get; }
    public SyntaxToken Token { get; }
    public bool ContainsEscapeSequence { get; }
    public bool ContainsLinefeed { get; }
    public bool IsRegular { get; }
    public bool IsVerbatim { get; }
    public bool Success { get; }
    public string InnerText { get; }
    public string Text { get; }
    public string ValueText { get; }
    public bool Equals(StringLiteralExpressionInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in StringLiteralExpressionInfo info1, in StringLiteralExpressionInfo info2);
    public static bool operator ==(in StringLiteralExpressionInfo info1, in StringLiteralExpressionInfo info2);
  }

  public readonly struct UsingDirectiveListInfo
  {
    public UsingDirectiveSyntax this[int index] { get; }
    public SyntaxList<UsingDirectiveSyntax> Usings { get; }
    public SyntaxNode Parent { get; }
    public bool Success { get; }
    public int Count { get; }
    public SyntaxList<UsingDirectiveSyntax>.Enumerator GetEnumerator();
    public UsingDirectiveListInfo Add(UsingDirectiveSyntax usingDirective);
    public UsingDirectiveListInfo AddRange(IEnumerable<UsingDirectiveSyntax> usings);
    public UsingDirectiveListInfo Insert(int index, UsingDirectiveSyntax usingDirective);
    public UsingDirectiveListInfo InsertRange(int index, IEnumerable<UsingDirectiveSyntax> usings);
    public UsingDirectiveListInfo Remove(UsingDirectiveSyntax usingDirective);
    public UsingDirectiveListInfo RemoveAt(int index);
    public UsingDirectiveListInfo RemoveNode(SyntaxNode node, SyntaxRemoveOptions options);
    public UsingDirectiveListInfo Replace(UsingDirectiveSyntax usingInLine, UsingDirectiveSyntax newUsingDirective);
    public UsingDirectiveListInfo ReplaceAt(int index, UsingDirectiveSyntax newUsingDirective);
    public UsingDirectiveListInfo ReplaceNode(SyntaxNode oldNode, SyntaxNode newNode);
    public UsingDirectiveListInfo ReplaceRange(UsingDirectiveSyntax usingInLine, IEnumerable<UsingDirectiveSyntax> newUsingDirectives);
    public UsingDirectiveListInfo WithUsings(IEnumerable<UsingDirectiveSyntax> usings);
    public UsingDirectiveListInfo WithUsings(SyntaxList<UsingDirectiveSyntax> usings);
    public UsingDirectiveSyntax First();
    public UsingDirectiveSyntax FirstOrDefault();
    public UsingDirectiveSyntax Last();
    public UsingDirectiveSyntax LastOrDefault();
    public bool Any();
    public bool Equals(UsingDirectiveListInfo other);
    public int IndexOf(Func<UsingDirectiveSyntax, bool> predicate);
    public int IndexOf(UsingDirectiveSyntax usingDirective);
    public int LastIndexOf(Func<UsingDirectiveSyntax, bool> predicate);
    public int LastIndexOf(UsingDirectiveSyntax usingDirective);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in UsingDirectiveListInfo info1, in UsingDirectiveListInfo info2);
    public static bool operator ==(in UsingDirectiveListInfo info1, in UsingDirectiveListInfo info2);
  }

  public readonly struct XmlElementInfo
  {
    public SyntaxKind Kind { get; }
    public XmlNodeSyntax Element { get; }
    public bool IsEmptyElement { get; }
    public bool Success { get; }
    public string LocalName { get; }
    public bool Equals(XmlElementInfo other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public override string ToString();
    public static bool operator !=(in XmlElementInfo info1, in XmlElementInfo info2);
    public static bool operator ==(in XmlElementInfo info1, in XmlElementInfo info2);
  }
}

namespace Roslynator.Text
{
  public class TextLineCollectionSelection
  {
    protected TextLineCollectionSelection(TextLineCollection lines, TextSpan span, int firstIndex, int lastIndex);
    public TextLine this[int index] { get; }
    public TextLineCollection UnderlyingLines { get; }
    public TextSpan OriginalSpan { get; }
    public int Count { get; }
    public int FirstIndex { get; }
    public int LastIndex { get; }
    public TextLine First();
    public TextLine Last();
    public TextLineCollectionSelection.Enumerator GetEnumerator();
    public static TextLineCollectionSelection Create(TextLineCollection lines, TextSpan span);
    public static bool TryCreate(TextLineCollection lines, TextSpan span, out TextLineCollectionSelection selectedLines);

    public struct TextLineCollectionSelection.Enumerator
    {
      public TextLine Current { get; }
      public bool MoveNext();
      public override bool Equals(object obj);
      public override int GetHashCode();
      public void Reset();
    }
  }
}

