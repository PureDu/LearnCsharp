using System;
using System.Threading;

namespace ThreadStatic
{
    class Program
    {
        [ThreadStatic] 
// ReSharper disable once ThreadStaticFieldHasInitializer
        private static string _str = "Initial value ";

        static void DisplayStr()
        {
            Console.WriteLine("Thread#{0} str={1}",
                Thread.CurrentThread.ManagedThreadId, _str);
        }


        static void ThreadProc()
        {
            DisplayStr();
            _str = "ThreadProc value";
            DisplayStr();
        }
        static void Main()
        {
            DisplayStr();
            Thread thread = new Thread(ThreadProc);
            thread.Start();
            thread.Join();
            DisplayStr();
        }
    }
}
