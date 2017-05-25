using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Models;

namespace WPFClient.ViewModels
{
    public class SinglePlayerSetUpViewModel : ViewModel
    {
        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public int Rows { get; set; }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>The columns.</value>
        public int Columns { get; set; }
        public SinglePlayerSetUpViewModel(ISettingsModel sm)
        {
            this.Rows = sm.MazeRows;
            this.Columns = sm.MazeCols;
        }
    }
}
