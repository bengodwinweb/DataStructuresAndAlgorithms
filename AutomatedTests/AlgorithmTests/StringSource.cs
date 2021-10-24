using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    public class StringSource
    {
        #region Singleton
        private static StringSource _instance { get; set; }

        public static StringSource Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new StringSource();
                }

                return _instance;
            }
        }
        #endregion

        private StringSource()
        {
            // Must be 10,000 lines in the file
            _unsortedArray = new string[10_000];

            string sourceFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Files", "RandomStrings.txt");
            using (var reader = File.OpenText(sourceFile))
            {
                string line;
                int i = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    _unsortedArray[i++] = line;
                }
            }


            SortedArray = new string[10_000];
            Array.Copy(_unsortedArray, SortedArray, _unsortedArray.Length);
            Array.Sort(SortedArray);
        }



        private string[] _unsortedArray { get; }

        /// <summary>
        /// Returns a copy of the unsorted souce data.
        /// Contents can be modified without affecting the next call to UnsortedArray.
        /// </summary>
        public string[] UnsortedArray
        {
            get
            {
                var copy = new string[_unsortedArray.Length];
                Array.Copy(_unsortedArray, copy, _unsortedArray.Length);
                return copy;
            }
        }

        /// <summary>
        /// Returns the original data sorted.
        /// This is not a copy, do not modify the contents of this array.
        /// </summary>
        public string[] SortedArray { get; }
    }
}
