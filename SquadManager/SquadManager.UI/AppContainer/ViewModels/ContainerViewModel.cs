using System;
using GalaSoft.MvvmLight;
using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Base;
using SquadManager.UI.SharedViewModels;
using SquadManager.UI.ManagerDetails.ViewModels;
using SquadManager.UI.TeamDetails.ViewModels;
using SquadManager.UI.Soccer.ViewModels;
using SquadManager.UI.Menu.ViewModels;
using SquadManager.UI.LoadTeam.ViewModels;
using SquadManager.UI.Models;

namespace SquadManager.UI.Container.ViewModels
{
    public class ContainerViewModel : ViewModel
    {
        private Injector _injector;

        private ViewModelBase _containerContent;
        public ViewModelBase ContainerContent
        {
            get { return _containerContent; }
            set
            {
                _containerContent = value;
                RaisePropertyChanged();
            }
        }

        public ContainerViewModel()
        {
            _injector = new Injector(this);

            Browsed(new BrowseArgs(BrowseArgsType.MenuArgs));
        }

        public void Browsed(BrowseArgs args)
        {
            switch (args.Type)
            {
                case BrowseArgsType.SoccerSquadArgs:
                    var soccerArgs = (SoccerArgs)args;
                    ContainerContent = _injector.New<SoccerViewModel>(team: soccerArgs.Team);
                    break;
                case BrowseArgsType.TeamDetailsArgs:
                    var teamDetailsArgs = (TeamDetailsArgs)args;
                    ContainerContent = _injector.New<TeamDetailsViewModel>(manager: teamDetailsArgs.Manager);
                    break;
                case BrowseArgsType.ManagerDetailsArgs:
                    ContainerContent = _injector.New<ManagerDetailsViewModel>(); 
                    break;
                case BrowseArgsType.MenuArgs:
                    ContainerContent = _injector.New<MenuViewModel>(); 
                    break;
                case BrowseArgsType.LoadTeamArgs:
                    ContainerContent = _injector.New<LoadTeamViewModel>();
                    break;
            }
        }
    }
}
