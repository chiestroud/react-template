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
    public class UserFoodRepository
    {
        readonly string _connectionString;
        public UserFoodRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MigraineAnalyzer");
        }

        internal IEnumerable GetUserFood(int id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT *
                        FROM User_Food
                        WHERE userId = @id";
            var food = db.Query<UserFood>(sql, new { id });
            return food;
        }

        // Add food to user
        internal void AddNewFood(UserFood userFood)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO User_Food(userId, dishName)
                        OUTPUT INSERTED.Id
                        VALUES(@userId, @dishName)";
            var parameters = new { 
                userId = userFood.UserId,
                dishName = userFood.DishName
            };

            var id = db.ExecuteScalar<int>(sql, parameters);
            userFood.Id = id;
        }
    }
}
