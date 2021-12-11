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
    public class DurationController : ControllerBase
    {
        DurationRepository _durationRepo;

        public DurationController(DurationRepository durationRepo)
        {
            _durationRepo = durationRepo;
        }

        [HttpGet]
        public IActionResult GetAllDurations()
        {
            var durations = _durationRepo.GetDurations();
            return Ok(durations);
        }
    }
}
