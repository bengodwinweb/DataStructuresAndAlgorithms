using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    [TestFixture]
    class ArrayListListTests : ListTests
    {
        protected override IList<T> GetList<T>()
        {
            return new ArrayList<T>();
        }
    }
}
