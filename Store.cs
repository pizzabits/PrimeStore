using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStore
{
    public sealed class Store
    {
        public const Int32 MAX_VALUE = 10000 - 2;
        private static Store _instance;
        private static object syncRoot = new Object();

        private Dictionary<Int32, Boolean> _primes;

        private Store()
        {
            _primes = new Dictionary<int, bool>(MAX_VALUE);

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

        public void BuyNumber(Int32 number, Action<NumberType> callback)
        {
            if (!_primes.ContainsKey(number))
            {
                callback(NumberType.NotExist);
            }
            else
            {
                BuyPrime(number, callback);
            }
        }

        private void BuyPrime(Int32 number, Action<NumberType> callback)
        {
            Boolean bought = false;

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
