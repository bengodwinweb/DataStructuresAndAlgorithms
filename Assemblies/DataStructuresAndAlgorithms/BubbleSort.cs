using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{

    public class BubbleSort : ISortingAlgorithm
    {

        public IList<T> Sort<T>(IList<T> array) where T : IComparable
        {
            return Sort(array, Comparer<T>.Default);
        }

        public IList<T> Sort<T>(IList<T> array, IComparer<T> comparer)
        {
            if (array is null)
            {
                throw new ArgumentException("BubbleSort.Sort() array cannot be null");
            }

            if (comparer is null)
            {
                throw new ArgumentException("BubbleSort.Sort() comparer cannot be null");
            }

            for (int i = array.Count - 1; i > 0; i--)
            {
                bool madeChange = false;

                for (int j = 0; j < i; j++)
                {
                    if (comparer.Compare(array[j], array[j + 1]) > 0) // check if the current item is larger than the next
                    {
                        // if so, swap the items
                        array.Swap<T>(j, j + 1);

                        madeChange = true;
                    }

                }

                if (!madeChange)
                {
                    break; // exit if the list is sorted earlier than the worst case
                }
            }

            return array;
        }


    }
}
