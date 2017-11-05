using SquadManager.UI.ManagerDetails.ViewModels;
using SquadManager.UI.Models;
using SquadManager.UI.Repositories;
using SquadManager.UI.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Base
{
    public class CollectionFactory
    {
        private Application _app;

        public List<ComboBoxItemViewModel> NationViewModels { get; set; }
        public List<ManagerViewModel> ManagerViewModels
        {
            get
            {
                return _app.Managers.Select(m => new ManagerViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Nationality = NationViewModels.Find(n => n.Id == m.Nationality.Id),
                    Age = m.Age
                }).ToList();
            }
        }

        public CollectionFactory(Application app, ISquadRepository squadRepository)
        {
            _app = app;

            NationViewModels = squadRepository.GetNations().Select(n => new ComboBoxItemViewModel(n.Id, n.Name)).ToList();
        }
    }
}
