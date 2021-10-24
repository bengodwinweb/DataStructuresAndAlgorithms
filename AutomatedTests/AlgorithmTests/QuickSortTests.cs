using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    [TestFixture]
    class QuickSortTests : TestSort
    {
        protected override ISortingAlgorithm Create()
        {
            return new QuickSort();
        }
    }
}
