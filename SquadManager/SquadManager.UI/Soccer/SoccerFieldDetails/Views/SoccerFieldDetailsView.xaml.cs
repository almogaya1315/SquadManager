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

            var editFormationControlTemplate = (ControlTemplate)Resources["EditFormationTemplate"];
            _formationCanvas = (Canvas)editFormationControlTemplate.Resources.FindName("FormationCanvas");
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            _player = (ToggleButton)sender;
            if (_player == null) return;
            AddHandler(MouseMoveEvent, new MouseEventHandler(OnMouseMoveChanged));
        }

        private void OnMouseMoveChanged(object sender, MouseEventArgs args)
        {
            Canvas.SetLeft(_player, args.GetPosition(_formationCanvas).X - _player.ActualWidth / 2);
        }
    }
}
