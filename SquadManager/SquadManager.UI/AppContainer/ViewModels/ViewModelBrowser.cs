using GalaSoft.MvvmLight;
using SquadManager.UI.Base;
using SquadManager.UI.Container.ViewModels;
using SquadManager.UI.ManagerDetails.ViewModels;
using SquadManager.UI.Menu.ViewModels;
using SquadManager.UI.Soccer.ViewModels;
using SquadManager.UI.TeamDetails.ViewModels;
using System.Collections.Generic;

namespace SquadManager.UI.AppContainer.ViewModels
{
    public class ViewModelBrowser
    {
        private ContainerViewModel _container { get; set; }

        public ViewModelBrowser(ContainerViewModel container) 
        {
            _container = container;
        }

        public void Browse(BrowseArgs args)
        {
            _container.Browsed(args);
        }
    }
}
