namespace Silver;

public static class ParserRules
{
    public static ParseRule GetRule(TokenType type)
    {
        var rule = GetRules().SingleOrDefault(r => r.Type == type);
        if (rule == null)
        {
            throw new Exception($"No matching rule found for TokenType: {type}");
        }

        return rule;
    }

    private static IEnumerable<ParseRule> GetRules()
    {
        return new List<ParseRule>
        {
            new ParseRule(TokenType.Eof, null, null, Precedence.None),
            new ParseRule(TokenType.Fun, null, null, Precedence.None),
            new ParseRule(TokenType.RightParen, null, null, Precedence.None),
            new ParseRule(TokenType.RightBrace, null, null, Precedence.None),
            new ParseRule(TokenType.Comma, null, null, Precedence.None),
            new ParseRule(TokenType.Number, Parser.Number, null, Precedence.None),
            new ParseRule(TokenType.Plus, null, Parser.Binary, Precedence.Term),
            // new ParseRule(TokenType.Minus, Parser.Unary, Parser.Binary, Precedence.Comparison),
            new ParseRule(TokenType.Slash, null, Parser.Binary, Precedence.Factor),
            new ParseRule(TokenType.Star, null, Parser.Binary, Precedence.Factor),
            new ParseRule(TokenType.Semicolon, null, null, Precedence.None),
            new ParseRule(TokenType.Equal, null, null, Precedence.None),
        };
    }
}

public class ParseRule
{
    public TokenType Type;
    public Func<Token, IExpressionKind>? Prefix;
    public Func<Token, IExpressionKind>? Infix;
    public Precedence Precedence;

    public ParseRule(
        TokenType type,
        Func<Token, IExpressionKind>? prefix,
        Func<Token, IExpressionKind>? infix,
        Precedence precedence)
    {
        Type = type;
        Prefix = prefix;
        Infix = infix;
        Precedence = precedence;
    }
}