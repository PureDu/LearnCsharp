using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace AssemblyCreate
{

    public interface IPolynome
    {
        int Eval(int x);
    }


    class Polynome
    {
// ReSharper disable once InconsistentNaming
        public IPolynome polynome;

        public Polynome(int[] coefs)
        {
            Assembly asm = BuildCodeInternal(coefs);
            polynome = (IPolynome)asm.CreateInstance("PloynomeInternal");
        }

        private Assembly BuildCodeInternal(int[] coefs)
        {
            AssemblyName asmName = new AssemblyName();
            asmName.Name = "EvalPolAsm";

            AssemblyBuilder asmBuilder = Thread.GetDomain().DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);


            ModuleBuilder modBuilder = asmBuilder.DefineDynamicModule("MainMod");

            TypeBuilder typeBuilder = modBuilder.DefineType("PloynomeInternal", TypeAttributes.Public);
            typeBuilder.AddInterfaceImplementation(typeof (IPolynome));

            MethodBuilder methodBuilder = typeBuilder.DefineMethod("Eval",
                MethodAttributes.Public | MethodAttributes.Virtual,
                typeof (int),
                new Type[] {typeof (int)});


            ILGenerator ilGen = methodBuilder.GetILGenerator();
            int deg = coefs.GetLength(0);
            for (int i = 0; i < deg - 1; i++)
            {
                ilGen.Emit(OpCodes.Ldc_I4, coefs[i]);
                ilGen.Emit(OpCodes.Ldarg, 1);
            }
            ilGen.Emit(OpCodes.Ldc_I4, coefs[deg - 1]);
            for (int i = 0; i < deg - 1; i++)
            {
                ilGen.Emit(OpCodes.Mul);
                ilGen.Emit(OpCodes.Add);
            }
            ilGen.Emit(OpCodes.Ret);


            MethodInfo methodInfo = typeof (IPolynome).GetMethod("Eval");
            typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
            typeBuilder.CreateType();
            return asmBuilder;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            int[] coefs = {30139, -13735, 83, 66};
            Polynome p = new Polynome(coefs);

            for (int x = -26; x <= 19; x++)
            {
                for (int i = 0; i < 10000000; i++)
                {
                    p.polynome.Eval(x);
                }
            }

            Console.WriteLine("Duration: " + sw.Elapsed);
            Console.ReadLine();
        }
    }
}
