using System.Text;
using Silver.VM;

namespace Silver.Compiler;

public class Bytecode
{
    private string Name;
    private List<byte> Code;
    private List<IRObjectType> Constants;

    public Bytecode()
    {
        Name = "";
        Code = new List<byte>();
        Constants = new List<IRObjectType>();
    }

    public void Write(Opcode opcode) => Code.Add((byte) opcode);

    public void Write(byte b) => Code.Add(b);

    public byte AddConstant(IRObjectType constant)
    {
        Constants.Add(constant);
        return (byte) (Constants.Count - 1); // TODO: Cast?
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine($"== disarm <{Name}> ==");

        for (var offset = 0; offset < Code.Count;)
        {
            offset = DisassembleInstruction(builder, offset);
        }

        return builder.ToString();
    }

    private int DisassembleInstruction(StringBuilder builder, int offset)
    {
        builder.Append($"{offset:X4} ");

        var instruction = (Opcode) Code[offset];
        switch (instruction)
        {
            case Opcode.PutObject:
                return PutInstruction(builder, "putobject", offset);
            case Opcode.Add:
                return SimpleInstruction(builder, "add", offset);
            default:
                throw new Exception($"Unknown opcode {instruction}");
        }
    }

    private static int SimpleInstruction(StringBuilder builder, string name, int offset)
    {
        builder.AppendLine(name);
        return offset + 1;
    }

    private int PutInstruction(StringBuilder builder, string name, int offset)
    {
        var constant = Code[offset + 1];
        builder.AppendLine($"{name,-16} '{Constants[constant]}'");
        return offset + 2;
    }
}