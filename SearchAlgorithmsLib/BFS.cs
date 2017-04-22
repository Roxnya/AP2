using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// A Class for Best First Search Search algorithm
    /// </summary>
    /// <typeparam name="T">Type of State</typeparam>
    public class BFS<T> : PrioritySearcher<T>
    {
        /// <summary>
        /// Ctor. Initializes cost comperator that is relevant for the algorithm.
        /// </summary>
        public BFS() : base (new CostComperator<T>())
        { }

        /// <summary>
        /// Searcher's abstract method overriding
        /// </summary>
        /// <param name="searchable">Search problem</param>
        /// <returns>Solution of search problem</returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            State<T> initialState = searchable.GetInitialState();
            AddToOpenList(initialState); // inherited from Searcher
            HashSet<State<T>> closed = new HashSet<State<T>>();

            while (OpenListSize > 0)
            {
                State<T> n = PopOpenList(); // inherited from Searcher, removes the best state
                closed.Add(n);
                if (n.Equals(searchable.GetGoalState())) return BackTrace(n, initialState);
                // private method, back traces through the parents
                // calling the delegated method, returns a list of states with n as a parent
                List<State<T>> succerssors = searchable.GetAllPossibleStates(n);

                foreach (State<T> s in succerssors)
                {
                    double interStateCost = searchable.GetInterStateCost(n, s);
                    if (!closed.Contains(s) && !OpenListContains(s))
                    {
                        s.CameFrom = n;
                        UpdateCost(s, n.Cost + interStateCost);
                        AddToOpenList(s);
                    }
                    else if((interStateCost + n.Cost) < s.Cost)
                    {
                        UpdateCost(s, n.Cost + interStateCost);
                        s.CameFrom = n;
                        if (!OpenListContains(s)) AddToOpenList(s);
                        else UpdateStatePriority(s);
                    }
                }
            }
            return new Solution<T>(evaluatedNodes);
        }

        /// <summary>
        /// Updates cost with new cost
        /// </summary>
        /// <param name="successor">successor to update</param>
        /// <param name="newCost">new cost</param>
        private void UpdateCost(State<T> successor, double newCost)
        {
            successor.Cost = newCost;
        }
    }
}
