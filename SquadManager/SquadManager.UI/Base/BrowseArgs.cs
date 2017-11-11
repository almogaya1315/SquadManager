using SquadManager.UI.ManagerDetails.ViewModels;
using SquadManager.UI.Models;
using SquadManager.UI.TeamDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Base
{
    public class BrowseArgs
    {
        public BrowseArgsType Type { get; set; }

        public BrowseArgs(BrowseArgsType type)
        {
            Type = type;
        }
    }

    public class TeamDetailsArgs : BrowseArgs
    {
        public ManagerViewModel Manager { get; set; }

        public TeamDetailsArgs(BrowseArgsType type, ManagerViewModel manager) : base(type)
        {
            Manager = manager;
        }
    }

    public class SoccerArgs : BrowseArgs
    {
        public Team Team { get; set; }

        public SoccerArgs(BrowseArgsType type, Team team) : base(type)
        {
            Team = team;
        }
    }

    public enum BrowseArgsType
    {
        MenuArgs,
        ManagerDetailsArgs,
        TeamDetailsArgs,
        SoccerSquadArgs,
        LoadTeamArgs,
    }
}
