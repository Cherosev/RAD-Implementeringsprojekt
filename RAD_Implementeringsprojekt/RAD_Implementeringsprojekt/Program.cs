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

            var table = new HashTable();
            Console.WriteLine($"{table.a}");
            Console.WriteLine($"{table.b}");
        }
    }
}
