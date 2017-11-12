using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Base
{
    public class ChangeManager : IChangeManager
    {
        public List<IChangeable> Changeables { get; set; }

        public void Change(ChangeArgs args)
        {
            //Changeables.ForEach(c => c.Changed(args));
        }
    }
}
