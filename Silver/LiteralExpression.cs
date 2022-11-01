namespace Silver;

public interface ILiteralExpression : IExpressionKind
{
}

public record Number(double Value) : ILiteralExpression;