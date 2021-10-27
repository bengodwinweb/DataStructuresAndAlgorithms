using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    [TestFixture]
    class MinHeapTests
    {
        IMinHeap<T> GetMinHeap<T>()
        {
            return new Heap<T>();
        }

        [Test]
        public void TestEmptyCount()
        {
            var o = GetMinHeap<int>();
            Assert.AreEqual(0, o.Count); // no exceptions
        }

        [Test]
        public void TestRemoveEmpty()
        {
            var o = GetMinHeap<int>();
            Assert.Throws<ArgumentOutOfRangeException>(delegate { o.Remove(); });
        }

        [Test]
        public void TestAdd()
        {
            var o = GetMinHeap<int>();

            var nums = new int[] // 100 items with repeats
            {
                    57, 2,  9,  28, 6,
                    38, 65, 32, 56, 42,
                    89, 41, 86, 95, 62,
                    84, 95, 62, 61, 44,
                    89, 64, 89, 32, 16,
                    84, 100, 31, 51, 19,
                    25, 92, 36, 46, 17,
                    67, 94, 92, 1,  54,
                    64, 51, 2,  70, 59,
                    4,  95, 80, 80, 74,
                    68, 15, 66, 64, 66,
                    95, 42, 4,  39, 12,
                    73, 86, 30, 24, 65,
                    45, 59, 98, 59, 52,
                    25, 12, 73, 48, 53,
                    11, 24, 74, 25, 61,
                    71, 66, 9,  55, 68,
                    93, 65, 38, 75, 14,
                    25, 80, 80, 57, 21,
                    19, 43, 79, 95, 7,
            };

            for (int i = 0; i < nums.Length; i++)
            {
                Assert.AreEqual(i, o.Count);
                o.Add(nums[i]);
                Assert.AreEqual(i + 1, o.Count);
            }

            var quickSort = new QuickSort();
            var sortedNums = quickSort.Sort(nums);

            for (int i = 0; i < sortedNums.Count; i++)
            {
                Assert.AreEqual(sortedNums[i], o.Remove(), "Unexpected value at index: " + i);
            }

            Assert.Throws<ArgumentOutOfRangeException>(delegate { o.Remove(); }, "Collection should be empty, Remove() should throw exception");
        }

        [Test]
        public void TestSortLargeStringCollection()
        {
            var o = GetMinHeap<string>();

            DateTime addStart = DateTime.UtcNow;
            foreach (var s in StringSource.Instance.UnsortedArray)
            {
                o.Add(s);
            }
            Console.WriteLine("Add 10,000 strings to heap: {0:N0} ms", (DateTime.UtcNow - addStart).TotalMilliseconds);

            DateTime removeStart = DateTime.UtcNow;
            foreach (var s in StringSource.Instance.SortedArray)
            {
                Assert.IsTrue(o.TryRemove(out string heapTop));
                Assert.AreEqual(s, heapTop);
            }
            Console.WriteLine("Remove and test 10,000 strings from heap: {0:N0} ms", (DateTime.UtcNow - removeStart).TotalMilliseconds);

            Assert.IsFalse(o.TryRemove(out string ignore));
        }


    }
}
