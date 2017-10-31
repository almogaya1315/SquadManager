using GalaSoft.MvvmLight;
using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Base;

namespace SquadManager.UI.Container.ViewModels
{
    public class ContainerViewModel : ViewModelBase
    {
        private Injector _injector;

        private ViewModelManager _viewModelManager;

        public ViewModelBase ContainerContent { get; set; }

        public ContainerViewModel()
        {
            _injector = new Injector();

            _viewModelManager = _injector.GetManager();

            ContainerContent = _viewModelManager.Menu;
        }
    }
}
