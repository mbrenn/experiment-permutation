using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PermutationTest
{
    /// <summary>
    /// Stores the memory efficient permutation
    /// </summary>
    public static class MemoryPermutation
    {
        /// <summary>
        /// Gets all permutations of the given element as a 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T[]> GetAllPermutations<T>(T[] source)
        {
            switch (source.Length)
            {
                case 0:
                    yield break;
                case 1:
                    yield return source.ToArray();
                    break;
                case 2:
                    // Creates the two possibilities for permutation
                    yield return new[] {source[0], source[1]};
                    yield return new[] {source[1], source[0]};
                    break;
                case 3:
                    // Creates the six possibilities for permutation
                    // Leads to a performance increase by 20%#
                    yield return new[] {source[0], source[1], source[2]};
                    yield return new[] {source[0], source[2], source[1]};
                    yield return new[] {source[1], source[0], source[2]};
                    yield return new[] {source[1], source[2], source[0]};
                    yield return new[] {source[2], source[0], source[1]};
                    yield return new[] {source[2], source[1], source[0]};
                    break;
            }

            for (var n = 0; n < source.Length; n++)
            {
                var resultArray = new T[source.Length];
                var subArray = new T[source.Length - 1];
                var first = true;

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

                    first = false;

                    subArray[i] = source[m];
                    i++;
                }

                resultArray[0] = element;
                var list = GetAllPermutations(subArray);
                foreach (var permutedSubArrays in list)
                {
                    for (var m = 0; m < permutedSubArrays.Length; m++)
                    {
                        resultArray[m + 1] = permutedSubArrays[m];
                    }

                    yield return resultArray.ToArray();
                }
            }
        }
    }
}
