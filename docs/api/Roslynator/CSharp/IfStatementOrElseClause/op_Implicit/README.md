# IfStatementOrElseClause\.Implicit Operator

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Operator | Summary |
| -------- | ------- |
| [Implicit(ElseClauseSyntax to IfStatementOrElseClause)](#Roslynator_CSharp_IfStatementOrElseClause_op_Implicit_Microsoft_CodeAnalysis_CSharp_Syntax_ElseClauseSyntax__Roslynator_CSharp_IfStatementOrElseClause) | |
| [Implicit(IfStatementOrElseClause to ElseClauseSyntax)](#Roslynator_CSharp_IfStatementOrElseClause_op_Implicit_Roslynator_CSharp_IfStatementOrElseClause___Microsoft_CodeAnalysis_CSharp_Syntax_ElseClauseSyntax) | |
| [Implicit(IfStatementOrElseClause to IfStatementSyntax)](#Roslynator_CSharp_IfStatementOrElseClause_op_Implicit_Roslynator_CSharp_IfStatementOrElseClause___Microsoft_CodeAnalysis_CSharp_Syntax_IfStatementSyntax) | |
| [Implicit(IfStatementSyntax to IfStatementOrElseClause)](#Roslynator_CSharp_IfStatementOrElseClause_op_Implicit_Microsoft_CodeAnalysis_CSharp_Syntax_IfStatementSyntax__Roslynator_CSharp_IfStatementOrElseClause) | |

## Implicit\(ElseClauseSyntax to IfStatementOrElseClause\)<a name="Roslynator_CSharp_IfStatementOrElseClause_op_Implicit_Microsoft_CodeAnalysis_CSharp_Syntax_IfStatementSyntax__Roslynator_CSharp_IfStatementOrElseClause"></a>

```csharp
public static implicit operator IfStatementOrElseClause(ElseClauseSyntax elseClause)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| elseClause | |

#### Returns

[IfStatementOrElseClause](../README.md)

## Implicit\(IfStatementOrElseClause to ElseClauseSyntax\)<a name="Roslynator_CSharp_IfStatementOrElseClause_op_Implicit_Microsoft_CodeAnalysis_CSharp_Syntax_IfStatementSyntax__Roslynator_CSharp_IfStatementOrElseClause"></a>

```csharp
public static implicit operator ElseClauseSyntax(in IfStatementOrElseClause ifOrElse)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| ifOrElse | |

#### Returns

[ElseClauseSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.elseclausesyntax)

## Implicit\(IfStatementOrElseClause to IfStatementSyntax\)<a name="Roslynator_CSharp_IfStatementOrElseClause_op_Implicit_Microsoft_CodeAnalysis_CSharp_Syntax_IfStatementSyntax__Roslynator_CSharp_IfStatementOrElseClause"></a>

```csharp
public static implicit operator IfStatementSyntax(in IfStatementOrElseClause ifOrElse)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| ifOrElse | |

#### Returns

[IfStatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.ifstatementsyntax)

## Implicit\(IfStatementSyntax to IfStatementOrElseClause\)<a name="Roslynator_CSharp_IfStatementOrElseClause_op_Implicit_Microsoft_CodeAnalysis_CSharp_Syntax_IfStatementSyntax__Roslynator_CSharp_IfStatementOrElseClause"></a>

```csharp
public static implicit operator IfStatementOrElseClause(IfStatementSyntax ifStatement)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| ifStatement | |

#### Returns

[IfStatementOrElseClause](../README.md)

