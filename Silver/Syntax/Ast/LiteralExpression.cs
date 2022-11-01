namespace Silver.Syntax.Ast;

public interface ILiteralExpression : IExpressionKind
{
}

public record Number(double Value) : ILiteralExpression;