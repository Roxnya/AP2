using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// ISearchable interface
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// GetInitialState
        /// </summary>
        /// <returns>Initial state</returns>
        State<T> GetInitialState();

        /// <summary>
        /// GEt the goal state
        /// </summary>
        /// <returns>goal state</returns>
        State<T> GetGoalState();

        /// <summary>
        /// Getting All Possible States
        /// </summary>
        /// <param name="s">state</param>
        /// <returns>list of possible states</returns>
        List<State<T>> GetAllPossibleStates(State<T> s);

        /// <summary>
        /// GetvIntervStatevCost
        /// </summary>
        /// <param name="from">initial state</param>
        /// <param name="to"> final state</param>
        /// <returns></returns>
        double GetInterStateCost(State<T> from, State<T> to);
    }
}
