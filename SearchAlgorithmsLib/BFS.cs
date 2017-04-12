using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class BFS<T> : PrioritySearcher<T>
    {
        public BFS()
        {
            comperator = new CostComperator();
        }

        // Searcher's abstract method overriding
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            State<T> initialState = searchable.GetInitialState();
            AddToOpenList(initialState, 0); // inherited from Searcher
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
                    if (!closed.Contains(s) && !OpenListContains(s))
                    {
                        s.CameFrom = n;
                        AddToOpenList(s, s.Cost);
                    }
                    else if(IsBetterRoute(s))
                    {
                        if (!OpenListContains(s)) AddToOpenList(s, s.Cost);
                        else UpdateStatePriority(s, s.Cost);
                    }
                }
            }
            return new Solution<T>();
        }
    }
}
