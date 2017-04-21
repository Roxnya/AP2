using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    class SearcherFactory
    {
        public static ISearcher<Position> GetSearcher(Algorithm alg)
        {
            ISearcher<Position> searcher = null;

            if (alg == Algorithm.BFS)
            {
                searcher = new BFS<Position>();
            }
            if (alg == Algorithm.DFS)
            {
                searcher = new DFS<Position>();
            }
            return searcher;
        }
    }
}
