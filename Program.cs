using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                Parallel.For(0, Store.MAX_VALUE, (number) =>
                    {
                        Store.Instance.BuyNumber(number, (numberType) =>
                            {
                                switch (numberType)
                                {
                                    case NumberType.NotAvailable:
                                        Console.WriteLine("{0} is not available for sale.", number);
                                        break;
                                    case NumberType.NotExist:
                                        Console.WriteLine("{0} is not a prime number.", number);
                                        break;
                                    case NumberType.SoldSuccessfully:
                                        Console.WriteLine("Successfully bought {0}.", number);
                                        break;
                                }
                            });
                    });
            }
        }
    }
}
