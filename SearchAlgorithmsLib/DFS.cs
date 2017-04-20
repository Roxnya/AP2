
using System;
using System.Collections.Generic;
namespace SearchAlgorithmsLib
{
    public class DFS<T> : StackSearcher<T>
    {


        public DFS()
        {
        }

        public override Solution<T> Search(ISearchable<T> searchable)
        {
            HashSet<State<T>> discovered = new HashSet<State<T>>();
            State<T> v = searchable.GetInitialState();
            v.CameFrom = null;
            bool found = false;
            Push(v);

            while (Count() > 0)
            {
                v = Pop();
                evaluatedNodes += 1;
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
                            Push(s);
                        }
                    }

                }


            }

            return found ? BackTrace(v) : new Solution<T>(evaluatedNodes);



        }
    }
}