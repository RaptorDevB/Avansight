using Avansight.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Avansight.Domain.Services
{
    public class DataAccessService
    {
        private readonly string _connectionString;
        public DataAccessService()
        {
            _connectionString = "Server=BUDDIKAW-ITDEV;User ID=sa;Initial Catalog=PatientDB;Password=sa123;Max Pool Size = 100000; Connect Timeout=36000";
        }

        public void ExecuteScopedTransaction(Action<SqlConnection> transaction)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                TransactionOptions options = new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                    Timeout = new TimeSpan(0, 15, 0)
                };

                using (var scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    transaction?.Invoke(conn);
                    scope.Complete();
                }
            }
        }
        public IEnumerable<T> Query<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, SqlConnection transactionConn = null)
        {
            if (transactionConn == null)
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    return conn.Query<T>(sql, param, null, true, null, commandType);
                }
            }
            else
            {
                return transactionConn.Query<T>(sql, param, null, true, null, commandType);
            }
        }
    }
}
