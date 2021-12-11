using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Migraine_Analyzer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Migraine_Analyzer.DataAccess
{
    public class MonthRepository
    {
        readonly string _connectionString;
        public MonthRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MigraineAnalyzer");
        }

        internal IEnumerable GetMonths()
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * FROM Month";
            var months = db.Query<Months>(sql);
            return months;
        }
    }
}
