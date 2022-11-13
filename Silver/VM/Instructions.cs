namespace Silver.VM;

public partial class VM
{
    private void Run()
    {
        while (_ip < CurrentBytecode().Code.Count)
        {
            var opcode = (Opcode) ReadByte();
            switch (opcode)
            {
                case Opcode.PutObject:
                    PutObject();
                    break;
                case Opcode.Add:
                    Add();
                    break;
                case Opcode.SetLocal:
                    SetLocal();
                    break;
                case Opcode.GetLocal:
                    GetLocal();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void PutObject()
    {
        var constant = ReadConstant();
        Push(constant);
    }
    
    private void Add()
    {
        var b = Pop();
        var a = Pop();
        Push(a.Add(b));
    }

    private void GetLocal()
    {
        var slot = ReadByte();
        Push(_stack[slot]);
    }
    
    private void SetLocal()
    {
        var slot = ReadByte();
        _stack[slot] = Peek();
    }
}