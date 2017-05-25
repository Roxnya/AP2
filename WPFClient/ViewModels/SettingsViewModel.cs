using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Models;

namespace WPFClient.ViewModels
{
    /// <summary>
    /// Class SettingsViewModel.
    /// </summary>
    /// <seealso cref="WPFClient.ViewModels.ViewModel" />
    public class SettingsViewModel : ViewModel
    {
        /// <summary>
        /// The model
        /// </summary>
        private ISettingsModel model;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="model">The model.</param>
        public SettingsViewModel(ISettingsModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Gets or sets the server ip.
        /// </summary>
        /// <value>The server ip.</value>
        public string ServerIP
        {
            get { return model.ServerIP; }
            set
            {
                model.ServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }

        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>The server port.</value>
        public int ServerPort
        {
            get { return model.ServerPort; }
            set
            {
                model.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>The columns.</value>
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

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public int Rows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("Rows");
            }
        }

        /// <summary>
        /// Gets or sets the selected algorithm.
        /// </summary>
        /// <value>The selected algorithm.</value>
        public int SelectedAlgorithm
        {
            get { return model.SearchAlgorithm; }
            set
            {
                model.SearchAlgorithm = value;
                NotifyPropertyChanged("SelectedAlgorithm");
            }
        }

        /// <summary>
        /// Cancels the settings.
        /// </summary>
        public void CancelSettings()
        {
            model.CancelSettings();
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            model.SaveSettings();
        }
    }
}
