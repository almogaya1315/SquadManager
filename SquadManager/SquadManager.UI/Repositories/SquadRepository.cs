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

        public void AddManager(ManagerViewModel manager)
        {
            using (var con = OpenConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", manager.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("@Nation", manager.Nationality.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Age", manager.Age, DbType.Int32, ParameterDirection.Input);

                con.Query("stp_SquadManager_AddManager", parameters, commandType: CommandType.StoredProcedure);
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

        public void CreateNewTeam()
        {
            throw new NotImplementedException();
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
    }
}
