using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PermutationTest;

namespace PermutationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var list = new[] {1, 2, 3, 4, 5};
            foreach (var permutation in Permutation.GetAllPermutations(list))
            {
                foreach (var val in permutation)
                {
                    Console.Write(val + ", ");
                }

                Console.WriteLine();
            }*/

            for (var n = 1; n < 11; n++)
            {
                var largeList = new int[n];
                for (var m = 0; m < n; m++)
                {
                    largeList[m] = m;
                }

                var watch = Stopwatch.StartNew();
                var count = Permutation.GetAllPermutations(largeList).LongCount();
                Console.WriteLine(count);
                watch.Stop();
                Console.WriteLine($"Elapsed: {watch.Elapsed} for {n} elements: " +
                                  $"{count * 1000 / (watch.ElapsedMilliseconds+1)} elements/s");
                
            }

            Console.ReadKey();
        }
    }
}