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
            var list = new[] {1, 2, 3, 4, 5};
            foreach (var permutation in Permutation.GetAllPermutations(list))
            {
                WritePermutation(permutation);
            }

            for (var n = 1; n < 12; n++)
            {
                var largeList = new int[n];
                for (var m = 0; m < n; m++)
                {
                    largeList[m] = m;
                }

                var watch = Stopwatch.StartNew();
                var permutations = Permutation.GetAllPermutations(largeList);
                var count = permutations.LongCount();

                Console.WriteLine(count);
                watch.Stop();
                Console.WriteLine($"Elapsed: {watch.Elapsed} for {n} elements: " +
                                  $"{count * 1000 / (watch.ElapsedMilliseconds+1)} elements/s");

                if (n <= 9)
                {
                    Console.Write("Writing to file... ");
                    WriteFile(permutations, n);
                    Console.WriteLine("Done");
                }

            }

            Console.ReadKey();
        }

        private static void WriteFile(List<int[]> permutations, int n)
        {
            var builder = new StringBuilder();
            foreach (var per in permutations)
            {
                for (var m = 0; m < per.Length; m++)
                {
                    builder.Append(per[m] + " ");
                }
                builder.AppendLine();
            }

            File.WriteAllText($"Permutations {n}.txt", builder.ToString());
        }

        private static void WritePermutation(int[] permutation)
        {
            foreach (var val in permutation)
            {
                Console.Write(val + ", ");
            }

            Console.WriteLine();
        }
    }
}