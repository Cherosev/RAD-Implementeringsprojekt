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
        public ulong a_value_64;
        public BigInteger a_value;
        public BigInteger b_value;
        public int   l_value;
        public ulong l_squared;
        public BigInteger p_value;
        public int   q_value;

        public BigInteger a_0;
        public BigInteger a_1;
        public BigInteger a_2;
        public BigInteger a_3;

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
                    a_value_64 = getRandomULong();
                    h = MultiplyShift;
                    break;
                case Hashfunctions.Multiply_Mod_Prime:
                    a_value = getRandomBigDigInt();
                    b_value = getRandomBigDigInt();
                    l_squared = (ulong) BigInteger.Pow(2, l);
                    h = Multiply_mod_prime;
                    break;

                case Hashfunctions.fourUniversal:
                    a_0 = getRandomBigDigInt();
                    a_1 = getRandomBigDigInt();
                    a_2 = getRandomBigDigInt();
                    a_3 = getRandomBigDigInt();
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
            if(a_value_64 % 2 != 1 || l_value <= 0 || l_value >= 64) {
                throw new Exception($"Multiply shift error. Args: l={l_value}, a={a_value_64}, x={x}");
            }
            var mult = a_value_64 * x;
            int shift = 64 - l_value;

            return (mult>>shift);
        }

        // a and b are less than p, and l is a positive integer less than 64.
        public ulong Multiply_mod_prime(UInt64 x)
        {
            BigInteger mult = (a_value * x + b_value);
            BigInteger firstMod = (mult & p_value) + (mult >> q_value);
            if (firstMod >= p_value) firstMod -= p_value;
            if (firstMod > p_value) throw new Exception("Dude det her er problemet");
            ulong result = (ulong)(firstMod & (l_squared-1));

            return result;
        }
        
        public ulong fourUniversal(UInt64 x)
        {
            BigInteger y = a_0;
            y = y*x + a_1;                     // Update y
            y = (y&p_value) + (y>>q_value);    // Update y
            y = y*x + a_2;                     // Update y
            y = (y&p_value) + (y>>q_value);    // Update y
            y = y*x + a_3;                     // Update y
            y = (y&p_value) + (y>>q_value);    // Update y
            if (y >= p_value) y -= p_value;

            var result = (ulong)( (y) & ((ulong)BigInteger.Pow(2, l_value)-1));
            return result;
        }

        private ulong getRandomULong()
        {
            Random randomGenerator = new Random();
            Byte[] byteArr = new Byte[8];
            randomGenerator.NextBytes(byteArr);

            var ret = BitConverter.ToUInt64(byteArr, 0);
            // Ensure ULong is uneven            
            return ret | 1;
        }

        public static BigInteger getRandomBigDigInt()
        {
            Random randomGenerator = new Random();
            Byte[] byteArr = new Byte[11];
            randomGenerator.NextBytes(byteArr);
            var ret = new BigInteger(byteArr);

            //If generated bigInt is negative - make it positive!
            if (ret < 0)
            {
                ret = ret * (-1);
            }
            return ret;
        }
    }
}
