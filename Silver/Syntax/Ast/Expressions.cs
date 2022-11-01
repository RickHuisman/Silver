using Silver.VM;

namespace Silver.Syntax.Ast;

public interface IExpressionKind
{
    public void Compile(Compiler.Compiler compiler);
}

public record ExpressionStatement(IExpressionKind Expr) : IExpressionKind
{
    public void Compile(Compiler.Compiler compiler)
    {
        throw new NotImplementedException();
    }
}

public record AssignExpression(IExpressionKind Variable, IExpressionKind Value) : IExpressionKind
{
    public void Compile(Compiler.Compiler compiler)
    {
        throw new NotImplementedException();
    }
}

public record Identifier(string Name) : IExpressionKind
{
    public void Compile(Compiler.Compiler compiler)
    {
        throw new NotImplementedException();
    }
}

public class BinaryExpression : IExpressionKind
{
    public BinaryOperator Operator;
    public IExpressionKind Left;
    public IExpressionKind Right;

    public BinaryExpression(BinaryOperator op, IExpressionKind left, IExpressionKind right)
    {
        Operator = op;
        Left = left;
        Right = right;
    }

    public override string ToString()
    {
        return $"Left: {Left} - Right: {Right} - Operator: {Operator}";
    }

    public void Compile(Compiler.Compiler compiler)
    {
        Left.Compile(compiler);
        Right.Compile(compiler);
        compiler.Emit(Opcode.Add);
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

    public void Compile(Compiler.Compiler compiler)
    {
        throw new NotImplementedException();
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

    public void Compile(Compiler.Compiler compiler)
    {
        throw new NotImplementedException();
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

    public void Compile(Compiler.Compiler compiler)
    {
        throw new NotImplementedException();
    }
}