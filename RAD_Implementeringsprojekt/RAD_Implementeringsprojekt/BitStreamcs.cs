using System;
using System.Collections.Generic;

namespace RAD_Implementeringsprojekt
{

    // Class given by lector of RAD. Create a stream of data to use for testing.
    class BitStreamcs
    {
        public static IEnumerable<Tuple<ulong, int>> CreateStream(int n, int l)
        {
            // We generate a random uint64 number .
            Random rnd = new System.Random();
            ulong a = 0UL;
            Byte[] b = new Byte[8];
            rnd.NextBytes(b);
            for (int i = 0; i < 8; ++i)
            {
                a = (a << 8) + (ulong)b[i];
            }
            // We demand that our random number has 30 zeros on the least
            // significant bits and then a one.
            a = (a | ((1UL << 31) -1UL ) ) ^((1UL << 30) -1UL ) ;
            ulong x = 0UL;
            for (int i = 0; i < n / 3; ++i)
            {
                x = x + a;
                yield return Tuple.Create(x & (((1UL << l) - 1UL ) << 30) , 1) ;
            }
            for(int i = 0; i<(n + 1) /3; ++ i )
            {
                x = x + a ;
                yield return Tuple.Create(x & (((1UL << l ) - 1UL ) << 30) , -1) ;
            }
            for(int i = 0; i<(n + 2) /3; ++ i )
            {
                x = x + a ;
                yield return Tuple.Create(x & (((1UL << l ) - 1UL ) << 30) , 1) ;
            }
        }

    }
}
