using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{


    public class Heap<T> : IMinHeap<T>
    {
        private T[] array;

        private readonly IComparer<T> comparer;

        private int size;

        public Heap() : this(Comparer<T>.Default) { }

        public Heap(IComparer<T> comparer)
        {
            this.comparer = comparer;
            array = new T[0];
        }

        public int Count => size;

        private void EnsureCapacity()
        {
            if (size == array.Length)
            {
                var newArray = new T[array.Length == 0 ? 4 : array.Length * 2];
                Array.Copy(array, newArray, array.Length);
                array = newArray;
            }
        }

        public void Add(T item)
        {
            EnsureCapacity();

            array[size] = item;
            HeapifyUp(size);
            size++;
        }

        public T Remove()
        {
            if (size == 0)
            {
                throw new ArgumentOutOfRangeException("Unable to remove from empty Heap");
            }

            var item = array[0];

            array.Swap(0, size - 1);
            array[size - 1] = default(T);

            size--;

            HeapifyDown(0);
            return item;
        }

        public bool TryRemove(out T item)
        {
            if (size == 0)
            {
                item = default(T);
                return false;
            }

            item = Remove();
            return true;
        }

        private void HeapifyDown(int index)
        {
            int leftIndex = LeftChild(index);
            int rightIndex = RightChild(index);

            if (leftIndex < size && rightIndex < size)
            {
                int smallerChild = comparer.Compare(array[leftIndex], array[rightIndex]) <= 0 ? leftIndex : rightIndex;
                if (comparer.Compare(array[index], array[smallerChild]) > 0)
                {
                    array.Swap(index, smallerChild);
                    HeapifyDown(smallerChild);
                }
            }
            else if (leftIndex < size)
            {
                if (comparer.Compare(array[index], array[leftIndex]) > 0)
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
            if (comparer.Compare(array[index], array[parent]) < 0)
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

    }
}
