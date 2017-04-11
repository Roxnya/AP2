﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using CompareSolvers;
namespace Server
{
    class MazeModel : IModel
    {
        private Dictionary<string, Maze> mazes;
        private Dictionary<Maze, Solution<Position>> solutions;
        private Dictionary<string,GameRoom> rooms;

        public MazeModel()
        {
            mazes = new Dictionary<string, Maze>();
            solutions = new Dictionary<Maze, Solution<Position>>();
            rooms = new Dictionary<string, GameRoom>();
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            Maze maze = new DFSMazeGenerator().Generate(rows, cols);
            maze.Name = name;
            mazes.Add(name, maze);
            return maze;
        }

        public Solution<Position> Solve(string name, Algorithm alg)
        {
            if (mazes.ContainsKey(name))
            {
                Maze m = mazes[name];
                if (solutions.ContainsKey(m))
                {
                    return solutions[m];
                }
                MazeAdapter ma = new MazeAdapter(m);
                if(alg == Algorithm.BFS)
                {
                    ISearcher<Position> bfs = new BFS<Position>();
                    Solution<Position> sol = bfs.Search(ma);
                    solutions.Add(m, sol);
                    return sol;

                }
                if (alg == Algorithm.DFS)
                {
                    ISearcher<Position> dfs = new DFS<Position>();
                    Solution<Position> sol = dfs.Search(ma);
                    solutions.Add(m, sol);
                    return sol;
                 

                }
            }
            
            return new Solution<Position>();
            
        }

        public Maze OpenRoom(string name, int rows, int cols)
        {
            //check that maze name is unique...
            Maze m = GenerateMaze(name, rows, cols);
            rooms.Add(m.Name, new GameRoom(m));
            //
            //ToDo:wait for another player
            //
            return m;
        }

        public List<string> GetJoinableGamesList()
        {
            return rooms.Values.Where(r => r.mode == Mode.WaitingForPlayer)
                                                .Select(r => r.maze.Name).ToList();
        }




    }

    public enum Algorithm
    {
        BFS = 0,
        DFS = 1
    }
}
