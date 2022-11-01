namespace Silver.Syntax;

public enum Precedence
{
    None,
    Term, // + -
    Factor, // * / %
    Assign, // =
    Call, // foo()
}