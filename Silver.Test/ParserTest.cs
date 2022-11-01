using System.Collections.Generic;
using NUnit.Framework;
using Tests;

namespace Silver.Test;

public class ParserTest
{
    [Test]
    public void Parse_Binary_ReturnsExpression()
    {
        const string input = "5 + 10";
        var expected = new List<IExpressionKind>
        {
            new ExpressionStatement(
                new BinaryExpression(
                    BinaryOperator.Plus,
                    new Number(5),
                    new Number(10)
                )
            )
        };

        var actual = Parser.Parse(input);
        TestHelper.AreEqual(expected, actual);
    }

    [Test]
    public void Parse_Unary_ReturnsExpression()
    {
        const string input = "-5";
        var expected = new List<IExpressionKind>
        {
            new ExpressionStatement(
                new UnaryExpression(
                    UnaryOperator.Negate,
                    new Number(5)
                )
            )
        };

        var actual = Parser.Parse(input);
        TestHelper.AreEqual(expected, actual);
    }

    // [Test]
    // public void Parse_SetVar_ReturnsSetVarExpression()
    // {
    //     const string input = "x = 10";
    //     var expected = new List<IExpressionKind>
    //     {
    //         new VarStatement(
    //             new Variable("x"),
    //             new Expression(new Number(10))
    //         )
    //     };
    //
    //     var actual = Parser.Parse(input);
    //
    //     TestHelper.AreEqual(expected, actual);
    // }
}