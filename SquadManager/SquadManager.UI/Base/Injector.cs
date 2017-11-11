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
        private IChangeManager _changeManager;

        public Injector(ContainerViewModel container)
        {
            _app = new Application();
            _browser = new ViewModelBrowser(container);
            _squadRepository = new SquadRepository();
            _collections = new CollectionFactory(_app, _squadRepository);
            _changeManager = new ChangeManager();

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
                return new LoadTeamViewModel(_app, _squadRepository, _collections, _browser) as T;
            }
            if (typeof(T) == typeof(MenuViewModel))
            {
                return new MenuViewModel(_browser) as T;
            }
            if (typeof(T) == typeof(ManagerDetailsViewModel))
            {
                return new ManagerDetailsViewModel(_app, _collections, _browser, _squadRepository) as T;
            }
            if (typeof(T) == typeof(TeamDetailsViewModel))
            {
                return new TeamDetailsViewModel(manager, _app, _squadRepository, _collections, _browser) as T;
            }
            if (typeof(T) == typeof(SoccerViewModel))
            {
                return new SoccerViewModel(team, _app, _collections, _squadRepository, _browser, _changeManager, this) as T;
            }

            throw new InvalidOperationException();
        }
    }
}
