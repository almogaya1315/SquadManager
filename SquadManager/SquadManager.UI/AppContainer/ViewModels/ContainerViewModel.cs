using System;
using GalaSoft.MvvmLight;
using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Base;
using SquadManager.UI.SharedViewModels;
using SquadManager.UI.ManagerDetails.ViewModels;
using SquadManager.UI.TeamDetails.ViewModels;

namespace SquadManager.UI.Container.ViewModels
{
    public class ContainerViewModel : ViewModel, IBrowseable
    {
        private Injector _injector;

        private ViewModelBrowser _viewModelBrowser;

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
            _injector = new Injector();

            _viewModelBrowser = _injector.GetBrowser(this);
            _viewModelBrowser.Menu.Browser = _viewModelBrowser;
            _viewModelBrowser.ManagerDetails.Browser = _viewModelBrowser;
            _viewModelBrowser.TeamDetails.Browser = _viewModelBrowser;

            ContainerContent = _viewModelBrowser.Menu;
        }

        public void Browsed(BrowseArgs args)
        {
            switch (args.Type)
            {
                case ArgsType.TeamDetailsArgs:
                    var teamDetailsArgs = (TeamDetailsArgs)args;
                    _viewModelBrowser.TeamDetails = new TeamDetailsViewModel(teamDetailsArgs.Manager);
                    ContainerContent = _viewModelBrowser.TeamDetails;
                    break;
                case ArgsType.ManagerDetailsArgs:
                    ContainerContent = _viewModelBrowser.ManagerDetails;
                    break;
                case ArgsType.MenuArgs:
                    ContainerContent = _viewModelBrowser.Menu;
                    break;
            }
        }
    }
}
