using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// A Cost Comperator Class For State
    /// </summary>
    /// <typeparam name="T">State Type</typeparam>
    public class CostComperator<T> : IComparer<State<T>>
    {
        /// <summary>
        /// Compares two states by their cost
        /// </summary>
        /// <param name="cFrom">State to compare by</param>
        /// <param name="cTo">State to compare cFrom to</param>
        /// <returns>A value that is > 0 if from is greater, < 0 if to is greater, 0 if they are equal(by cost)</returns>
        public int Compare(State<T> cFrom, State<T> cTo)
        {
            return cFrom.Cost.CompareTo(cTo.Cost);
        }

    }
}
