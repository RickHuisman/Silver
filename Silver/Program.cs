using Silver.Compiler;
using Silver.VM;

const string source = "10 + 2";
var bytecode = Compiler.Compile(source);

Console.WriteLine(bytecode);

var vm = new VM();
vm.Interpret(new List<Bytecode> {bytecode});