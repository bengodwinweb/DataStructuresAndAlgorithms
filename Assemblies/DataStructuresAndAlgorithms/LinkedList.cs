using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    public class LinkedList<T> : ICollection<T>
    {
        private class LinkedListNode<U>
        {
            public U Contents { get; set; }

            public LinkedListNode<U> Next { get; set; }
        }

        private readonly bool isReferenceType;

        public LinkedList()
        {
            isReferenceType = !typeof(T).IsValueType && typeof(T) != typeof(string);
        }

        private LinkedListNode<T> First { get; set; }

        private LinkedListNode<T> Last { get; set; }

        private int size { get; set; }

        public int Count => size;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            var node = new LinkedListNode<T>() { Contents = item };

            if (size == 0)
            {
                First = node;
                Last = node;
            }
            else
            {
                Last.Next = node;
                Last = node;
            }

            size++;
        }

        public void Clear()
        {
            First = null;
            size = 0;
        }

        public bool Contains(T item)
        {
            foreach (var nodeContents in this)
            {
                if (ContentsMatch(nodeContents, item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException("LinkedList.CopyTo() array was null");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("LinkedList.CopyTo() arrayIndex must be positive");
            }

            if (arrayIndex + size > array.Length)
            {
                throw new ArgumentException("LinkedList.CopyTo() index out of range");
            }

            foreach (var item in this)
            {
                array[arrayIndex++] = item;
            }
        }

        public bool Remove(T item)
        {
            if (size == 0)
            {
                return false;
            }

            LinkedListNode<T> previous = null;
            var node = First;
            while (node != null)
            {
                if (ContentsMatch(node.Contents, item))
                {
                    if (node == First)
                    {
                        First = node.Next;
                        if (node == Last)
                        {
                            Last = First;
                        }
                    }
                    else
                    {
                        previous.Next = node.Next;

                        if (node == Last)
                        {
                            Last = previous;
                        }
                    }

                    size--;
                    return true;
                }

                previous = node;
                node = previous.Next;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator(First);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool ContentsMatch(T contents, T item)
        {
            if (isReferenceType)
            {
                return ReferenceEquals(contents, item);
            }
            else
            {
                return EqualityComparer<T>.Default.Equals(contents, item);
            }
        }

        private class LinkedListEnumerator : IEnumerator<T>
        {
            private LinkedListNode<T> first;

            private LinkedListNode<T> current;

            public LinkedListEnumerator(LinkedListNode<T> first)
            {
                this.first = first;

                current = null;
            }

            public T Current
            {
                get
                {
                    if (current == null)
                    {
                        throw new ArgumentOutOfRangeException("Current out of range. Ensure MoveNext() has been called before first use of Current");
                    }

                    return current.Contents;
                }
            }

            object IEnumerator.Current => Current;

            private bool disposed = false;
            public void Dispose()
            {
                if (!disposed)
                {
                    first = null;
                    current = null;
                }
            }

            public bool MoveNext()
            {
                if (current == null)
                {
                    current = first;
                }
                else
                {
                    current = current.Next;
                }

                return current != null;
            }

            public void Reset()
            {
                current = null;
            }
        }

    }
}
