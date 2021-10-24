using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    abstract class ListTests
    {

        protected abstract IList<T> GetList<T>();

        [Test]
        public void TestGetIndex()
        {
            var o = GetList<int>();

            o.Insert(0, 1);

            Assert.Throws<ArgumentOutOfRangeException>(delegate { int n = o[-1]; });

            Assert.AreEqual(1, o[0]);

            Assert.Throws<ArgumentOutOfRangeException>(delegate { int n = o[1]; });

            o.RemoveAt(0);

            Assert.Throws<ArgumentOutOfRangeException>(delegate { int n = o[0]; });
        }

        [Test]
        public void TestSetIndex()
        {
            var o = GetList<int>();

            o.Insert(0, 1);
            Assert.AreEqual(1, o[0]);

            Assert.Throws<ArgumentOutOfRangeException>(delegate { o[-1] = 5; });
            Assert.AreEqual(1, o[0]);

            o[0] = 5;
            Assert.AreEqual(5, o[0]);

            Assert.Throws<ArgumentOutOfRangeException>(delegate { o[1] = 6; });
            Assert.AreEqual(1, o.Count);
            Assert.AreEqual(5, o[0]);

            o.RemoveAt(0);
            Assert.AreEqual(0, o.Count);
            Assert.Throws<ArgumentOutOfRangeException>(delegate { o[0] = 1; });
        }

        [Test]
        public void TestInsert()
        {
            var o = GetList<int>();

            Assert.AreEqual(0, o.Count);

            o.Insert(0, 1);
            Assert.AreEqual(1, o.Count);

            o.Insert(1, 3);
            Assert.AreEqual(2, o.Count);

            o.Insert(0, 2);
            Assert.AreEqual(3, o.Count);

            Assert.AreEqual(2, o[0]);
            Assert.AreEqual(1, o[1]);
            Assert.AreEqual(3, o[2]);
        }

        [Test]
        public void TestInsertManyAtEnd()
        {
            var o = GetList<int>();

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
                o.Insert(o.Count, nums[i]);
                Assert.AreEqual(i + 1, o.Count);
            }

            for (int i = 0; i < nums.Length; i++)
            {
                Assert.AreEqual(nums[i], o[i]);
            }
        }

        [Test]
        public void TestInsertManyAtFront()
        {
            var o = GetList<int>();

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
                o.Insert(0, nums[i]);
                Assert.AreEqual(i + 1, o.Count);
            }

            for (int i = 0, j = nums.Length - 1; i < nums.Length; i++, j--)
            {
                Assert.AreEqual(nums[j], o[i]);
            }
        }

        [Test]
        public void TestIndexOfInt()
        {
            var o = GetList<int>();

            Assert.AreEqual(0, o.Count);

            o.Insert(0, 1);
            o.Insert(1, 3);
            o.Insert(0, 2);

            Assert.AreEqual(1, o.IndexOf(1));
            Assert.AreEqual(0, o.IndexOf(2));
            Assert.AreEqual(2, o.IndexOf(3));
            Assert.AreEqual(-1, o.IndexOf(4));

            o.Insert(0, 3);
            Assert.AreEqual(0, o.IndexOf(3));
        }

        [Test]
        public void TestIndexOfString()
        {
            var o = GetList<string>();

            string red = "Red";
            string blue = "Blue";
            string green = "Green";
            string yellow = "Yellow";
            string sapphire = "Sapphire";


            o.Insert(0, red);
            o.Insert(1, blue);
            o.Insert(2, green);
            o.Insert(3, yellow);
            o.Insert(4, sapphire);

            Assert.AreEqual(0, o.IndexOf("Red")); // checks equality for strings
            Assert.AreEqual(4, o.IndexOf(sapphire));
            Assert.AreEqual(-1, o.IndexOf("Goldenrod"));

            o.Insert(1, "Yellow");
            Assert.AreEqual(1, o.IndexOf(yellow));
        }

        [Test]
        public void TestIndexOfPerson()
        {
            var o = GetList<Person>();

            var bob = new Person() { FirstName = "Bob", LastName = "Jones" };
            var tiffany = new Person() { FirstName = "Tiffany", LastName = "Smith" };
            var eric = new Person() { FirstName = "Eric", LastName = "Andre" };

            o.Insert(0, bob);
            o.Insert(1, tiffany);
            o.Insert(2, eric);

            Assert.AreEqual(1, o.IndexOf(tiffany));
            Assert.AreEqual(-1, o.IndexOf(new Person() { FirstName = "Tiffany", LastName = "Smith" }));

            Assert.AreEqual("Andre", eric.LastName);
            o[2].LastName = "Gordon";
            Assert.AreEqual("Gordon", eric.LastName);
        }
    }
}
