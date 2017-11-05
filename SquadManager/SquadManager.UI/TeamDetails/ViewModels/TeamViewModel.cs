using SquadManager.UI.Base;
using SquadManager.UI.ManagerDetails.ViewModels;
using SquadManager.UI.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.TeamDetails.ViewModels
{
    public class TeamViewModel : ViewModel
    {
        public string Name { get; set; }

        private ManagerViewModel _manager;
        public ManagerViewModel Manager
        {
            get { return _manager; }
            set
            {
                _manager = value;
                RaisePropertyChanged();
            }
        }
        public ComboBoxItemViewModel Nation { get; set; }
        public ComboBoxItemViewModel City { get; set; }
        public ComboBoxItemViewModel Sport { get; set; }
    }
}
