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
    public class DayRepository
    {
        readonly string _connectionString;
        public DayRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MigraineAnalyzer");
        }

        internal IEnumerable GetDays()
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * FROM Day";
            var days = db.Query<Days>(sql);
            return days;
        }
    }
}
