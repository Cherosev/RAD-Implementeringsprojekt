using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace RAD_Implementeringsprojekt
{
    class CountSketch
    {
        // Opgave 6
        public static long CountSketchEstimate(IEnumerable<Tuple<ulong, int>> stream, Hashing hashScheme)
        {
            var l = hashScheme.l_value;
            ulong k = (ulong)Math.Pow(2, l);
            Console.WriteLine($"Estimate array: {k}");
            long[] C = new long[k];

            foreach (var pair in stream)
            {
                var (hx, sx) = RunHashfunction(hashScheme, pair.Item1);
                //Console.WriteLine($"Key {pair.Item1} hashed to index {hx}");
                int delta = pair.Item2;
                var adding = sx * delta;
                //Console.WriteLine($"Adding value {adding} to C");
                C[hx] += adding;
                //foreach (var c in C)
                //{
                //    Console.WriteLine($"[{c}]");
                //}
                //Console.WriteLine();
            }
            

            long X = 0;
            foreach(long c in C) 
            {
                X += c*c;
            }


            // Array.ForEach(C, x => X += (long)BigInteger.Pow(x, 2));
            return X;
        }

        // Opgave 5
        public static (ulong, long) RunHashfunction(Hashing hashScheme, ulong x)
        {
            var t = hashScheme.l_value;
            //BigInteger m = (BigInteger)Math.Pow(2, t);
            int b = 89;
            BigInteger k = (BigInteger)Math.Pow(2, b);
            BigInteger p = k - 1;

            ulong gx = hashScheme.h(x); // 4universal - tak!

            //BigInteger hx = gx & (p);
            ulong bx = gx >> (t-1);
            long sx = 1 - (long)(2 * bx);

            return (gx, sx);
        }

    }
}
