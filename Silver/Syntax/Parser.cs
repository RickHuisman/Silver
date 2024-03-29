using Silver.Syntax.Ast;

namespace Silver.Syntax;

public partial class Parser
{
    private static List<Token> _tokens;

    public static List<IExpressionKind> Parse(List<Token> tokens)
    {
        _tokens = tokens;
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

    private static bool Check(params TokenType[] types)
    {
        return types.Any(t => PeekType() == t);
    }

    private static bool Match(TokenType type)
    {
        if (PeekType() != type) return false;

        Next();
        return true;
    }

    private static TokenType PeekType()
    {
        return HasNext() ? _tokens[^1].Type : TokenType.Eof;
    }

    private static bool HasNext()
    {
        if (_tokens.Any())
        {
            if (_tokens.Last().Type != TokenType.Eof) return true;
        }
        return false;
    }
}