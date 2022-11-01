namespace Silver.Compiler;

public interface IRObject
{
    public IRObject Add(IRObject right);
}

public record IRObjectInt(int Value) : IRObject
{
    public IRObject Add(IRObject right)
    {
        var r = (IRObjectInt) right; // TODO: Cast?
        return new IRObjectInt(Value + r.Value);
    }
}