using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RAD_Implementeringsprojekt
{
    public enum Hashfunctions
    {
        MultiplyShift,
        Multiply_Mod_Prime,
        fourUniversal
    }

    class Hashing
    {

        public ulong a_value;
        public ulong b_value;
        public int   l_value;
        public ulong l_squared;
        public BigInteger p_value;
        public int   q_value;
        public int   k = 4;

        public BigInteger a_0;
        public BigInteger a_1;
        public BigInteger a_2;
        public BigInteger a_3;

        public ulong a;
        public ulong b;

        public delegate ulong hashFunctionDelegate(ulong x);
        public hashFunctionDelegate h;


        // Constructor. Takes an argument that defines which hashfunction to use, and the size of the keys.
        // Sets the propper variables accordingly.
        public Hashing(Hashfunctions hashFun, int l)
        {   
            l_value = l;
            q_value = 89;
            p_value = BigInteger.Pow(2, q_value) - 1;
            switch (hashFun)
            {
                case Hashfunctions.MultiplyShift:
                    a_value = getRandomULong();
                    h = MultiplyShift;
                    break;
                case Hashfunctions.Multiply_Mod_Prime:
                    a_value = getRandomULong();
                    b_value = getRandomULong();
                    l_squared = (ulong) BigInteger.Pow(2, l);
                    h = Multiply_mod_prime;
                    break;

                case Hashfunctions.fourUniversal:
                    a_0 = getRandomBigDigInt();
                    a_1 = getRandomBigDigInt();
                    a_2 = getRandomBigDigInt();
                    a_3 = getRandomBigDigInt();
                    //l_squared = (ulong)BigInteger.Pow(2, l); // l is < 64
                    h = fourUniversal; 
                    break;
                default:
                    throw new Exception($"Function {hashFun} not yet impemented");
            }
        }

        // A is a random odd 64-bit integer, and l is a postive integer lower than 64.
        public ulong MultiplyShift(UInt64 x)
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
            BigInteger mult = (a_value * x + b_value);
            BigInteger firstMod = (mult & p_value) + (mult >> q_value);
            if (firstMod >= p_value) firstMod -= p_value;

            ulong result = (ulong)(firstMod & (l_squared-1));

            return result;
        }
        
        public ulong fourUniversal(UInt64 x)
        {
            BigInteger y = a_0;
            y = y*x + a_1;                     // Update y
            y = (y&p_value) + (y>>l_value);    // Update y
            y = y*x + a_2;                     // Update y
            y = (y&p_value) + (y>>l_value);    // Update y
            y = y*x + a_3;                     // Update y
            y = (y&p_value) + (y>>l_value);    // Update y

            if (y >= p_value) y -= p_value;
            //BigInteger y = new BigInteger(p_value);
            //BigInteger sum = a_0 + a_1 * x + a_2 * (BigInteger)(Math.Pow(x,2)) + a_3 * (BigInteger)(Math.Pow(x,3));
            var result = (ulong)((y) & ((ulong)BigInteger.Pow(2, l_value) - 1));
            return result;
        }

        private ulong getRandomULong()
        {
            Random randomGenerator = new Random();
            Byte[] byteArr = new Byte[8];
            randomGenerator.NextBytes(byteArr);

            // Ensure ULong is uneven
            var ret = BitConverter.ToUInt64(byteArr, 0);
            return ret | 1;
        }

        public static BigInteger getRandomBigDigInt()
        {
            Random randomGenerator = new Random();
            Byte[] byteArr = new Byte[12];
            Byte one = 1;
            byteArr[11] = (byte)(byteArr[11] & one);
            randomGenerator.NextBytes(byteArr);
            return new BigInteger(byteArr);
        }
    }
}
