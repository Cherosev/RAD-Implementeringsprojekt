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

        public delegate ulong hashFunctionDelegate(UInt64 x, UInt64 a, UInt64 b, Int32 l);


        public hashFunctionDelegate hashFunction;
        public List<(ulong, int)>[] hashTable;

        // Constructor
        public HashTable(hashFunctionDelegate h, int hashSize) 
        {
            a = getRandomULong();
            b = getRandomULong();
            hashFunction = h;
            l = hashSize;
            hashTable = new List<(ulong, int)>[l];
        }

        // public LinkedList<(ulong, int)> createList() 
        // {

        // }

        public ulong get(ulong x)
        {
            return 0;
        }
        public void set(ulong x, int v)
        {

        }
        public void increment(ulong x, ulong d)
        {

        }


        public void hashKeysTimeTest(IEnumerable<Tuple<ulong, int>> stream)
        {
            ulong hashSum = 0;
            foreach(var x in stream)
            {
                hashSum += hashFunction(x.Item1, a, b, l);
            }
            Console.WriteLine($"Sum of hashed vals: {hashSum}");
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