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
    public class DurationRepository
    {
        readonly string _connectionString;
        public DurationRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MigraineAnalyzer");
        }

        internal IEnumerable GetDurations()
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * FROM Duration";
            var durations = db.Query<Durations>(sql);
            return durations;
        }
    }
}
