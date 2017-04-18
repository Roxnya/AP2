using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;


namespace CompareSolvers
{
    public class MazeAdapter : ISearchable<Position>
    {

        private Maze maze;
        public MazeAdapter(Maze m)
        {
            this.maze = m;
        }


        public List<State<Position>> GetAllPossibleStates(State<Position> s)
        {
            List<State<Position>> list = new List<State<Position>>();
            int row = s.state.Row;
            int col = s.state.Col;
            int mazeRowSize = maze.Rows;
            int mazeColSize = maze.Cols;
            //todo check what if i am in the edge
            if ((row + 1 < mazeRowSize) && (maze[row + 1, col] == CellType.Free))
            {
                list.Add(new State<Position>(new Position(row + 1, col)));
            }
            if ((col + 1 < mazeColSize) && (maze[row, col + 1] == CellType.Free))
            {
                list.Add(new State<Position>(new Position(row, col + 1)));
            }
            if ((row - 1 >= 0) && (maze[row - 1, col] == CellType.Free))
            {
                list.Add(new State<Position>(new Position(row - 1, col)));
            }
            if ((col - 1 >= 0) && (maze[row, col - 1] == CellType.Free))
            {
                list.Add(new State<Position>(new Position(row, col - 1)));
            }
            return list;
        }




        public State<Position> GetGoalState()
        {
            return new State<Position>(maze.InitialPos);

        }

        public State<Position> GetInitialState()
        {
            return new State<Position>(maze.GoalPos);

        }

        public double GetInterStateCost(State<Position> from, State<Position> to)
        {
            return 1;
        }
    }
}
