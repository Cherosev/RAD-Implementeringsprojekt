using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace RAD_Implementeringsprojekt
{
    public enum Hashfunctions
    {
        MultiplyShift,
        Multiply_Mod_Prime,
        fourUniversal,
        Count_Sketch_h,
        Count_Sketch_s
    }

    class Hashing
    {

        public ulong a_value;
        public ulong b_value;
        public int   l_value;
        public ulong l_squared;
        public ulong p_value;
        public int   q_value;
        public int   k = 4;

        public ulong a_0;
        public ulong a_1;
        public ulong a_2;
        public ulong a_3;

        public ulong a;
        public ulong b;

        public delegate ulong hashFunctionDelegate(UInt64 x);
        public hashFunctionDelegate h;


        // Constructor. Takes an argument that defines which hashfunction to use, and the size of the keys.
        // Sets the propper variables accordingly.
        public Hashing(Hashfunctions hashFun, int l)
        {   
            l_value = l;
            q_value = 89;
            p_value = (ulong) Math.Pow(2, q_value) - 1;
            switch (hashFun)
            {
                case Hashfunctions.MultiplyShift:
                    a_value = getRandomULong();
                    h = MultiplyShift;
                    break;
                case Hashfunctions.Multiply_Mod_Prime:
                    a_value = getRandomULong();
                    b_value = getRandomULong();
                    l_squared = (ulong)Math.Pow(2, l);
                    h = Multiply_mod_prime;
                    break;

                case Hashfunctions.fourUniversal:
                    Random rnd = new Random();
                    a_0 = getRandomULong();
                    a_1 = getRandomULong();
                    a_2 = getRandomULong();
                    a_3 = getRandomULong();
                    h = fourUniversal; // DENNE HER LINJE FIXER LORTET
                    break;
                default:
                    throw new Exception("Fuck noget lort");
            }
        }

        // A is a random odd 64-bit integer, and l is a postive integer lower than 64.
        public UInt64 MultiplyShift(UInt64 x)
        {
            // Bound checks
            if( a_value % 2 != 1 || l_value <= 0 || l_value >= 64) {
                throw new Exception($"Multiply shift error. Args: l={l_value}, a={a_value}, x={x}");
            }

            var mult = a_value * x;
            //Console.WriteLine($"A and x multiplied to {mult}");
            int shift = 64 - l_value;
            //Console.WriteLine($"Shifting set to {shift}");

            return (mult>>shift);
        }

        // a and b are less than p, and l is a positive integer less than 64.
        public ulong Multiply_mod_prime(UInt64 x)
        {
            
            ulong mult = (a_value * x + b_value);
            ulong firstMod = (mult&p_value)+(mult>>q_value);
            if (firstMod >= p_value) firstMod -= p_value;

            ulong result = firstMod % l_squared;
            return result;
        }
        
        public ulong fourUniversal(UInt64 x)
        {
            ulong y = a_0;
            y = y*x + a_1;                     // Update y
            y = (y&p_value) + (y>>l_value);    // Update y
            y = y*x + a_2;                     // Update y
            y = (y&p_value) + (y>>l_value);    // Update y
            y = y*x + a_3;                     // Update y
            y = (y&p_value) + (y>>l_value);    // Update y

            if (y >= p_value) y -= p_value;
            //BigInteger y = new BigInteger(p_value);
            //BigInteger sum = a_0 + a_1 * x + a_2 * (BigInteger)(Math.Pow(x,2)) + a_3 * (BigInteger)(Math.Pow(x,3));
            var result = (ulong)(y) % (ulong)(Math.Pow(2, l_value)-1);
            return result;
        }

        public ulong Count_Sketch(UInt64 x)
        {



            return 0;
        }

        private ulong getRandomULong()
        {
            Random randomGenerator = new Random();
            Byte[] byteArr = new Byte[8];
            randomGenerator.NextBytes(byteArr);

            // Ensure ULong is uneven
            var ret = BitConverter.ToUInt64(byteArr, 0);
            if( ret % 2 != 1) ret++;
            return ret;
        }
    }
}
