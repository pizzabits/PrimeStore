PrimeStore
==========

A program that simulates a store for prime numbers.
A user comes in the store and ask to buy a number.
if the number is a prime number, then either its available for sale,
or it isn't available since it was soled before.
if the number isn't prime, then it doesn't exist in the system.

The store will contain as much primes numbers, for example MAX_VALUE = Int32.MaxValue.

Although, this way it would take a long time to calculate, since the method used for generating
prime numbers is the Sieve of Eratosthenes.

This implementation consist of a dictionary of pre-calculated prime numbers,
inside a singleton class.
The store supports concurrent calls, and doesn't delay the buyers (i.e asynchrounous calls).

