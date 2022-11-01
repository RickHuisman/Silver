namespace Silver;

public partial class Parser
{
    private static List<Token> _tokens;

    public static List<IExpressionKind> Parse(string source)
    {
        _tokens = Lexer.Lex(source);
        _tokens.Reverse();

        var expressions = new List<IExpressionKind>();
        while (HasNext())
        {
            var expr = ParseExpression();
            expressions.Add(expr);
        }

        return expressions;
    }

    private static Token Next()
    {
        var popped = _tokens[^1];
        _tokens.RemoveAt(_tokens.Count - 1);
        return popped;
    }

    private static Token Consume(TokenType type, string message)
    {
        if (PeekType() == type) return Next();
        throw new Exception(message);
    }

    private static bool Match(TokenType type)
    {
        if (!Check(type)) return false;

        Next();
        return true;
    }

    private static bool Check(TokenType type)
    {
        return PeekType() == type;
    }

    private static TokenType PeekType()
    {
        return HasNext() ? _tokens[^1].Type : TokenType.Eof;
    }

    private static bool HasNext()
    {
        return _tokens.Any();
    }
}