using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace RAD_Implementeringsprojekt
{
    class Hashing
    {

        // A is a random odd 64-bit integer, and l is a postive integer lower than 64.
        public static UInt64 MultiplyShift(UInt64 x, UInt64 a, UInt64 b, Int32 l)
        {
            // Bound checks
            if( a % 2 != 1 || l <= 0 || l >= 64) {
                throw new System.Exception($"Multiply shift error. Args: l={l}, a={a}, x={x}");
            }

            var mult = a * x;
            //Console.WriteLine($"A and x multiplied to {mult}");
            int shift = 64 - l;
            //Console.WriteLine($"Shifting set to {shift}");

            return (mult>>shift);
        }

        // a and b are less than p, and l is a positive integer less than 64.
        public static ulong Multiply_mod_prime(UInt64 x, UInt64 a, UInt64 b, Int32 l)
        {
            
            int q = 89;
            ulong p = (ulong) Math.Pow(2, q) - 1;
            ulong mult = (a * x + b);
            ulong firstMod = (mult&p)+(mult>>q);
            if (firstMod >= p) firstMod -= p;

            ulong result = firstMod % (ulong)Math.Pow(2,l);
            
            //FØR 
            //ulong p = (ulong) Math.Pow(2, 89) - 1;
            //ulong mult = (a * x + b);
            //ulong firstMod = mult % p;
            //ulong result = firstMod % (ulong)(2 ^ l);
            



            return result;
        }

    }
}
