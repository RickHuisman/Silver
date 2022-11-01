namespace Silver;

public interface IExpressionKind
{
}

public class Expression
{
    public IExpressionKind Node;

    public Expression(IExpressionKind node)
    {
        Node = node;
    }

    public override string ToString()
    {
        return $"({Node})";
    }
}

public record ExpressionStatement(Expression Expr) : IExpressionKind;

public class BinaryExpression : IExpressionKind
{
    public BinaryOperator Operator;
    public Expression Lhs;
    public Expression Rhs;

    public BinaryExpression(BinaryOperator op, Expression lhs, Expression rhs)
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
    public Expression Expr;

    public GroupingExpression(Expression expr)
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
    public Expression Unary;

    public UnaryExpression(UnaryOperator op, Expression unary)
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
    public Expression Expr { get; set; }
    public Expression Value { get; } // TODO Name

    public SetExpression(string name, Expression expr, Expression value)
    {
        Name = name;
        Expr = expr;
        Value = value;
    }
}

public class GetExpression : IExpressionKind
{
    public string Name { get; }
    public Expression Expr { get; set; }

    public GetExpression(string name, Expression expr)
    {
        Name = name;
        Expr = expr;
    }
}
