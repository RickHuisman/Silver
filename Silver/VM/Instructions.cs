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
}