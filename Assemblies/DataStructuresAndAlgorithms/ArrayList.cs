using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    public class ArrayList<T> : IList<T>, ICollection<T>
    {
        private readonly bool IsReferenceType;

        public ArrayList() : this(0) { }

        public ArrayList(int initialCapacity)
        {
            array = new T[initialCapacity];

            IsReferenceType = !typeof(T).IsValueType && typeof(T) != typeof(string);
        }

        private T[] array { get; set; }

        private int size { get; set; }

        void EnsureCapacity()
        {
            if (size == array.Length)
            {
                T[] larger = new T[array.Length == 0 ? 4 : array.Length * 2];
                Array.Copy(array, larger, size);
                array = larger;
            }
        }

        public int Count => size;

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= size)
                {
                    throw new ArgumentOutOfRangeException("index out of range");
                }

                return array[index];
            }
            set
            {
                if (index < 0 || index >= size)
                {
                    throw new ArgumentOutOfRangeException("index out of range");
                }

                array[index] = value;
            }
        }

        public void Add(T item)
        {
            EnsureCapacity();

            array[size++] = item;
        }

        public void Clear()
        {
            array = new T[0];
            size = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) > -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException("array is null");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex is less than 0");
            }

            if (size > array.Length - arrayIndex)
            {
                throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array");
            }

            Array.Copy(this.array, 0, array, arrayIndex, size);
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);

            if (index > -1)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException("index out of range");
            }

            for (int i = index; i < size - 1; i++) // move everything from index to the end one to the left
            {
                array[i] = array[i + 1];
            }

            array[size--] = default(T);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayListEnumerator(array, size);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ArrayListEnumerator(array, size);
        }

        public int IndexOf(T item)
        {
            int index = -1;

            for (int i = 0; i < size; i++)
            {
                if (IsReferenceType)
                {
                    if (ReferenceEquals(array[i], item))
                    {
                        index = i;
                        break;
                    }
                }
                else
                {
                    if (EqualityComparer<T>.Default.Equals(item, array[i]))
                    {
                        index = i;
                        break;
                    }
                }
            }

            return index;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > size) // can insert at size, same as Add()
            {
                throw new ArgumentOutOfRangeException("index out of range");
            }

            EnsureCapacity();

            for (int i = size; i > index; i--)
            {
                array[i] = array[i - 1];
            }

            size++;

            array[index] = item;
        }

        private class ArrayListEnumerator : IEnumerator<T>
        {
            private T[] array;
            private int size;
            private int position = -1;

            public ArrayListEnumerator(T[] array, int size)
            {
                this.array = array;
                this.size = size;
            }

            public T Current
            {
                get
                {
                    if (position < 0 || position >= size)
                    {
                        throw new ArgumentOutOfRangeException("position out of range");
                    }

                    return array[position];
                }
            }

            object IEnumerator.Current => Current;


            private bool disposed;
            public void Dispose()
            {
                if (!disposed)
                {
                    array = null;
                    disposed = true;
                }
            }

            public bool MoveNext()
            {
                position++;
                return position < size;
            }

            public void Reset()
            {
                position = -1;
            }
        }
    }
}
