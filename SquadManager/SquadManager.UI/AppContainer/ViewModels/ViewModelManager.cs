using SquadManager.UI.Base;
using SquadManager.UI.Menu.ViewModels;
using SquadManager.UI.SquadDetails.ViewModels;

namespace SquadManager.UI.AppContainer.ViewModels
{
    public class ViewModelManager
    {
        public MenuViewModel Menu { get; set; }
        public SquadDetailsViewModel SquadDetails { get; set; }

        public ViewModelManager(Injector injector)
        {
            Menu = injector.New<MenuViewModel>(new ConstructorParameter("Browser", Browser)); //new MenuViewModel(browser);
            SquadDetails = new SquadDetailsViewModel();
        }
    }
}
