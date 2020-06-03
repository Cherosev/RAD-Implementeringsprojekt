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

        private Hashing hashScheme;
        
        public LinkedList<(ulong, int)>[] hashTable;

        /// Constructor. Takes a hashfunction and the amount of bits in the keys.
        public HashTable(Hashfunctions hashType, int hashSize) 
        {
            l = Math.Min(hashSize, 30);
            hashScheme = new Hashing(hashType, l);
            // l > 29 results in array dimensions exceed supported range
            var tableSize = (ulong)Math.Pow(2, l);
            hashTable = new LinkedList<(ulong, int)>[(tableSize)];
        }

        /// Finds index of a key by running the tables hashfunction on it.
        public ulong FindIndex(ulong x)
        {
            return hashScheme.h(x);
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
            //if (hashScheme.h == hashScheme.fourUniversal)
            //{
            //    Console.WriteLine($"Looking for index {index}. Table has {hashTable.Length} indecies. P is {hashScheme.p_value}");
            //}
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

        public BigInteger hashKeysTimeTest(IEnumerable<Tuple<ulong, int>> stream)
        {
            BigInteger hashSum = 0;
            foreach(var x in stream)
            {
                hashSum += hashScheme.h(x.Item1);
            }
            return hashSum;
        }

        
    }
}