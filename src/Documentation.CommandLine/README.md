
# Roslynator.Documentation Command-Line Interface

[![NuGet](https://img.shields.io/nuget/v/Roslynator.Documentation.CommandLine.svg)](https://nuget.org/packages/Roslynator.Documentation.CommandLine)

## Commands

* [doc](#doc-command)
* [declarations](#declarations-command)

## `doc` Command

Generates documentation files from specified assemblies.

```
doc
-a|--assemblies
-h|--heading
-o|--output
-r|--references
[--additional-xml-documentation]
[--depth]
[--format-declaration-base-list]
[--format-declaration-constraints]
[--ignored-member-parts]
[--ignored-names]
[--ignored-namespace-parts]
[--ignored-root-parts]
[--ignored-type-parts]
[--include-all-derived-types]
[--include-attribute-arguments]
[--include-class-hierarchy]
[--include-containing-namespace]
[--include-inherited-interface-members]
[--include-member-constant-value]
[--include-member-inherited-from]
[--include-member-implements]
[--include-member-overrides]
[--mark-obsolete]
[--max-derived-types]
[--mode]
[--place-system-namespace-first]
[--preferred-culture]
[--omit-ienumerable]
```

### Options

#### `-a|--assemblies <ASSEMBLIES-TO-DOCUMENT>`
Defines one or more assemblies that should be used as a source for the documentation.

#### `-h|--heading <ROOT-FILE-HEADING>`
Defines a heading of the root documentation file.

#### `-o|--output <OUTPUT-DIRECTORY>`
Defines a path for the output directory.

#### `-r|--references <PATH-TO-FILE-WITH-ASSEMBLY-REFERENCES>`
Defines a path to a file that contains a list of all assemblies necessary to compile a project. Each assembly must be on separate line.

#### `[--additional-xml-documentation] <XML-DOCUMENTATION-FILES>`
Defines one or more xml documentation files that should be included. These files can contain a documentation for namespaces, for instance.

#### `[--depth] {member|type|namespace}`
Defines a depth of a documentation. Default value is `member`.

#### `[--format-declaration-base-list]`
Indicates whether a base list should be formatted on a multiple lines. Default value is `true`.

#### `[--format-declaration-constraints]`
Indicates whether constraints should be formatted on a multiple lines. Default value is `true`.

#### `[--ignored-member-parts] {Overloads ContainingType ContainingAssembly ObsoleteMessage Summary Declaration TypeParameters Parameters ReturnValue Implements Attributes Exceptions Examples Remarks SeeAlso}`
Defines parts of a member documentation that should be excluded. No part is excluded by default.

#### `[--ignored-names] <FULLY-QUALIFIED-METADATA-NAMES-TO-IGNORE>`
Defines a list of metadata names that should be excluded from a documentation. Namespace of type names can be specified.

#### `[--ignored-namespace-parts] {Content ContainingNamespace Summary Examples Remarks Classes Structs Interfaces Enums Delegates SeeAlso}`
Defines parts of a namespace documentation that should be excluded. No part is excluded by default.

#### `[--ignored-root-parts] {Content | Namespaces | Classes | StaticClasses | Structs | Interfaces | Enums | Delegates | Other}`
Defines parts of a root documentation that should be excluded. No part is excluded by default.

#### `[--ignored-type-parts] {Content ContainingNamespace ContainingAssembly ObsoleteMessage Summary Declaration TypeParameters Parameters ReturnValue Inheritance Attributes Derived Implements Examples Remarks Constructors Fields Indexers Properties Methods Operators Events ExplicitInterfaceImplementations ExtensionMethods Classes Structs Interfaces Enums Delegates SeeAlso}`
Defines parts of a type documentation that should be excluded. No part is excluded by default.

#### `[--include-all-derived-types]`
Indicates whether all derived types should be included in the list of derived types. By default only types that directly inherits from a specified type are displayed. Default value is `false`.

#### `[--include-attribute-arguments]`
Indicates whether attribute arguments should be included when displaying an attribute. Default value is `true`.

#### `[--include-class-hierarchy]`
Indicates whether classes should be displayed in a form of hierarchy tree. Default value is `true`.

#### `[--include-containing-namespace]`
Indicates whether a containing namespace should be included when displaying type name. Default value is `true`.

#### `[--include-inherited-interface-members]`
Indicates whether inherited interface members should be displayed in a list of members. Default values is `false`.

#### `[--include-member-constant-value]`
Indicates whether a constant value of a member should be displayed. Default value is `true`.

#### `[--include-member-inherited-from]`
Indicated whether a containing member of an inherited member should be displayed. Default value is `true`.

#### `[--include-member-implements]`
Indicates whether an interface member that is being implemented should be displayed. Default value is `true`.

#### `[--include-member-overrides]`
Indicates whether an overridden member should be displayed. Default value is `true`.

#### `[--mark-obsolete]`
Indicates whether obsolete types and members should be marked as `[deprecated]`. Default value is `true`.

#### `[--max-derived-types]`
Defines maximum number derived types that should be displayed. Default value is `5`.

#### `[--mode] {github}`
Defines documentation generation mode. Currently only supported mode is `github`.

#### `[--place-system-namespace-first]`
Indicated whether namespaces and types contained in `System` namespace should be ordered before other namespaces and types. Default value is `true`.

#### `[--preferred-culture]`
Defines culture that should be used when searching for xml documentation files.

#### `[--omit-ienumerable]`
Indicates whether interface `System.Collections.IEnumerable` should be omitted from documentation if a type also implements interface `System.Collections.Generic.IEnumerable<T>`. Default value is `true`.


## `declarations` Command
Generates a single file that contains all declarations from specified assemblies.

```
declarations
-a|--assemblies
-o|--output
-r|--references
[--empty-line-between-declarations]
[--format-base-list]
[--format-constraints]
[--ignored-names]
[--include-attribute-arguments]
[--indent]
[--indent-chars]
[--new-line-before-open-brace]
[--omit-ienumerable]
[--split-attributes]
[--use-default-literal]
```

### Options

#### `-a|--assemblies <ASSEMBLIES-TO-DOCUMENT>`
Defines one or more assemblies that should be used as a source for the documentation.

#### `-o|--output <OUTPUT-DIRECTORY>`
Defines a path for the output directory.

#### `-r|--references <PATH-TO-FILE-WITH-ASSEMBLY-REFERENCES>`
Defines a path to a file that contains a list of all assemblies necessary to compile a project. Each assembly must be on separate line.

#### `[--empty-line-between-declarations]`
Indicates whether an empty line should be added between two declarations. Default value is `false`.

#### `[--format-base-list]`
Indicates whether a base list should be formatted on a multiple lines. Default value is `false`.

#### `[--format-constraints]`
Indicates whether constraints should be formatted on a multiple lines. Default value is `false`.

#### `[--ignored-names] <FULLY-QUALIFIED-METADATA-NAMES-TO-IGNORE>`
Defines a list of metadata names that should be excluded from a documentation. Namespace of type names can be specified.

#### `[--include-attribute-arguments]`
Indicates whether attribute arguments should be included when displaying an attribute. Default value is `true`.

#### `[--indent]`
Indicates whether declarations should be indented. Default value is true.

#### `[--indent-chars] <INDENT-CHARS>`
Defines characters that should be used for indentation. Default value are four spaces.

#### `[--new-line-before-open-brace]`
Indicates whether opening braced should be placed on a new line. Default value is `true`.

#### `[--omit-ienumerable]`
Indicates whether interface `System.Collections.IEnumerable` should be omitted from documentation if a type also implements interface `System.Collections.Generic.IEnumerable<T>`. Default value is `true`.

#### `[--split-attributes]`
Indicates whether each attribute should be on separate line. Default value is `true`.

#### `[--use-default-literal]`
Indicated whether default literal (`default`) should be used instead of default expression (`default(T)`). Default value is `true`.
