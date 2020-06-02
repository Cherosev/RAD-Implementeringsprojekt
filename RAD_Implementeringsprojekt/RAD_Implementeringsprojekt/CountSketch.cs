using System;
using System.Collections.Generic;
using System.Text;

namespace RAD_Implementeringsprojekt
{
    class CountSketch
    {
        // Opgave 6
        public static ulong CountSketchEstimate()
        {

            return 0;
        }

        // Opgave 5
        public static (ulong, ulong) RunHashfunction(Hashing hashScheme, ulong x, int l)
        {
            int   b  = 89;
            ulong gx = hashScheme.h(x);
            ulong hx = gx & ((ulong)Math.Pow(2, l) - 1);
            ulong bx = gx >> (b-1);
            ulong sx = 1-(2 * bx);
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
