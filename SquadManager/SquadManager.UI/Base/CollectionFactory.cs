using SquadManager.UI.ManagerDetails.ViewModels;
using SquadManager.UI.Models;
using SquadManager.UI.Repositories;
using SquadManager.UI.SharedViewModels;
using SquadManager.UI.TeamDetails.ViewModels;
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
        public List<ComboBoxItemViewModel> CityViewModels { get; set; }
        public List<ComboBoxItemViewModel> SportViewModels { get; set; }

        public List<PositionRole> PositionRoles { get; set; }

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

        public List<TeamViewModel> TeamViewModels
        {
            get
            {
                return _app.Teams.Select(t => new TeamViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Manager = ManagerViewModels.Find(m => m.Id == t.ManagerId),
                    Nation = NationViewModels.Find(n => n.Id == t.NationId),
                    City = CityViewModels.Find(c => c.Id == t.CityId),
                    Sport = SportViewModels.Find(s => s.Id == t.SportId),
                }).ToList();
            }
        }

        public CollectionFactory(Application app, ISquadRepository squadRepository)
        {
            _app = app;

            NationViewModels = squadRepository.GetNations().Select(n => new ComboBoxItemViewModel(n.Id, n.Name)).ToList();
            CityViewModels = squadRepository.GetCities().Select(n => new ComboBoxItemViewModel(n.Id, n.Name)).ToList();
            SportViewModels = squadRepository.GetSports().Select(n => new ComboBoxItemViewModel(n.Id, n.Name)).ToList();

            PositionRoles = Enum.GetValues(typeof(PositionRole)).Cast<PositionRole>().ToList(); //.Select(pr => new ComboBoxItemViewModel((int)pr, pr.ToString())).ToList();
        }
    }
}
