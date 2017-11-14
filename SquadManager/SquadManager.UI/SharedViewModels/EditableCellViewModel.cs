using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.SharedViewModels
{
    public class EditableCellViewModel : CellViewModel
    {
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
            }
        }

        public EditableCellViewModel(object value, bool isEnabled = true) : base(value)
        {
            IsEnabled = isEnabled;
        }
    }
}
