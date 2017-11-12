using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Models
{
    public class Position
    {
        public PositionGroup Group { get; set; }
        public PositionRole Role { get; set; }
    }

    public enum PositionGroup
    {
        GoalKeepers = 1,
        Defenders = 2,
        Midfielders = 3,
        Attackers = 4,
    }

    public enum PositionRole
    {
        GK = 1,
        RB = 2,
        RWB = 3,
        CB = 4,
        LWB = 5,
        LB = 6,
        RDM = 7,
        CDM = 8,
        LDM = 9,
        LM = 10,
        LWM = 11,
        CM = 12,
        RWM = 13,
        RM = 14,
        RCAM = 15,
        CAM = 16,
        LCAM = 17,
        RW = 18,
        LW = 19,
        RF = 20,
        CF = 21,
        LF = 22,
        RS = 23,
        ST = 24,
        LS = 25,
    }
}
