using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using Server.Model;

namespace Server
{
    interface IModel
    {
        Maze GenerateMaze(string name, int rows, int cols);
        SolutionDetails Solve(string name, Algorithm alg);
        List<string> GetJoinableGamesList();
        string GetPathAsString(Solution<Position> sol);
        void OpenRoom(string name, int rows, int cols);
    }
 }
