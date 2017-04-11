using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeGeneratorLib;
using MazeLib;
using main;
class Program
{
    static void Main(string[] args)
    {
        CompareSolvers();

    }

    private static void CompareSolvers()
    {

        Maze m = new Maze(7, 7);
        IMazeGenerator maze = new DFSMazeGenerator();
        m = maze.Generate(7, 7);
        Console.WriteLine(m.ToString());

        MazeAdapter ma = new MazeAdapter(m);
        ISearcher<Position> dfs = new DFS<Position>();
        ISearcher<Position> bfs = new BFS<Position>();
        Solution<Position> sol1 = dfs.Search(ma);
        Console.WriteLine("The number of nodes evaluated by dfs is:" + dfs.GetNumberOfNodesEvaluated());
        Solution<Position> sol2 = bfs.Search(ma);
        Console.WriteLine("The number of nodes evaluated by bfs is:" + bfs.GetNumberOfNodesEvaluated());




    }
}

