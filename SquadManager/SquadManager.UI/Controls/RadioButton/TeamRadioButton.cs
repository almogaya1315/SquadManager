using SquadManager.UI.TeamDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SquadManager.UI.Controls.RadioButton
{
    public class TeamRadioButton : System.Windows.Controls.RadioButton
    {
        public TeamViewModel SelectedTeam
        {
            get { return (TeamViewModel)GetValue(SelectedTeamProperty); }
            set { SetValue(SelectedTeamProperty, value); }
        }

        public static readonly DependencyProperty SelectedTeamProperty =
            DependencyProperty.Register("SelectedTeam", 
                typeof(TeamViewModel), typeof(TeamRadioButton), 
                new PropertyMetadata(null, SelectedTeamPropertyChanged));

        private static void SelectedTeamPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var teamButton = (TeamRadioButton)d;
            if (teamButton == null) return;

            teamButton.Checked += (s, e) =>
            {
                if (teamButton.IsChecked.Value)
                {
                    var team = (TeamViewModel)teamButton.DataContext;
                    if (team == null) return;

                    d.SetValue(SelectedTeamProperty, team);
                }
            };
        }
    }
}
