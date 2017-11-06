using System;
using GalaSoft.MvvmLight;
using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Base;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SquadManager.UI.Repositories;
using SquadManager.UI.ManagerDetails.ViewModels;
using System.Collections.Generic;
using SquadManager.UI.Models;
using SquadManager.UI.SharedViewModels;

namespace SquadManager.UI.TeamDetails.ViewModels
{
    public class TeamDetailsViewModel : ViewModel
    {
        public TeamViewModel Team { get; set; }

        public List<ManagerViewModel> Managers { get; set; }

        public ICommand Back { get; set; }
        public ICommand Save { get; set; }

        public TeamDetailsViewModel() { }
        public TeamDetailsViewModel(Application app, ManagerViewModel manager, CollectionFactory collections)
        {
            App = app;
            Collections = collections;

            Managers = Collections.ManagerViewModels;

            Team = new TeamViewModel();
            if (manager != null) Team.Manager = Managers.Find(m => m.Id == manager.Id);

            Back = new RelayCommand(BackToManagerDetails);
            Save = new RelayCommand(SaveTeam);
        }

        private void SaveTeam()
        {
            var team = new Team()
            {
                ManagerId = Team.Manager.Id,
                Name = Team.Name,
                NationId = Team.Nation.Id,
                CityId = Team.City.Id,
                SportId = Team.Sport.Id,
            };

            SquadRepository.AddTeam(team);
        }

        private void BackToManagerDetails()
        {
            Browser.Browse(new BrowseArgs(ArgsType.ManagerDetailsArgs));
        }
    }
}
