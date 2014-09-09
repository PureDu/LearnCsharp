using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;

[Synchronization(SynchronizationAttribute.REQUIRES_NEW, true)]
public class Foo1 : ContextBoundObject
{
    public void DisplayThreadId()
    {
        Console.WriteLine("Foo1 Begin: managedThreadId = " + Thread.CurrentThread.ManagedThreadId);
        Thread.Sleep(1000);
        var objFoo2 = new Foo2();
        objFoo2.DisplayThreadId();
        Console.WriteLine("Foo1 End: MannagedThreadId = " + Thread.CurrentThread.ManagedThreadId);
    }
}

[Synchronization(SynchronizationAttribute.REQUIRED)]
public class Foo2 : ContextBoundObject
{
    public void DisplayThreadId()
    {
        Console.WriteLine("Foo3 Begin: ManagedThreadId = " + Thread.CurrentThread.ManagedThreadId);
        Thread.Sleep(1000);
        var objFoo3 = new Foo3();
        objFoo3.DisplayThreadId();
        Console.WriteLine("Foo2 End: ManagedThreadId = " + Thread.CurrentThread.ManagedThreadId);
    }
}


[Synchronization(SynchronizationAttribute.NOT_SUPPORTED)]
public class Foo3 : ContextBoundObject
{
    public void DisplayThreadId()
    {
        Console.WriteLine("Foo3 Begin: MannagedThreadId = " + Thread.CurrentThread.ManagedThreadId);
        Thread.Sleep(1000);
        Console.WriteLine("Foo3 End: ManagedThreadId = " + Thread.CurrentThread.ManagedThreadId);
    }
}

public class Program
{
    private static readonly Foo1 m_Object = new Foo1();

    private static void Main()
    {
        var t1Thread = new Thread(ThreadProc);
        var t2Thread = new Thread(ThreadProc);
        t1Thread.Start();
        t2Thread.Start();
        t1Thread.Join();
        t2Thread.Join();

        Console.ReadLine();
    }

    private static void ThreadProc()
    {
        m_Object.DisplayThreadId();
    }
}