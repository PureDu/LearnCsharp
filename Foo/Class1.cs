using System;

namespace NMFoo
{
    public class Calc
    {
        public Calc()
        {
            Console.WriteLine("Calc.Constructor called!");
        }

        public int Sum(int a, int b)
        {
            Console.WriteLine("Method Calc.Sum() Called!");
            return a + b;
        }
    }
}