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

    private static IExpressionKind ExpressionStatement()
    {
        return Expression();
    }

    public static AssignExpression ParseAssign(Token token, IExpressionKind left)
    {
        var value = ExpressionStatement();
        return new AssignExpression(left, value);
    }

    private static IExpressionKind Expression()
    {
        return ParsePrecedence(Precedence.None + 1);
    }

    private static IExpressionKind ParsePrecedence(Precedence precedence)
    {
        var token = Next();
        var prefixRule = ParserRules.GetRule(token.Type).Prefix;
        if (prefixRule == null) throw new Exception("Expected expression");

        var expr = prefixRule(token);

        while (precedence <= ParserRules.GetRule(PeekType()).Precedence)
        {
            token = Next();
            var infixRule = ParserRules.GetRule(token.Type).Infix;
            if (infixRule == null) throw new Exception("Expected expression");
            return infixRule(token, expr);
        }

        return expr;
    }

    public static IExpressionKind Number(Token token)
    {
        var number = Convert.ToDouble(token.Source);
        return new Number(number);
    }

    public static IExpressionKind Identifier(Token token)
    {
        return new Identifier(token.Source);
    }

    public static IExpressionKind Binary(Token token, IExpressionKind lhs)
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

        return new BinaryExpression(op, lhs, rhs);
    }

    public static IExpressionKind Unary(Token token)
    {
        var operatorType = token.Type;
        var expr = Expression();
        var op = operatorType switch
        {
            TokenType.Minus => UnaryOperator.Negate,
            TokenType.Bang => UnaryOperator.Not,
            _ => throw new Exception() // TODO
        };

        return new UnaryExpression(op, expr);
    }
}