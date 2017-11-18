using SquadManager.UI.Base;
using SquadManager.UI.Enums;
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
        private ColumnName? _column;

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

                if (_changeManager != null && _player != null && _column.HasValue)
                {
                    //_player[ColumnName] = value;

                    if (_column == ColumnName.IsCaptain) _player.IsCaptain = (bool)value;
                    if (_column == ColumnName.Position) _player.Position.Role = (PositionRole)value;

                    _changeManager.Change(new SoccerPlayerArgs(_player, ChangeType.PlayerChanged, _column));
                }
            }
        }

        public void SetValueToBinding(object value)
        {
            if (!Equals(Value, value)) _value = value;
            RaisePropertyChanged("Value");
        }

        public EditableCellViewModel(object value, IChangeManager changeManager, ColumnName? column = null, SoccerPlayer player = null, bool isEnabled = true) : base(value)
        {
            _changeManager = changeManager;
            IsEnabled = isEnabled;
            if (player != null) _player = player;
            if (column.HasValue) _column = column;
        }
    }
}
