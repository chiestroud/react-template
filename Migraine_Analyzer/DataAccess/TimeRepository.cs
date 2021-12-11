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
    public class TimeRepository
    {
        readonly string _connectionString;
        public TimeRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MigraineAnalyzer");
        }

        internal IEnumerable GetTimeOfTheDay()
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * FROM TimeOfTheDay";
            var time = db.Query<TimeOfTheDay>(sql);
            return time;
        }
    }
}
