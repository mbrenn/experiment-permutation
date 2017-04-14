using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermutationTest
{
    public static class Permutation
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
            var resultArray = new T[source.Length];
            var subArray = new T[source.Length - 1];
            var first = true;
            // for (var n = 0; n < source.Length; n++)

            var func = new Action<int>(n =>
            {
                // Creates a subArray for the given array. 
                var element = source[n];
                var i = 0;
                for (var m = 0; m < source.Length; m++)
                {
                    if (m == n)
                    {
                        if (first)
                        {
                            continue;
                        }
                        break;
                    }

                    subArray[i] = source[m];
                    i++;
                }
                first = false;

                resultArray[0] = element;
                foreach (var permutedSubArrays in GetAllPermutations(subArray))
                {
                    for (var m = 0; m < permutedSubArrays.Length; m++)
                    {
                        resultArray[m + 1] = permutedSubArrays[m];
                    }

                    lock (result)
                    {
                        result.Add(resultArray.ToArray());
                    }
                }
            });

            if (source.Length > 7)
            {
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
