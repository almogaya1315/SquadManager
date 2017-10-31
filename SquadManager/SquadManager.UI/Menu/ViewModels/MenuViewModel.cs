using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using SquadManager.UI.Base;

namespace SquadManager.UI.Menu.ViewModels
{
    public class MenuViewModel : ViewModelBase, IBrowsable
    {
        private readonly Browser _browser;

        public ICommand NewSquad { get; set; }

        public MenuViewModel(Browser browser)
        {
            _browser = browser;

            NewSquad = new RelayCommand(CreateNewSquad);
        }

        private void CreateNewSquad()
        {
            _browser.Browse();
        }

        public void Browsed()
        {
            throw new NotImplementedException();
        }
    }
}
