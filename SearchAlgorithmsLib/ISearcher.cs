using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    interface ISearcher
    {
        // the search method
        Solution<T> Search<T>(ISearchable searchable);
        // get how many nodes were evaluated by the algorithm
        int GetNumberOfNodesEvaluated();
    }
}
