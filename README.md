PrimeStore
==========

Simulator of a prime numbers store.

Users enter the store and ask to buy a number.
If the number asked is a prime number,

1.1. then it's either available for sale

1.2. or was purchased earlier, hence not available for sale.

If the number asked is not a prime number, then it doesn't exist.



Let us assume that the store contains the maximum possible number of primes
that could be represented by a primitive type
(in the following implementation I assume that the largest prime number isn't larger than
Int32's MaxValue, and to actually run this in a reasonable time, I set a MAX_VALUE constant to 100000).


- The store must handle concurrent calls.

- Buying should be fast as possible!

- On opening for business, it should already contain the numbers for sale.

- Users cannot be blocked by shopping for numbers, nor by other buyers.


Example: UserA asks to purchase a number and then UserB arrives and asks to purchase a number,
then UserB won't need to wait until the system's dealt with UserA's transaction.
So when UserA's transaction finishes, a response will be delivered to UserA,
then UserB's transaction will start and once finished a response will be delivered to UserB.
