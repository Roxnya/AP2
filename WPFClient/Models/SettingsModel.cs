using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Models
{
    class SettingsModel : ISettingsModel
    {

        public string ServerIP
        {
            get { return Properties.Settings.Default.ServerIP; }
            set { Properties.Settings.Default.ServerIP = value; }
        }

        public int ServerPort
        {
            get { return Properties.Settings.Default.ServerPort; }
            set { Properties.Settings.Default.ServerPort = value; }
        }

        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRows; }
            set { Properties.Settings.Default.MazeRows = value; }
        }

        public int MazeCols
        {
            get { return Properties.Settings.Default.MazeColumns; }
            set { Properties.Settings.Default.MazeColumns = value; }
        }

        public int SearchAlgorithm
        {
            get { return Properties.Settings.Default.SearchAlgorithm; }
            set { Properties.Settings.Default.SearchAlgorithm = value; }
        }

        public void CancelSettings()
        {
            Properties.Settings.Default.Reload();
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
