using System.Collections.Generic;
using NUnit.Framework;
using Silver.Syntax;
using Silver.Syntax.Ast;

namespace Silver.Test;

public class ParserTest
{
    private static IList<IExpressionKind> LexAndParse(string source)
    {
        var tokens = Lexer.Lex(source);
        return Parser.Parse(tokens);
    }

    [Test]
    public void Parse_Binary_ReturnsExpression()
    {
        const string input = "5 + 10";
        var expected = new List<IExpressionKind>
        {
            new BinaryExpression(
                new Number(5),
                "+",
                new Number(10)
            )
        };

        var actual = LexAndParse(input);
        TestHelper.AreEqual(expected, actual);
    }

    [Test]
    public void Parse_Unary_ReturnsExpression()
    {
        const string input = "-5";
        var expected = new List<IExpressionKind>
        {
            new UnaryExpression(
                "-",
                new Number(5)
            )
        };

        var actual = LexAndParse(input);
        TestHelper.AreEqual(expected, actual);
    }

    [Test]
    public void Parse_Identifier_ReturnsExpression()
    {
        const string input = "x";
        var expected = new List<IExpressionKind>
        {
            new Identifier("x"),
        };

        var actual = LexAndParse(input);
        TestHelper.AreEqual(expected, actual);
    }

    [Test]
    public void Parse_SetVariable_ReturnsExpression()
    {
        const string input = "x = 10";
        var expected = new List<IExpressionKind>
        {
            new AssignExpression(
                new Identifier("x"),
                new Number(10)
            )
        };

        var actual = LexAndParse(input);
        TestHelper.AreEqual(expected, actual);
    }
}