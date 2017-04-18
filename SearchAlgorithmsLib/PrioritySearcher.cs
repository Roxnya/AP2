using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIBRIS.Hippie;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Class for PrioritySearcher
    /// </summary>
    /// <typeparam name="T">State type</typeparam>
    public abstract class PrioritySearcher<T> : Searcher<T>
    {
        private IHeap<State<T>> openList; 

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="comperator">comperator by which OpenList will prioritize states</param>
        public PrioritySearcher(IComparer<State<T>> comperator)
        {
            openList = HeapFactory.NewBinaryHeap<State<T>>(comperator);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>OpenList next state (the one with the lowest priority)</returns>
        protected State<T> PopOpenList()
        {
            evaluatedNodes++;
            if (openList.Count == 0) return null;
            return openList.RemoveMin();
        }
        
        /// <summary>
        /// Read-only property. Returns OpenList Size.
        /// </summary>
        public int OpenListSize
        {
            get { return openList.Count; }
        }

        /// <summary>
        /// Adds given state to open list
        /// </summary>
        /// <param name="s">state to add</param>
        protected void AddToOpenList(State<T> s)
        {
            openList.Add(s);
        }

        /// <summary>
        /// looks up given state in open list
        /// </summary>
        /// <param name="s">state to look up</param>
        /// <returns>true if given state exists, false otherwise.</returns>
        protected bool OpenListContains(State<T> s)
        {
            return openList.Contains(s);
        }

        /// <summary>
        /// Updates the priority of a given state
        /// </summary>
        /// <param name="s">state to update</param>
        protected void UpdateStatePriority(State<T> s)
        {
            openList.Remove(s);
            AddToOpenList(s);
        }

        /// <summary>
        /// Searcher's method.
        /// </summary>
        /// <param name="searchable"></param>
        /// <returns></returns>
        public override abstract Solution<T> Search(ISearchable<T> searchable);

    }
}
