using SquadManager.UI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.SharedViewModels
{
    public class ComboBoxItemViewModel : ViewModel
    {
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (Equals(_name, value)) return;

                _name = value;
                RaisePropertyChanged();
            }
        }

        public ComboBoxItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
