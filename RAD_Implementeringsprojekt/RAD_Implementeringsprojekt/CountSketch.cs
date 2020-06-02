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
        public static long CountSketchEstimate(IEnumerable<Tuple<ulong, int>> stream, Hashing hashScheme, int l)
        {
            ulong k = (ulong)Math.Pow(2, 89);
            long[] C = new long[k];

            foreach (var pair in stream)
            {
                var (hx, sx) = RunHashfunction(hashScheme, pair.Item1, k);
                int delta = pair.Item2;
                var adding = C[hx] + sx * delta;
                //Console.WriteLine($"Adding value {adding} to C");
                C[hx] += adding;
            }

            long X = 0;
            Array.ForEach(C, x => X += (long)Math.Pow(x,2));
            return X;
        }

        // Opgave 5
        public static (ulong, long) RunHashfunction(Hashing hashScheme, ulong x, ulong t)
        {
            BigInteger m = (BigInteger)Math.Pow(2, t);
            int   b  = 89;
            ulong k = (ulong)Math.Pow(2,b);
            ulong p = k - 1;

            ulong gx = hashScheme.h(x); // 4universal - tak!

            ulong hx = gx & p;
            ulong bx = gx >> (b-1);
            long sx = 1-(long)(2 * bx);
            return (hx, sx);
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
