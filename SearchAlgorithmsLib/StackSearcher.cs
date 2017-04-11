using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public abstract class StackSearcher<T> : Searcher<T>
    {
        Stack<State<T>> stack = new Stack<State<T>>();
        public override abstract Solution<T> Search(ISearchable<T> searchable);

        protected void Push(State<T> s)
        {
            stack.Push(s);
        }

        protected State<T> Pop()
        {
            return stack.Pop();
        }

        protected int Count()
        {
            return stack.Count();
        }

    }
}
