using Silver.Syntax;
using Silver.VM;

namespace Silver.Compiler;

public class Local
{
    public string Name { get; }
    public ushort Slot { get; }

    public Local(string name, ushort slot)
    {
        Name = name;
        Slot = slot;
    }
}

public class Compiler
{
    private Bytecode _bytecode = new();
    private List<Local> _locals = new();

    public static Bytecode Compile(string source)
    {
        var tokens = Lexer.Lex(source);
        var ast = Parser.Parse(tokens);

        var compiler = new Compiler();
        foreach (var expr in ast)
        {
            expr.Compile(compiler);
        }

        return compiler._bytecode;
    }

    public void DeclareVariable(string name)
    {
        if (_locals.Any(l => name == l.Name))
        {
            throw new Exception();
        }

        // Add local.
        if (_locals.Count == ushort.MaxValue)
        {
            throw new Exception();
            // TODO: Throw error if too many locals.
        }

        var local = new Local(name, (ushort) _locals.Count); // TODO: Casting works?
        _locals.Add(local);
    }

    public int ResolveLocal(string name)
    {
        foreach (var local in _locals)
        {
            if (name == local.Name)
            {
//      if (!local.initialized) throw std::exception(); TODO
                return local.Slot;
            }
        }

        return -1;
    }

    public void Emit(Opcode opcode) => CurrentBytecode().Write(opcode);

    public void Emit(byte b) => CurrentBytecode().Write(b);

    public Bytecode CurrentBytecode()
    {
        return _bytecode;
    }
}