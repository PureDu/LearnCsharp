using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncWriteSum
{
    class Program
    {
        public delegate int Deleg(int a, int b);

        static int WriteSum(int a, int b)
        {
            var sum = a + b;
            Console.WriteLine("Thread#{0}: WriteSum() sum = {1}",
                Thread.CurrentThread.ManagedThreadId, sum);
            return sum;
        }
        static void Main(string[] args)
        {
            Deleg procDeleg = WriteSum;
            var asyncResult = procDeleg.BeginInvoke(10, 10, null, null);
            var sum = procDeleg.EndInvoke(asyncResult);
            Console.WriteLine("Thread#{0}: Main() sum = {1}",
                Thread.CurrentThread.ManagedThreadId, sum);

            Console.ReadLine();
        }
    }
}
