using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace Server.Model
{
    public class PathDetails
    {
        public static string ConvertSolutionToString(Solution<Position> sol)
        {
            StringBuilder result = new StringBuilder();
            int length = sol.RouteSize;
            Position current = sol[0].state;
            for(int i = 1; i < length; i++)
            {
                Position next = sol[i].state;
                if(next.Col == current.Col - 1)
                {
                    result.Append("0");
                }
                if(next.Col == current.Col + 1)
                {
                    result.Append("1");
                }
                if(next.Row == current.Row + 1)
                {
                    result.Append("2");
                }
                if(next.Row == current.Row - 1)
                {
                    result.Append("3");
                }
                current = next;
            }
            return result.ToString();
        }
    }
}
