using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// ISearcher interface
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    public interface ISearcher<T>
    {
        /// <summary>
        /// the search method
        /// </summary>
        /// <param name="searchable">searchable</param>
        /// <returns>solution</returns>
        Solution<T> Search(ISearchable<T> searchable);

        /// <summary>
        /// get how many nodes were evaluated by the algorithm
        /// </summary>
        /// <returns>number of nodes evaluated</returns>
        int GetNumberOfNodesEvaluated();
    }
}
