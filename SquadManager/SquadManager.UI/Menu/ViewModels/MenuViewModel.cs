using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using SquadManager.UI.Base;
using SquadManager.UI.AppContainer.ViewModels;

namespace SquadManager.UI.Menu.ViewModels
{
    public class MenuViewModel : ViewModel
    {
        public ICommand NewSquad { get; set; }
        public ICommand Load { get; set; }

        public MenuViewModel() { }
        public MenuViewModel(ViewModelBrowser browser) 
        {
            Browser = browser;

            NewSquad = new RelayCommand(CreateNewSquad);
            Load = new RelayCommand(LoadSquad);
        }

        private void LoadSquad()
        {
            Browser.Browse(new BrowseArgs(BrowseArgsType.LoadTeamArgs));
        }

        private void CreateNewSquad()
        {
            Browser.Browse(new BrowseArgs(BrowseArgsType.ManagerDetailsArgs));
        }
    }
}
