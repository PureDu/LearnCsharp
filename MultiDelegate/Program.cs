using System;

namespace MultiDelegate
{
    public class Article
    {
        public int m_Price = 0;

        public Article(int price)
        {
            m_Price = price;
        }

        public int IncPrice(int i)
        {
            m_Price += i;
            return m_Price;
        }
    }
    class Program
    {
        public delegate int Deleg(int i);
        static void Main(string[] args)
        {
            Article a = new Article(100);
            Article b = new Article(103);
            Article c = new Article(107);

            Deleg deleg = a.IncPrice;
            deleg += b.IncPrice;
            deleg += c.IncPrice;

            int p1 = deleg(20);


            Console.WriteLine("Prices of articles: a:{0} b:{1} c:{2}",
                a.m_Price, b.m_Price, c.m_Price);

            int p2 = deleg(-10);

            Console.WriteLine("Prices of articles: a:{0} b:{1} c:{2}",
                a.m_Price, b.m_Price, c.m_Price);


            Console.ReadLine();

        }
    }
}
