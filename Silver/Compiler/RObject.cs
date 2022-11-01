namespace Silver.Compiler;

public interface IRObjectType
{
}

public record RObjectInt(int Value) : IRObjectType;