using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    [TestFixture]
    class LinkedListTests : CollectionTests
    {
        protected override ICollection<T> GetCollection<T>()
        {
            return new LinkedList<T>();
        }
    }
}
