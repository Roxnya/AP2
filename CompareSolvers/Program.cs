using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeGeneratorLib;
using MazeLib;
using CompareSolvers;
class Program
{
    static void Main(string[] args)
    {
        CompareSolvers();

    }

    /// <summary>
    /// Comparing dfs and Bfs algorithms 
    /// </summary>
    private static void CompareSolvers()
    {
        Maze m = new Maze(40, 40);
        IMazeGenerator maze = new DFSMazeGenerator();
        m = maze.Generate(40, 40);
        Console.WriteLine(m.ToString());

        MazeAdapter ma = new MazeAdapter(m);
        ISearcher<Position> dfs = new DFS<Position>();
        ISearcher<Position> bfs = new BFS<Position>();
        Solution<Position> sol1 = dfs.Search(ma);
        Console.WriteLine("The number of nodes evaluated by dfs is:" + dfs.GetNumberOfNodesEvaluated());
        Solution<Position> sol2 = bfs.Search(ma);
        Console.WriteLine("The number of nodes evaluated by bfs is:" + bfs.GetNumberOfNodesEvaluated());
        Console.ReadLine();
    }
}

