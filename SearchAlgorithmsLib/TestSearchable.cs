using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{

    class TestSearchable<T> : ISearchable<T>
    {
        private State<T> from, to;
        Dictionary<State<T>, List<State<T>>> Adj;

        public TestSearchable(State<T> from, State<T> to, Dictionary<State<T>, List<State<T>>> Adj)
        {
            this.from = from;
            this.to = to;
            this.Adj = Adj;
        }

        public State<T> GetInitialState()
        {
            return from;
        }

        public State<T> GetGoalState()
        {
            return to;
        }

        public List<State<T>> GetAllPossibleStates(State<T> s)
        {
            List<State<T>> states = null;
            // if found the vertex
            if (Adj.ContainsKey(s))
            {
                states = Adj[s];
            }

            // if vertex does not exist or no neighbors found
            if (states == null)
            {
                states = new List<State<T>>();
            }
            return states;
        }
    }
}
