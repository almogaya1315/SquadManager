using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Extensions
{
    public class SquadList<T> : List<T> where T : SoccerPlayerViewModel
    {
        public void RemoveFirstNames()
        {
            string firstName = string.Empty;
            foreach (var player in this)
            {
                foreach (var c in (string)player.Name.Value)
                {
                    if (Char.IsWhiteSpace(c)) break;
                    firstName += c;
                }
                var sureName = (player.Name.Value as string).Replace(firstName, string.Empty).TrimStart(new char[] { ' ' });
                player.Name.SetValueToBinding(sureName);
                firstName = string.Empty;
                sureName = string.Empty;
            }
        }
    }
}
