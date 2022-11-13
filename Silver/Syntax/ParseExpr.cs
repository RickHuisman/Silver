using Silver.Syntax.Ast;

namespace Silver.Syntax;

public partial class Parser
{
    private static IExpressionKind ParseExpression()
    {
        switch (PeekType())
        {
            default:
                return Expression();
        }
    }

    public static AssignExpression ParseAssign(Token token, IExpressionKind left)
    {
        var value = Expression();
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

    public static IExpressionKind Binary(Token token, IExpressionKind left)
    {
        var rule = ParserRules.GetRule(token.Type);
        var right = ParsePrecedence(rule.Precedence + 1);
        return new BinaryExpression(left, token.Source, right);
    }

    public static IExpressionKind Unary(Token token)
    {
        var value = Expression();
        return new UnaryExpression(token.Source, value);
    }
}