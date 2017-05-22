using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;


namespace CompareSolvers
{
    /// <summary>
    /// MazeAdapter class
    /// </summary>
    public class MazeAdapter : ISearchable<Position>
    {

        private Maze maze;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="m"></param>
        public MazeAdapter(Maze m)
        {
            this.maze = m;
        }

        /// <summary>
        /// GEtting all the possible states
        /// </summary>
        /// <param name="s">state</param>
        /// <returns>lis of all possible states</returns>
        public List<State<Position>> GetAllPossibleStates(State<Position> s)
        {
            List<State<Position>> list = new List<State<Position>>();
            int row = s.state.Row;
            int col = s.state.Col;
            int mazeRowSize = maze.Rows;
            int mazeColSize = maze.Cols;
            
            //from the up
            if ((row + 1 < mazeRowSize) && (maze[row + 1, col] == CellType.Free))
            {
                list.Add(new State<Position>(new Position(row + 1, col)));
            }

            //right
            if ((col + 1 < mazeColSize) && (maze[row, col + 1] == CellType.Free))
            {
                list.Add(new State<Position>(new Position(row, col + 1)));
            }

            //down
            if ((row - 1 >= 0) && (maze[row - 1, col] == CellType.Free))
            {
                list.Add(new State<Position>(new Position(row - 1, col)));
            }

            //left
            if ((col - 1 >= 0) && (maze[row, col - 1] == CellType.Free))
            {
                list.Add(new State<Position>(new Position(row, col - 1)));
            }
            return list;
        }



        /// <summary>
        /// Get goal state
        /// </summary>
        /// <returns>goal</returns>
        public State<Position> GetGoalState()
        {
            return new State<Position>(maze.GoalPos);

        }

        /// <summary>
        /// GetInitialState
        /// </summary>
        /// <returns>initial state</returns>
        public State<Position> GetInitialState()
        {
            return new State<Position>(maze.InitialPos);

        }

        /// <summary>
        /// GetInterStateCost
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>cost </returns>
        public double GetInterStateCost(State<Position> from, State<Position> to)
        {
            return 1;
        }
    }
}
