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
    public class TempController : ControllerBase
    {
        TempRepository _tempRepo;

        public TempController(TempRepository tempRepo)
        {
            _tempRepo = tempRepo;
        }

        [HttpGet]
        public IActionResult GetAllTemp()
        {
            var temp = _tempRepo.GetTemp();
            return Ok(temp);
        }
    }
}
