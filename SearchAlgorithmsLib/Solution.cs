using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class Solution<T>
    {
        private Stack<State<T>> states;

        public Solution()
        {}

        internal void AddState(State<T> state)
        {
            states.Push(state);
        }
    }
}
