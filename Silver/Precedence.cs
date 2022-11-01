namespace Silver;

public enum Precedence
{
    None,
    Term,    // + -
    Factor,  // * / %
    Assign,  // =
    Call,    // foo()
}