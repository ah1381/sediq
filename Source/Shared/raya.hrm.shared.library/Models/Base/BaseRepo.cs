using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Npgsql;
using Raya.Hrm.Shared.Library.Models.Configs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Models.Base
{
    public abstract class BaseRepository(string connectionString)
    {
        protected string CountQuery = "select count(*) from ";
        protected string QueryStart = "select * from ";

        //protected IDbConnection DbConnection;
        protected IDbConnection CreateConnection(string? ConnectionType = "")
        {
            if (ConnectionType == "sql")
            {
                var DbsqlConnection = new SqlConnection(connectionString);
                DbsqlConnection.Open();
                return DbsqlConnection;
            }
            else
            {
                var DbNpgsqlConnection = new NpgsqlConnection(connectionString);
                DbNpgsqlConnection.Open();
                return DbNpgsqlConnection;
            }
        }
    }
}
