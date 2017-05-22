using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace Server.Model
{
    /// <summary>
    /// Path details class
    /// </summary>
    public class PathDetails
    {
        /// <summary>
        /// Convert solution to the string
        /// </summary>
        /// <param name="sol">solution to convert</param>
        /// <returns>path</returns>
        public static string ConvertSolutionToString(Solution<Position> sol)
        {
            StringBuilder result = new StringBuilder();
            int length = sol.RouteSize;
            Position current = sol[0].state;
            for (int i = 1; i < length; i++)
            {
                //0 for going left
                Position next = sol[i].state;
                if (next.Col == current.Col - 1)
                {
                    result.Append((int)Direction.Left);
                }

                //1 for going right
                else if (next.Col == current.Col + 1)
                {
                    result.Append((int)Direction.Right);
                }

                //2 for going up
                else if (next.Row == current.Row + 1)
                {
                    result.Append((int)Direction.Down);
                }

                //3 for going down
                else if (next.Row == current.Row - 1)
                {
                    result.Append((int)Direction.Up);
                }
                else
                {
                    result.Append((int)Direction.Unknown);
                }
                current = next;
            }
            return result.ToString();
        }
    }
}
