using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class GameData
    {
        private Dictionary<string, Maze> mazes;
        private Dictionary<Maze, Solution<Position>> solutions;
        private Dictionary<string, IGameRoom> rooms;

        public GameData()
        {
            mazes = new Dictionary<string, Maze>();
            solutions = new Dictionary<Maze, Solution<Position>>();
            rooms = new Dictionary<string, IGameRoom>();
        }

        public List<string> GetJoinableRooms()
        {
            return rooms.Values.Where(r => r.Mode == Mode.WaitingForPlayer)
                                                .Select(r => r.Name).ToList();
        }

        public void AddGame(IGameRoom room)
        {
            rooms.Add(room.Name, room);
        }

        public void AddMaze(Maze maze)
        {
            mazes.Add(maze.Name, maze);
        }

        public Maze GetMaze(string maze)
        {
            return mazes.ContainsKey(maze) ? mazes[maze] : null;
        }
    }
}
