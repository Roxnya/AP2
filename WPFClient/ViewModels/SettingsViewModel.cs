using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Models;

namespace WPFClient.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        private ISettingsModel model;

        public SettingsViewModel(ISettingsModel model)
        {
            this.model = model;
        }

        public string ServerIP
        {
            get { return model.ServerIP; }
            set
            {
                model.ServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }

        public int ServerPort
        {
            get { return model.ServerPort; }
            set
            {
                model.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }

        public int Columns
        {
            get { 
                return model.MazeCols; 
            }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("Columns");
            }
        }

        public int Rows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("Rows");
            }
        }

        public int SelectedAlgorithm
        {
            get { return model.SearchAlgorithm; }
            set
            {
                model.SearchAlgorithm = value;
                NotifyPropertyChanged("SelectedAlgorithm");
            }
        }

        public void CancelSettings()
        {
            model.CancelSettings();
        }
        
        public void SaveSettings()
        {
            model.SaveSettings();
        }
    }
}
