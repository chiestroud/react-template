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
    public class TempRepository
    {
        readonly string _connectionString;
        public TempRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MigraineAnalyzer");
        }

        internal IEnumerable GetTemp()
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * FROM Temperature";
            var temp = db.Query<Temperature>(sql);
            return temp;
        }
    }
}
