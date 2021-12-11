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
    public class MonthController : ControllerBase
    {
        MonthRepository _monthRepo;
        public MonthController(MonthRepository monthRepo)
        {
            _monthRepo = monthRepo;
        }

        [HttpGet]
        public IActionResult GetAllMonths()
        {
            var months = _monthRepo.GetMonths();
            return Ok(months);
        }
    }
}
