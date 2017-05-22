using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Class For State Representation of a searchable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class State<T>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="state"></param>
        public State(T state)
        {
            this.state = state;
        }

        /// <summary>
        /// State get 
        /// </summary>
        public T state { get; private set; }

        /// <summary>
        /// Cost get and set
        /// </summary>
        public double Cost { get; set; }

        // the state we came from to this state (setter)
        public State<T> CameFrom { get; internal set; }


        /// <summary>
        /// Compares given state to this state
        /// </summary>
        /// <param name="s">State to compare to</param>
        /// <returns>True if they are equal. False otherwise.</returns>
        public bool Equals(State<T> s)
        {
            return s != null &&  state.Equals(s.state);
        }

        /// <summary>
        /// Compares given object to this state. 
        /// </summary>
        /// <param name="s">State to compare to</param>
        /// <returns>True if they are equal(the object is the same state). False otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            State<T> s = (State<T>)obj;
            return Equals(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns state(T) in it's string representation</returns>
        public override string ToString()
        {
            return state.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns state's hash code</returns>
        public override int GetHashCode()
        {
            return state.ToString().GetHashCode();
        }

        /// <summary>
        /// Class for state pool,
        /// in order to prevent memory inflation by creating duplicate states every time a search is needed.
        /// </summary>
        public static class StatePool
        {
            private static Dictionary<T, State<T>> states = new Dictionary<T, State<T>>();

            /// <summary>
            /// 
            /// </summary>
            /// <param name="state">required state</param>
            /// <returns>Returns required state</returns>
            public static State<T> GetState(T state)
            {
                if (!states.ContainsKey(state))
                {
                    states.Add(state, new State<T>(state));
                }
                return states[state];
            }
        }
    }
}
