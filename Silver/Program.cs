using Silver.Compiler;

const string source = "10 + 2";
var bytecode = Compiler.Compile(source);

Console.WriteLine(bytecode);