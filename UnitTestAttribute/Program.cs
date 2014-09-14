using System;
using System.Reflection;

namespace UnitTestAttribute
{

    [AttributeUsage(AttributeTargets.Method,AllowMultiple = false)]
    public class TestAttribute : Attribute
    {

        public TestAttribute() : this(0)
        {
        }


        public TestAttribute(int nTime)
        {
            Console.WriteLine("TestAttribute.ctor(int)");
            _mNTime = nTime;
        }

        private int _mNTime = 1;
        private bool _mIgnore;
        public bool Ignore 
        {
            get { return _mIgnore; }
            set { _mIgnore = value; }
        }

        public int NTime
        {
            get { return _mNTime; }
            set { _mNTime = value; }
        }
    }
    class Program
    {
        static void Main()
        {
            TestAssembly(Assembly.GetExecutingAssembly());
            Console.ReadLine();
        }


        static void TestAssembly(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                foreach (MethodInfo method in type.GetMethods())
                {
                    object[] attributeObjects = method.GetCustomAttributes(typeof (TestAttribute), false);

                    if (attributeObjects.Length == 1)
                    {
                        TestAttribute testAttribute = attributeObjects[0] as TestAttribute;
                        if (testAttribute != null && !testAttribute.Ignore)
                        {
                            object[] parameters = new object[0];
                            object instatnce = Activator.CreateInstance(type);
                            for (int i = 0; i < testAttribute.NTime; i++)
                            {
                                try
                                {
                                    method.Invoke(instatnce, parameters);
                                }
                                catch (TargetInvocationException ex)
                                {
                                    Console.WriteLine("The method {" + type.FullName + "." +
                                                      method.Name +
                                                      "} threw an exception of type " + ex.InnerException.GetType() +
                                                      " during run #" + (i + 1) + ".");
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    class  Foo
    {
        [Test]
        public void Crash()
        {
            Console.WriteLine("Crash()");
            throw new AppDomainUnloadedException();
        }

        private int _state;

        [Test(4)]
        public void CrashTheSecondTime()
        {
            Console.WriteLine("CrashTheSecondTime()");
            _state++;
            if (_state == 2)
                throw new ApplicationException();
        }

        [Test]
        public void DontCrash()
        {
            Console.WriteLine("DontCrash");
        }

        [Test(Ignore = true)]
        public void CrashButIgnored()
        {
            Console.WriteLine("CrashButIgnored()");
            throw new ApplicationException();
        }

    }
}
