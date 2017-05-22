using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient
{
    class CommandsFactory
    {
        public static string GetGenerateCommand(string name, int rows, int cols)
        {
            return string.Format("generate {0} {1} {2}", name, rows, cols);
        }

        public static string GetSolveCommand(string name, int alg)
        {
            string algorithm = string.Empty;
            return string.Format("solve {0} {1}", name, alg);
        }

        public static string GetGamesListCommand()
        {
            return "list";
        }

        public static string GetJoinCommand(string name)
        {
            return string.Format("join {0}", name);
        }

        public static string GetPlayCommand(Direction direction)
        {
            return string.Format("play {0}", direction.ToString().ToLower());
        }

        public static string GetStartCommand(string name, int rows, int cols)
        {
            return string.Format("start {0} {1} {2}", name, rows, cols);
        }

        public static string GetCloseCommand(string name)
        {
            return string.Format("close {0}", name);
        }
    }
}
