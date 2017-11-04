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
        public List<ComboBoxItemViewModel> NationViewModels { get; set; }

        public CollectionFactory(ISquadRepository squadRepository)
        {
            NationViewModels = squadRepository.GetNations().Select(n => new ComboBoxItemViewModel(n.Id, n.Name)).ToList();
        }
    }
}
