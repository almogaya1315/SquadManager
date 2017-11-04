using SquadManager.UI.Base;
using SquadManager.UI.Models;
using SquadManager.UI.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.ManagerDetails.ViewModels
{
    public class ManagerViewModel : ViewModel
    {
        public event EventHandler PropertyChanged;

        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (Id > 0) PropertyChanged?.Invoke(this, null);
            }
        }
        private ComboBoxItemViewModel _nationality;
        public ComboBoxItemViewModel Nationality
        {
            get { return _nationality; }
            set
            {
                _nationality = value;
                if (Id > 0) PropertyChanged?.Invoke(this, null);
            }
        }
        private int? _age;
        public int? Age
        {
            get { return _age; }
            set
            {
                _age = value;
                if (Id > 0) PropertyChanged?.Invoke(this, null);
            }
        }
    }
}
