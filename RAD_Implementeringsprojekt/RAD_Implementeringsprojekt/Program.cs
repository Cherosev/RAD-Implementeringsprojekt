﻿using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
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

            //var test = Hashing.MultiplyShift((UInt64) 4, (UInt64) 5, 0,61);
            // (a*b)>>(64-l) 
            // (4*5)>>(64-61) = 20 >> 3 = 2
            //Console.WriteLine($"MultiplyShift (a=5, x=4, l=61) gives 2: {test == 2}");

            var timer = System.Diagnostics.Stopwatch.StartNew();
            long timestamp = 0;
            var n = 1000000;

            Console.WriteLine($" --- Testing time for MultiplyShift. Hashing {n} values---");
            HashTable table;

            #region simpleHashingTests
            long timeSumShift = 0;
            for (int i = 5; i < 25; i += 2)
            {
                table = new HashTable(Hashfunctions.MultiplyShift, i);
                timer.Start();
                var streamer = BitStreamcs.CreateStream(n, i);
                table.hashKeysTimeTest(streamer);
                timestamp = timer.ElapsedMilliseconds;
                //Console.WriteLine($"Shift: Time for l={i}:   {timestamp} ms");
                timeSumShift += timestamp;
                timer.Stop();
                timer.Reset();
            }
            Console.WriteLine($" >> SimpleHashing - Total time for shift: {timeSumShift} ms");

            long timeSumMod = 0;
            Console.WriteLine($" --- Testing time for Mod. Hashing {n} values ---");
            for (int i = 5; i < 25; i += 2)
            {
                table = new HashTable(Hashfunctions.Multiply_Mod_Prime, i);
                timer.Start();
                var streamer = BitStreamcs.CreateStream(n, i);
                table.hashKeysTimeTest(streamer);
                timestamp = timer.ElapsedMilliseconds;
                //Console.WriteLine($"Mod: Time for l={i}:   {timestamp} ms");
                timeSumMod += timestamp;
                timer.Stop();
                timer.Reset();
            }
            Console.WriteLine($" >> SimpleHashing - Total time for mod: {timeSumMod} ms");
            #endregion

            #region GetSetTesting
            var getSetTestTableMod = new HashTable(Hashfunctions.Multiply_Mod_Prime, keySize);
            var getSetTestTableFour = new HashTable(Hashfunctions.fourUniversal, keySize);
            var getSetTestTableShift = new HashTable(Hashfunctions.MultiplyShift, keySize);
            var getSetStreamer = BitStreamcs.CreateStream(50, keySize).ToList();

            foreach (var x in getSetStreamer)
            {
                //Console.WriteLine($" >> Adding value: {x} to table.");
                getSetTestTableMod.set(x.Item1, x.Item2);
                getSetTestTableFour.set(x.Item1, x.Item2);
                getSetTestTableShift.set(x.Item1, x.Item2);
            }
            foreach (var x in getSetStreamer)
            {
                //Console.WriteLine($" >> Adding value: {x} to table.");
                getSetTestTableMod.set(x.Item1, 0);
                getSetTestTableFour.set(x.Item1, 0);
                getSetTestTableShift.set(x.Item1, 0);
            }

            foreach (var x in getSetStreamer)
            {
                //Console.WriteLine($" >> Incrementing value: {x}.");
                getSetTestTableMod.increment(x.Item1, 1);
                getSetTestTableFour.increment(x.Item1, 1);
                getSetTestTableShift.increment(x.Item1, 1);
            }

            foreach (var x in getSetStreamer)
            {
                var checkShift = getSetTestTableShift.get(x.Item1);
                var checkMod   = getSetTestTableMod.get(x.Item1);
                var checkFour  = getSetTestTableFour.get(x.Item1);

                //Console.WriteLine($" >> Searching for value: {x}. Shift: {checkShift}, Mod: {checkMod}, Four: {checkFour}");
            }
            #endregion

            #region SquareSumTest
            n = 10000;
            timer.Reset();
            
            //long sqTotalTimeFour = 0;
            for (int i = 12; i <= 21; i += 3)
            {
                long sqTotalTimeShift = 0;
                long sqTotalTimeMod = 0;
                for (int streamSize = 5000; streamSize <= 100000; streamSize += 5000)
                {
                    var stream = BitStreamcs.CreateStream(streamSize, i);
                    //--------
                    // ShiftTest
                    timer.Start();
                    var sqSumTestTableShift = new HashTable(Hashfunctions.MultiplyShift, i);
                    var shiftSum = SquareSum.ComputeSquareSum(stream, sqSumTestTableShift);
                    timestamp = timer.ElapsedMilliseconds;
                    sqTotalTimeShift += timestamp;
                    timer.Stop();
                    timer.Reset();

                    //Console.WriteLine($"Shift: Time for n={streamSize}, l={i}: {timestamp} ms");

                    //------
                    // ModTest
                    timer.Start();
                    var sqSumTestTableMod = new HashTable(Hashfunctions.Multiply_Mod_Prime, i);
                    var modSum = SquareSum.ComputeSquareSum(stream, sqSumTestTableMod);
                    timestamp = timer.ElapsedMilliseconds;
                    sqTotalTimeMod += timestamp;
                    timer.Stop();
                    timer.Reset();

                    //Console.WriteLine($"Mod: Time for n={streamSize}, l={i}: {timestamp} ms");
                }
                Console.WriteLine($" >> Squaresum - Total time for shift for l={i}: {sqTotalTimeShift} ms");
                Console.WriteLine($" >> Squaresum - Total time for mod for   l={i}: {sqTotalTimeMod} ms");
            }

            
            // Console.WriteLine($" >> Total time for fourUni: {sqTotalTimeFour} ms");

            #endregion

            #region SaveHitTest

            //var SaveHitTestKeySize = 6;
            //var SaveHitStreamSize = 1000;
            //var SaveHitStream = BitStreamcs.CreateStream(SaveHitStreamSize, SaveHitStreamSize);
            //var SaveHitShiftTable = new HashTable(Hashfunctions.MultiplyShift, SaveHitStreamSize);
            //var SaveHitModTable = new HashTable(Hashfunctions.Multiply_Mod_Prime, SaveHitStreamSize);
            //var SaveHitFourTable = new HashTable(Hashfunctions.fourUniversal, SaveHitStreamSize);

            //foreach (var pair in SaveHitStream)
            //{
            //    SaveHitShiftTable.set(pair.Item1, pair.Item2);
            //    SaveHitModTable.set(pair.Item1, pair.Item2);
            //    SaveHitFourTable.set(pair.Item1, pair.Item2);
            //}

            //foreach (var pair in SaveHitStream)
            //{
            //    if (SaveHitShiftTable.get(pair.Item1) != pair.Item2) throw new Exception($"Shift: Key {pair.Item1} did not return correct value.");
            //    if (SaveHitModTable.get(pair.Item1) != pair.Item2) throw new Exception($"Mod: Key {pair.Item1} did not return correct value.");
            //    if (SaveHitFourTable.get(pair.Item1) != pair.Item2) throw new Exception($"Four: Key {pair.Item1} did not return correct value.");
            //}

            #endregion

            #region CountSketchTests
            var CountSketchKeySize = 12;
            var CountStreamSize = 1000;
            var CountSketchStream  = BitStreamcs.CreateStream(CountStreamSize, CountSketchKeySize);

            var fourUniHashing = new Hashing(Hashfunctions.fourUniversal, CountSketchKeySize);
            var StoreCFour = CountSketch.CountSketchEstimate(CountSketchStream, fourUniHashing);

            var CountSketchTable = new HashTable(Hashfunctions.MultiplyShift, CountSketchKeySize);
            var actualStoreC = SquareSum.ComputeSquareSum(CountSketchStream, CountSketchTable);

            //var streamSize = 1000000;
            //var stream = BitStreamcs.CreateStream(streamSize, i);


            Console.WriteLine($"CountSketch esimate: Four: {StoreCFour}");
            Console.WriteLine($"Actual value:              {actualStoreC}");
            #endregion


        }
    }
}
