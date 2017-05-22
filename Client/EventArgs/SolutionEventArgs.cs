using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class SolutionEventArgs : EventArgs
    {
        public SolutionEventArgs(string solution, string mazeName)
        {
            this.Solution = solution;
            this.MazeName = mazeName;
        }

        public string Solution { get; private set; }
        public string MazeName { get; private set; }
    }
}
