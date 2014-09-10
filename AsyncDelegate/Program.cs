using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace AsyncDelegate
{
    class Program
    {
        public delegate int Deleg(int a, int b);

        private static AutoResetEvent ev = new AutoResetEvent(false);
        static void Main()
        {
            Deleg proDeleg = WriteSum;

            proDeleg.BeginInvoke(10, 10, SumDone, null);
            Console.WriteLine("Thread#{0}:BeginInvoke() called! wait for SumDone() completion.",
                Thread.CurrentThread.ManagedThreadId);
            ev.WaitOne();
            Console.WriteLine("{0}: Bye...", Thread.CurrentThread.ManagedThreadId);

            Console.ReadLine();
        }


        static int WriteSum(int a, int b)
        {
            Console.WriteLine(" Thread#{0}: Sum = {1}",
                Thread.CurrentThread.ManagedThreadId, a + b);
            return a + b;
        }

        static void SumDone(IAsyncResult asyncResult)
        {
            Thread.Sleep(1000);
            Deleg proDeleg = ((AsyncResult) asyncResult).AsyncDelegate as Deleg;
            if (proDeleg != null)
            {
                int sum = proDeleg.EndInvoke(asyncResult);
                Console.WriteLine("Thread#{0}: Callback method sum = {1}",
                    Thread.CurrentThread.ManagedThreadId, sum);
            }
            ev.Set();
        }
    }
}
