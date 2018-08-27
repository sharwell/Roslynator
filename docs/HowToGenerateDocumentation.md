
# How to Generate Documentation for a .NET Project

1) Install package [Roslynator.Documentation.CommandLine](http://www.nuget.org/packages/Roslynator.Documentation.CommandLine/)

2) Add MSBuild Target to your csproj (vbproj) file

```xml
<Target Name="PreRoslynatorDocumention" BeforeTargets="RoslynatorDocumentation" Condition=" '$(Configuration)' == 'Release'">

  <PropertyGroup>

    <!-- Define output directory -->
    <RoslynatorDocumentationDirectory>$(SolutionDir)docs\api</RoslynatorDocumentationDirectory>

  </PropertyGroup>

  <!-- Remove output directory -->
  <RemoveDir Directories="$(RoslynatorDocumentationDirectory)" />

  <PropertyGroup>

    <!-- One or more assembly paths you want generator documentation for, for example: "A.dll B.dll" -->
    <RoslynatorDocumentationAssemblies>$(TargetPath)</RoslynatorDocumentationAssemblies>

    <!-- Specify parameters for a command that will generate files to output directory -->
    <RoslynatorDocumentationParameters> -a &quot;$(RoslynatorDocumentationAssemblies)&quot; -o &quot;$(RoslynatorDocumentationDirectory)&quot; -h &quot;API Reference&quot; -m github</RoslynatorDocumentationParameters>

    <!-- Specify parameters for a command that will generate one file containing all declarations -->
    <RoslynatorDocumentationDeclarationsParameters> -a &quot;$(RoslynatorDocumentationAssemblies)&quot; -o &quot;$(RoslynatorDocumentationDirectory)\api.cs&quot;</RoslynatorDocumentationDeclarationsParameters>

  </PropertyGroup>

</Target>
```

See [documentation](../src/Documentation.CommandLine/README.md) to all command options.

3) Build project in **Release** configuration

4) Publish documentation to GitHub