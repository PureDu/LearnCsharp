using System;
using System.Threading;

namespace ThreadInterlocked
{
    class Program
    {
        static void Main()
        {
            Thread t1 = new Thread(F1);
            Thread t2 = new Thread(f2);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            Console.ReadLine();
        }

        private static long _counter = 1;

        static void F1()
        {
            for (int i = 0; i < 5; i++)
            {
                Monitor.Enter(typeof (Program));
                try
                {
                    _counter *= _counter;
                }
                finally
                {
                    Monitor.Enter(typeof (Program));
                }
                Console.WriteLine("counter^2 {0}", _counter);
                Thread.Sleep(10);
            }
        }

        static void f2()
        {
            for (int i = 0; i < 5; i++)
            {
                Monitor.Enter(typeof (Program));
                try
                {
                    _counter *= 2;
                }
                finally
                {
                    Monitor.Exit(typeof (Program));
                }
                Console.WriteLine("counter*2{0}", _counter);
                Thread.Sleep(10);
            }
        }
    }
}
