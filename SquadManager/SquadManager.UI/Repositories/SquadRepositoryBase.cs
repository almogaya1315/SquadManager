using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Dapper;

namespace SquadManager.UI.Repositories
{
    public class SquadRepositoryBase
    {
        private readonly string _connectionString;

        public SquadRepositoryBase()
        {
            _connectionString = @"data source=(localdb)\sqldev;initial catalog=SquadManager.DB;integrated security=True;MultipleActiveResultSets=True";
        }

        public SqlConnection OpenConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
