using System;
using System.Reflection;

namespace Type_InvokeMember
{
    class Program
    {
        static void Main(string[] args)
        {
            object obj = AppDomain.CurrentDomain.CreateInstanceAndUnwrap("Foo", 
                "NMFoo.Calc");
            Type type = obj.GetType();


            object[] parameters = new object[2];
            parameters[0] = 7;
            parameters[1] = 8;
            int result = (int) type.InvokeMember("Sum", BindingFlags.InvokeMethod,
                null, obj, parameters);

            Console.WriteLine("7+8= {0}", result);

            Console.ReadLine();

        }
    }
}
