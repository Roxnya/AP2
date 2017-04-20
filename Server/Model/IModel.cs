using MazeLib;
using System;
using System.Collections.Generic;
using SearchAlgorithmsLib;
using System.Net.Sockets;
using Server.Model;

namespace Server
{
    interface IModel
    {
        Maze GenerateMaze(string name, int rows, int cols);
        SolutionDetails Solve(string name, Algorithm alg);
        List<string> GetJoinableGamesList();
        string GetPathAsString(Solution<Position> sol);
        bool OpenRoom(string name, int rows, int cols);
        bool Join(string name);
    }
}
