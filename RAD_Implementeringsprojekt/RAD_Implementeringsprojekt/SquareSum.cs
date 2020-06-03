using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace RAD_Implementeringsprojekt
{
    class SquareSum
    {
        public static BigInteger ComputeSquareSum(IEnumerable<Tuple<ulong, int>> stream, HashTable table)
        {
            // Add all keys to table.
            foreach (var pair in stream)
            {
                table.increment(pair.Item1, pair.Item2);
            }

            BigInteger sum = 0;
            foreach (var linkedList in table.hashTable)
            {
                if (linkedList == null) continue;
                foreach(var keypair in linkedList)
                {
                    sum += (ulong)BigInteger.Pow(keypair.Item2, 2);
                }
            }

            return sum;
        }

    }
}


