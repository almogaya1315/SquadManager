using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Container.ViewModels;
using SquadManager.UI.LoadTeam.ViewModels;
using SquadManager.UI.ManagerDetails.ViewModels;
using SquadManager.UI.Menu.ViewModels;
using SquadManager.UI.Models;
using SquadManager.UI.Repositories;
using SquadManager.UI.Soccer.ViewModels;
using SquadManager.UI.TeamDetails.ViewModels;
using System;

namespace SquadManager.UI.Base
{
    public class Injector
    {
        private Application _app;
        private ViewModelBrowser _browser;
        private ISquadRepository _squadRepository;
        private CollectionFactory _collections;

        public Injector(ContainerViewModel container)
        {
            _app = new Application();
            _browser = new ViewModelBrowser(container);
            _squadRepository = new SquadRepository();
            _collections = new CollectionFactory(_app, _squadRepository);

            _app.Managers = _squadRepository.GetManagers();
            _app.Teams = _squadRepository.GetTeams();
        }

        public ViewModelBrowser GetBrowser(ContainerViewModel container)
        {
            return _browser = new ViewModelBrowser(container);
        }

        public T New<T>(ManagerViewModel manager = null, TeamViewModel team = null) where T : ViewModel, new()
        {
            if (typeof(T) == typeof(LoadTeamViewModel))
            {
                var loadTeamViewModel = new LoadTeamViewModel(_app, _squadRepository, _collections);
                loadTeamViewModel.Browser = _browser;
                return loadTeamViewModel as T;
            }
            if (typeof(T) == typeof(MenuViewModel))
            {
                var menuViewModel = new MenuViewModel();
                menuViewModel.Browser = _browser;
                return menuViewModel as T;
            }
            if (typeof(T) == typeof(ManagerDetailsViewModel))
            {
                var managerViewModel = new ManagerDetailsViewModel();
                managerViewModel.App = _app;
                managerViewModel.Browser = _browser;
                managerViewModel.Collections = _collections;
                managerViewModel.SquadRepository = _squadRepository;
                return managerViewModel as T;
            }
            if (typeof(T) == typeof(TeamDetailsViewModel))
            {
                var teamViewModel = new TeamDetailsViewModel(manager);
                teamViewModel.App = _app;
                teamViewModel.Browser = _browser;
                teamViewModel.Collections = _collections;
                teamViewModel.SquadRepository = _squadRepository;
                return teamViewModel as T;
            }
            if (typeof(T) == typeof(SoccerViewModel))
            {
                var soccerViewModel = new SoccerViewModel(team);
                soccerViewModel.App = _app;
                soccerViewModel.Browser = _browser;
                soccerViewModel.Collections = _collections;
                soccerViewModel.SquadRepository = _squadRepository;
                return soccerViewModel as T;
            }

            throw new InvalidOperationException();
        }
    }
}
