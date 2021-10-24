using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    internal class Person : IComparable
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CompareTo(object obj)
        {
            var other = obj as Person;

            if (other == null)
            {
                return -1;
            }

            return (LastName + FirstName).CompareTo(other.LastName + other.FirstName);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Person;

            if (other == null)
            {
                return false;
            }

            return LastName == other.LastName &&
                   FirstName == other.FirstName;
        }

        public override int GetHashCode()
        {
            return (LastName + FirstName).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Person: {0}, {1}", LastName, FirstName);
        }


    }
}
