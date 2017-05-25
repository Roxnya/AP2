using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Models;

namespace WPFClient.ViewModels
{
    /// <summary>
    /// Class MenuViewModel.
    /// </summary>
    /// <seealso cref="WPFClient.ViewModels.ViewModel" />
    public class MenuViewModel : ViewModel
    {
        /// <summary>
        /// The settings model
        /// </summary>
        public ISettingsModel SettingsModel { get; set; }
        /// <summary>
        /// Ctor.
        /// </summary>
        public MenuViewModel()
        {
            //settings always represents the same window - one for the entire game
            //thus we only need on view instance of it's vm.
            SettingsModel = new SettingsModel();
        }        
    }
}
