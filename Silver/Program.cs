using Silver.Syntax;

const string source = "10 + 2";
var tokens = Lexer.Lex(source);

foreach (var t in tokens)
{
    Console.WriteLine(t);
}