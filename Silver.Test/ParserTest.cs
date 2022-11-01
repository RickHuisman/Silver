using System.Collections.Generic;
using NUnit.Framework;
using Tests;

namespace Silver.Test;

public class ParserTest
{
    [Test]
    public void Parse_NumberExpr_ReturnsAst()
    {
        const string input = "5 + 10";
        var expected = new List<IExpressionKind>
        {
            new ExpressionStatement(
                new Expression(
                    new BinaryExpression(
                        BinaryOperator.Plus,
                        new Expression(new Number(5)),
                        new Expression(new Number(10)))
                )
            )
        };

        var actual = Parser.Parse(input);

        TestHelper.AreEqual(expected, actual);
    }
}