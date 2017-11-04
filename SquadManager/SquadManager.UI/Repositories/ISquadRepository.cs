using SquadManager.UI.ManagerDetails.ViewModels;
using SquadManager.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Repositories
{
    public interface ISquadRepository
    {
        List<Nation> GetNations();

        void AddManager(ManagerViewModel manager);
        List<Manager> GetManagers();

        void CreateNewTeam();
        Team GetTeam(int teamId);
    }
}
