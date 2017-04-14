using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PermutationTest
{
    /// <summary>
    /// Stores the memory efficient permutation
    /// </summary>
    public class Permutation
    {

        /// <summary>
        /// Gets all permutations of the given element as a 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T[]> GetAllPermutations<T>(T[] source)
        {
            foreach (var p in new MemoryPermutationInternal<T>(source.Length).GetAllPermutationsInternal(source))
            {
                yield return p;
            }
        }
    }

    /// <summary>
        /// Stores the memory efficient permutation
        /// </summary>
    internal class MemoryPermutationInternal<T>
    {
        private T[][] _resultArray;
        private T[][] _subArray;

        public MemoryPermutationInternal(int length)
        {
            _resultArray = new T[length + 1][];
            _subArray = new T[length + 1][];
            for (var n = 0; n <= length; n++)
            {
                _resultArray[n] = new T[n];
                _subArray[n] = new T[n];
            }
        }

        internal IEnumerable<T[]> GetAllPermutationsInternal(IReadOnlyList<T> source)
        {
            switch (source.Count)
            {
                case 0:
                    yield break;
                case 1:
                    _resultArray[1][0] = source[0];
                    yield return _resultArray[1];
                    yield break;
                case 2:
                    // Creates the two possibilities for permutation
                    yield return CreateTwo(source[0], source[1]);
                    yield return CreateTwo(source[1], source[0]);
                    yield break;
                case 3:
                    // Creates the six possibilities for permutation
                    // Leads to a performance increase by 20%#
                    yield return CreateThree(source[0], source[1], source[2]);
                    yield return CreateThree(source[0], source[2], source[1]);
                    yield return CreateThree(source[1], source[0], source[2]);
                    yield return CreateThree(source[1], source[2], source[0]);
                    yield return CreateThree(source[2], source[0], source[1]);
                    yield return CreateThree(source[2], source[1], source[0]);
                    yield break;
            }

            var resultArray = _resultArray[source.Count];
            var subArray = _subArray[source.Count - 1];

            for (var n = 0; n < source.Count; n++)
            {
                // Creates a subArray for the given array. 
                var element = source[n];
                var i = 0;
                for (var m = 0; m < source.Count; m++)
                {
                    if (m == n)
                    {
                        continue;
                    }

                    subArray[i] = source[m];
                    i++;
                }

                resultArray[0] = element;

                var list = GetAllPermutationsInternal(subArray);
                foreach (var permutedSubArrays in list)
                {
                    for (var m = 0; m < permutedSubArrays.Length; m++)
                    {
                        resultArray[m + 1] = permutedSubArrays[m];
                    }

                    yield return resultArray;
                }
            }
        }

        private T[] CreateTwo(T p0, T p1)
        {
            _resultArray[2][0] = p0;
            _resultArray[2][1] = p1;
            return _resultArray[2];
        }

        private T[] CreateThree(T p0, T p1, T p2)
        {
            _resultArray[3][0] = p0;
            _resultArray[3][1] = p1;
            _resultArray[3][2] = p2;
            return _resultArray[3];
        }
    }
}
