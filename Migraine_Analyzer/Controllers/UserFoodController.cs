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
    public class UserFoodController : ControllerBase
    {
        UserFoodRepository _userFoodRepo;
        public UserFoodController(UserFoodRepository userFoodRepo)
        {
            _userFoodRepo = userFoodRepo;
        }

        [HttpGet("{userId}")]
        public IActionResult GetFoodFromUser(int userId)
        {
            var food = _userFoodRepo.GetUserFood(userId);
            return Ok(food);
        }

        [HttpPost]
        public IActionResult AddNewFoodToUser(UserFood userFood)
        {
            _userFoodRepo.AddNewFood(userFood);
            return Created($"/userFood/{userFood.Id}", userFood);
        }
    }
}
