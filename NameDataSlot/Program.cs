using System;
using System.Threading;

namespace NameDataSlot
{
    class Program
    {
        private static readonly int NTHREAD = 3;
        private static readonly int MAXCALL = 3;
        private static readonly int PERIOD = 1000;

        static bool FServer()
        {
            LocalDataStoreSlot dSlot = Thread.GetNamedDataSlot("Counter");
            var counter = (int) Thread.GetData(dSlot);
            counter++;
            Thread.SetData(dSlot, counter);
            return !(counter == MAXCALL);
        }

        static void ThreadProc()
        {
            LocalDataStoreSlot dSlot = Thread.GetNamedDataSlot("Counter");
            Thread.SetData(dSlot, 0);
            do
            {
                Thread.Sleep(PERIOD);
                Console.WriteLine("Thread#{0} I've called fServer(), Counter = {1}",
               Thread.CurrentThread.ManagedThreadId, (int)Thread.GetData(dSlot));
            } while (FServer());

            Console.WriteLine("Thread#{0} bye",
                Thread.CurrentThread.ManagedThreadId);
        }
        static void Main()
        {
            Console.WriteLine("Thread#{0} I'm the main Thread, hello world",
                Thread.CurrentThread.ManagedThreadId);
            Thread.AllocateNamedDataSlot("Counter");

            for (int i = 0; i < NTHREAD; i++)
            {
                Thread thread = new Thread(ThreadProc);
                thread.Start();
            }

            Thread.Sleep(PERIOD*(MAXCALL + 1));
            Thread.FreeNamedDataSlot("Counter");
            Console.WriteLine("Thread#{0} I'm the main Thread, bye.",
                Thread.CurrentThread.ManagedThreadId);

            Console.ReadLine();
        }
    }
}
