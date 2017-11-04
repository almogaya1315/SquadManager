﻿using SquadManager.UI.Models;
using SquadManager.UI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Base
{
    public class CollectionFactory
    {
        public List<Nation> Nations { get; set; }

        public CollectionFactory(ISquadRepository squadRepository)
        {
            Nations = squadRepository.GetNations();
        }
    }
}
