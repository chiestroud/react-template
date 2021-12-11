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
    public class TimeController : ControllerBase
    {
        TimeRepository _timeRepo;
        public TimeController(TimeRepository timeRepo)
        {
            _timeRepo = timeRepo;
        }

        [HttpGet]
        public IActionResult GetTime()
        {
            var time = _timeRepo.GetTimeOfTheDay();
            return Ok(time);
        }
    }
}
