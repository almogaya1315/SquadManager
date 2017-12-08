using SquadManager.UI.Base;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SquadManager.UI.Soccer.SoccerFieldDetails.Views
{
    public partial class SoccerFieldDetailsView : UserControl
    {
        private Canvas _formationCanvas;
        private ToggleButton _player;

        public SoccerFieldDetailsView()
        {
            InitializeComponent();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var editFormationControlTemplate = (ControlTemplate)Resources["EditFormationTemplate"];
            var formationItemsControl = (ItemsControl)editFormationControlTemplate.FindName("FormationItemsControl", FormationContentControl);
            _formationCanvas = XamlHelper.FindChild<Canvas>(this, "FormationCanvas"); 

            _player = (ToggleButton)sender;
            if (_player == null) return;
            AddHandler(MouseMoveEvent, new MouseEventHandler(OnMouseMoveChanged));
        }

        private void OnMouseMoveChanged(object sender, MouseEventArgs args)
        {
            var mouseX = args.GetPosition(_formationCanvas).X;
            var mouseY = args.GetPosition(_formationCanvas).Y;

            var playerViewModel = (SoccerPlayerViewModel)_player.DataContext;
            playerViewModel.X = (int)(mouseX - _player.ActualWidth / 2);
            playerViewModel.Y = (int)(mouseY - _player.ActualHeight / 2);
        }
    }
}
