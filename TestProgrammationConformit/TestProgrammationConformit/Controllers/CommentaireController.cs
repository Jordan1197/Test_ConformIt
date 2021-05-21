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
    public class CommentaireController : Controller
    {
        private readonly ConformitContext _context;
        DataAccessProvider dataProvider;
        public CommentaireController(ConformitContext context)
        {
            _context = context;
            dataProvider = new DataAccessProvider(context);
        }

        /// <summary>
        /// recupere la liste complete des commentaires
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Commentaire>> Get()
        {
            List<Commentaire> commentaires= dataProvider.GetAllComms();
            return Ok(commentaires);
        }

        /// <summary>
        /// recupere un commentaire selon sont identifiant
        /// </summary>
        /// <param name="id">l'identifiant du commentaire</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            Commentaire c;
            c = dataProvider.GetCommsById(id);
            if (c is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(c);
            }


        }

        /// <summary>
        /// ajout d'un commentaire
        /// </summary>
        /// <param name="idEvent">id de l'evenement qui possede le commentaire</param>
        /// <param name="commentaire">le nouveau commentaire</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Post(int idEvent,[FromBody] Commentaire commentaire)
        {
            Evenement evenement = dataProvider.GetEventById(idEvent);

            if (commentaire != null)
            {
                commentaire.idevenement = idEvent;
                dataProvider.AddComms(commentaire);

                //evenement.listecommentaires.Add(commentaire);
                return Ok(commentaire.description + " à bien été crée");
            }
            else
            {
                return UnprocessableEntity();
            }

        }

        /// <summary>
        /// permet la modification d'un commentaire selon son identifiant
        /// </summary>
        /// <param name="id">identifiant du commentaire</param>
        /// <param name="commentaire">les donnees du nouveau commentaire</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] Commentaire commentaire)
        {
            if (commentaire != null)
            {
                commentaire.id = id;
                dataProvider.UpdateComms(commentaire);
                return Ok();
            }
            else
            {
                return UnprocessableEntity();
            }

        }

        /// <summary>
        /// supprime un commentaire selon son identifiant
        /// </summary>
        /// <param name="id">l'identifiant du commentaire</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            Commentaire c = dataProvider.GetCommsById(id);
            if (c != null)
            {
                dataProvider.DeleteComms(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
