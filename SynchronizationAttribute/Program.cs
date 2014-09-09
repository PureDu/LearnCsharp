using System;
using  System.Threading;
using  System.Runtime.Remoting.Contexts;
namespace SynchronizationAttribute
{
    class Program
    {
        static FOO m_Object = new FOO();
        static void Main(string[] args)
        {
            Thread t0 = new Thread(ThreadProc);
            Thread t1 = new Thread(ThreadProc);
            t0.Start();
            t1.Start();
            t0.Join();
            t1.Join();
            Console.ReadLine();
        }

        static void ThreadProc()
        {
            for (int i = 0; i < 2; i++)
            {
                m_Object.DisplayThreadId();
            }
        }
    }



    [System.Runtime.Remoting.Contexts.Synchronization(System.Runtime.Remoting.Contexts.SynchronizationAttribute.REQUIRED)]
    internal class FOO : ContextBoundObject
    {
        public void DisplayThreadId()
        {
            Console.WriteLine("Begin: ManagedThreadId = " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
            Console.WriteLine("End: ManagedThreadId = " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
