using GalaSoft.MvvmLight;
using SquadManager.UI.Base;
using SquadManager.UI.Container.ViewModels;
using SquadManager.UI.Menu.ViewModels;
using SquadManager.UI.SquadDetails.ViewModels;
using System.Collections.Generic;

namespace SquadManager.UI.AppContainer.ViewModels
{
    public class ViewModelManager
    {
        private List<IBrowseable> _browseables;
        private ContainerViewModel _container { get; set; }

        public MenuViewModel Menu { get; set; }
        public SquadDetailsViewModel SquadDetails { get; set; }

        public ViewModelManager(Injector injector, ContainerViewModel container)
        {
            _container = container;

            Menu = injector.New<MenuViewModel>();
            SquadDetails = injector.New<SquadDetailsViewModel>();

            _browseables = new List<IBrowseable>()
            {
                _container, Menu, SquadDetails
            };
        }

        public void Browse(BrowseArgs args)
        {
            _browseables.ForEach(b => b.Browsed(args));
        }
    }
}
