namespace Silver;

public record Token(TokenType Type, string Source);

public enum TokenType
{
    // Single-character tokens
    LeftParen,
    RightParen,
    LeftBrace,
    RightBrace,
    Comma,
    Dot,
    Minus,
    Plus,
    Percent,
    Semicolon,
    Star,

    // One or two character tokens
    Bang,
    BangEqual,
    Equal,
    EqualEqual,
    LessThan,
    LessThanEqual,
    GreaterThan,
    GreaterThanEqual,
    Slash,
    Comment,

    // Literals
    Identifier,
    String,
    Number,

    // Keywords
    False,
    True,
    For,
    End,

    Eof
}

public static class TokenTypeTranslator
{
    public static TokenType FromString(string str)
    {
        return str switch
        {
            _ => TokenType.Identifier
        };
    }
}