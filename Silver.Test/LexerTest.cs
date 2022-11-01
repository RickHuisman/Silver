using System.Collections.Generic;
using NUnit.Framework;
using Silver.Syntax;

namespace Silver.Test;

public class LexerTest
{
    [Test]
    public void Parse_Numbers_ReturnsTokens()
    {
        const string input = "10 2.4 5";
        var expected = new List<Token>
        {
            new(TokenType.Number, "10"),
            new(TokenType.Number, "2.4"),
            new(TokenType.Number, "5"),
        };

        var actual = Lexer.Lex(input);
        Assert.AreEqual(expected, actual);
    }
}