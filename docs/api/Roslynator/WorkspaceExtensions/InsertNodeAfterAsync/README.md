# WorkspaceExtensions\.InsertNodeAfterAsync\(Document, SyntaxNode, SyntaxNode, CancellationToken\) Method

[Home](../../../README.md)

**Containing Type**: [Roslynator](../../README.md)\.[WorkspaceExtensions](../README.md)

**Assembly**: Roslynator\.CSharp\.Workspaces\.dll

## Summary

Creates a new document with a new node inserted after the specified node\.

```csharp
public static Task<Document> InsertNodeAfterAsync(this Document document, SyntaxNode nodeInList, SyntaxNode newNode, CancellationToken cancellationToken = default(CancellationToken))
```

### Parameters

document



nodeInList



newNode



cancellationToken



### Returns

System\.Threading\.Tasks\.[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<Microsoft\.CodeAnalysis\.[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>

