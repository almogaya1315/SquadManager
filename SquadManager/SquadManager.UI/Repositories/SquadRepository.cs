using SquadManager.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using SquadManager.UI.ManagerDetails.ViewModels;

namespace SquadManager.UI.Repositories
{
    public class SquadRepository : SquadRepositoryBase, ISquadRepository
    {
        public List<Nation> GetNations()
        {
            using (var con = OpenConnection())
            {
                return con.Query<Nation>("stp_SquadManager_GetNations", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<City> GetCities()
        {
            using (var con = OpenConnection())
            {
                return con.Query<City>("stp_SquadManager_GetCities", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<Sport> GetSports()
        {
            using (var con = OpenConnection())
            {
                return con.Query<Sport>("stp_SquadManager_GetSports", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public int AddManager(Manager manager)
        {
            using (var con = OpenConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", manager.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("@Nation", manager.Nationality.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Age", manager.Age, DbType.Int32, ParameterDirection.Input);

                return con.Query<int>("stp_SquadManager_AddManager", parameters, commandType: CommandType.StoredProcedure).Single();
            }
        }

        public List<Manager> GetManagers()
        {
            using (var con = OpenConnection())
            {
                var lookup = new Dictionary<int, Manager>();
                var managers = con.Query<Manager, Nation, Manager>("stp_SquadManager_GetManagers",
                    (manager, nation) =>
                    {
                        Manager innerManager;

                        if (!lookup.TryGetValue(manager.Id, out innerManager)) lookup.Add(manager.Id, innerManager = manager);

                        innerManager.Nationality = nation;

                        return innerManager;
                    },
                    commandType: CommandType.StoredProcedure).ToList();

                return managers;
            }
        }

        public int AddTeam(Team team)
        {
            using (var con = OpenConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TeamName", team.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("@TeamManagerId", team.ManagerId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@TeamNationId", team.NationId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@TeamCityId", team.CityId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@TeamSportId", team.SportId, DbType.Int32, ParameterDirection.Input);

                return con.Query<int>("stp_SquadManager_AddTeam", parameters, commandType: CommandType.StoredProcedure).Single();
            }
        }

        public Team GetTeam(int teamId)
        {
            using (var con = OpenConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TeamId", teamId, DbType.Int32, ParameterDirection.Input);

                return con.Query<Team>("stp_SquadManager_GetTeam", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public List<Team> GetTeams()
        {
            using (var con = OpenConnection())
            {
                return con.Query<Team>("stp_SquadManager_GetTeams", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public int AddPlayer(int teamId, SoccerPlayer player)
        {
            using (var con = OpenConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TeamId", teamId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Name", player.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("@Age", player.Age, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@BirthDate", player.BirthDate, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@Rotation", player.Rotation, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@IsCaptain", player.IsCaptain, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("@PositionRoleId", player.Position.Role, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@PositionGroupId", player.Position.Group, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Rating", player.Rating, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@NationId", player.Nationality, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@IsLineup", player.IsLineup, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("@IsInjured", player.IsInjured, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("@IsOnLoan", player.IsOnLoan, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("@IsLoaned", player.IsLoaned, DbType.Boolean, ParameterDirection.Input);

                return con.Query<int>("stp_SquadManager_AddPlayer", parameters, commandType: CommandType.StoredProcedure).Single();
            }
        }

        public void UpdatePlayer(int teamId, SoccerPlayer player)
        {
            using (var con = OpenConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TeamId", teamId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@PlayerId", player.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Name", player.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("@Age", player.Age, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@BirthDate", player.BirthDate, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@NationId", player.Nationality, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Rating", player.Rating, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Rotation", player.Rotation, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@IsCaptain", player.IsCaptain, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("@PositionRoleId", player.Position.Role, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@PosiotionGroupId", player.Position.Group, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@IsLineup", player.IsLineup, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("@IsInjured", player.IsInjured, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("@IsOnLoan", player.IsOnLoan, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("@IsLoaned", player.IsLoaned, DbType.Boolean, ParameterDirection.Input);

                con.Query("stp_SquadManager_UpdatePlayer", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public List<SoccerPlayer> GetTeamSquad(int teamId)
        {
            using (var con = OpenConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TeamId", teamId, DbType.Int32, ParameterDirection.Input);

                var lookup = new Dictionary<int, SoccerPlayer>();
                var players = con.Query<SoccerPlayer, Position, SoccerPlayer>("stp_SquadManager_GetTeamSquad",  
                    (player, position) =>
                    {
                        SoccerPlayer innerPlayer;
                        if (!lookup.TryGetValue(player.Id, out innerPlayer)) lookup.Add(player.Id, innerPlayer = player);
                        innerPlayer.Position = position;
                        return innerPlayer;
                    }, param: parameters, splitOn: "Role",
                    commandType: CommandType.StoredProcedure).ToList();

                return players;
            }
        }

        public void DeletePlayer(int teamId, int playerId)
        {
            using (var con = OpenConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TeamId", teamId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@PlayerId", playerId, DbType.Int32, ParameterDirection.Input);

                con.Query("stp_SquadManager_DeletePlayer", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public Formation GetDefaultFormation()
        {
            using (var con = OpenConnection())
            {
                return con.Query<Formation>("stp_SquadManager_GetDfaultFormation", commandType: CommandType.StoredProcedure).Single();
            }
        }
    }
}

