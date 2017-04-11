using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;

namespace Server
{
    interface IModel
    {
        Maze GenerateMaze(string name, int rows, int cols);
        Solution<Position> Solve(string name, Algorithm alg);
        List<string> GetJoinableGamesList();
    }
 }
