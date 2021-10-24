using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    /// <summary>
    /// Int comparer.
    /// Sorts evens ahead of odds, then by value in ascending order
    /// </summary>
    class EvenOddComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            bool xEven = IsEven(x);
            bool yEven = IsEven(y);

            if (xEven == yEven)
            {
                return x.CompareTo(y);
            }

            return xEven ? -1 : 1;
        }

        bool IsEven(int n)
        {
            return n % 2 == 0;
        }
    }
}
