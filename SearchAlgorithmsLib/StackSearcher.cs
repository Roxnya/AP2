using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// StackSearcher
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    public abstract class StackSearcher<T> : Searcher<T>
    {
        Stack<State<T>> stack = new Stack<State<T>>();

        /// <summary>
        /// Search method
        /// </summary>
        /// <param name="searchable">searchable</param>
        /// <returns>solution</returns>
        public override abstract Solution<T> Search(ISearchable<T> searchable);


        /// <summary>
        /// Pushing to the stack
        /// </summary>
        /// <param name="s"></param>
        protected void Push(State<T> s)
        {
            stack.Push(s);
        }

        /// <summary>
        /// Pop to the stack
        /// </summary>
        /// <returns></returns>
        protected State<T> Pop()
        {
            return stack.Pop();
        }

        /// <summary>
        /// the size of the stack
        /// </summary>
        /// <returns></returns>
        protected int Count()
        {
            return stack.Count();
        }

    }
}
