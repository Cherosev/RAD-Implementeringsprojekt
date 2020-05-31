using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace RAD_Implementeringsprojekt
{
    class Hashing
    {

        // A is a random odd 64-bit integer, and l is a postive integer lower than 64.
        public static UInt64 MultiplyShift(UInt64 x, UInt64 a, Int32 l)
        {
            // Bound checks
            if( a % 2 != 1 || l <= 0 || l >= 64) {
                Console.WriteLine("Multiply shift error");
                return (UInt64) 0;
            }

            var mult = a * x;
            int shift = 64 - l;

            return (mult>>shift);
        }

        // a and b are less than p, and l is a positive integer less than 64.
        public static BigInteger Multiply_mod_prime(UInt64 x, UInt64 a, UInt64 b, UInt64 l)
        {
            // TODO: Ikke inspireret af Exercise 2.7 og 2.8 fra hasingnoterne. Kan sikker optimeres.
            BigInteger p = (2 ^ 89) - 1;
            BigInteger mult = (a * x + b);
            BigInteger firstMod = mult % p;
            BigInteger result = firstMod % (2 ^ l);
            
            return result;
        }

    }
}
