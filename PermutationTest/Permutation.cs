using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        public static IEnumerable<T[]> GetAllPermutations<T>(T[] source)
        {
            if (source.Length == 0)
            {
                yield break;
            }
            if (source.Length == 1)
            {
                yield return source.ToArray();
            }
            else if (source.Length == 2) // this are permutations of array of size 2
            {
                // Creates the two possibilities for permutation
                yield return new[] {source[0], source[1]};
                yield return new[] {source[1], source[0]};
            }
            else if (source.Length == 3) // this are permutations of array of size 3
            {
                // Creates the six possibilities for permutation
                // Leads to a performance increase by 20%
                yield return new[] { source[0], source[1], source[2] };
                yield return new[] { source[0], source[2], source[1] };
                yield return new[] { source[1], source[0], source[2] };
                yield return new[] { source[1], source[2], source[0] };
                yield return new[] { source[2], source[0], source[1] };
                yield return new[] { source[2], source[1], source[0] };
            }
            else
            {
                var resultArray = new T[source.Length];
                var subArray = new T[source.Length - 1];

                for (var n = 0; n < source.Length; n++)
                {
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
                    //foreach (var permutedSubArrays in GetAllPermutations(subArray))
                    Parallel.ForEach(GetAllPermutations<T>(subArray),
                        permutedSubArrays =>
                        {
                            for (var m = 0; m < permutedSubArrays.Length; m++)
                            {
                                resultArray[m + 1] = permutedSubArrays[m];
                            }

                            yield return resultArray;
                        });
                }
            }
        }
    }
}
