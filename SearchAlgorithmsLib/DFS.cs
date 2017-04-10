using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    public class DFSSearcher<T> : Searcher<T>
    {
        public DFSSearcher()
        {
        }

        public override Solution<T> Search(ISearchable<T> searchable)
        {
            Stack<State<T>> stack = new Stack<State<T>>();
            HashSet<State<T>> discovered = new HashSet<State<T>>();
            State<T> v = searchable.GetInitialState();
            v.CameFrom = null;
            bool found = false;
            stack.Push(v);

            while (stack.Count > 0)
            {
                v = stack.Pop();
                if (!discovered.Contains(v))
                {
                    discovered.Add(v);
                    if (searchable.GetGoalState().Equals(v))
                    {
                        found = true;
                        break;
                    }
                    foreach (State<T> s in searchable.GetAllPossibleStates(v))
                    {
                        if (!discovered.Contains(s))
                        {
                            //update the path list
                            s.CameFrom = v;
                            stack.Push(s);
                        }
                    }

                }
            }
            return found? BackTrace(v) : new Solution<T>();
        }
    }
}