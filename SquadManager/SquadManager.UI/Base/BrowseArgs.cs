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

    public enum ArgsType
    {
        MenuArgs,
        SquadDetailsArgs,
    }
}
