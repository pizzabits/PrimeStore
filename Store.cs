using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeStore
{
    public sealed class Store
    {
        public const Int32 MAX_VALUE = 100000 - 2;
        private static Store _instance;
        private static object syncRoot = new Object();

        private readonly Dictionary<Int32, Boolean> _primes;

        private Store()
        {
            _primes = new Dictionary<int, bool>();

            IEnumerable<int> numbers = Enumerable.Range(2, MAX_VALUE);

            var parallelQuery =
              from n in numbers.AsParallel()
              where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
              select n;

            foreach (var number in parallelQuery)
            {
                _primes.Add(number, false);
            }
        }

        public static Store Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new Store();
                    }
                }

                return _instance;
            }
        }

        // The delegate must have the same signature as the method 
        // it will call asynchronously. 
        private delegate void AsyncMethodCaller(Int32 number, Action<NumberType> callback);

        public void BuyNumber(Int32 number, Action<NumberType> callback)
        {
            // no lock is needed here since just checking if a number is prime
            // and its a readonly operation.
            if (!_primes.ContainsKey(number))
            {
                callback(NumberType.NotExist);
            }
            else
            {
                // Create the delegate.
                AsyncMethodCaller caller = new AsyncMethodCaller(BuyPrime);

                // Initiate the asychronous call.
                caller.BeginInvoke(number, callback, null, null);
            }
        }

        private void BuyPrime(Int32 number, Action<NumberType> callback)
        {
            Boolean bought = false;

            // the number is prime, then obtain exclusive access
            // to the dictionary and try to buy it.
            lock (_primes)
            {
                if (_primes[number] == false)
                {
                    _primes[number] = true;
                    bought = true;
                }
            }

            if (bought)
            {
                callback(NumberType.SoldSuccessfully);
            }
            else
            {
                callback(NumberType.NotAvailable);
            }
        }
    }
}
