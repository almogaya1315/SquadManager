using SquadManager.UI.ManagerDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Base
{
    public class BrowseArgs
    {
        public ArgsType Type { get; set; }

        public BrowseArgs(ArgsType type)
        {
            Type = type;
        }
    }

    public class TeamDetailsArgs : BrowseArgs
    {
        public ManagerViewModel Manager { get; set; }

        public TeamDetailsArgs(ArgsType type, ManagerViewModel manager) : base(type)
        {
            Manager = manager;
        }
    }

    public enum ArgsType
    {
        MenuArgs,
        ManagerDetailsArgs,
        TeamDetailsArgs,
    }
}
