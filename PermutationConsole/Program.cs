using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using PermutationTest;

namespace PermutationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var n = 1; n < 11; n++)
            {
                var largeList = new int[n];
                for (var m = 0; m < n; m++)
                {
                    largeList[m] = m;
                }

                var watch = Stopwatch.StartNew();

                /*var permutations = ParallelPermutation.GetAllPermutations(largeList);
                Permutate(permutations, watch, n, "Parallel");*/

                watch = Stopwatch.StartNew();
                var enumPermutations = MemoryPermutation.GetAllPermutations(largeList);
                Permutate(enumPermutations, watch, n, "Memory");
            }
        }

        private static void Permutate(IEnumerable<int[]> permutations, Stopwatch watch, int n, string type)
        {
            var list = permutations.ToList();
            var count = list.LongCount();
            watch.Stop();
            Console.WriteLine($"{type}: {watch.Elapsed} for {n} ({count}) elements: " +
                              $"{count * 1000 / (watch.ElapsedMilliseconds + 1)} elements/s");

            if (n <= 10)
            {
                Console.Write("Writing to file... ");
                //WriteFile(list, type, n);
                Console.WriteLine("Done");
            }
        }

        private static void WriteFile(IEnumerable<int[]> permutations, string type, int n)
        {
            var builder = new StringBuilder();
            foreach (var per in permutations)
            {
                foreach (var t in per)
                {
                    builder.Append(t + " ");
                }

                builder.AppendLine();
            }

            File.WriteAllText($"Permutations {n} - {type}.txt", builder.ToString());
        }
    }
}