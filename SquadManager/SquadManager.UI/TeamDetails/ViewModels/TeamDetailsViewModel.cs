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

        public TeamDetailsViewModel() { }
        public TeamDetailsViewModel(ManagerViewModel manager) 
        {
            Team.Manager = Collections.ManagerViewModels.Find(m => m.Id == manager.Id);
        }
        public TeamDetailsViewModel(Application app, CollectionFactory collections)
        {
            App = app;
            Collections = collections;

            Managers = Collections.ManagerViewModels;

            Team = new TeamViewModel();
        }
    }
}
