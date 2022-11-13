using System;
using System.Collections.Generic;
using NUnit.Framework;
using Silver.Compiler;

namespace Silver.Test;

public class VMTest
{
    [Test]
    public void VM_Numbers()
    {
        const string input = "2 + 5";
        var expected = new IRObjectInt(7);
        Run(input, expected);
    }
    
    [Test]
    public void VM_Locals()
    {
        const string input = @"
x = 2
y = 3
x + y
";
        var expected = new IRObjectInt(5);
        Run(input, expected);
    }

    [Test]
    public void VM_Def()
    {
        const string input = @"
def foobar()
  5
end

foobar()
";
        var expected = new IRObjectInt(5);
        Run(input, expected);
    }

    private static void Run(string input, IRObjectInt expected)
    {
        var bytecode = Compiler.Compiler.Compile(input);
        
        Console.WriteLine(bytecode);

        var vm = new VM.VM();
        vm.Interpret(new List<Bytecode> {bytecode});

        var value = vm.TopValue();
        TestHelper.AreEqual(value, expected);
    }
}