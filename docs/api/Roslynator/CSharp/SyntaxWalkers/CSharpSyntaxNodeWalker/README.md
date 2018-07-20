# CSharpSyntaxNodeWalker Class

Namespace: [Roslynator.CSharp.SyntaxWalkers](../README.md)

Assembly: Roslynator\.CSharp\.dll

```csharp
public abstract class CSharpSyntaxNodeWalker : Microsoft.CodeAnalysis.CSharp.CSharpSyntaxWalker
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [CSharpSyntaxVisitor](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxvisitor) &#x2192; [CSharpSyntaxWalker](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxwalker) &#x2192; CSharpSyntaxNodeWalker

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [CSharpSyntaxNodeWalker()](-ctor/README.md) | |

## Properties

| Property | Summary |
| -------- | ------- |
| [Depth](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxwalker.depth) |  \(Inherited from [CSharpSyntaxWalker](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxwalker)\) |
| [ShouldVisit](ShouldVisit/README.md) | |

## Methods

| Method | Summary |
| ------ | ------- |
| [DefaultVisit(SyntaxNode)](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxwalker.defaultvisit) |  \(Inherited from [CSharpSyntaxWalker](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxwalker)\) |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [Visit(SyntaxNode)](Visit/README.md) | |
| [VisitAccessorDeclaration(AccessorDeclarationSyntax)](VisitAccessorDeclaration/README.md) | |
| [VisitAccessorList(AccessorListSyntax)](VisitAccessorList/README.md) | |
| [VisitAliasQualifiedName(AliasQualifiedNameSyntax)](VisitAliasQualifiedName/README.md) | |
| [VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax)](VisitAnonymousMethodExpression/README.md) | |
| [VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax)](VisitAnonymousObjectCreationExpression/README.md) | |
| [VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax)](VisitAnonymousObjectMemberDeclarator/README.md) | |
| [VisitArgument(ArgumentSyntax)](VisitArgument/README.md) | |
| [VisitArgumentList(ArgumentListSyntax)](VisitArgumentList/README.md) | |
| [VisitArrayCreationExpression(ArrayCreationExpressionSyntax)](VisitArrayCreationExpression/README.md) | |
| [VisitArrayRankSpecifier(ArrayRankSpecifierSyntax)](VisitArrayRankSpecifier/README.md) | |
| [VisitArrayType(ArrayTypeSyntax)](VisitArrayType/README.md) | |
| [VisitArrowExpressionClause(ArrowExpressionClauseSyntax)](VisitArrowExpressionClause/README.md) | |
| [VisitAssignmentExpression(AssignmentExpressionSyntax)](VisitAssignmentExpression/README.md) | |
| [VisitAttribute(AttributeSyntax)](VisitAttribute/README.md) | |
| [VisitAttributeArgument(AttributeArgumentSyntax)](VisitAttributeArgument/README.md) | |
| [VisitAttributeArgumentList(AttributeArgumentListSyntax)](VisitAttributeArgumentList/README.md) | |
| [VisitAttributeList(AttributeListSyntax)](VisitAttributeList/README.md) | |
| [VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax)](VisitAttributeTargetSpecifier/README.md) | |
| [VisitAwaitExpression(AwaitExpressionSyntax)](VisitAwaitExpression/README.md) | |
| [VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax)](VisitBadDirectiveTrivia/README.md) | |
| [VisitBaseExpression(BaseExpressionSyntax)](VisitBaseExpression/README.md) | |
| [VisitBaseList(BaseListSyntax)](VisitBaseList/README.md) | |
| [VisitBaseType(BaseTypeSyntax)](VisitBaseType/README.md) | |
| [VisitBinaryExpression(BinaryExpressionSyntax)](VisitBinaryExpression/README.md) | |
| [VisitBlock(BlockSyntax)](VisitBlock/README.md) | |
| [VisitBracketedArgumentList(BracketedArgumentListSyntax)](VisitBracketedArgumentList/README.md) | |
| [VisitBracketedParameterList(BracketedParameterListSyntax)](VisitBracketedParameterList/README.md) | |
| [VisitBreakStatement(BreakStatementSyntax)](VisitBreakStatement/README.md) | |
| [VisitCasePatternSwitchLabel(CasePatternSwitchLabelSyntax)](VisitCasePatternSwitchLabel/README.md) | |
| [VisitCaseSwitchLabel(CaseSwitchLabelSyntax)](VisitCaseSwitchLabel/README.md) | |
| [VisitCastExpression(CastExpressionSyntax)](VisitCastExpression/README.md) | |
| [VisitCatchClause(CatchClauseSyntax)](VisitCatchClause/README.md) | |
| [VisitCatchDeclaration(CatchDeclarationSyntax)](VisitCatchDeclaration/README.md) | |
| [VisitCatchFilterClause(CatchFilterClauseSyntax)](VisitCatchFilterClause/README.md) | |
| [VisitClassDeclaration(ClassDeclarationSyntax)](VisitClassDeclaration/README.md) | |
| [VisitClassOrStructConstraint(ClassOrStructConstraintSyntax)](VisitClassOrStructConstraint/README.md) | |
| [VisitCompilationUnit(CompilationUnitSyntax)](VisitCompilationUnit/README.md) | |
| [VisitConditionalAccessExpression(ConditionalAccessExpressionSyntax)](VisitConditionalAccessExpression/README.md) | |
| [VisitConditionalExpression(ConditionalExpressionSyntax)](VisitConditionalExpression/README.md) | |
| [VisitConstantPattern(ConstantPatternSyntax)](VisitConstantPattern/README.md) | |
| [VisitConstructorConstraint(ConstructorConstraintSyntax)](VisitConstructorConstraint/README.md) | |
| [VisitConstructorDeclaration(ConstructorDeclarationSyntax)](VisitConstructorDeclaration/README.md) | |
| [VisitConstructorInitializer(ConstructorInitializerSyntax)](VisitConstructorInitializer/README.md) | |
| [VisitContinueStatement(ContinueStatementSyntax)](VisitContinueStatement/README.md) | |
| [VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax)](VisitConversionOperatorDeclaration/README.md) | |
| [VisitConversionOperatorMemberCref(ConversionOperatorMemberCrefSyntax)](VisitConversionOperatorMemberCref/README.md) | |
| [VisitCref(CrefSyntax)](VisitCref/README.md) | |
| [VisitCrefBracketedParameterList(CrefBracketedParameterListSyntax)](VisitCrefBracketedParameterList/README.md) | |
| [VisitCrefParameter(CrefParameterSyntax)](VisitCrefParameter/README.md) | |
| [VisitCrefParameterList(CrefParameterListSyntax)](VisitCrefParameterList/README.md) | |
| [VisitDeclarationExpression(DeclarationExpressionSyntax)](VisitDeclarationExpression/README.md) | |
| [VisitDeclarationPattern(DeclarationPatternSyntax)](VisitDeclarationPattern/README.md) | |
| [VisitDefaultExpression(DefaultExpressionSyntax)](VisitDefaultExpression/README.md) | |
| [VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax)](VisitDefaultSwitchLabel/README.md) | |
| [VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax)](VisitDefineDirectiveTrivia/README.md) | |
| [VisitDelegateDeclaration(DelegateDeclarationSyntax)](VisitDelegateDeclaration/README.md) | |
| [VisitDestructorDeclaration(DestructorDeclarationSyntax)](VisitDestructorDeclaration/README.md) | |
| [VisitDiscardDesignation(DiscardDesignationSyntax)](VisitDiscardDesignation/README.md) | |
| [VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax)](VisitDocumentationCommentTrivia/README.md) | |
| [VisitDoStatement(DoStatementSyntax)](VisitDoStatement/README.md) | |
| [VisitElementAccessExpression(ElementAccessExpressionSyntax)](VisitElementAccessExpression/README.md) | |
| [VisitElementBindingExpression(ElementBindingExpressionSyntax)](VisitElementBindingExpression/README.md) | |
| [VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax)](VisitElifDirectiveTrivia/README.md) | |
| [VisitElseClause(ElseClauseSyntax)](VisitElseClause/README.md) | |
| [VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax)](VisitElseDirectiveTrivia/README.md) | |
| [VisitEmptyStatement(EmptyStatementSyntax)](VisitEmptyStatement/README.md) | |
| [VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax)](VisitEndIfDirectiveTrivia/README.md) | |
| [VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax)](VisitEndRegionDirectiveTrivia/README.md) | |
| [VisitEnumDeclaration(EnumDeclarationSyntax)](VisitEnumDeclaration/README.md) | |
| [VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax)](VisitEnumMemberDeclaration/README.md) | |
| [VisitEqualsValueClause(EqualsValueClauseSyntax)](VisitEqualsValueClause/README.md) | |
| [VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax)](VisitErrorDirectiveTrivia/README.md) | |
| [VisitEventDeclaration(EventDeclarationSyntax)](VisitEventDeclaration/README.md) | |
| [VisitEventFieldDeclaration(EventFieldDeclarationSyntax)](VisitEventFieldDeclaration/README.md) | |
| [VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax)](VisitExplicitInterfaceSpecifier/README.md) | |
| [VisitExpression(ExpressionSyntax)](VisitExpression/README.md) | |
| [VisitExpressionStatement(ExpressionStatementSyntax)](VisitExpressionStatement/README.md) | |
| [VisitExternAliasDirective(ExternAliasDirectiveSyntax)](VisitExternAliasDirective/README.md) | |
| [VisitFieldDeclaration(FieldDeclarationSyntax)](VisitFieldDeclaration/README.md) | |
| [VisitFinallyClause(FinallyClauseSyntax)](VisitFinallyClause/README.md) | |
| [VisitFixedStatement(FixedStatementSyntax)](VisitFixedStatement/README.md) | |
| [VisitForEachStatement(ForEachStatementSyntax)](VisitForEachStatement/README.md) | |
| [VisitForEachVariableStatement(ForEachVariableStatementSyntax)](VisitForEachVariableStatement/README.md) | |
| [VisitForStatement(ForStatementSyntax)](VisitForStatement/README.md) | |
| [VisitFromClause(FromClauseSyntax)](VisitFromClause/README.md) | |
| [VisitGenericName(GenericNameSyntax)](VisitGenericName/README.md) | |
| [VisitGlobalStatement(GlobalStatementSyntax)](VisitGlobalStatement/README.md) | |
| [VisitGotoStatement(GotoStatementSyntax)](VisitGotoStatement/README.md) | |
| [VisitGroupClause(GroupClauseSyntax)](VisitGroupClause/README.md) | |
| [VisitCheckedExpression(CheckedExpressionSyntax)](VisitCheckedExpression/README.md) | |
| [VisitCheckedStatement(CheckedStatementSyntax)](VisitCheckedStatement/README.md) | |
| [VisitIdentifierName(IdentifierNameSyntax)](VisitIdentifierName/README.md) | |
| [VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax)](VisitIfDirectiveTrivia/README.md) | |
| [VisitIfStatement(IfStatementSyntax)](VisitIfStatement/README.md) | |
| [VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax)](VisitImplicitArrayCreationExpression/README.md) | |
| [VisitImplicitElementAccess(ImplicitElementAccessSyntax)](VisitImplicitElementAccess/README.md) | |
| [VisitImplicitStackAllocArrayCreationExpression(ImplicitStackAllocArrayCreationExpressionSyntax)](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxvisitor.visitimplicitstackallocarraycreationexpression) |  \(Inherited from [CSharpSyntaxVisitor](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxvisitor)\) |
| [VisitIncompleteMember(IncompleteMemberSyntax)](VisitIncompleteMember/README.md) | |
| [VisitIndexerDeclaration(IndexerDeclarationSyntax)](VisitIndexerDeclaration/README.md) | |
| [VisitIndexerMemberCref(IndexerMemberCrefSyntax)](VisitIndexerMemberCref/README.md) | |
| [VisitInitializerExpression(InitializerExpressionSyntax)](VisitInitializerExpression/README.md) | |
| [VisitInterfaceDeclaration(InterfaceDeclarationSyntax)](VisitInterfaceDeclaration/README.md) | |
| [VisitInterpolatedStringContent(InterpolatedStringContentSyntax)](VisitInterpolatedStringContent/README.md) | |
| [VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax)](VisitInterpolatedStringExpression/README.md) | |
| [VisitInterpolatedStringText(InterpolatedStringTextSyntax)](VisitInterpolatedStringText/README.md) | |
| [VisitInterpolation(InterpolationSyntax)](VisitInterpolation/README.md) | |
| [VisitInterpolationAlignmentClause(InterpolationAlignmentClauseSyntax)](VisitInterpolationAlignmentClause/README.md) | |
| [VisitInterpolationFormatClause(InterpolationFormatClauseSyntax)](VisitInterpolationFormatClause/README.md) | |
| [VisitInvocationExpression(InvocationExpressionSyntax)](VisitInvocationExpression/README.md) | |
| [VisitIsPatternExpression(IsPatternExpressionSyntax)](VisitIsPatternExpression/README.md) | |
| [VisitJoinClause(JoinClauseSyntax)](VisitJoinClause/README.md) | |
| [VisitJoinIntoClause(JoinIntoClauseSyntax)](VisitJoinIntoClause/README.md) | |
| [VisitLabeledStatement(LabeledStatementSyntax)](VisitLabeledStatement/README.md) | |
| [VisitLeadingTrivia(SyntaxToken)](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxwalker.visitleadingtrivia) |  \(Inherited from [CSharpSyntaxWalker](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxwalker)\) |
| [VisitLetClause(LetClauseSyntax)](VisitLetClause/README.md) | |
| [VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax)](VisitLineDirectiveTrivia/README.md) | |
| [VisitLiteralExpression(LiteralExpressionSyntax)](VisitLiteralExpression/README.md) | |
| [VisitLoadDirectiveTrivia(LoadDirectiveTriviaSyntax)](VisitLoadDirectiveTrivia/README.md) | |
| [VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax)](VisitLocalDeclarationStatement/README.md) | |
| [VisitLocalFunctionStatement(LocalFunctionStatementSyntax)](VisitLocalFunctionStatement/README.md) | |
| [VisitLockStatement(LockStatementSyntax)](VisitLockStatement/README.md) | |
| [VisitMakeRefExpression(MakeRefExpressionSyntax)](VisitMakeRefExpression/README.md) | |
| [VisitMemberAccessExpression(MemberAccessExpressionSyntax)](VisitMemberAccessExpression/README.md) | |
| [VisitMemberBindingExpression(MemberBindingExpressionSyntax)](VisitMemberBindingExpression/README.md) | |
| [VisitMemberCref(MemberCrefSyntax)](VisitMemberCref/README.md) | |
| [VisitMemberDeclaration(MemberDeclarationSyntax)](VisitMemberDeclaration/README.md) | |
| [VisitMethodDeclaration(MethodDeclarationSyntax)](VisitMethodDeclaration/README.md) | |
| [VisitNameColon(NameColonSyntax)](VisitNameColon/README.md) | |
| [VisitNameEquals(NameEqualsSyntax)](VisitNameEquals/README.md) | |
| [VisitNameMemberCref(NameMemberCrefSyntax)](VisitNameMemberCref/README.md) | |
| [VisitNamespaceDeclaration(NamespaceDeclarationSyntax)](VisitNamespaceDeclaration/README.md) | |
| [VisitNullableType(NullableTypeSyntax)](VisitNullableType/README.md) | |
| [VisitObjectCreationExpression(ObjectCreationExpressionSyntax)](VisitObjectCreationExpression/README.md) | |
| [VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax)](VisitOmittedArraySizeExpression/README.md) | |
| [VisitOmittedTypeArgument(OmittedTypeArgumentSyntax)](VisitOmittedTypeArgument/README.md) | |
| [VisitOperatorDeclaration(OperatorDeclarationSyntax)](VisitOperatorDeclaration/README.md) | |
| [VisitOperatorMemberCref(OperatorMemberCrefSyntax)](VisitOperatorMemberCref/README.md) | |
| [VisitOrderByClause(OrderByClauseSyntax)](VisitOrderByClause/README.md) | |
| [VisitOrdering(OrderingSyntax)](VisitOrdering/README.md) | |
| [VisitParameter(ParameterSyntax)](VisitParameter/README.md) | |
| [VisitParameterList(ParameterListSyntax)](VisitParameterList/README.md) | |
| [VisitParenthesizedExpression(ParenthesizedExpressionSyntax)](VisitParenthesizedExpression/README.md) | |
| [VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax)](VisitParenthesizedLambdaExpression/README.md) | |
| [VisitParenthesizedVariableDesignation(ParenthesizedVariableDesignationSyntax)](VisitParenthesizedVariableDesignation/README.md) | |
| [VisitPattern(PatternSyntax)](VisitPattern/README.md) | |
| [VisitPointerType(PointerTypeSyntax)](VisitPointerType/README.md) | |
| [VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax)](VisitPostfixUnaryExpression/README.md) | |
| [VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax)](VisitPragmaChecksumDirectiveTrivia/README.md) | |
| [VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax)](VisitPragmaWarningDirectiveTrivia/README.md) | |
| [VisitPredefinedType(PredefinedTypeSyntax)](VisitPredefinedType/README.md) | |
| [VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax)](VisitPrefixUnaryExpression/README.md) | |
| [VisitPropertyDeclaration(PropertyDeclarationSyntax)](VisitPropertyDeclaration/README.md) | |
| [VisitQualifiedCref(QualifiedCrefSyntax)](VisitQualifiedCref/README.md) | |
| [VisitQualifiedName(QualifiedNameSyntax)](VisitQualifiedName/README.md) | |
| [VisitQueryBody(QueryBodySyntax)](VisitQueryBody/README.md) | |
| [VisitQueryClause(QueryClauseSyntax)](VisitQueryClause/README.md) | |
| [VisitQueryContinuation(QueryContinuationSyntax)](VisitQueryContinuation/README.md) | |
| [VisitQueryExpression(QueryExpressionSyntax)](VisitQueryExpression/README.md) | |
| [VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax)](VisitReferenceDirectiveTrivia/README.md) | |
| [VisitRefExpression(RefExpressionSyntax)](VisitRefExpression/README.md) | |
| [VisitRefType(RefTypeSyntax)](VisitRefType/README.md) | |
| [VisitRefTypeExpression(RefTypeExpressionSyntax)](VisitRefTypeExpression/README.md) | |
| [VisitRefValueExpression(RefValueExpressionSyntax)](VisitRefValueExpression/README.md) | |
| [VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax)](VisitRegionDirectiveTrivia/README.md) | |
| [VisitReturnStatement(ReturnStatementSyntax)](VisitReturnStatement/README.md) | |
| [VisitSelectClause(SelectClauseSyntax)](VisitSelectClause/README.md) | |
| [VisitSelectOrGroupClause(SelectOrGroupClauseSyntax)](VisitSelectOrGroupClause/README.md) | |
| [VisitShebangDirectiveTrivia(ShebangDirectiveTriviaSyntax)](VisitShebangDirectiveTrivia/README.md) | |
| [VisitSimpleBaseType(SimpleBaseTypeSyntax)](VisitSimpleBaseType/README.md) | |
| [VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax)](VisitSimpleLambdaExpression/README.md) | |
| [VisitSimpleName(SimpleNameSyntax)](VisitSimpleName/README.md) | |
| [VisitSingleVariableDesignation(SingleVariableDesignationSyntax)](VisitSingleVariableDesignation/README.md) | |
| [VisitSizeOfExpression(SizeOfExpressionSyntax)](VisitSizeOfExpression/README.md) | |
| [VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax)](VisitSkippedTokensTrivia/README.md) | |
| [VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax)](VisitStackAllocArrayCreationExpression/README.md) | |
| [VisitStatement(StatementSyntax)](VisitStatement/README.md) | |
| [VisitStructDeclaration(StructDeclarationSyntax)](VisitStructDeclaration/README.md) | |
| [VisitSwitchLabel(SwitchLabelSyntax)](VisitSwitchLabel/README.md) | |
| [VisitSwitchSection(SwitchSectionSyntax)](VisitSwitchSection/README.md) | |
| [VisitSwitchStatement(SwitchStatementSyntax)](VisitSwitchStatement/README.md) | |
| [VisitThisExpression(ThisExpressionSyntax)](VisitThisExpression/README.md) | |
| [VisitThrowExpression(ThrowExpressionSyntax)](VisitThrowExpression/README.md) | |
| [VisitThrowStatement(ThrowStatementSyntax)](VisitThrowStatement/README.md) | |
| [VisitToken(SyntaxToken)](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxwalker.visittoken) |  \(Inherited from [CSharpSyntaxWalker](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxwalker)\) |
| [VisitTrailingTrivia(SyntaxToken)](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxwalker.visittrailingtrivia) |  \(Inherited from [CSharpSyntaxWalker](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxwalker)\) |
| [VisitTrivia(SyntaxTrivia)](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxwalker.visittrivia) |  \(Inherited from [CSharpSyntaxWalker](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxwalker)\) |
| [VisitTryStatement(TryStatementSyntax)](VisitTryStatement/README.md) | |
| [VisitTupleElement(TupleElementSyntax)](VisitTupleElement/README.md) | |
| [VisitTupleExpression(TupleExpressionSyntax)](VisitTupleExpression/README.md) | |
| [VisitTupleType(TupleTypeSyntax)](VisitTupleType/README.md) | |
| [VisitType(TypeSyntax)](VisitType/README.md) | |
| [VisitTypeArgumentList(TypeArgumentListSyntax)](VisitTypeArgumentList/README.md) | |
| [VisitTypeConstraint(TypeConstraintSyntax)](VisitTypeConstraint/README.md) | |
| [VisitTypeCref(TypeCrefSyntax)](VisitTypeCref/README.md) | |
| [VisitTypeOfExpression(TypeOfExpressionSyntax)](VisitTypeOfExpression/README.md) | |
| [VisitTypeParameter(TypeParameterSyntax)](VisitTypeParameter/README.md) | |
| [VisitTypeParameterConstraint(TypeParameterConstraintSyntax)](VisitTypeParameterConstraint/README.md) | |
| [VisitTypeParameterConstraintClause(TypeParameterConstraintClauseSyntax)](VisitTypeParameterConstraintClause/README.md) | |
| [VisitTypeParameterList(TypeParameterListSyntax)](VisitTypeParameterList/README.md) | |
| [VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax)](VisitUndefDirectiveTrivia/README.md) | |
| [VisitUnsafeStatement(UnsafeStatementSyntax)](VisitUnsafeStatement/README.md) | |
| [VisitUsingDirective(UsingDirectiveSyntax)](VisitUsingDirective/README.md) | |
| [VisitUsingStatement(UsingStatementSyntax)](VisitUsingStatement/README.md) | |
| [VisitVariableDeclaration(VariableDeclarationSyntax)](VisitVariableDeclaration/README.md) | |
| [VisitVariableDeclarator(VariableDeclaratorSyntax)](VisitVariableDeclarator/README.md) | |
| [VisitVariableDesignation(VariableDesignationSyntax)](VisitVariableDesignation/README.md) | |
| [VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax)](VisitWarningDirectiveTrivia/README.md) | |
| [VisitWhenClause(WhenClauseSyntax)](VisitWhenClause/README.md) | |
| [VisitWhereClause(WhereClauseSyntax)](VisitWhereClause/README.md) | |
| [VisitWhileStatement(WhileStatementSyntax)](VisitWhileStatement/README.md) | |
| [VisitXmlAttribute(XmlAttributeSyntax)](VisitXmlAttribute/README.md) | |
| [VisitXmlCDataSection(XmlCDataSectionSyntax)](VisitXmlCDataSection/README.md) | |
| [VisitXmlComment(XmlCommentSyntax)](VisitXmlComment/README.md) | |
| [VisitXmlCrefAttribute(XmlCrefAttributeSyntax)](VisitXmlCrefAttribute/README.md) | |
| [VisitXmlElement(XmlElementSyntax)](VisitXmlElement/README.md) | |
| [VisitXmlElementEndTag(XmlElementEndTagSyntax)](VisitXmlElementEndTag/README.md) | |
| [VisitXmlElementStartTag(XmlElementStartTagSyntax)](VisitXmlElementStartTag/README.md) | |
| [VisitXmlEmptyElement(XmlEmptyElementSyntax)](VisitXmlEmptyElement/README.md) | |
| [VisitXmlName(XmlNameSyntax)](VisitXmlName/README.md) | |
| [VisitXmlNameAttribute(XmlNameAttributeSyntax)](VisitXmlNameAttribute/README.md) | |
| [VisitXmlNode(XmlNodeSyntax)](VisitXmlNode/README.md) | |
| [VisitXmlPrefix(XmlPrefixSyntax)](VisitXmlPrefix/README.md) | |
| [VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax)](VisitXmlProcessingInstruction/README.md) | |
| [VisitXmlText(XmlTextSyntax)](VisitXmlText/README.md) | |
| [VisitXmlTextAttribute(XmlTextAttributeSyntax)](VisitXmlTextAttribute/README.md) | |
| [VisitYieldStatement(YieldStatementSyntax)](VisitYieldStatement/README.md) | |

