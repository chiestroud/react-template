using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Migraine_Analyzer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Migraine_Analyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayController : ControllerBase
    {
        DayRepository _dayRepo;

        public DayController(DayRepository dayRepo)
        {
            _dayRepo = dayRepo;
        }

        [HttpGet]
        public IActionResult GetAllDays()
        {
            var days = _dayRepo.GetDays();
            return Ok(days);
        }
    }
}
