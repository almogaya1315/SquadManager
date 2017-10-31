using SquadManager.UI.Base;
using SquadManager.UI.Menu.ViewModels;

namespace SquadManager.UI.AppContainer.ViewModels
{
    public class ViewModelManager
    {
        private Injector _injector;

        public MenuViewModel Menu { get; set; }

        public ViewModelManager(Browser browser)
        {
            _injector = new Injector();

            Menu = new MenuViewModel(browser);
        }
    }
}
