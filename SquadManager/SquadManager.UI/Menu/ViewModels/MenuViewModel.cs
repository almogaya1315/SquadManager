using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using SquadManager.UI.Base;
using SquadManager.UI.AppContainer.ViewModels;

namespace SquadManager.UI.Menu.ViewModels
{
    public class MenuViewModel : ViewModelBase, IViewModel
    {
        public ViewModelManager Manager { get; set; }

        public ICommand NewSquad { get; set; }

        public MenuViewModel() 
        {
            NewSquad = new RelayCommand(CreateNewSquad);
        }

        private void CreateNewSquad()
        {

        }

        public void Browsed()
        {
            
        }
    }
}
