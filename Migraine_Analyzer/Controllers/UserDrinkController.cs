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
    public class UserDrinkController : ControllerBase
    {
        UserDrinkRepository _userDrinkRepo;
        public UserDrinkController(UserDrinkRepository userDrinkRepo)
        {
            _userDrinkRepo = userDrinkRepo;
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserDrinksFromId(int userId)
        {
            var drinks = _userDrinkRepo.GetUserDrinks(userId);
            return Ok(drinks);
        }

        [HttpPost]
        public IActionResult AddNewDrinksToUser(UserDrinks userDrinks)
        {
            _userDrinkRepo.AddNewDrinks(userDrinks);
            return Created($"/userDrinks/{userDrinks.Id}", userDrinks);
        }
    }
}
