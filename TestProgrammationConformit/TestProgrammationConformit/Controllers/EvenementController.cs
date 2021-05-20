using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammationConformit.Infrastructures;
using TestProgrammationConformit.Models;
using TestProgrammationConformit.DataAccessLayer;

namespace TestProgrammationConformit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EvenementController : Controller
    {

        private readonly IDataAccessProvider _dataAccessProvider;

        public EvenementController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        private readonly ILogger<EvenementController> _logger;
        public EvenementController(ILogger<EvenementController> logger)
        {
            _logger = logger;
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Evenement>> Get()
        {
            return _dataAccessProvider.GetAllEvenement();
        }

        /*[HttpGet("[action]")]
        public IActionResult GetById(int id)
        {
            var evenement = _context.Find<Evenement>(Convert.ToInt32(id));

            return Ok(evenement);
        }*/

        [HttpPost]
        public IActionResult Create([FromBody] Evenement evenement)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddEvenement(evenement);
                return Ok();
            }
            return BadRequest();

        }
    }
}
