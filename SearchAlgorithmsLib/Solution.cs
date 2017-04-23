using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Class for solution representation
    /// </summary>
    /// <typeparam name="T">states type</typeparam>
    public class Solution<T>
    {
        private List<State<T>> states;

        /// <summary>
        /// Ctor
        /// </summary>
        public Solution(int nodesEvaluated)
        {
            states = new List<State<T>>();
            this.NodesEvaluated = nodesEvaluated;
        }

        /// <summary>
        /// Nodes evaluated get and set
        /// </summary>
        public int NodesEvaluated { get; private set; }

        /// <summary>
        /// route size get and set
        /// </summary>
        public int RouteSize
        {
            get { return states.Count; }
        }

        /// <summary>
        /// Adds given state to solution
        /// </summary>
        /// <param name="state">state to add</param>
        internal void AddState(State<T> state)
        {
            states.Insert(0, state);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string representation of this solution. Specifically, solution's states, ordered.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(State<T> state in states)
            {
                state.ToString();
            }
            return sb.ToString();
        }

        /// <summary>
        /// Allows access to a specific State in this solution
        /// </summary>
        /// <param name="key">index of state to access</param>
        /// <returns>required state if exists, null otherwise.</returns>
        public State<T> this[int key]
        {
            get
            {
                if(key >= 0 && key < RouteSize)
                    return states[key];
                return null;
            }
        }
    }
}
