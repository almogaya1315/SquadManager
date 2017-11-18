using SquadManager.UI.Base;
using SquadManager.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.SharedViewModels
{
    public class EditableCellViewModel : CellViewModel
    {
        private IChangeManager _changeManager;
        private SoccerPlayer _player;

        public bool IsEnabled { get; set; }

        private object _value;
        public override object Value
        {
            get { return _value; }
            set
            {
                if (Equals(_value, value)) return;

                _value = value;
                RaisePropertyChanged();

                if (_changeManager != null && _player != null)
                {
                    //_player[ColumnName] = value;

                    _player.IsCaptain = (bool)value;

                    _changeManager.Change(new SoccerPlayerArgs(_player, ChangeType.PlayerChanged));
                }
            }
        }

        public void SetValueToBinding(object value)
        {
            if (!Equals(Value, value)) _value = value;
            RaisePropertyChanged("Value");
        }

        public EditableCellViewModel(object value, IChangeManager changeManager, SoccerPlayer player = null, bool isEnabled = true) : base(value)
        {
            _changeManager = changeManager;
            IsEnabled = isEnabled;
            if (player != null) _player = player;
        }
    }
}
