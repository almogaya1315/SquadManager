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
        private ToggleButton _player;
        private bool _playerDragged;
        private MouseEventHandler _onMouseMoveChanged;

        public SoccerFieldDetailsView()
        {
            InitializeComponent();

            _onMouseMoveChanged = new MouseEventHandler(OnMouseMoveChanged);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            _player = (ToggleButton)sender;
            if (_player == null) return;

            if (!_playerDragged)
            {
                AddHandler(MouseMoveEvent, _onMouseMoveChanged);
                _playerDragged = true;
            }
            else
            {
                RemoveHandler(MouseMoveEvent, _onMouseMoveChanged);
                _playerDragged = false;
            }
        }

        private void OnMouseMoveChanged(object sender, MouseEventArgs args)
        {
            var mouseX = args.GetPosition(FormationCanvas()).X;
            var mouseY = args.GetPosition(FormationCanvas()).Y;

            var playerViewModel = (SoccerPlayerViewModel)_player.DataContext;
            playerViewModel.X = (int)(mouseX - _player.ActualWidth / 2);
            playerViewModel.Y = (int)(mouseY - _player.ActualHeight / 2);

            // TODO:
            // stay inside canvas
        }

        private Canvas FormationCanvas()
        {
            var editFormationControlTemplate = (ControlTemplate)Resources["EditFormationTemplate"];
            var formationItemsControl = (ItemsControl)editFormationControlTemplate.FindName("FormationItemsControl", FormationContentControl);
            return XamlHelper.FindChild<Canvas>(this, "FormationCanvas");
        }
    }
}
