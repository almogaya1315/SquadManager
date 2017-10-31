using GalaSoft.MvvmLight;
using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Base;
using SquadManager.UI.Menu.ViewModels;

namespace SquadManager.UI.Container.ViewModels
{
    public class ContainerViewModel : ViewModelBase
    {
        private ViewModelManager _viewModelManager;

        public ViewModelBase ContainerContent { get; set; }

        public ContainerViewModel()
        {
            _viewModelManager = new ViewModelManager(new Browser());

            ContainerContent = _viewModelManager.Menu;
        }
    }
}
