using System;
using System.Linq;
using System.Reflection;

namespace ReflectionAppdomain
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                Console.WriteLine("Class: " + type);
                foreach (var method in type.GetMethods())
                {
                    Console.WriteLine(" Method: " + method);
                    foreach (var param in method.GetParameters())
                    {
                        Console.WriteLine("     Param: " + param.GetType());
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
