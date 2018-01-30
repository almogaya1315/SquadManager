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
                con.Query<Manager, Nation, Manager>("stp_SquadManager_GetManagers",
                    (manager, nation) =>
                    {
                        Manager innerManager;

                        if (!lookup.TryGetValue(manager.Id, out innerManager)) lookup.Add(manager.Id, innerManager = manager);

                        innerManager.Nationality = nation;

                        return innerManager;
                    },
                    commandType: CommandType.StoredProcedure).ToList();

                return lookup.Values.ToList();
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

        public void UpdatePlayer(SoccerPlayer player)
        {
            using (var con = OpenConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TeamId", player.TeamId, DbType.Int32, ParameterDirection.Input);
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

        public List<Formation> GetTeamFormations(int teamId)
        {
            using (var con = OpenConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TeamId", teamId, DbType.Int32, ParameterDirection.Input);

                return con.Query<Formation>("stp_SquadManager_GetTeamFormations", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public int AddFormation(Formation formation)
        {
            using (var con = OpenConnection())
            {
                var parameters = SetFormationParameters(formation);
                return con.Query<int>("stp_SquadManager_AddFormation", parameters, commandType: CommandType.StoredProcedure).Single();
            }
        }

        public void UpdateFormation(Formation formation)
        {
            using (var con = OpenConnection())
            {
                var parameters = SetFormationParameters(formation);
                con.Query("stp_SquadManager_UpdateFormation", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        private DynamicParameters SetFormationParameters(Formation formation)
        {
            var parameters = new DynamicParameters();
            if (formation.Id > 0)
            {
                parameters.Add("@FormationId", formation.Id, DbType.Int32, ParameterDirection.Input);
            }
            parameters.Add("@TeamId", formation.TeamId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Name", formation.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("@PlayerId1", formation.Player_1Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerX1", formation.Player_1X, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerY1", formation.Player_1Y, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerId2", formation.Player_2Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerX2", formation.Player_2X, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerY2", formation.Player_2Y, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerId3", formation.Player_3Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerX3", formation.Player_3X, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerY3", formation.Player_3Y, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerId4", formation.Player_4Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerX4", formation.Player_4X, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerY4", formation.Player_4Y, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerId5", formation.Player_5Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerX5", formation.Player_5X, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerY5", formation.Player_5Y, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerId6", formation.Player_6Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerX6", formation.Player_6X, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerY6", formation.Player_6Y, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerId7", formation.Player_7Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerX7", formation.Player_7X, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerY7", formation.Player_7Y, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerId8", formation.Player_8Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerX8", formation.Player_8X, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerY8", formation.Player_8Y, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerId9", formation.Player_9Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerX9", formation.Player_9X, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerY9", formation.Player_9Y, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerId10", formation.Player_10Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerX10", formation.Player_10X, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerY10", formation.Player_10Y, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerId11", formation.Player_11Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerX11", formation.Player_11X, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PlayerY11", formation.Player_11Y, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }
    }
}

