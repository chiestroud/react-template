using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Migraine_Analyzer.DataAccess;
using Migraine_Analyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Migraine_Analyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MigrainesController : ControllerBase
    {
        MigrainesRepository _migraineRepo;
        public MigrainesController(MigrainesRepository migraineRepo)
        {
            _migraineRepo = migraineRepo;
        }

        // Get migraines with user Id
        [HttpGet("user/{userId}")]
        public IActionResult GetMigraines(int userId)
        {
            var migraines = _migraineRepo.GetMigraineInfoFromUserId(userId);
            return Ok(migraines);
        }

        //Get migraines with specific id
       [HttpGet("migraine/{migraineId}")]
        public IActionResult GetMigrainesFromId(int migraineId)
        {
            var migraine = _migraineRepo.GetSingleMigraine(migraineId);
            return Ok(migraine);
        }

        // Get detailed migraine info with userId => other than drink, food, medicine
        [HttpGet("migraineDetails/{userId}")]
        public IActionResult GetDetailedMigraineInfoFromId(int userId)
        {
            var detailedMigraine = _migraineRepo.GetDetailedMigraineInfo(userId);
            return Ok(detailedMigraine);
        }

        // Get detailed migraine info with specific id => drink, food, medicine
        [HttpGet("migrainieWithDrinkMedicineFood/{userId}")]
        public IActionResult GetDrinkFoodMedicineInfoFromMigraineId(int userId)
        {
            var detailedMigraine = _migraineRepo.GetMoreDetailedMigraineInfo(userId);
            return Ok(detailedMigraine);
        }

        // Get migraine count for all time
        [HttpGet("count/{userId}")]
        public IActionResult GetTotalMigraineCount(int userId)
        {
            var count = _migraineRepo.GetMigraineCount(userId);
            return Ok(count);
        }

        // Add new migraine
        [HttpPost]
        public IActionResult AddMigraine(Migraines migraine)
        {
            _migraineRepo.AddNewMigraine(migraine);
            return Created($"/migraine/{migraine.Id}", migraine);
        }

        // Edit migraine
        [HttpPut("update/{id}")]
        public IActionResult UpdateMigraine(int id, Migraines migraine)
        {
            var updatedMigraine = _migraineRepo.UpdateMigraine(id, migraine);
            return Ok(updatedMigraine);
        }

        // Get all enum values of intensity type
        [HttpGet("intensityType")]
        public IActionResult GetIntensityValues()
        {
            return Ok(_migraineRepo.GetIntensityType());
        }

        // Get all enum values of weather type
        [HttpGet("weatherType")]
        public IActionResult GetWeatherValues()
        {
            return Ok(_migraineRepo.GetWeatherType());
        }

        // Get all enum values of emotion type
        [HttpGet("emotionType")]
        public IActionResult GetEmotionValues()
        {
            return Ok(_migraineRepo.GetEmotionType());
        }

        // Get top 3 medicines used
        [HttpGet("topMedicine/{userId}")]
        public IActionResult GetTop3Medicines(int userId)
        {
            return Ok(_migraineRepo.GetMigraineMedicineTop3(userId));
        }

        // Get top 3 food consumed before migraine
        [HttpGet("topFood/{userId}")]
        public IActionResult GetTop3Food(int userId)
        {
            return Ok(_migraineRepo.GetMigraineFoodTop3(userId));
        }

        // Get top 3 drinks consumed before migraine
        [HttpGet("topDrinks/{userId}")]
        public IActionResult GetTop3Drinks(int userId)
        {
            return Ok(_migraineRepo.GetMigraineDrinkTop3(userId));
        }

        // Get migraine count per year
        [HttpGet("migraineCount/{userId}")]
        public IActionResult MigraineCount(int userId)
        {
            return Ok(_migraineRepo.GetMigraineCountPerYear(userId));
        }
    }
}
