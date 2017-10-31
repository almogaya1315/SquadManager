using SquadManager.UI.Container.ViewModels;
using System.Windows;

namespace SquadManager.UI.AppContainer.Views
{
    public partial class Container : Window
    {
        public Container()
        {
            InitializeComponent();

            DataContext = new ContainerViewModel();
        }
    }
}
