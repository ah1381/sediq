using Dapper;
using Microsoft.Extensions.Options;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Models.Configs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.GeneralHistoryUpdateService
{

    public interface IHistoryTableService
    {
        Task<long> CreateAsync<T>(HistoryTableRequestBase<T> updatedecord , IDbConnection connection, IDbTransaction transaction);
    }
    public class HistoryTableService : BaseRepository, IHistoryTableService
    {

        public HistoryTableService(IOptions<ServicesDbConfig> dbSettings, IOptions<RequestConfig> requestSettings)
            : base(dbSettings.Value.DefaultConnection)
        {
        }

        public async Task<long> CreateAsync<T>(HistoryTableRequestBase<T> model, IDbConnection connection, IDbTransaction transaction)
        {
            var sql = @"
                SELECT 
                    row_id 
                FROM rule.table_names
                WHERE table_name = @Tname";

            var TableRowId = await connection.QueryFirstOrDefaultAsync<long>(sql, new { Tname = model.TableName });
             
            if (TableRowId == 0)
            {
                var TableName = new StringBuilder($"""
                    INSERT INTO rule.table_names (
                        table_name,
                        cr
                    )
                    VALUES (
                        @TableName,
                        NOW()
                    )
                    RETURNING row_id;
                    """);

                TableRowId = await connection.ExecuteScalarAsync<long>(
                    TableName.ToString(),
                    new
                    {
                        TableName = model.TableName,
                    },
                      transaction: transaction
                );
            }



            var historysql = new StringBuilder($"""
                    INSERT INTO rule.table_history (
                        table_name_id,
                        affected_table_row_id,
                        cr,
                        changed_by_user_id,
                        previous_data
                    )
                    VALUES (
                        @TableNameId,
                        @AffectedRowId,
                        NOW(),
                        @ChangedByUserId,
                        CAST(@PreviousData AS jsonb)
                    )
                    RETURNING row_id;
                    """);

            var historyUpdateId = await connection.ExecuteScalarAsync<long>(
                historysql.ToString(),
                new
                {
                    TableNameId = TableRowId,
                    AffectedRowId = model.AffectedRowId,
                    ChangedByUserId = model.ChangedByUserId,
                    PreviousData = model.ToJson()
                },
                  transaction: transaction
            );

            return historyUpdateId;
        }
    }
}