using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Models;

namespace WPFClient.ViewModels
{
    public class MenuViewModel : ViewModel
    {
        ISettingsModel settingsModel;
        public MenuViewModel()
        {
            //settings always represents the same window - one for the entire game
            //thus we only need on view instance of it's vm.
            settingsModel = new SettingsModel();
        }

        public void OpenSinglePlayerRoom()
        {
            DialogHelper.OpenModalWindow(new SinglePlayerSetUp(settingsModel));
        }

        public void OpenMultiPlayerRoom()
        {
            DialogHelper.OpenModalWindow(new MultiPlayerSetUp(settingsModel));
        }

        public void OpenSettings()
        {
            DialogHelper.OpenModalWindow(new Settings(settingsModel));
        }
    }
}
