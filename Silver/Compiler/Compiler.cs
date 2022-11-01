using Silver.Syntax;
using Silver.VM;

namespace Silver.Compiler;

public class Compiler
{
    private Bytecode _bytecode = new();

    public static Bytecode Compile(string source)
    {
        var tokens = Lexer.Lex(source);
        var ast = Parser.Parse(tokens);

        var compiler = new Compiler();
        foreach (var expr in ast)
        {
            expr.Compile(ref compiler);
        }

        return compiler._bytecode;
    }

    public void Emit(Opcode opcode) => CurrentBytecode().Write(opcode);
    
    public void Emit(byte b) => CurrentBytecode().Write(b);

    public Bytecode CurrentBytecode()
    {
        return _bytecode;
    }
}