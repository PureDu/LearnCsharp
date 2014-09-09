using System;
using System.Threading;

namespace ThreadReaderWRiterLock
{
    class Program
    {
        private static int theResource = 0;
        static  ReaderWriterLock rwl = new ReaderWriterLock();
        static void Main(string[] args)
        {
            Thread tr0Thread = new Thread(ThreadReader);
            Thread tr1Thread = new Thread(ThreadReader);
            Thread twThread = new Thread(ThreadWriter);

            tr0Thread.Start();
            tr1Thread.Start();
            twThread.Start();
            tr0Thread.Join();
            tr1Thread.Start();
            twThread.Join();
            twThread.Join();
            Console.ReadLine();
        }

        static void ThreadReader()
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    rwl.AcquireReaderLock(1000);
                    Console.WriteLine("Begin REad theResource = {0}", theResource);
                    Thread.Sleep(10);
                    Console.WriteLine("End Read theResource = {0}", theResource);
                    rwl.ReleaseReaderLock();
                }
                catch (ApplicationException)
                {
                    
                }
            }
        }

        static void ThreadWriter()
        {
            try
            {
            for (int i = 0; i < 3; i++)
            {
                rwl.AcquireWriterLock(1000);
                Console.WriteLine("Begin Write theResource = {0}", theResource);
                Thread.Sleep(100);
                theResource++;
                Console.WriteLine("End Write theResource = {0}", theResource);
                rwl.ReleaseWriterLock();
            }

            }
            catch (Exception)
            {
                
            }
        }

    }
}
