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
    public class MigrainesRepository
    {
        readonly string _connectionString;
        public MigrainesRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MigraineAnalyzer");
        }

        internal IEnumerable GetMigraineInfoFromUserId(int id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT *
                        FROM Migraines
                        WHERE userId = @id";
            var migraines = db.Query<Migraines>(sql, new { id });
            return migraines;
        }

        internal Migraines GetSingleMigraine(int id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT *
                        FROM Migraines
                        WHERE id = @id";
            var migraine = db.QueryFirstOrDefault<Migraines>(sql, new { id });
            return migraine;
        }

        internal object GetMigraineCount(int id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT COUNT(*) AS Total
                        FROM Migraines
                        WHERE userId = @id";
            var migraineCount = db.QuerySingleOrDefault<object>(sql, new { id });
            return migraineCount;
        }

        // Get detailed migraine info
        internal object GetDetailedMigraineInfo(int id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT *
                        FROM Migraines m
                        JOIN Users u
                        ON m.userId = u.id
                        JOIN Day d
                        ON m.dayId = d.id
                        JOIN Month mo
                        ON m.monthId = mo.id
                        JOIN Temperature t
                        ON m.temperatureId = t.id
                        JOIN TimeOfTheDay td
                        ON m.timeId = td.id
                        JOIN Duration du
                        ON m.durationId = du.id
                        WHERE m.userId = @id";

            var results = db.Query<MigrainesExpanded, Users, Days, Months, Temperature, TimeOfTheDay, Durations, MigrainesExpanded>
                (sql, (migraine, user, day, month, temp, time, duration) =>
                {
                    migraine.UserInfo = user;
                    migraine.DayInfo = day;
                    migraine.MonthsInfo = month;
                    migraine.TempInfo = temp;
                    migraine.TimeInfo = time;
                    migraine.DurationInfo = duration;
                    return migraine;
                }, new { id }, splitOn: "id");
            return results;
        }

        // Detailed migraine Info with user drinks, food, and medicine
        internal object GetMoreDetailedMigraineInfo(int id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT *
                        FROM Migraines m
                        JOIN Users u
                        ON m.userId = u.id
                        JOIN User_Food uf
                        ON m.foodId = uf.id
                        JOIN User_Medicines um
                        ON m.medicineId = um.id
                        JOIN User_Drinks ud
                        ON m.drinkId = ud.id
                        WHERE m.userId = @id";
            var results = db.Query<MigraineWithFoodDrinkMedicine, Users, UserFood,
                UserMedicines, UserDrinks, MigraineWithFoodDrinkMedicine>
                (sql, (migraine, user, food, medicine, drink) =>
                {
                    migraine.UserInfoForFoodDrinkMedicine = user;
                    migraine.FoodInfo = food;
                    migraine.MedicineInfo = medicine;
                    migraine.DrinkInfo = drink;
                    return migraine;
                }, new { id }, splitOn:"id");
            return results;
        }

        // Add new migraine
        internal void AddNewMigraine(Migraines migraine)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Migraines
                        (userId, logDate, dayId, monthId, 
                         currentYear, timeId, intensity, 
                         durationId, vomit, weather, emotion, temperatureId,
                         comment, foodId, drinkId, medicineId)
                        OUTPUT INSERTED.id
                        VALUES(@userId, GETUTCDATE(), @dayId, 
                        @monthId, @currentYear, @timeId, @intensity, 
                        @durationId, @vomit, @weather, @emotion, @comment, 
                        @temperatureId, @foodId, @drinkId, @medicineId)";
            var parameters = new
            {
                userId = migraine.UserId,
                dayId = migraine.DayId,
                monthId = migraine.MonthId,
                currentYear = migraine.CurrentYear,
                timeId = migraine.TimeId,
                intensity = migraine.Intensity,
                durationId = migraine.DurationId,
                vomit = migraine.Vomit,
                weather = migraine.Weather,
                emotion = migraine.Emotion,
                temperatureId = migraine.TemperatureId,
                comment = migraine.Comment,
                foodId = migraine.FoodId,
                drinkId = migraine.DrinkId,
                medicineId = migraine.MedicineId
            };
            var id = db.ExecuteScalar<int>(sql, parameters);
            migraine.Id = id;
        }

        internal Migraines UpdateMigraine(int id, Migraines migraineToUpdate)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"UPDATE Migraines
                        SET userId = userId,
	                        logDate = logDate,
	                        dayId = @dayId,
	                        monthId = @monthId,
	                        currentYear = @currentYear,
	                        timeId = @timeId,
	                        intensity = @intensity,
	                        durationId = @durationId,
	                        vomit = @vomit,
	                        weather = @weather,
	                        emotion = @emotion,
	                        temperatureId = @temperatureId,
	                        comment = @comment,
	                        foodId = @foodId,
	                        drinkId = @drinkId,
	                        medicineId = @medicineId
                        OUTPUT INSERTED.*
                        WHERE id = @id";
            migraineToUpdate.Id = id;
            var updatedMigraine = db.QuerySingleOrDefault<Migraines>(sql, migraineToUpdate);
            return updatedMigraine;
        }

        // Get all enum values - intensity
        internal object GetIntensityType()
        {
            var intensity = Enum.GetValues(typeof(IntensityType)).Cast<IntensityType>().ToList();
            return intensity;
        }

        // Get all enum values - weather
        internal object GetWeatherType()
        {
            var weather = Enum.GetValues(typeof(WeatherType)).Cast<WeatherType>().ToList();
            return weather;
        }

        // Get all enum values - emotion
        internal object GetEmotionType()
        {
            var emotions = Enum.GetValues(typeof(EmotionType)).Cast<EmotionType>().ToList();
            return emotions;
        }

        // Top 3 medicines used for treating migraines
        internal object GetMigraineMedicineTop3(int id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT TOP(3) um.medicineName, COUNT(*) AS medicineCount
                        FROM Migraines m
                        JOIN User_Medicines um
                        ON m.medicineId = um.id
                        WHERE m.userId = @id
                        GROUP BY medicineName
                        ORDER BY medicineCount DESC";
            var topMedicines = db.Query(sql, new { id });
            return topMedicines;
        }

        // Top 3 food before migraines
        internal object GetMigraineFoodTop3(int id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT TOP(3) uf.dishName, COUNT(*) AS foodCount
                        FROM Migraines m
                        JOIN User_Food uf
                        ON m.foodId = uf.id
                        WHERE m.userId = @id
                        GROUP BY uf.dishName
                        ORDER BY foodCount DESC";
            var topFood = db.Query(sql, new { id });
            return topFood;
        }

        // Top 3 drinks before migraines
        internal object GetMigraineDrinkTop3(int id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT TOP(3) ud.drinkName, COUNT(*) AS drinkCount
                        FROM Migraines m
                        JOIN User_Drinks ud
                        ON m.foodId = ud.id
                        WHERE m.userId = @id
                        GROUP BY ud.drinkName
                        ORDER BY drinkCount DESC";
            var topDrinks = db.Query(sql, new { id });
            return topDrinks;
        }

        // Migraine count per year
        internal object GetMigraineCountPerYear(int id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT currentYear, COUNT(*) as migraineCount
                        FROM Migraines
                        WHERE userId = @id
                        GROUP BY currentYear";
            var migraineCount = db.Query(sql, new { id });
            return migraineCount;
        }
    }
}
