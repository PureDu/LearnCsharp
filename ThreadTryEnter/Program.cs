using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTryEnter
{
    class Program
    {
        private  static object staticSyncRoot = new object();

        static void Main(string[] args)
        {
            Monitor.Enter(staticSyncRoot);
            Thread t1Thread = new Thread(f1);
            t1Thread.Start();
            t1Thread.Join();

            Console.ReadLine();
        }

        static void f1()
        {
            if(!Monitor.TryEnter(staticSyncRoot))
                return;
            try
            {
                //...
            }
            finally
            {
                Monitor.Enter(staticSyncRoot);
            }
        }
    }
}
