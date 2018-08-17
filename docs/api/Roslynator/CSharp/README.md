<a name="_Top"></a>

# Roslynator\.CSharp Namespace

[Home](../../README.md#_Top) &#x2022; [Classes](#classes) &#x2022; [Structs](#structs) &#x2022; [Enums](#enums)

## Classes

| Class | Summary |
| ----- | ------- |
| [CSharpExtensions](CSharpExtensions/README.md#_Top) | A set of extension methods for a [SemanticModel](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.semanticmodel)\. |
| [CSharpFactory](CSharpFactory/README.md#_Top) | A factory for syntax nodes, tokens and trivia\. This class is built on top of [SyntaxFactory](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxfactory) members\. |
| [CSharpFacts](CSharpFacts/README.md#_Top) | |
| [EnumExtensions](EnumExtensions/README.md#_Top) | A set of extension methods for enumerations\. |
| [MemberDeclarationListSelection](MemberDeclarationListSelection/README.md#_Top) | Represents selected member declarations in a [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |
| [ModifierList](ModifierList/README.md#_Top) | A set of static methods that allows manipulation with modifiers\. |
| [ModifierList\<TNode>](ModifierList-1/README.md#_Top) | Represents a list of modifiers\. |
| [Modifiers](Modifiers/README.md#_Top) | Serves as a factory for a modifier list\. |
| [StatementListSelection](StatementListSelection/README.md#_Top) | Represents selected statements in a [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |
| [SymbolExtensions](SymbolExtensions/README.md#_Top) | A set of static methods for [ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol) and derived types\. |
| [SyntaxAccessibility](SyntaxAccessibility/README.md#_Top) | A set of static methods that are related to C\# accessibility\. |
| [SyntaxExtensions](SyntaxExtensions/README.md#_Top) | A set of extension methods for syntax \(types derived from [CSharpSyntaxNode](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxnode)\)\. |
| [SyntaxInfo](SyntaxInfo/README.md#_Top) | Serves as a factory for types in Roslynator\.CSharp\.Syntax namespace\. |
| [WorkspaceExtensions](WorkspaceExtensions/README.md#_Top) | A set of extension methods for the workspace layer\. |
| [WorkspaceSyntaxExtensions](WorkspaceSyntaxExtensions/README.md#_Top) | A set of extension methods for syntax\. These methods are dependent on the workspace layer\. |

## Structs

| Struct | Summary |
| ------ | ------- |
| [ExpressionChain](ExpressionChain/README.md#_Top) | Enables to enumerate expressions of a binary expression and expressions of nested binary expressions of the same kind as parent binary expression\. |
| [ExpressionChain.Enumerator](ExpressionChain/Enumerator/README.md#_Top) | |
| [ExpressionChain.Reversed](ExpressionChain/Reversed/README.md#_Top) | Enables to enumerate expressions of [ExpressionChain](ExpressionChain/README.md#_Top) in a reversed order\. |
| [ExpressionChain.Reversed.Enumerator](ExpressionChain/Reversed/Enumerator/README.md#_Top) | |
| [IfStatementCascade](IfStatementCascade/README.md#_Top) | Enables to enumerate if statement cascade\. |
| [IfStatementCascade.Enumerator](IfStatementCascade/Enumerator/README.md#_Top) | |
| [IfStatementOrElseClause](IfStatementOrElseClause/README.md#_Top) | A wrapper for either an [IfStatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.ifstatementsyntax) or an [ElseClauseSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.elseclausesyntax)\. |

## Enums

| Enum | Summary |
| ---- | ------- |
| [CommentKinds](CommentKinds/README.md#_Top) | Specifies C\# comments\. |
| [ModifierKinds](ModifierKinds/README.md#_Top) | Specifies C\# modifier\. |
| [NullCheckStyles](NullCheckStyles/README.md#_Top) | Specifies a null check\. |
| [PreprocessorDirectiveKinds](PreprocessorDirectiveKinds/README.md#_Top) | Specifies C\# preprocessor directives\. |

