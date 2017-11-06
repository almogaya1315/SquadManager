using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Container.ViewModels;
using SquadManager.UI.ManagerDetails.ViewModels;
using SquadManager.UI.Models;
using SquadManager.UI.Repositories;
using SquadManager.UI.TeamDetails.ViewModels;

namespace SquadManager.UI.Base
{
    public class Injector
    {
        private Application _app;
        private ViewModelBrowser _browser;
        private ISquadRepository _squadRepository;
        private CollectionFactory _collections;

        public Injector()
        {
            _app = new Application();
            _squadRepository = new SquadRepository();
            _collections = new CollectionFactory(_app, _squadRepository);

            _app.Managers = _squadRepository.GetManagers();
        }

        public ViewModelBrowser GetBrowser(ContainerViewModel container)
        {
            return _browser = new ViewModelBrowser(this, container);
        }

        public T New<T>(ManagerViewModel manager = null) where T : ViewModel, new()
        {
            if (typeof(T) == typeof(ManagerDetailsViewModel))
            {
                var managerViewModel = new ManagerDetailsViewModel(_app, _collections);
                managerViewModel.SquadRepository = _squadRepository;
                return managerViewModel as T;
            }
            if (typeof(T) == typeof(TeamDetailsViewModel))
            {
                var teamViewModel = new TeamDetailsViewModel(_app, manager, _collections);
                teamViewModel.Browser = _browser;
                teamViewModel.SquadRepository = _squadRepository;
                return teamViewModel as T;
            }

            var instance = new T();
            instance.App = _app;
            instance.SquadRepository = _squadRepository;
            return instance as T;
        }
    }
}
