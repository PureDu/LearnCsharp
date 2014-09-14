using System;
using System.Runtime.InteropServices;
using System.Text;

namespace P_Invoke
{
    class Program
    {

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetCommandLine();
        static void Main(string[] args)
        {
            IntPtr ptr = GetCommandLine();
            string sTmp = Marshal.PtrToStringAuto(ptr);

            Console.WriteLine(sTmp);

            Console.ReadLine();
        }
    }
}
