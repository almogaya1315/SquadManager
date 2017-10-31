using GalaSoft.MvvmLight;
using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Menu.ViewModels;

namespace SquadManager.UI.Container.ViewModels
{
    public class ContainerViewModel : ViewModelBase
    {
        public ViewModelBase ContainerContent { get; set; }

        private ViewModelManager _viewModelManager;

        public ContainerViewModel()
        {
            _viewModelManager = new ViewModelManager();

            ContainerContent = _viewModelManager.Menu;
        }
    }
}
