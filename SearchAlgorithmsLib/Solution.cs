using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        //Convert to action
        private List<State<T>> states;

        public Solution()
        {
            states = new List<State<T>>();
        }

        public int RouteSize
        {
            get { return states.Count; }
        }

        internal void AddState(State<T> state)
        {
            states.Insert(0, state);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(State<T> state in states)
            {
                state.ToString();
            }
            return sb.ToString();
        }
        public State<T> this[int key]
        {
            get
            {
                return states[key];
            }
        }
    }
}
