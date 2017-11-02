using System;
using GalaSoft.MvvmLight;
using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Base;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SquadManager.UI.Repositories;

namespace SquadManager.UI.SquadDetails.ViewModels
{
    public class SquadDetailsViewModel : ViewModel, IBrowseable
    {
        public ICommand NewTeam { get; set; }

        public SquadDetailsViewModel()
        {
            
        }

        public void Browsed(BrowseArgs args)
        {
            NewTeam = new RelayCommand(GetTeam);
        }

        private void GetTeam()
        {
            var team = SquadRepository.GetTeam(1);
            if (team == null)
            {

            }
        }
    }
}
