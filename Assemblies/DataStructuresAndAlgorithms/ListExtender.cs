using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    public static class ListExtender
    {

        public static void Swap<T>(this IList<T> collection, int index1, int index2)
        {
            T temp = collection[index1];
            collection[index1] = collection[index2];
            collection[index2] = temp;
        }

        public static IList<T> Slice<T>(this IList<T> collection, int count)
        {
            return collection.Slice(0, count);
        }

        public static IList<T> Slice<T>(this IList<T> collection, int start, int count)
        {
            var slice = new T[count];

            for (int i = 0; i < count; i++)
            {
                slice[i] = collection[i + start];
            }

            return slice;
        }
    }
}
