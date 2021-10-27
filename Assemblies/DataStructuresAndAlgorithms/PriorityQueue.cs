using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{


    public class PriorityQueue<K, T> : IPriorityQueue<K, T>
    {
        private KeyValuePair<K, T>[] array { get; set; }
        private readonly IComparer<K> _comparer;

        public PriorityQueue() : this(Comparer<K>.Default) { }

        public PriorityQueue(IComparer<K> comparer)
        {
            array = new KeyValuePair<K, T>[0];
            _comparer = comparer;
        }

        public int Count { get; private set; }

        private void EnsureCapacity()
        {
            if (Count == array.Length)
            {
                var newArray = new KeyValuePair<K, T>[array.Length == 0 ? 4 : array.Length * 2];
                Array.Copy(array, newArray, array.Length);
                array = newArray;
            }
        }

        public void Enqueue(K priority, T item)
        {
            EnsureCapacity();

            array[Count] = new KeyValuePair<K, T>(priority, item);
            HeapifyUp(Count);
            Count++;
        }

        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new ArgumentOutOfRangeException("Unable to remove from empty Heap");
            }

            var item = array[0].Value;

            array.Swap(0, Count - 1);
            array[Count - 1] = default(KeyValuePair<K, T>);

            Count--;

            HeapifyDown(0);
            return item;
        }

        public bool TryDequeue(out T item)
        {
            if (Count == 0)
            {
                item = default(T);
                return false;
            }

            item = Dequeue();
            return true;
        }


        #region Heap
        private void HeapifyDown(int index)
        {
            int leftIndex = LeftChild(index);
            int rightIndex = RightChild(index);

            if (leftIndex < Count && rightIndex < Count)
            {
                int smallerChild = _comparer.Compare(array[leftIndex].Key, array[rightIndex].Key) <= 0 ? leftIndex : rightIndex;
                if (_comparer.Compare(array[index].Key, array[smallerChild].Key) > 0)
                {
                    array.Swap(index, smallerChild);
                    HeapifyDown(smallerChild);
                }
            }
            else if (leftIndex < Count)
            {
                if (_comparer.Compare(array[index].Key, array[leftIndex].Key) > 0)
                {
                    array.Swap(index, leftIndex);
                }
            }
        }

        private void HeapifyUp(int index)
        {
            if (index == 0)
            {
                return;
            }

            int parent = Parent(index);
            if (_comparer.Compare(array[index].Key, array[parent].Key) < 0)
            {
                array.Swap(index, parent);
                HeapifyUp(parent);
            }
        }

        private int LeftChild(int index)
        {
            return index * 2 + 1;
        }

        private int RightChild(int index)
        {
            return index * 2 + 2;
        }

        private int Parent(int index)
        {
            var parent = index / 2;

            if (index % 2 == 0)
            {
                parent--;
            }

            return parent;
        }
        #endregion

    }
}
