using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class BFS<T> : Searcher<T>
    {
        // Searcher's abstract method overriding
        public override Solution<T> Search(ISearchable searchable)
        {
            State<T> prevState = null;
            AddToOpenList(searchable.getInitialState<T>(), 0); // inherited from Searcher
            HashSet<State<T>> closed = new HashSet<State<T>>();

            while (OpenListSize > 0)
            {
                State<T> n = PopOpenList(); // inherited from Searcher, removes the best state
                closed.Add(n);
                if (n.Equals(searchable.getGoalState<T>())) return BackTrace(n);
                // private method, back traces through the parents
                // calling the delegated method, returns a list of states with n as a parent
                List<State<T>> succerssors = searchable.getAllPossibleStates(n);

                foreach (State<T> s in succerssors)
                {
                    if(prevState == null) prevState = s;
                    if (!closed.Contains(s) && !OpenListContains(s))
                    {
                        //cost = 
                        // s.setCameFrom(n); // already done by getSuccessors
                        AddToOpenList(s, s.Cost);
                    }
                    else if(s.Cost < n.Cost)
                    {
                        if (!OpenListContains(s)) AddToOpenList(s, s.Cost);
                        else UpdateStatePriority(s, n.Cost);
                    }
                }
            }
            return new Solution<T>();
        }

        private Solution<T> BackTrace(State<T> goalState)
        {
            Solution<T> solution = new Solution<T>();
            while (goalState != null)
            {
                solution.AddState(goalState);
                goalState = goalState.GetParentState;
            }

            return solution;
        }
    }
}
