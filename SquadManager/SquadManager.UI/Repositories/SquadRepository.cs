using SquadManager.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

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

        public List<Manager> GetManagers()
        {
            using (var con = OpenConnection())
            {
                return con.Query<Manager>("stp_SquadManager_GetManagers", commandType: CommandType.StoredProcedure).ToList();
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
