using System;
using System.Threading;

namespace Threadpool
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ThreadPool.RegisterWaitForSingleObject(
                new AutoResetEvent(false),
                ThreadTaskWait,
                null,
                2000,
                false);


            for (int count = 0; count < 3; count++)
            {
                ThreadPool.QueueUserWorkItem(ThreadTask, count);
            }

            Thread.Sleep(12000);
        }


        private static void ThreadTask(object obj)
        {
            Console.WriteLine("Thread#{0} Task#{1} Begin", Thread.CurrentThread.ManagedThreadId,
                obj.ToString());
            Thread.Sleep(5000);
            Console.WriteLine("Thread#{0} Task#{1} End",
                Thread.CurrentThread.ManagedThreadId,obj.ToString());
        }

        private static void ThreadTaskWait(object obj, bool signaled)
        {
            Console.WriteLine("Thread#{0} TaskWait",
                Thread.CurrentThread.ManagedThreadId);
        }
    }
}