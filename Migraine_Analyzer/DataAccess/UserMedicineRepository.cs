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
    public class UserMedicineRepository
    {
        readonly string _connectionString;
        public UserMedicineRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MigraineAnalyzer");
        }

        internal IEnumerable GetUserMedicines(int id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * 
                        FROM User_Medicines
                        WHERE userId = @id";
            var medicines = db.Query<UserMedicines>(sql, new { id });
            return medicines;
        }

        // Add new medicine to user
        internal void AddNewMedicine(UserMedicines userMed)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO User_Medicines(userId, medicineName)
                        OUTPUT INSERTED.Id
                        VALUES(@userId, @medicineName)";

            var parameters = new
            {
                userId = userMed.UserId,
                medicineName = userMed.MedicineName,
            };

            var id = db.ExecuteScalar<int>(sql, parameters);
            userMed.Id = id;
        }
    }
}
