using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class State<T>
    {
        private T state; // the state represented by a string
        private double cost; // cost to reach this state (set by a setter)
        private State<T> prevState; // the state we came from to this state (setter)
        
        public State(T state)
        {
            this.state= state;
        }

        public double Cost { get; }

        public State<T> CameFrom { get { return prevState; } internal set { prevState = value; } }

        public bool Equals(State<T> s)
        {
            return state.Equals(s.state);
        }

        public override bool Equals(object obj)
        {
            State<T> s = (State<T>)obj;
            return s != null && Equals(s);
        }

        public override string ToString()
        {
            return state.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
