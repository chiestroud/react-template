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
    public class UserDrinkRepository
    {
        readonly string _connectionString;
        public UserDrinkRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MigraineAnalyzer");
        }

        // Get drinks from user ID
        internal IEnumerable GetUserDrinks(int id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT *
                        FROM User_Drinks
                        WHERE userId = @id";
            var userDrinks = db.Query<UserDrinks>(sql, new { id });
            return userDrinks;
        }

        // Add new drinks
        internal void AddNewDrinks(UserDrinks userDrinks)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO User_Drinks(userId, drinkName)
                        OUTPUT INSERTED.Id
                        VALUES(@userId, @drinkName)";
            var parameters = new
            {
                userId = userDrinks.UserId,
                drinkName = userDrinks.DrinkName
            };

            var id = db.ExecuteScalar<int>(sql, parameters);
            userDrinks.Id = id;
        }
    }
}
