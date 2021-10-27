using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    [TestFixture]
    class AvlTreeTests
    {

        private ICollection<T> GetCollection<T>()
        {
            return new AvlTree<T>();
        }

        [Test]
        public void TestIsReadonly()
        {
            Assert.IsFalse(GetCollection<int>().IsReadOnly);
        }

        [Test]
        public void TestAddDuplicate()
        {
            var o = GetCollection<int>();

            var nums = new int[] // 100 items with no repeats
            {
                240, 799, 533, 672, 662,
                732, 863, 541, 646, 578,
                700, 339, 926, 324, 381,
                905, 454, 697, 298, 69 ,
                900, 318, 318, 435, 88 ,
                562, 300, 429, 487, 424,
                952, 526, 467, 779, 591,
                941, 252, 701, 870, 604,
                256, 19 , 376, 542, 284,
                548, 135, 815, 390, 776,
                616, 917, 4, 569, 319,
                265, 843, 539, 756, 918,
                411, 247, 148, 355, 823,
                617, 480, 253, 546, 660,
                853, 21 , 292, 536, 865,
                828, 581, 810, 894, 720,
                805, 913, 51 , 639, 16 ,
                178, 698, 285, 977, 76 ,
                789, 929, 928, 455, 899,
                428, 432, 184, 216, 262,
            };

            int i = 0;
            for (; i < 22; i++)
            {
                Assert.AreEqual(i, o.Count);
                o.Add(nums[i]);
                Assert.AreEqual(i + 1, o.Count);
            }

            Assert.Throws<InvalidOperationException>(delegate { o.Add(nums[i]); });
        }

        [Test]
        public void TestAdd()
        {
            var o = GetCollection<int>();

            var nums = new int[] // 100 items with no repeats
            {
                240, 799, 533, 672, 662,
                732, 863, 541, 646, 578,
                700, 339, 926, 324, 381,
                905, 454, 697, 298, 69 ,
                900, 318, 882, 435, 88 ,
                562, 300, 429, 487, 424,
                952, 526, 467, 779, 591,
                941, 252, 701, 870, 604,
                256, 19 , 376, 542, 284,
                548, 135, 815, 390, 776,
                616, 917, 4, 569, 319,
                265, 843, 539, 756, 918,
                411, 247, 148, 355, 823,
                617, 480, 253, 546, 660,
                853, 21 , 292, 536, 865,
                828, 581, 810, 894, 720,
                805, 913, 51 , 639, 16 ,
                178, 698, 285, 977, 76 ,
                789, 929, 928, 455, 899,
                428, 432, 184, 216, 262,
            };

            for (int i = 0; i < nums.Length; i++)
            {
                Assert.AreEqual(i, o.Count);
                o.Add(nums[i]);
                Assert.AreEqual(i + 1, o.Count);
            }

            var tree = o as AvlTree<int>;
            Assert.IsTrue(AvlTree<int>.Height(tree.Root) <= 1.44 * Math.Log(nums.Length, 2));

            var copy = new int[nums.Length];
            o.CopyTo(copy, 0);

            Array.Sort(nums);

            Assert.IsTrue(Enumerable.SequenceEqual(nums, copy));
        }

        [Test]
        public void TestRemoveRightLeftBalance()
        {
            var o = GetCollection<int>();

            var nums = new int[] // 100 items with no repeats
            {
                240, 799, 533, 672, 662,
                732, 863, 541, 646, 578,
                700, 339, 926, 324, 381,
                905, 454, 697, 298, 69 ,
                900, 318, 882, 435, 88 ,
                562, 300, 429, 487, 424,
                952, 526, 467, 779, 591,
                941, 252, 701, 870, 604,
                256, 19 , 376, 542, 284,
                548, 135, 815, 390, 776,
                616, 917, 4, 569, 319,
                265, 843, 539, 756, 918,
                411, 247, 148, 355, 823,
                617, 480, 253, 546, 660,
                853, 21 , 292, 536, 865,
                828, 581, 810, 894, 720,
                805, 913, 51 , 639, 16 ,
                178, 698, 285, 977, 76 ,
                789, 929, 928, 455, 899,
                428, 432, 184, 216, 262,
            };

            for (int i = 0; i < nums.Length; i++)
            {
                Assert.AreEqual(i, o.Count);
                o.Add(nums[i]);
                Assert.AreEqual(i + 1, o.Count);
            }

            var tree = o as AvlTree<int>;
            Assert.IsTrue(AvlTree<int>.Height(tree.Root) <= 1.44 * Math.Log(nums.Length, 2));
            tree.TraverseNodes((n) =>
            {
                Assert.IsTrue(tree.BalanceFactor(n) <= 1);
            }, tree.Root);
            TestIsBinaryTree(tree.Root, 0, 1000);

            o.Add(543);
            var node = tree.GetNode(542, tree.Root);

            PrintTree(tree);

            Assert.IsTrue(o.Remove(541));
            Assert.IsFalse(o.Contains(541));
            Assert.IsTrue(o.Contains(543));

            Assert.IsTrue(AvlTree<int>.Height(tree.Root) <= 1.44 * Math.Log(nums.Length, 2));
            tree.TraverseNodes((n) =>
            {
                Assert.IsTrue(tree.BalanceFactor(n) <= 1);
            }, tree.Root);
            TestIsBinaryTree(tree.Root, 0, 1000);
        }

        [Test]
        public void TestRemoveLeaf()
        {
            var o = GetCollection<int>();

            var nums = new int[] // 100 items with no repeats
            {
                240, 799, 533, 672, 662,
                732, 863, 541, 646, 578,
                700, 339, 926, 324, 381,
                905, 454, 697, 298, 69 ,
                900, 318, 882, 435, 88 ,
                562, 300, 429, 487, 424,
                952, 526, 467, 779, 591,
                941, 252, 701, 870, 604,
                256, 19 , 376, 542, 284,
                548, 135, 815, 390, 776,
                616, 917, 4, 569, 319,
                265, 843, 539, 756, 918,
                411, 247, 148, 355, 823,
                617, 480, 253, 546, 660,
                853, 21 , 292, 536, 865,
                828, 581, 810, 894, 720,
                805, 913, 51 , 639, 16 ,
                178, 698, 285, 977, 76 ,
                789, 929, 928, 455, 899,
                428, 432, 184, 216, 262,
            };

            for (int i = 0; i < nums.Length; i++)
            {
                Assert.AreEqual(i, o.Count);
                o.Add(nums[i]);
                Assert.AreEqual(i + 1, o.Count);
            }

            var tree = o as AvlTree<int>;
            Assert.IsTrue(AvlTree<int>.Height(tree.Root) <= 1.44 * Math.Log(nums.Length, 2));
            tree.TraverseNodes((n) =>
            {
                Assert.IsTrue(tree.BalanceFactor(n) <= 1);
            }, tree.Root);
            TestIsBinaryTree(tree.Root, 0, 1000);

            o.Add(543);
            var node = tree.GetNode(542, tree.Root);

            Assert.IsTrue(o.Remove(543));
            Assert.IsFalse(o.Contains(543));

            Assert.IsTrue(AvlTree<int>.Height(tree.Root) <= 1.44 * Math.Log(nums.Length, 2));
            tree.TraverseNodes((n) =>
            {
                Assert.IsTrue(tree.BalanceFactor(n) <= 1);
            }, tree.Root);
            TestIsBinaryTree(tree.Root, 0, 1000);
        }

        void PrintTree<T>(AvlTree<T> tree)
        {
            Queue<AvlTree<T>.AvlNode<T>> nodes = new Queue<AvlTree<T>.AvlNode<T>>();
            AvlTree<T>.AvlNode<T> endOfLine = new AvlTree<T>.AvlNode<T>();

            nodes.Enqueue(tree.Root);
            nodes.Enqueue(endOfLine);

            bool seenNonNull = false;
            List<string> strings = new List<string>() { "" };

            AddBreadthfirst(nodes, seenNonNull, endOfLine, strings);

            foreach (var line in strings)
            {
                Console.WriteLine(line);
            }
        }

        void AddBreadthfirst<T>(Queue<AvlTree<T>.AvlNode<T>> queue, bool seenNonNull, AvlTree<T>.AvlNode<T> endOfLine, List<string> strings)
        {
            if (queue.Count == 0)
            {
                return;
            }

            var node = queue.Dequeue();

            if (ReferenceEquals(endOfLine, node))
            {
                if (seenNonNull && queue.Count > 0)
                {
                    strings.Add("");

                    queue.Enqueue(endOfLine);
                    seenNonNull = false;
                }
                else
                {
                    strings.RemoveAt(strings.Count - 1);
                    return;
                }
            }
            else if (node is null)
            {
                if (string.IsNullOrEmpty(strings[strings.Count - 1]))
                {
                    strings[strings.Count - 1] = "[___]";
                }
                else
                {
                    strings[strings.Count - 1] += ", [___]";
                }

                queue.Enqueue(null);
                queue.Enqueue(null);
            }
            else
            {
                seenNonNull = true;
                queue.Enqueue(node.Left);
                queue.Enqueue(node.Right);

                if (string.IsNullOrEmpty(strings[strings.Count - 1]))
                {
                    strings[strings.Count - 1] = string.Format("[{0:000}]", node.Contents);
                }
                else
                {
                    strings[strings.Count - 1] += string.Format(", [{0:000}]", node.Contents);
                }
            }

            AddBreadthfirst(queue, seenNonNull, endOfLine, strings);
        }

        void TestIsBinaryTree(AvlTree<int>.AvlNode<int> node, int min, int max)
        {
            if (node is null)
            {
                return;
            }

            Assert.IsTrue(node.Contents >= min && node.Contents <= max);

            TestIsBinaryTree(node.Left, min, node.Contents);
            TestIsBinaryTree(node.Right, node.Contents, max);
        }

        [Test]
        public void TestGetEnumerator()
        {
            var o = GetCollection<int>();

            o.Add(1);
            o.Add(3);
            o.Add(2);

            var enumerator = o.GetEnumerator();

            var nums = new int[] { 1, 2, 3 };
            int i = 0;

            foreach (var item in o)
            {
                Assert.AreEqual(nums[i++], item);
            }
        }

        [Test]
        public void TestClear()
        {
            var o = GetCollection<int>();

            o.Add(1);
            o.Add(3);
            o.Add(2);

            Assert.AreEqual(3, o.Count);

            o.Clear();

            Assert.AreEqual(0, o.Count);

            var enumerator = o.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void TestCopyTo()
        {
            var o = GetCollection<int>();

            o.Add(1);
            o.Add(3);
            o.Add(2);

            var array = new int[5];
            o.CopyTo(array, 2);

            Assert.AreEqual(0, array[0]);
            Assert.AreEqual(0, array[1]);
            Assert.AreEqual(1, array[2]);
            Assert.AreEqual(2, array[3]);
            Assert.AreEqual(3, array[4]);
        }


        [Test]
        public void TestCopyToNull()
        {
            var o = GetCollection<int>();

            o.Add(1);
            o.Add(3);
            o.Add(2);

            Assert.Throws<ArgumentNullException>(delegate { o.CopyTo(null, 0); });
        }

        [Test]
        public void TestCopyToNeg1()
        {
            var o = GetCollection<int>();

            o.Add(1);
            o.Add(3);
            o.Add(2);

            var array = new int[3];
            Assert.Throws<ArgumentOutOfRangeException>(delegate { o.CopyTo(array, -1); });
        }

        [Test]
        public void TestCopyToOutOfRange()
        {
            var o = GetCollection<int>();

            o.Add(1);
            o.Add(3);
            o.Add(2);

            var array = new int[3];
            Assert.Throws<ArgumentException>(delegate { o.CopyTo(array, 1); });
        }

        [Test]
        public void TestContainsInt()
        {
            var o = GetCollection<int>();

            o.Add(1);
            o.Add(3);
            o.Add(2);

            Assert.IsTrue(o.Contains(1));
            Assert.IsTrue(o.Contains(2));
            Assert.IsTrue(o.Contains(3));
            Assert.IsFalse(o.Contains(4));
        }

        [Test]
        public void TestContainsString()
        {
            var o = GetCollection<string>();

            o.Add("Red");
            o.Add("Blue");
            o.Add("Green");

            Assert.IsTrue(o.Contains("Red"));
            Assert.IsTrue(o.Contains("Blue"));
            Assert.IsTrue(o.Contains("Green"));
            Assert.IsFalse(o.Contains("Orange"));
            Assert.IsFalse(o.Contains("green"));
        }

        [Test]
        public void TestContainsPerson()
        {
            var o = GetCollection<Person>();

            var bob = new Person() { FirstName = "Bob", LastName = "Jones" };
            var tiffany = new Person() { FirstName = "Tiffany", LastName = "Smith" };
            var eric = new Person() { FirstName = "Eric", LastName = "Andre" };

            o.Add(bob);
            o.Add(tiffany);
            o.Add(eric);

            Assert.IsTrue(o.Contains(bob));
            Assert.IsTrue(o.Contains(tiffany));
            Assert.IsTrue(o.Contains(eric));

            Assert.IsFalse(o.Contains(new Person() { FirstName = "Bob", LastName = "Saget" }));
            Assert.IsTrue(o.Contains(new Person() { FirstName = "Bob", LastName = "Jones" })); // match by equality
        }

        [Test]
        public void TestRemoveInt()
        {
            var o = GetCollection<int>();

            o.Add(1);
            o.Add(3);
            o.Add(2);

            Assert.AreEqual(3, o.Count);

            Assert.IsFalse(o.Remove(4));
            Assert.AreEqual(3, o.Count);

            Assert.IsTrue(o.Remove(1));
            Assert.AreEqual(2, o.Count);

            var array = new int[2];
            o.CopyTo(array, 0);
            Assert.AreEqual(2, array[0]);
            Assert.AreEqual(3, array[1]);

            Assert.IsTrue(o.Remove(3));
            Assert.AreEqual(1, o.Count);

            Assert.IsTrue(o.Remove(2));
            Assert.AreEqual(0, o.Count);
        }

        [Test]
        public void TestRemoveString()
        {
            var colors = new string[] { "Red", "Blue", "Orange", "Yellow", "Green", "Magenta" };

            var o = GetCollection<string>();

            foreach (var color in colors)
            {
                o.Add(color);
            }

            Assert.AreEqual(6, o.Count);

            Assert.IsTrue(o.Remove("Orange"));
            Assert.AreEqual(5, o.Count);

            var arr = new string[5];
            o.CopyTo(arr, 0);
            Assert.AreEqual("Blue", arr[0]);
            Assert.AreEqual("Green", arr[1]);
            Assert.AreEqual("Magenta", arr[2]);
            Assert.AreEqual("Red", arr[3]);
            Assert.AreEqual("Yellow", arr[4]);

            Assert.IsFalse(o.Remove("Gold"));
            Assert.AreEqual(5, o.Count);
        }

        [Test]
        public void TestRemovePerson()
        {
            var o = GetCollection<Person>();

            var bob = new Person() { FirstName = "Bob", LastName = "Jones" };
            var tiffany = new Person() { FirstName = "Tiffany", LastName = "Smith" };
            var eric = new Person() { FirstName = "Eric", LastName = "Andre" };

            o.Add(bob);
            o.Add(tiffany);
            o.Add(eric);

            Assert.AreEqual(3, o.Count);

            Assert.IsTrue(o.Remove(eric));
            Assert.AreEqual(2, o.Count);

            var arr = new Person[2];
            o.CopyTo(arr, 0);
            Assert.AreSame(bob, arr[0]);
            Assert.AreSame(tiffany, arr[1]);

            Assert.IsTrue(o.Remove(new Person() { FirstName = "Bob", LastName = "Jones" })); // uses equality for match, not reference
            Assert.IsFalse(o.Remove(eric));
            Assert.IsFalse(o.Remove(new Person() { FirstName = "Michael", LastName = "Jordan" }));

            Assert.AreEqual(1, o.Count);
            o.CopyTo(arr, 0);
            Assert.AreSame(tiffany, arr[0]);
        }

        [Test]
        public void TestSortStrings()
        {
            var o = GetCollection<string>();

            var strings = StringSource.Instance.UnsortedArray;

            var start = DateTime.UtcNow;
            foreach (var s in strings)
            {
                o.Add(s);
            }
            Console.WriteLine("AVL tree, added 10,000 strings in: {0:N0} ms", (DateTime.UtcNow - start).TotalMilliseconds);

            Assert.AreEqual(10_000, o.Count);

            var sortedArray = new string[10_000];
            o.CopyTo(sortedArray, 0);

            for (int i = 0; i < 10_000; i++)
            {
                Assert.AreEqual(sortedArray[i], StringSource.Instance.SortedArray[i], "Difference at index " + i);
            }

            var tree = o as AvlTree<string>;
            Assert.IsTrue(AvlTree<string>.Height(tree.Root) <= 1.44 * Math.Log(10_000, 2));
        }
    }
}
