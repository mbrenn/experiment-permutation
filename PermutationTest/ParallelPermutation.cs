using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PermutationTest
{
    public static class ParallelPermutation
    {
        /// <summary>
        /// Gets all permutations of the given element as a 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<T[]> GetAllPermutations<T>(T[] source)
        {
            switch (source.Length)
            {
                case 0:
                    return new List<T[]>();
                case 1:
                    return new List<T[]> {source.ToArray()};
                case 2:
                    // Creates the two possibilities for permutation
                    return new List<T[]>
                    {
                        new[] {source[0], source[1]},
                        new[] {source[1], source[0]}
                    };
                case 3:
                    // Creates the six possibilities for permutation
                    // Leads to a performance increase by 20%#
                    return new List<T[]>
                    {
                        new[] {source[0], source[1], source[2]},
                        new[] {source[0], source[2], source[1]},
                        new[] {source[1], source[0], source[2]},
                        new[] {source[1], source[2], source[0]},
                        new[] {source[2], source[0], source[1]},
                        new[] {source[2], source[1], source[0]},
                    };
            }

            var result = new List<T[]>();
            // for (var n = 0; n < source.Length; n++)
            bool[] doLock = {false};

            var func = new Action<int>(n =>
            {
                var resultArray = new T[source.Length];
                var subArray = new T[source.Length - 1];

                // Creates a subArray for the given array. 
                var element = source[n];
                var i = 0;
                for (var m = 0; m < source.Length; m++)
                {
                    if (m == n)
                    {
                        continue;
                    }

                    subArray[i] = source[m];
                    i++;
                }

                resultArray[0] = element;
                var list = GetAllPermutations(subArray);
                
                if (doLock[0])
                {
                    var sub = new List<T[]>();

                    foreach (var permutedSubArrays in list)
                    {
                        for (var m = 0; m < permutedSubArrays.Length; m++)
                        {
                            resultArray[m + 1] = permutedSubArrays[m];
                        }

                        sub.Add(resultArray.ToArray());
                    }

                    Monitor.Enter(result);
                    result.AddRange(sub);
                    Monitor.Exit(result);
                }
                else
                {
                    foreach (var permutedSubArrays in list)
                    {
                        for (var m = 0; m < permutedSubArrays.Length; m++)
                        {
                            resultArray[m + 1] = permutedSubArrays[m];
                        }

                        result.Add(resultArray.ToArray());
                    }
                }
            });

            if (source.Length > 7)
            {
                doLock[0] = true;
                Parallel.For(0, source.Length, func);
            }
            else
            {
                for (var n = 0; n < source.Length; n++)
                {
                    func(n);
                }
            }

            return result;
        }
    }
}
