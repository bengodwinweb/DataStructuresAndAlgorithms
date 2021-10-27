using System;

namespace DataStructuresAndAlgorithms
{
    public interface IPriorityQueue<K, T>
    {

        /// <summary>
        /// Adds the item to the collection
        /// </summary>
        /// <param name="item"></param>
        void Enqueue(K priority, T item);

        /// <summary>
        /// Returns the smallest value in the collection
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        T Dequeue();

        /// <summary>
        /// Returns the smallest value in the collection if available, or false if the collection is empty
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool TryDequeue(out T item);

        /// <summary>
        /// The size of the collection
        /// </summary>
        int Count { get; }
    }
}
