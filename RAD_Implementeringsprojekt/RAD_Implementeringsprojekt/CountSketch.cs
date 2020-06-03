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
            long[] C = new long[k];

            foreach (var pair in stream)
            {
                var (hx, sx) = RunHashfunction(hashScheme, pair.Item1);
                int delta = pair.Item2;
                var adding = sx * delta;
                //Console.WriteLine($"Adding value {adding} to C");
                C[hx] += adding;
            }

            long X = 0;
            Array.ForEach(C, x => X += x*x);
            return X;
        }

        // Opgave 5
        public static (ulong, long) RunHashfunction(Hashing hashScheme, ulong x)
        {
            var t = hashScheme.l_value;
            BigInteger m = (BigInteger)Math.Pow(2, t);
            int   b  = 89;
            BigInteger k = (BigInteger)Math.Pow(2,b);
            BigInteger p = k - 1;

            ulong gx = hashScheme.h(x); // 4universal - tak!

            // ulong hx = gx ;
            ulong bx = gx & 1;
            long sx = 1-(long)(2 * bx);
            return (gx, sx);
        }

        // Opgave 5
        public static ulong CountSketch_h(ulong hashValue, int t)
        {
            ulong m = (ulong)Math.Pow(2, t);
            return 0;
        }


        // Opgave 5
        public static ulong CountSketch_s(ulong hashValue, int k)
        {
            ulong hx = hashValue & ((ulong)k-1);
            ulong bx = hashValue >> (89-1);
            return (ulong) 1-(ulong)2*bx; 
        }

    }
}
