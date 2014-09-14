using System;
using System.Runtime.InteropServices;

namespace Invoke
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);




        static void Main(string[] args)
        {
            MessageBox(new IntPtr(0), "Hello World!", "Hello Dialog", 0);
        }
    }
}
