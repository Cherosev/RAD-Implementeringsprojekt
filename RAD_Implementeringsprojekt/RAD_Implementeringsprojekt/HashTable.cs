using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace RAD_Implementeringsprojekt {

    public class HashTable 
    {
        public ulong a;
        public ulong b;

        public int l;

        public BigInteger sumHxShift = 0;
        public BigInteger sumHxMod = 0;


        // Constructor
        public HashTable() 
        {
            a = getRandomULong();
            b = getRandomULong();

            Random rnd = new Random();
            l = rnd.Next(1,63);
        }

        public void hashKeyShift(IEnumerable<Tuple<ulong, int>> stream)
        {
            sumHxShift = 0;
            foreach(var x in stream)
            {
                sumHxShift += Hashing.MultiplyShift(x.Item1, a, l);
            }
            //Console.WriteLine($"Sum of hashed vals (key shift): {sumHxShift}");
        }

        public void hashKeyMod(IEnumerable<Tuple<ulong, int>> stream)
        {
            sumHxMod = 0;
            foreach(var x in stream)
            {
                sumHxMod += Hashing.Multiply_mod_prime(x.Item1, a, b, l);
            }
            //Console.WriteLine($"Sum of hashed vals (mod): {sumHxMod}");
        }

        private ulong getRandomULong()
        {
            Random randomGenerator = new Random();
            Byte[] byteArr = new Byte[8];
            randomGenerator.NextBytes(byteArr);
            var bitArr = new BitArray(byteArr);

            // Ensure ULong is uneven
            var ret = BitConverter.ToUInt64(byteArr, 0);
            if( ret % 2 != 1) ret++;
            return ret  ;
        }


    }

}