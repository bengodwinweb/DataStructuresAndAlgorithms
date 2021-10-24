using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    abstract class TestSort
    {

        protected ISortingAlgorithm o;

        protected abstract ISortingAlgorithm Create();

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            o = Create();

            var stringSource = StringSource.Instance;
        }

        [Test]
        public void TestNull()
        {
            Assert.Throws<ArgumentException>(delegate { o.Sort<int>(null); });
        }

        [Test]
        public void TestNullComparer()
        {
            var nums = new int[] { 1, 2, 3 };
            Assert.Throws<ArgumentException>(delegate { o.Sort(nums, null); });
        }

        [Test, TestCaseSource(nameof(IntSources))]
        public void TestSortInts(int[] source)
        {
            var expected = new int[source.Length];
            Array.Copy(source, expected, source.Length);
            Array.Sort(expected);

            var start = DateTime.UtcNow;
            var actual = o.Sort(source);
            var sortTime = DateTime.UtcNow - start;

            Console.WriteLine("Sorted {0:N0} ints in {1:N0} ms", source.Length, sortTime.TotalMilliseconds);

            Assert.AreEqual(source.Length, actual.Count);
            for (int i = 0; i < source.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i], "Difference at index " + i.ToString("N0"));
            }
        }

        [Test]
        public void TestSortPeople()
        {
            var people = new List<Person>();
            people.Add(new Person() { LastName = "Smith", FirstName = "Reagan" });
            people.Add(new Person() { LastName = "Godwin", FirstName = "Ben" });
            people.Add(new Person() { LastName = "Anderson", FirstName = "Thomas" });
            people.Add(new Person() { LastName = "Zorn", FirstName = "Jim" });
            people.Add(new Person() { LastName = "Smith", FirstName = "Brian" });
            people.Add(new Person() { LastName = "Smith", FirstName = "Tim" });
            people.Add(new Person() { LastName = "Keller", FirstName = "Thomas" });

            var sortedPeople = o.Sort(people);

            Assert.AreEqual(new Person() { LastName = "Anderson", FirstName = "Thomas" }, sortedPeople[0]);
            Assert.AreEqual(new Person() { LastName = "Godwin", FirstName = "Ben" }, sortedPeople[1]);
            Assert.AreEqual(new Person() { LastName = "Keller", FirstName = "Thomas" }, sortedPeople[2]);
            Assert.AreEqual(new Person() { LastName = "Smith", FirstName = "Brian" }, sortedPeople[3]);
            Assert.AreEqual(new Person() { LastName = "Smith", FirstName = "Reagan" }, sortedPeople[4]);
            Assert.AreEqual(new Person() { LastName = "Smith", FirstName = "Tim" }, sortedPeople[5]);
            Assert.AreEqual(new Person() { LastName = "Zorn", FirstName = "Jim" }, sortedPeople[6]);
        }

        [Test]
        public void TestSortWithComparable()
        {
            var comparer = new EvenOddComparer();

            var nums = new int[]
            {
                27, 98, 26, 88, 91,
                67, 1, 35, 2, 62,
                36, 50, 54, 43, 11,
                21, 92, 21, 98, 48,
            };

            var sortedNums = o.Sort(nums, comparer);

            Assert.AreEqual(2, sortedNums[0]);
            Assert.AreEqual(26, sortedNums[1]);
            Assert.AreEqual(36, sortedNums[2]);
            Assert.AreEqual(48, sortedNums[3]);
            Assert.AreEqual(50, sortedNums[4]);
            Assert.AreEqual(54, sortedNums[5]);
            Assert.AreEqual(62, sortedNums[6]);
            Assert.AreEqual(88, sortedNums[7]);
            Assert.AreEqual(92, sortedNums[8]);
            Assert.AreEqual(98, sortedNums[9]);
            Assert.AreEqual(98, sortedNums[10]);
            Assert.AreEqual(1, sortedNums[11]);
            Assert.AreEqual(11, sortedNums[12]);
            Assert.AreEqual(21, sortedNums[13]);
            Assert.AreEqual(21, sortedNums[14]);
            Assert.AreEqual(27, sortedNums[15]);
            Assert.AreEqual(35, sortedNums[16]);
            Assert.AreEqual(43, sortedNums[17]);
            Assert.AreEqual(67, sortedNums[18]);
            Assert.AreEqual(91, sortedNums[19]);
        }

        [Test]
        public void TestLargeStringCollection()
        {
            var source = StringSource.Instance.UnsortedArray; // 10,000 items

            var start = DateTime.UtcNow;
            var actual = o.Sort(source);
            var sortTime = DateTime.UtcNow - start;

            Console.WriteLine("Sorted {0:N0} strings in {1:N0} ms", source.Length, sortTime.TotalMilliseconds);

            Assert.IsNotNull(actual);
            Assert.AreEqual(source.Length, actual.Count);
            for (int i = 0; i < source.Length; i++)
            {
                Assert.AreEqual(StringSource.Instance.SortedArray[i], actual[i], "Difference at index " + i.ToString("N0"));
            }
        }


        public static object[] IntSources =
        {
            new object[]
            {
                new int[] // 99 items
                {
                    76, 64, 76, 7,  29,
                    99, 45, 55, 10, 65,
                    92, 69, 59, 97, 95,
                    57, 15, 18, 9,  20,
                    56, 68, 95, 62, 50,
                    45, 22, 74, 13, 16,
                    73, 87, 34, 88, 72,
                    82, 81, 80, 23, 15,
                    71, 9,  69, 20, 23,
                    52, 20, 23, 28, 95,
                    82, 37, 48, 16, 47,
                    69, 78, 34, 81, 69,
                    86, 60, 18, 2,  96,
                    71, 28, 48, 78, 6 ,
                    89, 62, 13, 36, 99,
                    38, 57, 92, 87, 49,
                    49, 98, 2,  62, 44,
                    7,  41, 58, 38, 91,
                    65, 49, 88, 42, 21,
                    49, 37, 54, 34,
                }
            },
            new object[]
            {
                new int[] // 100 items
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
                }
            },
            new object[]
            {
                new int[] // 100 items
                {
                    48, 24, 3,  69, 75,
                    75, 33, 3,  76, 95,
                    74, 84, 70, 49, 31,
                    78, 6,  62, 32, 22,
                    54, 77, 52, 89, 19,
                    31, 38, 62, 74, 9 ,
                    4,  24, 78, 59, 78,
                    59, 65, 24, 80, 15,
                    73, 54, 9,  18, 59,
                    87, 79, 21, 24, 24,
                    83, 82, 7,  19, 89,
                    78, 66, 15, 49, 51,
                    55, 33, 87, 97, 61,
                    82, 45, 43, 1,  86,
                    46, 82, 14, 69, 78,
                    56, 91, 85, 40, 74,
                    35, 34, 59, 63, 20,
                    63, 14, 69, 97, 82,
                    13, 39, 34, 96, 21,
                    5,  92, 63, 6,  70,
                }
            }
        };



    }
}
