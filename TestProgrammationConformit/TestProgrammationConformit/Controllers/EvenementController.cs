using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammationConformit.Infrastructures;
using TestProgrammationConformit.Models;
using Microsoft.EntityFrameworkCore;
using TestProgrammationConformit.DataAccessLayer;

namespace TestProgrammationConformit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EvenementController : Controller
    {
        private readonly  ConformitContext _context;
        DataAccessProvider dataProvider;
        public EvenementController(ConformitContext context)
        {
            _context = context;
            dataProvider = new DataAccessProvider(context);
        }

        /// <summary>
        /// recupere la liste complete des evenements
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Evenement>> Get()
        {
            List<Evenement> evenements = dataProvider.GetAllEvent();
            return Ok(evenements);
        }

        /// <summary>
        /// recupere un evenement selon sont identifiant
        /// </summary>
        /// <param name="id">l'identifiant de  l'evenement</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            Evenement e;
            e= dataProvider.GetEventById(id);
            if(e is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(e);
            }

           
        }

        /// <summary>
        /// ajout d'un evenement
        /// </summary>
        /// <param name="evenement">le nouveau evenement</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Post([FromBody] Evenement evenement)
        {
            if(evenement != null)
            {
                dataProvider.AddEvent(evenement);

                return Ok(evenement.titre + " à bien été crée");
            }
            else
            {
                return UnprocessableEntity();
            }
            
        }

        /// <summary>
        /// permet la modification d'un evenement selon son identifiant
        /// </summary>
        /// <param name="id">identifiant de l'evenement</param>
        /// <param name="evenement">les donnees du nouvelle evenement</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id,[FromBody] Evenement evenement)
        {
            if(evenement != null)
            {
                evenement.id = id;
                dataProvider.UpdateEvent(evenement);
                return Ok();
            }
            else
            {
                return UnprocessableEntity();
            }
            
        }

        /// <summary>
        /// supprime un evenement selon son identifiant
        /// </summary>
        /// <param name="id">l'identifiant de l'evenement</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            Evenement e = dataProvider.GetEventById(id);
            if (e != null)
            {
                dataProvider.DeleteEvent(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
