using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace RAD_Implementeringsprojekt
{
    // Strøm at par (x1, d1)...(xn dn) hvor xn er en nøgle i 64-bit, og dn er et heltal (både positivt og negativt)

    class Program
    {
        
        static void Main(string[] args)
        {
            // Size of keys in bits.
            int keySize = 4;

            var test = Hashing.MultiplyShift((UInt64) 4, (UInt64) 5, 0,61);
            // (a*b)>>(64-l) 
            // (4*5)>>(64-61) = 20 >> 3 = 2
            Console.WriteLine($"MultiplyShift (a=5, x=4, l=61) gives 2: {test == 2}");

            var timer = System.Diagnostics.Stopwatch.StartNew();
            long timestamp = 0;
            var n = 10000;

            Console.WriteLine($" --- Testing time for MultiplyShift. Hashing {n} values---");
            HashTable table;

            #region simpleTests
            long timeSumShift = 0;
            for (int i = 5; i < 25; i += 2)
            {
                table = new HashTable(Hashing.MultiplyShift, i);
                timer.Start();
                var streamer = BitStreamcs.CreateStream(n, i);
                table.hashKeysTimeTest(streamer);
                timestamp = timer.ElapsedMilliseconds;
                Console.WriteLine($"Shift: Time for l={i}:   {timestamp} ms");
                timeSumShift += timestamp;
                timer.Stop();
                timer.Reset();
            }
            Console.WriteLine($" >> Total time for shift: {timeSumShift} ms");

            long timeSumMod = 0;
            Console.WriteLine($" --- Testing time for Mod. Hashing {n} values ---");
            for (int i = 5; i < 25; i += 2)
            {
                table = new HashTable(Hashing.Multiply_mod_prime, i);
                timer.Start();
                var streamer = BitStreamcs.CreateStream(n, i);
                table.hashKeysTimeTest(streamer);
                timestamp = timer.ElapsedMilliseconds;
                Console.WriteLine($"Mod: Time for l={i}:   {timestamp} ms");
                timeSumMod += timestamp;
                timer.Stop();
                timer.Reset();
            }
            Console.WriteLine($" >> Total time for mod: {timeSumMod} ms");
            #endregion

            #region GetSetTesting
            var getSetTestTable = new HashTable(Hashing.Multiply_mod_prime, keySize);
            var getSetStreamer = BitStreamcs.CreateStream(50, keySize).ToList();

            foreach (var x in getSetStreamer)
            {
                //Console.WriteLine($" >> Adding value: {x} to table.");
                getSetTestTable.set(x.Item1, x.Item2);
            }
            foreach (var x in getSetStreamer)
            {
                //Console.WriteLine($" >> Adding value: {x} to table.");
                getSetTestTable.set(x.Item1, 0);
            }

            foreach (var x in getSetStreamer)
            {
                //Console.WriteLine($" >> Incrementing value: {x}.");
                getSetTestTable.increment(x.Item1, 1);
            }

            foreach (var x in getSetStreamer)
            {
                var check = getSetTestTable.get(x.Item1);
                Console.WriteLine($" >> Searching for value: {x}.   Got {check}");
            }
            #endregion

            #region SquareSumTest
            n = 10000;
            timer.Reset();
            

            long sqTotalTimeShift = 0;
            long sqTotalTimeMod = 0;
            for (int i = 5; i < 64; i += 2)
            {
                var streamSize = 1000000;
                var stream = BitStreamcs.CreateStream(streamSize, i);
                //--------
                // ShiftTest
                timer.Start();
                var sqSumTestTableShift = new HashTable(Hashing.MultiplyShift, i);
                var shiftSum = SquareSum.ComputeSquareSum(stream, sqSumTestTableShift);
                timestamp = timer.ElapsedMilliseconds;
                sqTotalTimeShift += timestamp;
                timer.Stop();
                timer.Reset();

                Console.WriteLine($"Shift: Time for n={streamSize}, l={i}: {timestamp} ms");
                    
                //------
                // ModTest
                timer.Start();
                var sqSumTestTableMod = new HashTable(Hashing.Multiply_mod_prime, i);
                var modSum = SquareSum.ComputeSquareSum(stream, sqSumTestTableMod);
                timestamp = timer.ElapsedMilliseconds;
                sqTotalTimeMod += timestamp;
                timer.Stop();
                timer.Reset();
                    
                Console.WriteLine($"Mod: Time for n={streamSize}, l={i}: {timestamp} ms");
                
            }

            Console.WriteLine($" >> Total time for shift: {sqTotalTimeShift} ms");
            Console.WriteLine($" >> Total time for mod: {sqTotalTimeMod} ms");

            #endregion
        }
    }
}
