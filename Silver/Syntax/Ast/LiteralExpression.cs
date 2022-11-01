using Silver.Compiler;
using Silver.VM;

namespace Silver.Syntax.Ast;

public interface ILiteralExpression : IExpressionKind
{
}

public record Number(double Value) : ILiteralExpression
{
    public void Compile(ref Compiler.Compiler compiler)
    {
        compiler.Emit(Opcode.PutObject);

        var constantId = compiler.CurrentBytecode().AddConstant(
            new RObjectInt(int.Parse(Value.ToString())) // TODO: Fix.
        );
        compiler.Emit(constantId);
    }
}