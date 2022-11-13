using NUnit.Framework;
using Silver.VM;

namespace Silver.Test;

public class StackTest
{
    [Test]
    public void Stack_Test()
    {
        var stack = new Stack<int>();
        
        stack.Push(2);
        stack.Push(3);
        
        Assert.AreEqual(3, stack.First());
        
        stack.Push(4);
        stack.Push(5);
        stack.Pop();
        var pop = stack.Pop();
        
        Assert.AreEqual(4, pop);
        
        stack.Push(6);
        
        Assert.AreEqual(6, stack.First());

        stack[0] = 10;
        Assert.AreEqual(stack[0], 10);
    }
}