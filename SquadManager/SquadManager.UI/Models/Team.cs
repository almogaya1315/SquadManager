using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CaptainId { get; set; }
        public int ManagerId { get; set; }
        public int NationId { get; set; }
        public int CityId { get; set; }
        public int SportId { get; set; }
    }
}
