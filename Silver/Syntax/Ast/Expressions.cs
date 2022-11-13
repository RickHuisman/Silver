using Silver.VM;

namespace Silver.Syntax.Ast;

public interface IExpressionKind
{
    public void Compile(Compiler.Compiler compiler);
}

public record AssignExpression(IExpressionKind Variable, IExpressionKind Value) : IExpressionKind
{
    public void Compile(Compiler.Compiler compiler)
    {
        // Cast to identifier.
        var identifier = (Identifier) Variable; // TODO: Safe cast?

        compiler.DeclareVariable(identifier.Name);

        Value.Compile(compiler);

        compiler.Emit(Opcode.SetLocal);

        var slot = compiler.ResolveLocal(identifier.Name);
        if (slot == -1) throw new Exception();

        compiler.Emit((byte) slot);
    }
}

public record Identifier(string Name) : IExpressionKind
{
    public void Compile(Compiler.Compiler compiler)
    {
        var slot = compiler.ResolveLocal(Name);
        if (slot == -1) throw new Exception();

        compiler.Emit(Opcode.GetLocal);
        compiler.Emit((byte) slot);
    }
}

public record BinaryExpression(IExpressionKind Left, string Operator, IExpressionKind Right) : IExpressionKind
{
    public void Compile(Compiler.Compiler compiler)
    {
        Left.Compile(compiler);
        Right.Compile(compiler);
        compiler.Emit(Opcode.Add);
    }
}

public record UnaryExpression(string Operator, IExpressionKind Value) : IExpressionKind
{
    public void Compile(Compiler.Compiler compiler)
    {
        throw new NotImplementedException();
    }
}