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
            for (var n = 1; n < 12; n++)
            {
                var largeList = new int[n];
                for (var m = 0; m < n; m++)
                {
                    largeList[m] = m;
                }

                var watch = Stopwatch.StartNew();

                if (n < 11)
                {
                    var permutations = ParallelPermutation.GetAllPermutations(largeList);
                    Permutate(permutations, watch, n, "P");
                }

                watch = Stopwatch.StartNew();
                var enumPermutations = Permutation.GetAllPermutations(largeList);
                Permutate(enumPermutations, watch, n, "M");
            }
        }

        private static void Permutate(IEnumerable<int[]> permutations, Stopwatch watch, int n, string type)
        {
            permutations = permutations.Select(x => x.ToArray()).ToArray();
            var count = permutations.Select(x=>x.ToArray()).LongCount();
            watch.Stop();
            Console.WriteLine($"{type} - {n}: {watch.Elapsed} for {count} elements: " +
                              $"{count * 1000 / (watch.ElapsedMilliseconds + 1)} elements/s");

            if (n <= 10)
            {
                WriteFile(permutations, type, n);
            }
        }

        private static void WriteFile(IEnumerable<int[]> permutations, string type, int n)
        {
            using (var file = File.OpenWrite($"Permutations {n} - {type}.txt"))
            {
                using (var stream = new StreamWriter(file))
                {
                    var builder = new StringBuilder();
                    foreach (var per in permutations)
                    {
                        builder.Clear();
                        foreach (var t in per)
                        {
                            builder.Append(t + " ");
                        }

                        stream.WriteLine(builder.ToString());
                    }
                }
            }
        }
    }
}