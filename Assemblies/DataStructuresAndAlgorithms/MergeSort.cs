using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    public class MergeSort : ISortingAlgorithm
    {

        /// <summary>
        /// O(n * ln(n)) Average time complexity.
        /// Not in place, returned value is a List to avoid 
        /// returning an array where the user initially called the method with a List/Collection
        /// and expects the full IList implementation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public IList<T> Sort<T>(IList<T> collection) where T : IComparable
        {
            return Sort(collection, Comparer<T>.Default);
        }

        /// <summary>
        /// O(n * ln(n)) Average time complexity.
        /// Not in place, returned value is a List to avoid 
        /// returning an array where the user initially called the method with a List/Collection
        /// and expects the full IList implementation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public IList<T> Sort<T>(IList<T> collection, IComparer<T> comparer)
        {
            /*
             * Recursive.
             * 
             * Splits the array down to arrays of individual elements then sorts
             * on the way back up by merging the arrays together
             */

            if (collection == null)
            {
                throw new ArgumentException("MergeSort.Sort() called with null collection");
            }

            if (comparer == null)
            {
                throw new ArgumentException("MergeSort.Sort() called with null comparer");
            }

            if (collection.Count == 1) // recursive base case
            {
                return collection;
            }

            int half = collection.Count / 2;

            var left = Sort(collection.Slice(half), comparer); // Split the left half and sort it
            var right = Sort(collection.Slice(half, collection.Count - half), comparer); // Split the right half and sort it

            return Merge(left, right, comparer); // merge the sorted halves
        }


        private IList<T> Merge<T>(IList<T> left, IList<T> right, IComparer<T> comparer)
        {
            int i = 0;
            int leftIndex = 0;
            int rightIndex = 0;

            IList<T> merged = new List<T>(left.Count + right.Count);

            while (leftIndex < left.Count || rightIndex < right.Count)
            {
                if (leftIndex < left.Count && rightIndex < right.Count)
                {
                    merged.Insert(i++, comparer.Compare(left[leftIndex], right[rightIndex]) < 0 ? left[leftIndex++] : right[rightIndex++]);
                }
                else
                {
                    while (leftIndex < left.Count)
                    {
                        merged.Insert(i++, left[leftIndex++]);
                    }

                    while (rightIndex < right.Count)
                    {
                        merged.Insert(i++, right[rightIndex++]);
                    }
                }
            }

            return merged;
        }
    }
}
