using System;
using System.Linq;
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
        public LinkedList<(ulong, int)>[] hashTable;

        /// Constructor. Takes a hashfunction and the amount of bits in the keys.
        public HashTable(hashFunctionDelegate h, int hashSize) 
        {
            a = getRandomULong();
            b = getRandomULong();
            hashFunction = h;
            l = hashSize;
            var tableSize = (ulong) Math.Pow(2, hashSize);
            hashTable = new LinkedList<(ulong, int)>[(tableSize)];
        }

        /// Finds index of a key by running the tables hashfunction on it.
        public ulong FindIndex(ulong x)
        {
            return hashFunction(x, a, b, l);
        }

        public int get(ulong x)
        {
            ulong index = FindIndex(x);
            //Console.WriteLine($"Key {x} is expected to be found at index {index}");
            var list = hashTable[index];
            if  (list == null) return 0;

            var node = list.First;
            while (node != null)
            {
                if(node.Value.Item1 == x) return node.Value.Item2;
                node = node.Next;
            }
            return 0;
        }
        public void set(ulong x, int v)
        {
            ulong index = FindIndex(x);

            //Console.WriteLine($"Key {x} will be stored at index {index}");

            var list = hashTable[index];
            if (list == null)
            {
                list = new LinkedList<(ulong, int)>();
                list.AddFirst((x, v));
                hashTable[index] = list;
                return;
            }

            var node = list.First;
            while (node != null)
            {
                if (node.Value.Item1 == x)
                {
                    node.Value = (x,v);
                    return;
                }
                node = node.Next;
            }
            list.AddLast((x,  v));
        }
        public void increment(ulong x, int d)
        {
            ulong index = FindIndex(x);

            // Find linked list.
            var list = hashTable[index];
            // Check that it is not empty.
            if (list == null)
            {
                list = new LinkedList<(ulong, int)>();
                list.AddFirst((x, d));
                hashTable[index] = list;
                return;
            }

            var node = list.First;
            // Itterate trough linked list.
            while (node != null)
            {
                if (node.Value.Item1 == x)
                {
                    // Increment value
                    node.Value = (x, node.Value.Item2 + d);
                    return;
                }
                // Next node
                node = node.Next;
            }
            // Element not found. Add it.
            list.AddLast((x, d));
        }

        public void hashKeysTimeTest(IEnumerable<Tuple<ulong, int>> stream)
        {
            BigInteger hashSum = 0;
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

            // Ensure ULong is uneven
            var ret = BitConverter.ToUInt64(byteArr, 0);
            if( ret % 2 != 1) ret++;
            return ret;
        }
    }
}