using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    [TestFixture]
    class TestCompare
    {

        [Test]
        public void TestIntCompareTo()
        {
            Assert.AreEqual(0, 76.CompareTo(76));
        }

        [Test]
        public void TestIntDefaultComparator()
        {
            var comparator = Comparer<int>.Default;

            Assert.AreEqual(0, comparator.Compare(76, 76));
        }
    }
}
