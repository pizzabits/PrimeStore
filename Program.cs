using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // initialization must be done before users enters the store,
            // since the store implementation is (conveniently defined) lazy-initialization.
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Store.Instance.BuyNumber(0, (numtype) =>
            {
                sw.Stop();
                Console.WriteLine("It took {0} to initialize a store with primes not larger than {1}", sw.Elapsed, Store.MAX_VALUE);
            });

            for (int i = 0; i < 3; i++)
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