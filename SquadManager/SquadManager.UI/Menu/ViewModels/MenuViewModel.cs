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

        public MenuViewModel() 
        {
            NewSquad = new RelayCommand(CreateNewSquad);
        }

        private void CreateNewSquad()
        {
            Manager.Browse(new BrowseArgs(ArgsType.SquadDetailsArgs));
        }
    }
}
