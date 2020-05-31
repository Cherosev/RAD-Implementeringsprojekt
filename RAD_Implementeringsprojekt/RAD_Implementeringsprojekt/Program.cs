using System;

namespace RAD_Implementeringsprojekt
{
    // Strøm at par (x1, d1)...(xn dn) hvor xn er en nøgle i 64-bit, og dn er et heltal (både positivt og negativt).

    class Program
    {
        static void Main(string[] args)
        {
            var test = Hashing.MultiplyShift((UInt64) 4, (UInt64) 5, 61);
            // (a*b)>>(64-l) 
            // (4*5)>>(64-61) = 20 >> 3 = 2
            Console.WriteLine($"MultiplyShift (a=5, x=4, l=61) gives 2: {test == 2}");

            var timer = System.Diagnostics.Stopwatch.StartNew();
            long timestamp = 0;

            Console.WriteLine(" --- Testing time for MultiplyShift ---");
            HashTable table = new HashTable();
            // Console.WriteLine($"{table.a}");

            long timeSumShift = 0;
            for (int i = 5; i < 50; i += 2)
            {
                timer.Start();
                var streamer = BitStreamcs.CreateStream(1000000, i);
                table.hashKeyShift(streamer);
                timestamp = timer.ElapsedMilliseconds;
                Console.WriteLine($"Shift: Time for l={i}:   {timestamp} ms      Sum: {table.sumHxShift}");
                timeSumShift += timestamp;
                timer.Stop();
                timer.Reset();
            }
            Console.WriteLine($" >> Total time for shift: {timeSumShift} ms");


            long timeSumMod = 0;
            Console.WriteLine(" --- Testing time for Mod ---");
            for (int i = 5; i < 50; i += 2)
            {
                timer.Start();
                var streamer = BitStreamcs.CreateStream(1000000, i);
                table.hashKeyMod(streamer);
                timestamp = timer.ElapsedMilliseconds;
                Console.WriteLine($"Mod: Time for l={i}:   {timestamp} ms      Sum: {table.sumHxMod}");
                timeSumMod += timestamp;
                timer.Stop();
                timer.Reset();
            }
            Console.WriteLine($" >> Total time for mod: {timeSumMod} ms");
        }
    }
}
