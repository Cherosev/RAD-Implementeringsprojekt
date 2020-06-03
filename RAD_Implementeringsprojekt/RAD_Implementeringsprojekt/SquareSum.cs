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
            var streamList = stream.ToList();
            Console.WriteLine(streamList.Select(x => x.Item1).Distinct().Count());
            
            // Add all keys to table.
            
            
            foreach (var pair in stream)
            {
                table.increment(pair.Item1, pair.Item2);
            }



            BigInteger sum = 0;
            foreach (LinkedList<(ulong, int)> linkedList in table.hashTable)
            {
                if (linkedList == null) continue;
                var node = linkedList.First;

                while (node != null){
                    sum += BigInteger.Pow(node.Value.Item2, 2);
                    // Console.WriteLine(node.Value.Item2);
                    node = node.Next;
                }
            }

            return sum;
        }

    }
}


