using System;
using System.Collections.Generic;

namespace DataStructuresAndAlgorithms
{
    public interface ISortingAlgorithm
    {

        /// <summary>
        /// Sorts the list/array. May modify the original array or return a copy, depending on the implementation.
        /// Uses the default comparer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        IList<T> Sort<T>(IList<T> collection) where T : IComparable;

        /// <summary>
        /// Sorts the list/array. May modify the original array or return a copy, depending on the implementation.
        /// Uses the default comparer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        IList<T> Sort<T>(IList<T> collection, IComparer<T> comparer);
    }

}
