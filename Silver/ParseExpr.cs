namespace Silver;

public partial class Parser
{
    private static IExpressionKind ParseExpression()
    {
        switch (PeekType())
        {
            default:
                return ExpressionStatement();
        }
    }

    private static ExpressionStatement ExpressionStatement()
    {
        var expr = ParsePrecedence(Precedence.Assignment);
        return new ExpressionStatement(expr);
    }

    private static Expression ParsePrecedence(Precedence precedence)
    {
        var lhsToken = Next();
        var prefixRule = ParserRules.GetRule(lhsToken.Type).Prefix; // TODO Previous ???

        if (prefixRule == null)
        {
            throw new Exception("Expected expression");
        }

        var expr = prefixRule(lhsToken);

        while (precedence < ParserRules.GetRule(PeekType()).Precedence)
        {
            var rhsToken = Next();
            var infixRule = ParserRules.GetRule(rhsToken.Type).Infix;
            var infixExpr = infixRule(rhsToken);

            switch (infixExpr)
            {
                case BinaryExpression binary2:
                    binary2.Lhs = new Expression(expr);
                    expr = binary2;
                    break;
                default:
                    throw new Exception("TODO");
            }
        }

        return new Expression(expr);
    }

    public static IExpressionKind Number(Token token)
    {
        var number = Convert.ToDouble(token.Source);
        return new Number(number);
    }

    public static IExpressionKind Binary(Token token)
    {
        var operatorType = token.Type;

        var rule = ParserRules.GetRule(operatorType);
        var rhs = ParsePrecedence(rule.Precedence + 1);

        var op = operatorType switch
        {
            TokenType.BangEqual => BinaryOperator.BangEqual,
            TokenType.EqualEqual => BinaryOperator.Equal,
            TokenType.GreaterThan => BinaryOperator.GreaterThan,
            TokenType.GreaterThanEqual => BinaryOperator.GreaterThanEqual,
            TokenType.LessThan => BinaryOperator.LessThan,
            TokenType.LessThanEqual => BinaryOperator.LessThanEqual,
            TokenType.Plus => BinaryOperator.Plus,
            TokenType.Minus => BinaryOperator.Minus,
            TokenType.Star => BinaryOperator.Multiply,
            TokenType.Slash => BinaryOperator.Divide,
            _ => throw new Exception() // TODO
        };

        return new BinaryExpression(op, null, rhs);
    }
}