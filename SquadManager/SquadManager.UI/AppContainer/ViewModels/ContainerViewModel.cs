using System;
using GalaSoft.MvvmLight;
using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Base;

namespace SquadManager.UI.Container.ViewModels
{
    public class ContainerViewModel : ViewModel, IBrowseable
    {
        private Injector _injector;

        private ViewModelManager _viewModelManager;

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

            _viewModelManager = _injector.GetManager(this);
            _viewModelManager.Menu.Manager = _viewModelManager;
            _viewModelManager.SquadDetails.Manager = _viewModelManager;

            ContainerContent = _viewModelManager.Menu;
        }

        public void Browsed(BrowseArgs args)
        {
            if (args.Type == ArgsType.SquadDetailsArgs)
            {
                ContainerContent = _viewModelManager.SquadDetails;
            }
        }
    }
}
