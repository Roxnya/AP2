using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// An abstract class for searcher - Search algorithms
    /// </summary>
    /// <typeparam name="T">searchables state type</typeparam>
    public abstract class Searcher<T> : ISearcher<T>
    {
        /// <summary>
        /// number of nodes that were evaluated for solution
        /// </summary>
        protected int evaluatedNodes;
     
        /// <summary>
        /// Ctor
        /// </summary>
        public Searcher()
        {
            evaluatedNodes = 0;
        }

        /// <summary>
        /// ISearcher’s method
        /// </summary>
        /// <returns>number of evaluated nodes</returns>
        public int GetNumberOfNodesEvaluated(){
            return evaluatedNodes;
        }

        /// <summary>
        /// Searches a solution (e.g path from a to b) in given searchable.
        /// Each search algorithm must implement this method.
        /// </summary>
        /// <param name="searchable">Search problem to solver (e.g maze)</param>
        /// <returns>search problem's solution</returns>
        public abstract Solution<T> Search(ISearchable<T> searchable);

        /// <summary>
        /// Back traces goal state to it's parent in order to represent it's solution.
        /// </summary>
        /// <param name="goalState">state to get to</param>
        /// <param name="initialState">state to start from</param>
        /// <returns>Solution of goal state. </returns>
        protected Solution<T> BackTrace(State<T> goalState, State<T> initialState = null)
        {
            Solution<T> solution = new Solution<T>(evaluatedNodes);
            while (goalState != null)
            {
                solution.AddState(goalState);
                goalState = goalState.CameFrom;
            }

            return solution;
        }
    }
}
