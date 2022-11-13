using System.Text;
using Silver.VM;

namespace Silver.Compiler;

public class Bytecode
{
    private string Name;
    public List<byte> Code { get; }
    public List<IRObject> Constants { get; }

    public Bytecode()
    {
        Name = "";
        Code = new List<byte>();
        Constants = new List<IRObject>();
    }

    public void Write(Opcode opcode) => Code.Add((byte) opcode);

    public void Write(byte b) => Code.Add(b);

    public byte AddConstant(IRObject constant)
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
        return instruction switch
        {
            Opcode.PutObject => PutInstruction(builder, "putobject", offset),
            Opcode.Add => SimpleInstruction(builder, "add", offset),
            Opcode.SetLocal => ByteInstruction(builder, "set_local", offset),
            Opcode.GetLocal => ByteInstruction(builder, "get_local", offset),
            _ => throw new Exception($"Unknown opcode {instruction}")
        };
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

    private int ByteInstruction(StringBuilder builder, string name, int offset)
    {
        var slot = Code[offset + 1];

        var constant = Code[offset + 1];
        builder.AppendLine($"{name,-16} '{slot}'");
        return offset + 2;
    }
}