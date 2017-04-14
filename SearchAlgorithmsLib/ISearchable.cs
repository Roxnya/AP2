using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public interface ISearchable<T>
    {
        State<T> GetInitialState();
        State<T> GetGoalState();
        List<State<T>> GetAllPossibleStates(State<T> s);
        double GetInterStateCost(State<T> from, State<T> to);
    }
}
