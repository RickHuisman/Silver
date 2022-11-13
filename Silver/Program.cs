using Silver.Compiler;
using Silver.VM;

const string source = @"
x = 2
y = 3
x + y
";
var bytecode = Compiler.Compile(source);

Console.WriteLine(bytecode);

var vm = new VM();
vm.Interpret(new List<Bytecode> {bytecode});