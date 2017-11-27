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
        List<City> GetCities();
        List<Sport> GetSports();

        int AddManager(Manager manager);
        List<Manager> GetManagers();

        int AddTeam(Team team);
        Team GetTeam(int teamId);
        List<Team> GetTeams();

        int AddPlayer(int teamId, SoccerPlayer player);
        void UpdatePlayer(int teamId, SoccerPlayer player);
        List<SoccerPlayer> GetTeamSquad(int teamId);
        void DeletePlayer(int teamId, int playerId);

        Formation GetDefaultFormation(); //stp_SquadManager_GetDfaultFormation
    }
}
