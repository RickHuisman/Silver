using Silver.Compiler;

namespace Silver.VM;

public partial class VM
{
    private int _ip;
    private Stack<IRObject> _stack = new();
    private IList<Bytecode> _bytecodes = new List<Bytecode>();

    public void Interpret(IList<Bytecode> bytecodes)
    {
        _bytecodes = bytecodes;
        Run();
        Console.WriteLine("foo");
    }

    private void Push(IRObject value) => _stack.Push(value);

    private IRObject Pop() => _stack.Pop();

    private byte ReadByte()
    {
        var b = CurrentBytecode().Code[_ip];
        _ip += 1;
        return b;
    }

    private IRObject ReadConstant()
    {
        var index = ReadByte();
        return CurrentBytecode().Constants[index];
    }

    private Bytecode CurrentBytecode() => _bytecodes.Last();
}