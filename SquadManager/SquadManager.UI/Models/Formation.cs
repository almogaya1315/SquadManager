using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Models
{
    public class Formation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamId { get; set; }

        public int Player_1Id { get; set; }
        public int Player_1X { get; set; }
        public int Player_1Y { get; set; }

        public int Player_2Id { get; set; }
        public int Player_2X { get; set; }
        public int Player_2Y { get; set; }

        public int Player_3Id { get; set; }
        public int Player_3X { get; set; }
        public int Player_3Y { get; set; }

        public int Player_4Id { get; set; }
        public int Player_4X { get; set; }
        public int Player_4Y { get; set; }

        public int Player_5Id { get; set; }
        public int Player_5X { get; set; }
        public int Player_5Y { get; set; }

        public int Player_6Id { get; set; }
        public int Player_6X { get; set; }
        public int Player_6Y { get; set; }

        public int Player_7Id { get; set; }
        public int Player_7X { get; set; }
        public int Player_7Y { get; set; }

        public int Player_8Id { get; set; }
        public int Player_8X { get; set; }
        public int Player_8Y { get; set; }

        public int Player_9Id { get; set; }
        public int Player_9X { get; set; }
        public int Player_9Y { get; set; }

        public int Player_10Id { get; set; }
        public int Player_10X { get; set; }
        public int Player_10Y { get; set; }

        public int Player_11Id { get; set; }
        public int Player_11X { get; set; }
        public int Player_11Y { get; set; }

        public List<int> LineupIds;
    }
}
