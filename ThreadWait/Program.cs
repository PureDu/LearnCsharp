using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadWait
{
    class Program
    {
        static  object ball = new object();
        static void Main(string[] args)
        {
            Thread threadPing = new Thread(ThreadPingProc);
            Thread threadPong = new Thread(ThreadPongProc);
            threadPing.Start();
            threadPong.Start();
            threadPing.Join();
            threadPong.Join();

            Console.ReadLine();
        }


        static void ThreadPongProc()
        {
            Console.WriteLine("ThreadPong: Hello!");
            lock (ball)
            {
               for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("ThreadPong: Pong");
                    Monitor.Pulse(ball);
                    Monitor.Wait(ball);
                }
            }
            Console.WriteLine("ThreadPong: Bye!");
        }

        static void ThreadPingProc()
        {
            Console.WriteLine("ThreadPing: Hello!");
            lock (ball)
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("ThreadPing: Ping");
                    Monitor.Pulse(ball);
                    Monitor.Wait(ball);
                }
            }
        }
    }
}
