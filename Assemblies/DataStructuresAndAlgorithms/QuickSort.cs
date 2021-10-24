using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    public class QuickSort : ISortingAlgorithm
    {

        /// <summary>
        /// Sorts in place, modifies the original collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public IList<T> Sort<T>(IList<T> collection) where T : IComparable
        {
            return Sort(collection, Comparer<T>.Default);
        }

        /// <summary>
        /// Sorts in place, modifies the original collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public IList<T> Sort<T>(IList<T> collection, IComparer<T> comparer)
        {
            if (collection == null)
            {
                throw new ArgumentException("QuickSort.Sort() cannot sort null collection");
            }

            if (comparer == null)
            {
                throw new ArgumentException("QuickSort.Sort() comparer is required");
            }


            PerformQuickSort(collection, comparer, 0, collection.Count - 1);
            return collection;
        }

        /// <summary>
        /// Recursive, in place. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="comparer"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void PerformQuickSort<T>(IList<T> collection, IComparer<T> comparer, int start, int end)
        {
            if (end <= start + 1)
            {
                return;
            }

            int pivot = SelectPivot(collection, comparer, start, end);

            int left = start;
            int right = end;

            if (pivot != right)
            {
                collection.Swap(right, pivot);
            }

            pivot = right;
            right--;

            while (left <= right)
            {
                while (comparer.Compare(collection[left], collection[pivot]) <= 0 && left < pivot)
                {
                    left++;
                }

                while (comparer.Compare(collection[right], collection[pivot]) > 0 && right >= left)
                {
                    right--;
                }

                if (left < right)
                {
                    collection.Swap(left, right);
                }
            }


            collection.Swap(left, pivot);
            pivot = left;

            PerformQuickSort(collection, comparer, start, pivot - 1);
            PerformQuickSort(collection, comparer, pivot, end);
        }


        /// <summary>
        /// Chooses a pivot index using Median of 3. 
        /// Looks at the first, middle, and last elements and chooses the median
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        private int SelectPivot<T>(IList<T> collection, IComparer<T> comparer, int start, int end)
        {
            int middle = ((end - start) / 2) + start;

            if (comparer.Compare(collection[start], collection[middle]) > 0 ^ comparer.Compare(collection[start], collection[end]) > 0)
            {
                return start;
            }
            // Flipping the > to < here handles equality
            else if (comparer.Compare(collection[middle], collection[start]) < 0 ^ comparer.Compare(collection[middle], collection[end]) < 0)
            {
                return middle;
            }
            else
            {
                return end;
            }
        }
    }
}
