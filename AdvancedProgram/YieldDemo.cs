using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgram
{
    public class YieldDemo
    {
        public void GetGenerators()
        {
            Console.WriteLine("开始");
          //var primeNumbers=  Generators.GetPrimeNumbers().Take(10);//Take则是取循环数据，可以理解执行yield几次
            var primeNumbers = Generators.GetPrimeNumbers();
            var iterator = primeNumbers.GetEnumerator();
            //或者通过for循环。
            for (int i = 0; i < 10; i++)
            {
                if (iterator.MoveNext())
                {
                    Console.WriteLine(iterator.Current);
                }
                else {
                    break;
               }
            }
            foreach (var item in primeNumbers)//yield一次则执行一次
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("结束");
        }
    }
    public class Generators
    {
        public static IEnumerable<int> GetPrimeNumbers()
        {
            int counter = 1;
            while (true)
            {
                if (IsPrimeNumbers(counter))
                {
                    yield return counter; //返回，但不会终止
                }
                counter++;
            }
        }
        public static bool IsPrimeNumbers(int value)
        {
            bool output = true;
            for (int i = 2; i < value/2; i++)
            {
                if (value % i == 0)
                {
                    output = false;
                    break;
                }
            }
            return output;
        }
    }
}
