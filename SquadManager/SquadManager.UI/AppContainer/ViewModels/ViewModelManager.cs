using GalaSoft.MvvmLight;
using SquadManager.UI.Base;
using SquadManager.UI.Container.ViewModels;
using SquadManager.UI.Menu.ViewModels;
using SquadManager.UI.TeamDetails.ViewModels;
using System.Collections.Generic;

namespace SquadManager.UI.AppContainer.ViewModels
{
    public class ViewModelManager
    {
        private ContainerViewModel _container { get; set; }

        public MenuViewModel Menu { get; set; }
        public TeamDetailsViewModel TeamDetails { get; set; }

        public ViewModelManager(Injector injector, ContainerViewModel container)
        {
            _container = container;

            Menu = injector.New<MenuViewModel>();
            TeamDetails = injector.New<TeamDetailsViewModel>();
        }

        public void Browse(BrowseArgs args)
        {
            _container.Browsed(args);
        }
    }
}
