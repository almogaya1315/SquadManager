using SquadManager.UI.Base;
using SquadManager.UI.ManagerDetails.ViewModels;
using SquadManager.UI.SharedViewModels;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.TeamDetails.ViewModels
{
    public class TeamViewModel : ViewModel
    {
        IChangeManager _changeManager;

        public int Id { get; set; }
        public string Name { get; set; }

        private ManagerViewModel _manager;
        public ManagerViewModel Manager
        {
            get { return _manager; }
            set
            {
                _manager = value;
                RaisePropertyChanged();

                if (_changeManager != null)
                    _changeManager.Change(new ChangeArgs(ChangeType.TeamChanged));
            }
        }

        public SoccerPlayerViewModel Captain { get; set; }

        public List<SoccerPlayerViewModel> Squad { get; set; }

        public ComboBoxItemViewModel Nation { get; set; }
        public ComboBoxItemViewModel City { get; set; }
        public ComboBoxItemViewModel Sport { get; set; }
        public EditableCellViewModel Crest { get; set; }

        public TeamViewModel(IChangeManager changeManager = null)
        {
            _changeManager = changeManager;
        }
    }
}
