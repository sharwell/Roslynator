namespace Roslynator
{
  public static class DiagnosticsExtensions
  {
  }
  public static class EnumExtensions
  {
  }
  public static class FileLinePositionSpanExtensions
  {
  }
  public abstract class NameGenerator
  {
  }
  public static class SemanticModelExtensions
  {
  }
  public class SeparatedSyntaxListSelection<TNode> where TNode : SyntaxNode
  {
  }
  public static class SymbolExtensions
  {
  }
  public static class SyntaxExtensions
  {
  }
  public class SyntaxListSelection<TNode> where TNode : SyntaxNode
  {
  }
  public static class SyntaxTreeExtensions
  {
  }
  public static class WorkspaceExtensions
  {
  }
  public readonly struct ExtensionMethodSymbolInfo
  {
  }
  public struct Enumerator
  {
  }
  public struct Enumerator
  {
  }
  public abstract interface ISelection<T>
  {
  }
}

namespace Roslynator.CSharp
{
  public static class CSharpExtensions
  {
  }
  public static class CSharpFactory
  {
  }
  public static class CSharpFacts
  {
  }
  public static class EnumExtensions
  {
  }
  public sealed class MemberDeclarationListSelection
  {
  }
  public static class ModifierList
  {
  }
  public abstract class ModifierList<TNode> where TNode : SyntaxNode
  {
  }
  public static class Modifiers
  {
  }
  public sealed class StatementListSelection
  {
  }
  public static class SymbolExtensions
  {
  }
  public static class SyntaxAccessibility
  {
  }
  public static class SyntaxExtensions
  {
  }
  public static class SyntaxInfo
  {
  }
  public static class WorkspaceExtensions
  {
  }
  public static class WorkspaceSyntaxExtensions
  {
  }
  public readonly struct ExpressionChain
  {
  }
  public readonly struct IfStatementCascade
  {
  }
  public readonly struct IfStatementOrElseClause
  {
  }
  public readonly struct Reversed
  {
  }
  public struct Enumerator
  {
  }
  public struct Enumerator
  {
  }
  public struct Enumerator
  {
  }
  public enum CommentKinds
  {
    None = 0,
    SingleLine = 1,
    MultiLine = 2,
    NonDocumentation = SingleLine | MultiLine,
    SingleLineDocumentation = 4,
    MultiLineDocumentation = 8,
    Documentation = SingleLineDocumentation | MultiLineDocumentation,
    All = NonDocumentation | Documentation,
  }
  public enum ModifierKinds
  {
    None = 0,
    New = 1,
    Public = 2,
    Private = 4,
    Protected = 8,
    Internal = 16,
    Accessibility = Public | Private | Protected | Internal,
    Const = 32,
    Static = 64,
    Virtual = 128,
    Sealed = 256,
    Override = 512,
    Abstract = 1024,
    AbstractVirtualOverride = Virtual | Override | Abstract,
    ReadOnly = 2048,
    Extern = 4096,
    Unsafe = 8192,
    Volatile = 16384,
    Async = 32768,
    Partial = 65536,
    Ref = 131072,
    Out = 262144,
    In = 524288,
    Params = 1048576,
    This = 2097152,
  }
  public enum NullCheckStyles
  {
    None = 0,
    EqualsToNull = 1,
    NotEqualsToNull = 2,
    ComparisonToNull = EqualsToNull | NotEqualsToNull,
    IsNull = 4,
    NotIsNull = 8,
    IsPattern = IsNull | NotIsNull,
    NotHasValue = 16,
    CheckingNull = EqualsToNull | IsNull | NotHasValue,
    HasValue = 32,
    CheckingNotNull = NotEqualsToNull | NotIsNull | HasValue,
    HasValueProperty = NotHasValue | HasValue,
    All = ComparisonToNull | IsPattern | HasValueProperty,
  }
  public enum PreprocessorDirectiveKinds
  {
    None = 0,
    If = 1,
    Elif = 2,
    Else = 4,
    EndIf = 8,
    Region = 16,
    EndRegion = 32,
    Define = 64,
    Undef = 128,
    Error = 256,
    Warning = 512,
    Line = 1024,
    PragmaWarning = 2048,
    PragmaChecksum = 4096,
    Pragma = PragmaWarning | PragmaChecksum,
    Reference = 8192,
    Load = 16384,
    Bad = 32768,
    Shebang = 65536,
    All = If | Elif | Else | EndIf | Region | EndRegion | Define | Undef | Error | Warning | Line | Pragma | Reference | Load | Bad | Shebang,
  }
}

namespace Roslynator.CSharp.Syntax
{
  public readonly struct AsExpressionInfo
  {
  }
  public readonly struct AssignmentExpressionInfo
  {
  }
  public readonly struct BinaryExpressionInfo
  {
  }
  public readonly struct ConditionalExpressionInfo
  {
  }
  public readonly struct GenericInfo
  {
  }
  public readonly struct IsExpressionInfo
  {
  }
  public readonly struct LocalDeclarationStatementInfo
  {
  }
  public readonly struct MemberDeclarationListInfo
  {
  }
  public readonly struct ModifierListInfo
  {
  }
  public readonly struct NullCheckExpressionInfo
  {
  }
  public readonly struct RegionInfo
  {
  }
  public readonly struct SimpleAssignmentExpressionInfo
  {
  }
  public readonly struct SimpleAssignmentStatementInfo
  {
  }
  public readonly struct SimpleIfElseInfo
  {
  }
  public readonly struct SimpleIfStatementInfo
  {
  }
  public readonly struct SimpleMemberInvocationExpressionInfo
  {
  }
  public readonly struct SimpleMemberInvocationStatementInfo
  {
  }
  public readonly struct SingleLocalDeclarationStatementInfo
  {
  }
  public readonly struct SingleParameterLambdaExpressionInfo
  {
  }
  public readonly struct StatementListInfo
  {
  }
  public readonly struct StringConcatenationExpressionInfo
  {
  }
  public readonly struct StringLiteralExpressionInfo
  {
  }
  public readonly struct UsingDirectiveListInfo
  {
  }
  public readonly struct XmlElementInfo
  {
  }
}

namespace Roslynator.Text
{
  public class TextLineCollectionSelection
  {
  }
  public struct Enumerator
  {
  }
}

