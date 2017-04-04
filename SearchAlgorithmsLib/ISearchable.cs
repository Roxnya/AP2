using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    interface ISearchable
    {
        State<T> getInitialState<T>();
        State<T> getGoalState<T>();
        List<State<T>> getAllPossibleStates<T>(State<T> s);
    }
}
