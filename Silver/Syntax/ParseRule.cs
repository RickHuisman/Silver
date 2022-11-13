using Silver.Syntax.Ast;

namespace Silver.Syntax;

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
            new(TokenType.Eof, null, null, Precedence.None),
            new(TokenType.End, null, null, Precedence.None),
            new(TokenType.RightParen, null, null, Precedence.None),
            new(TokenType.RightBrace, null, null, Precedence.None),
            new(TokenType.Comma, null, null, Precedence.None),
            new(TokenType.Number, Parser.Number, null, Precedence.None),
            new(TokenType.Plus, null, Parser.Binary, Precedence.Term),
            new(TokenType.Minus, Parser.Unary, Parser.Binary, Precedence.Term),
            new(TokenType.Slash, null, Parser.Binary, Precedence.Factor),
            new(TokenType.Star, null, Parser.Binary, Precedence.Factor),
            new(TokenType.Semicolon, null, null, Precedence.None),
            new(TokenType.Identifier, Parser.ParseIdentifier, null, Precedence.None),
            new(TokenType.Equal, null, Parser.ParseAssign, Precedence.Assign),
        };
    }
}

public class ParseRule
{
    public readonly TokenType Type;
    public readonly Func<Token, IExpressionKind>? Prefix;
    public readonly Func<Token, IExpressionKind, IExpressionKind>? Infix;
    public readonly Precedence Precedence;

    public ParseRule(
        TokenType type,
        Func<Token, IExpressionKind>? prefix,
        Func<Token, IExpressionKind, IExpressionKind>? infix,
        Precedence precedence)
    {
        Type = type;
        Prefix = prefix;
        Infix = infix;
        Precedence = precedence;
    }
}