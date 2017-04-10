using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class PrioritySearcher<T> : Searcher<T>
    {
        private SimplePriorityQueue<State<T>, double> openList;
        protected IComparable comperator;

        public PrioritySearcher()
        {
            openList = new SimplePriorityQueue<State<T>, double>();
        }

        protected State<T> PopOpenList()
        {
            evaluatedNodes++;
            if (openList.Count == 0) return null;
            return openList.Dequeue();
        }

        protected double GetPriority(State<T> state)
        {
            //not working, temporary
            return openList.Where(s => s.Equals(state)).First().Cost;
        }

        protected bool IsBetterRoute(State<T> state)
        {
            //return comperator.Compare(state.Cost, GetPriority(state)) > 0;
            return true;
        }

        // a property of openList
        public int OpenListSize
        { // it is a read-only property :)
            get { return openList.Count; }
        }

        protected void AddToOpenList(State<T> s, double cost)
        {
            openList.Enqueue(s, cost);
        }

        protected bool OpenListContains(State<T> s)
        {
            return openList.Contains(s);
        }

        protected void UpdateStatePriority(State<T> s, double priority)
        {
            openList.Remove(s);
            AddToOpenList(s, priority);
        }

        public override abstract Solution<T> Search(ISearchable<T> searchable);

    }
}
