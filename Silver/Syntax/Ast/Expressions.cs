namespace Silver.Syntax.Ast;

public interface IExpressionKind
{
}

public record ExpressionStatement(IExpressionKind Expr) : IExpressionKind;

public record AssignExpression(IExpressionKind Variable, IExpressionKind Value) : IExpressionKind;

public record Identifier(string Name) : IExpressionKind;

public class BinaryExpression : IExpressionKind
{
    public BinaryOperator Operator;
    public IExpressionKind Lhs;
    public IExpressionKind Rhs;

    public BinaryExpression(BinaryOperator op, IExpressionKind lhs, IExpressionKind rhs)
    {
        Operator = op;
        Lhs = lhs;
        Rhs = rhs;
    }

    public override string ToString()
    {
        return $"Lhs: {Lhs} - Rhs: {Rhs} - Operator: {Operator}";
    }
}

public enum BinaryOperator
{
    Equal,
    BangEqual,
    GreaterThan,
    GreaterThanEqual,
    LessThan,
    LessThanEqual,
    Minus,
    Plus,
    Divide,
    Multiply,
}

public class GroupingExpression : IExpressionKind
{
    public IExpressionKind Expr;

    public GroupingExpression(IExpressionKind expr)
    {
        Expr = expr;
    }
}

public enum UnaryOperator
{
    Negate,
    Not
}

public class UnaryExpression : IExpressionKind
{
    public UnaryOperator Operator;
    public IExpressionKind Unary;

    public UnaryExpression(UnaryOperator op, IExpressionKind unary)
    {
        Operator = op;
        Unary = unary;
    }

    public override string ToString()
    {
        return $"Node: {Unary} - Operator: {Operator}";
    }
}

public class SetExpression : IExpressionKind
{
    public string Name { get; }
    public IExpressionKind Expr { get; set; }
    public IExpressionKind Value { get; } // TODO Name

    public SetExpression(string name, IExpressionKind expr, IExpressionKind value)
    {
        Name = name;
        Expr = expr;
        Value = value;
    }
}

public class GetExpression : IExpressionKind
{
    public string Name { get; }
    public IExpressionKind Expr { get; set; }

    public GetExpression(string name, IExpressionKind expr)
    {
        Name = name;
        Expr = expr;
    }
}