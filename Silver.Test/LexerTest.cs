using System.Collections.Generic;
using NUnit.Framework;

namespace Silver.Test;

public class LexerTest
{
    [Test]
    public void Parse_Numbers_ReturnsTokens()
    {
        var expected = new List<Token>
        {
            new Token(TokenType.Number, "10"),
            new Token(TokenType.Number, "2.4"),
            new Token(TokenType.Number, "5"),
        };

        const string input = "10 2.4 5";
        var actual = Lexer.Lex(input);
        
        Assert.AreEqual(expected, actual);
    }
}