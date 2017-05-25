using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient
{
    /// <summary>
    /// Class CommandsFactory.
    /// </summary>
    class CommandsFactory
    {
        /// <summary>
        /// Gets the generate command.
        /// </summary>
        /// <param name="name">maze name.</param>
        /// <param name="rows">maze rows.</param>
        /// <param name="cols">maze cols.</param>
        /// <returns>requested command formatted string.</returns>
        public static string GetGenerateCommand(string name, int rows, int cols)
        {
            return string.Format("generate {0} {1} {2}", name, rows, cols);
        }

        /// <summary>
        /// Gets the solve command.
        /// </summary>
        /// <param name="name">maze name.</param>
        /// <param name="alg">which alg to use for solving the maze.</param>
        /// <returns>requested command formatted string.</returns>
        public static string GetSolveCommand(string name, int alg)
        {
            string algorithm = string.Empty;
            return string.Format("solve {0} {1}", name, alg);
        }

        /// <summary>
        /// Gets the games list command.
        /// </summary>
        /// <returns>requested command formatted string.</returns>
        public static string GetGamesListCommand()
        {
            return "list";
        }

        /// <summary>
        /// Gets the join command.
        /// </summary>
        /// <param name="name">maze name.</param>
        /// <returns>requested command formatted string.</returns>
        public static string GetJoinCommand(string name)
        {
            return string.Format("join {0}", name);
        }

        /// <summary>
        /// Gets the play command.
        /// </summary>
        /// <param name="direction">The direction of player's movement.</param>
        /// <returns>requested command formatted string.</returns>
        public static string GetPlayCommand(Direction direction)
        {
            return string.Format("play {0}", direction.ToString().ToLower());
        }

        /// <summary>
        /// Gets the start command.
        /// </summary>
        /// <param name="name">maze name.</param>
        /// <param name="rows">maze rows.</param>
        /// <param name="cols">maze cols.</param>
        /// <returns>requested command formatted string.</returns>
        public static string GetStartCommand(string name, int rows, int cols)
        {
            return string.Format("start {0} {1} {2}", name, rows, cols);
        }

        /// <summary>
        /// Gets the close command.
        /// </summary>
        /// <param name="name">maze name.</param>
        /// <returns>requested command formatted string.</returns>
        public static string GetCloseCommand(string name)
        {
            return string.Format("close {0}", name);
        }
    }
}
