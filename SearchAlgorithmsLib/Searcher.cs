using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T>
    {
        protected int evaluatedNodes;
        
        public Searcher()
        {
            evaluatedNodes = 0;
        }
        
        // ISearcher’s methods:
        public int GetNumberOfNodesEvaluated(){
            return evaluatedNodes;
        }

        public abstract Solution<T> Search(ISearchable<T> searchable);

        protected Solution<T> BackTrace(State<T> goalState, State<T> initialState = null)
        {
            Solution<T> solution = new Solution<T>();
            while (goalState != null)
            {
                solution.AddState(goalState);
                //if (goalState.Equals(initialState)) break;
                goalState = goalState.CameFrom;
            }

            return solution;
        }
    }
}
