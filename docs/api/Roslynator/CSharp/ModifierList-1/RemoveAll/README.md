# ModifierList\.RemoveAll Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| RemoveAll\(TNode\) | Creates a new node with all modifiers removed\. |
| RemoveAll\(TNode, Func\<SyntaxToken, Boolean>\) | Creates a new node with modifiers that matches the predicate removed\. |

## RemoveAll\(TNode\)<a name="Roslynator_CSharp_ModifierList_1_RemoveAll__0_"></a>

### Summary

Creates a new node with all modifiers removed\.

```csharp
public TNode RemoveAll(TNode node)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |

#### Returns

TNode

## RemoveAll\(TNode, Func\<SyntaxToken, Boolean>\)<a name="Roslynator_CSharp_ModifierList_1_RemoveAll__0_"></a>

### Summary

Creates a new node with modifiers that matches the predicate removed\.

```csharp
public TNode RemoveAll(TNode node, Func<SyntaxToken, bool> predicate)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| predicate | |

#### Returns

TNode

