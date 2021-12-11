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
    public class UserMedicineController : ControllerBase
    {
        UserMedicineRepository _userMedRepo;
        public UserMedicineController(UserMedicineRepository userMedRepo)
        {
            _userMedRepo = userMedRepo;
        }

        // Get medicines with user ID
        [HttpGet("{userId}")]
        public IActionResult GetMedicineFromUserId(int userId)
        {
            var medicine = _userMedRepo.GetUserMedicines(userId);
            return Ok(medicine);
        }

        // Add medicines
        [HttpPost]
        public IActionResult AddNewMed(UserMedicines userMed)
        {
            _userMedRepo.AddNewMedicine(userMed);
            return Created($"/userMeds/{userMed.Id}", userMed);
        }
    }
}
