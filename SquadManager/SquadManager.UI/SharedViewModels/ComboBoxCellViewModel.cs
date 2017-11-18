using SquadManager.UI.Base;
using SquadManager.UI.Enums;
using SquadManager.UI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.SharedViewModels
{
    public class ComboBoxCellViewModel : EditableCellViewModel
    {
        public IEnumerable Items { get; set; }

        public ComboBoxCellViewModel(object value, IEnumerable items, IChangeManager changeManager, SoccerPlayer player = null, ColumnName? column = null, bool isEnabled = true) 
            : base(value, changeManager, column, player, isEnabled)
        {
            Items = items;
        }
    }
}
