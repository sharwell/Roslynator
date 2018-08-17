<a name="_Top"></a>

# WorkspaceExtensions\.RemoveTriviaAsync\(Document, TextSpan, CancellationToken\) Method

[Home](../../../../README.md#_Top)

**Containing Type**: [Roslynator.CSharp](../../README.md#_Top)\.[WorkspaceExtensions](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.Workspaces\.dll

## Summary

Creates a new document with trivia inside the specified span removed\.

```csharp
public static Task<Document> RemoveTriviaAsync(this Document document, TextSpan span, CancellationToken cancellationToken = default(CancellationToken))
```

### Parameters

#### document

#### span

#### cancellationToken

### Returns

System\.Threading\.Tasks\.[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<Microsoft\.CodeAnalysis\.[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>

