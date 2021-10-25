using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    abstract class CollectionTests
    {

        protected abstract ICollection<T> GetCollection<T>();

        [Test]
        public void TestIsReadonly()
        {
            Assert.IsFalse(GetCollection<int>().IsReadOnly);
        }

        [Test]
        public void TestAdd()
        {
            var list = GetCollection<int>();

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
                Assert.AreEqual(i, list.Count);
                list.Add(nums[i]);
                Assert.AreEqual(i + 1, list.Count);
            }
        }

        [Test]
        public void TestGetEnumerator()
        {
            var list = GetCollection<int>();

            list.Add(1);
            list.Add(3);
            list.Add(2);

            var enumerator = list.GetEnumerator();

            Assert.Throws<ArgumentOutOfRangeException>(delegate { int n = enumerator.Current; });

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(1, enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(3, enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(2, enumerator.Current);

            Assert.IsFalse(enumerator.MoveNext());
            Assert.Throws<ArgumentOutOfRangeException>(delegate { int n = enumerator.Current; });
        }

        [Test]
        public void TestClear()
        {
            var list = GetCollection<int>();

            list.Add(1);
            list.Add(3);
            list.Add(2);

            Assert.AreEqual(3, list.Count);

            list.Clear();

            Assert.AreEqual(0, list.Count);

            var enumerator = list.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void TestCopyTo()
        {
            var list = GetCollection<int>();

            list.Add(1);
            list.Add(3);
            list.Add(2);

            var array = new int[5];
            list.CopyTo(array, 2);

            Assert.AreEqual(0, array[0]);
            Assert.AreEqual(0, array[1]);
            Assert.AreEqual(1, array[2]);
            Assert.AreEqual(3, array[3]);
            Assert.AreEqual(2, array[4]);
        }


        [Test]
        public void TestCopyToNull()
        {
            var list = GetCollection<int>();

            list.Add(1);
            list.Add(3);
            list.Add(2);

            Assert.Throws<ArgumentNullException>(delegate { list.CopyTo(null, 0); });
        }

        [Test]
        public void TestCopyToNeg1()
        {
            var list = GetCollection<int>();

            list.Add(1);
            list.Add(3);
            list.Add(2);

            var array = new int[3];
            Assert.Throws<ArgumentOutOfRangeException>(delegate { list.CopyTo(array, -1); });
        }

        [Test]
        public void TestCopyToOutOfRange()
        {
            var list = GetCollection<int>();

            list.Add(1);
            list.Add(3);
            list.Add(2);

            var array = new int[3];
            Assert.Throws<ArgumentException>(delegate { list.CopyTo(array, 1); });
        }

        [Test]
        public void TestContainsInt()
        {
            var list = GetCollection<int>();

            list.Add(1);
            list.Add(3);
            list.Add(2);

            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(list.Contains(2));
            Assert.IsTrue(list.Contains(3));
            Assert.IsFalse(list.Contains(4));
        }

        [Test]
        public void TestContainsString()
        {
            var list = GetCollection<string>();

            list.Add("Red");
            list.Add("Blue");
            list.Add("Green");

            Assert.IsTrue(list.Contains("Red"));
            Assert.IsTrue(list.Contains("Blue"));
            Assert.IsTrue(list.Contains("Green"));
            Assert.IsFalse(list.Contains("Orange"));
            Assert.IsFalse(list.Contains("green"));
        }

        [Test]
        public void TestContainsPerson()
        {
            var list = GetCollection<Person>();

            var bob = new Person() { FirstName = "Bob", LastName = "Jones" };
            var tiffany = new Person() { FirstName = "Tiffany", LastName = "Smith" };
            var eric = new Person() { FirstName = "Eric", LastName = "Andre" };

            list.Add(bob);
            list.Add(tiffany);
            list.Add(eric);

            Assert.IsTrue(list.Contains(bob));
            Assert.IsTrue(list.Contains(tiffany));
            Assert.IsTrue(list.Contains(eric));

            Assert.IsFalse(list.Contains(new Person() { FirstName = "Bob", LastName = "Saget" }));
            Assert.IsFalse(list.Contains(new Person() { FirstName = "Bob", LastName = "Jones" }));
        }

        [Test]
        public void TestRemoveInt()
        {
            var list = GetCollection<int>();

            list.Add(1);
            list.Add(3);
            list.Add(2);

            Assert.AreEqual(3, list.Count);

            Assert.IsFalse(list.Remove(4));
            Assert.AreEqual(3, list.Count);

            Assert.IsTrue(list.Remove(1));
            Assert.AreEqual(2, list.Count);

            var array = new int[2];
            list.CopyTo(array, 0);
            Assert.AreEqual(3, array[0]);
            Assert.AreEqual(2, array[1]);

            Assert.IsTrue(list.Remove(3));
            Assert.AreEqual(1, list.Count);

            Assert.IsTrue(list.Remove(2));
            Assert.AreEqual(0, list.Count);
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
            Assert.AreEqual("Red", arr[0]);
            Assert.AreEqual("Blue", arr[1]);
            Assert.AreEqual("Yellow", arr[2]);
            Assert.AreEqual("Green", arr[3]);
            Assert.AreEqual("Magenta", arr[4]);

            Assert.IsFalse(o.Remove("Gold"));
            Assert.AreEqual(5, o.Count);
        }

        [Test]
        public void TestRemovePerson()
        {
            var list = GetCollection<Person>();

            var bob = new Person() { FirstName = "Bob", LastName = "Jones" };
            var tiffany = new Person() { FirstName = "Tiffany", LastName = "Smith" };
            var eric = new Person() { FirstName = "Eric", LastName = "Andre" };

            list.Add(bob);
            list.Add(tiffany);
            list.Add(eric);

            Assert.AreEqual(3, list.Count);

            Assert.IsTrue(list.Remove(eric));
            Assert.AreEqual(2, list.Count);

            var arr = new Person[2];
            list.CopyTo(arr, 0);
            Assert.AreSame(bob, arr[0]);
            Assert.AreSame(tiffany, arr[1]);

            Assert.IsFalse(list.Remove(new Person() { FirstName = "Bob", LastName = "Jones" }));
            Assert.IsFalse(list.Remove(eric));
            Assert.IsFalse(list.Remove(new Person() { FirstName = "Michael", LastName = "Jordan" }));

            Assert.AreEqual(2, list.Count);
            list.CopyTo(arr, 0);
            Assert.AreSame(bob, arr[0]);
            Assert.AreSame(tiffany, arr[1]);
        }
    }
}
