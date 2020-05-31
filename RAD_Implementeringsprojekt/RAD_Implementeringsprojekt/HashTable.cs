using System;
using System.Collections;

namespace RAD_Implementeringsprojekt {

    public class HashTable 
    {
        public ulong a;
        public ulong b;

        // Constructor
        public HashTable() 
        {
            a = getRandomULong();
            b = getRandomULong();
        }

        private ulong getRandomULong()
        {
            Random randomGenerator = new Random();
            Byte[] byteArr = new Byte[8];
            randomGenerator.NextBytes(byteArr);
            var bitArr = new BitArray(byteArr);

            // Ensure ULong is uneven
            bitArr.Set(bitArr.Length-1, true);
            bitArr.CopyTo(byteArr, 0);
            return BitConverter.ToUInt64(byteArr, 0);
        }


    }

}