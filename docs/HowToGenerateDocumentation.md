
# How to Generate Documentation for .NET Project

1) Install package [Roslynator.Documentation.Build](http://www.nuget.org/packages/Roslynator.Documentation.Build/)&ensp;[![NuGet](https://img.shields.io/nuget/v/Roslynator.Documentation.Build.svg)](https://nuget.org/packages/Roslynator.Documentation.Build)

2) Add MSBuild Target to your csproj (vbproj) file

```xml
<Target Name="PreRoslynatorDocumention" BeforeTargets="RoslynatorDocumentation" Condition=" '$(Configuration)' == 'Release'">

  <PropertyGroup>

    <!-- Define output directory -->
    <RoslynatorDocumentationDirectory>$(SolutionDir)docs\api</RoslynatorDocumentationDirectory>

  </PropertyGroup>

  <!-- Delete output directory -->
  <RemoveDir Directories="$(RoslynatorDocumentationDirectory)" />

  <PropertyGroup>

    <!-- One or more assembly paths you want generator documentation for, for example: A.dll B.dll -->
    <RoslynatorDocumentationAssemblies>$(TargetPath)</RoslynatorDocumentationAssemblies>

    <!-- Specify parameters for 'doc' command.
         This command will generate documentation files from specified assemblies -->
    <RoslynatorDocumentationDocParameters> ^
-a &quot;$(RoslynatorDocumentationAssemblies)&quot; ^
-o &quot;$(RoslynatorDocumentationDirectory)&quot; ^
-h &quot;API Reference&quot;
    </RoslynatorDocumentationDocParameters>

    <!-- Specify parameters for 'declarations' command.
         This command will generate a single file that contains all declarations from specified assemblies -->
    <RoslynatorDocumentationDeclarationsParameters> ^
-a &quot;$(RoslynatorDocumentationAssemblies)&quot; ^
-o &quot;$(RoslynatorDocumentationDirectory)\api.cs&quot; ^
    </RoslynatorDocumentationDeclarationsParameters>

  </PropertyGroup>

</Target>
```

* [`doc` command reference](../src/Documentation.Build/README.md#doc-command)
* [`declarations` command reference](../src/Documentation.Build/README.md#declarations-command)

*Note: Do not define option `-r|--references`. It is already defined as a part of target RoslynatorDocumentation.*

3) Build project in **Release** configuration

4) Publish documentation to GitHub