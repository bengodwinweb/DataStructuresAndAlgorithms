using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    public class AvlTree<T> : ICollection<T>
    {
        internal class AvlNode<U>
        {
            public U Contents { get; set; }

            public AvlNode<U> Left { get; set; }

            public AvlNode<U> Right { get; set; }

            public override string ToString()
            {
                return Contents.ToString();
            }
        }

        private IComparer<T> comparer { get; set; }

        public AvlTree() : this(Comparer<T>.Default) { }

        public AvlTree(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        internal AvlNode<T> Root { get; set; }

        private int size { get; set; }

        public int Count => size;

        public bool IsReadOnly => false;

        #region Rotations
        private AvlNode<T> RightRotate(AvlNode<T> node)
        {
            AvlNode<T> newRoot = node.Left;
            node.Left = newRoot.Right;
            newRoot.Right = node;
            return newRoot;
        }

        private AvlNode<T> RightLeftRotate(AvlNode<T> node)
        {
            var newRoot = node.Right.Left;
            node.Right.Left = newRoot.Right;
            newRoot.Right = node.Right;
            node.Right = newRoot.Left;
            newRoot.Left = node;
            return newRoot;
        }

        private AvlNode<T> LeftRotate(AvlNode<T> node)
        {
            AvlNode<T> newRoot = node.Right;
            node.Right = newRoot.Left;
            newRoot.Left = node;
            return newRoot;
        }

        private AvlNode<T> LeftRightRotate(AvlNode<T> node)
        {
            AvlNode<T> newRoot = node.Left.Right;
            node.Left.Right = newRoot.Left;
            newRoot.Left = node.Left;
            node.Left = newRoot.Right;
            newRoot.Right = node;
            return newRoot;
        }
        #endregion 

        internal int BalanceFactor(AvlNode<T> node)
        {
            return Height(node.Left) - Height(node.Right);
        }

        internal static int Height(AvlNode<T> node)
        {
            if (node is null)
            {
                return -1;
            }

            return Math.Max(Height(node.Left), Height(node.Right)) + 1;
        }

        public void Add(T item)
        {
            Root = Add(item, Root);
        }

        private AvlNode<T> Add(T item, AvlNode<T> node)
        {
            if (node is null)
            {
                size++;
                return new AvlNode<T>() { Contents = item };
            }

            int comparison = comparer.Compare(item, node.Contents);

            if (comparison == 0) // if the item is equal to the current node, we are trying to add something that is already present
            {
                throw new InvalidOperationException("Cannot add item to AvlTree, value already present");
            }
            else if (comparison < 0)
            {
                node.Left = Add(item, node.Left);

                // Fix balance
                if (Math.Abs(BalanceFactor(node)) > 1)
                {
                    if (Height(node.Left.Left) > Height(node.Left.Right))
                    {
                        node = RightRotate(node);
                    }
                    else
                    {
                        node = LeftRightRotate(node);
                    }
                }
            }
            else
            {
                node.Right = Add(item, node.Right);

                // Fix balance
                if (Math.Abs(BalanceFactor(node)) > 1)
                {
                    if (Height(node.Right.Right) > Height(node.Right.Left))
                    {
                        node = LeftRotate(node);
                    }
                    else
                    {
                        node = RightLeftRotate(node);
                    }
                }
            }


            return node;
        }

        public void Clear()
        {
            Root = null;
            size = 0;
        }

        public bool Contains(T item)
        {
            return Contains(item, Root);
        }

        internal bool Contains(T item, AvlNode<T> root)
        {
            return GetNode(item, Root) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException("AvlTree.CopyTo() called with null array");
            }
            else if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("AvlTree.CopyTo() called with negative arrayIndex");
            }
            else if (arrayIndex + size > array.Length)
            {
                throw new ArgumentException("AvlTree.CopyTo(), not enough capacity in array");
            }

            Traverse((item) =>
            {
                array[arrayIndex++] = item;
            }, Root);
        }

        public bool Remove(T item)
        {
            try
            {
                Root = Remove(item, Root);
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal AvlNode<T> Remove(T item, AvlNode<T> root)
        {
            if (root == null)
            {
                throw new InvalidOperationException("Item not found in AVL tree");
            }

            int comparison = comparer.Compare(item, root.Contents);

            if (comparison == 0)
            {
                if (root.Left == null && root.Right == null)
                {
                    root = null;
                }
                else if (root.Right == null)
                {
                    root = root.Left;
                }
                else if (root.Left == null || root.Right.Left == null)
                {
                    root.Right.Left = root.Left;
                    root = root.Right;
                }
                else if (root.Left.Right == null)
                {
                    root.Left.Right = root.Right;
                    root = root.Left;
                }
                else
                {
                    // swap the contents with the left-most child of the right node
                    root.Contents = GetLeftmost(root.Right).Contents;

                    // find the parent of the swap source, and set its left child to the swapped source's right
                    var node = root.Right;
                    while (comparer.Compare(node.Left.Contents, root.Contents) != 0)
                    {
                        node = node.Left;
                    }
                    node.Left = node.Left.Right;
                }

                size--;
            }
            else if (comparison < 0)
            {
                root.Left = Remove(item, root.Left);

                // Fix imbalance
                if (Math.Abs(BalanceFactor(root)) > 1)
                {
                    if (Height(root.Left.Left) > Height(root.Left.Right))
                    {
                        root = LeftRotate(root);
                    }
                    else
                    {
                        root = LeftRightRotate(root);
                    }
                }
            }
            else
            {
                root.Right = Remove(item, root.Right);

                // Fix imbalance
                if (Math.Abs(BalanceFactor(root)) > 1)
                {
                    if (Height(root.Right.Right) > Height(root.Right.Left))
                    {
                        root = RightRotate(root);
                    }
                    else
                    {
                        root = RightLeftRotate(root);
                    }
                }
            }

            return root;
        }

        private AvlNode<T> GetLeftmost(AvlNode<T> node)
        {
            if (node.Left is null)
            {
                return node;
            }

            return GetLeftmost(node.Left);
        }

        private AvlNode<T> GetRightMost(AvlNode<T> node)
        {
            if (node.Right is null)
            {
                return node;
            }

            return GetRightMost(node);
        }

        internal AvlNode<T> GetNode(T item, AvlNode<T> node)
        {
            if (node is null)
            {
                return null;
            }

            int comparison = comparer.Compare(item, node.Contents);

            if (comparison == 0)
            {
                return node;
            }
            else if (comparison < 0)
            {
                return GetNode(item, node.Left);
            }
            else
            {
                return GetNode(item, node.Right);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var list = new List<T>(size);
            Traverse((item) =>
            {
                list.Add(item);
            }, Root);

            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var array = new T[size];
            CopyTo(array, 0);
            return array.GetEnumerator();
        }


        internal delegate void TraversalDelegate(T item);

        internal void Traverse(TraversalDelegate del, AvlNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            Traverse(del, node.Left);
            del(node.Contents);
            Traverse(del, node.Right);
        }

        internal delegate void NodeTraversalDelegate(AvlNode<T> node);
        internal void TraverseNodes(NodeTraversalDelegate del, AvlNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            if (node.Left != null)
            {
                TraverseNodes(del, node.Left);
            }

            del(node);

            if (node.Right != null)
            {
                TraverseNodes(del, node.Right);
            }
        }
    }
}
