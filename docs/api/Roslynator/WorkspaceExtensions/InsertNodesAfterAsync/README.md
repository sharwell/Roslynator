# WorkspaceExtensions\.InsertNodesAfterAsync\(Document, SyntaxNode, IEnumerable\<SyntaxNode>, CancellationToken\) Method <a name="_Top"></a>

[Home](../../../README.md)

**Containing Type**: [Roslynator](../../README.md#_Top)\.[WorkspaceExtensions](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.Workspaces\.dll

## Summary

Creates a new document with new nodes inserted after the specified node\.

```csharp
public static Task<Document> InsertNodesAfterAsync(this Document document, SyntaxNode nodeInList, IEnumerable<SyntaxNode> newNodes, CancellationToken cancellationToken = default(CancellationToken))
```

### Parameters

#### document

#### nodeInList

#### newNodes

#### cancellationToken

### Returns

System\.Threading\.Tasks\.[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<Microsoft\.CodeAnalysis\.[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>

