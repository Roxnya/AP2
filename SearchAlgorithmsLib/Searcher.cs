using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher
    {
        private SimplePriorityQueue<State<T>, double> openList;
        private int evaluatedNodes;
        
        public Searcher()
        {
            openList = new SimplePriorityQueue<State<T>, double>();
            evaluatedNodes = 0;
        }
        
        protected State<T> PopOpenList() {
            evaluatedNodes++;
            if (openList.Count == 0) return null;
            return openList.Dequeue();
        }
        
        // a property of openList
        public int OpenListSize
        { // it is a read-only property :)
            get{ return openList.Count; }
        }
        
        // ISearcher’s methods:
        public int GetNumberOfNodesEvaluated(){
            return evaluatedNodes;
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
            openList.UpdatePriority(s, priority);
        }
        
        public abstract Solution<T> Search(ISearchable searchable);
    }
}
