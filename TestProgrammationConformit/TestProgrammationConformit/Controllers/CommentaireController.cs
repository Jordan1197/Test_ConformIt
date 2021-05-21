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
using Microsoft.AspNetCore.Authorization;
using TestProgrammationConformit.Authorization;

namespace TestProgrammationConformit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        [Authorize(Roles = Roles.Admin + "," + Roles.User)]
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
        [Authorize(Roles = Roles.Admin + "," + Roles.User)]
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
        [Authorize(Roles = Roles.Admin + "," + Roles.User)]
        public IActionResult Post(int idEvent,[FromBody] Commentaire commentaire)
        {
            Evenement evenement = dataProvider.GetEventById(idEvent);
            if(evenement.listecommentaire == null)
            {
                evenement.listecommentaire = new List<string>();
            }
            
            if (commentaire != null)
            {
                commentaire.evenementid = idEvent;
                dataProvider.AddComms(commentaire);

                evenement.listecommentaire.Add("Commentaire id: " + commentaire.id.ToString() +" "+commentaire.description);
                dataProvider.UpdateEvent(evenement);

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
        /// <param name="idEvent">l'id de l'event apartenant au commentaire</param>
        /// <param name="commentaire">les donnees du nouveau commentaire</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = Roles.Admin + "," + Roles.User)]
        public IActionResult Put(int id,int idEvent, [FromBody] Commentaire commentaire)
        {
           
            Evenement evenement = dataProvider.GetEventById(idEvent);
            

            if (commentaire != null)
            {
                commentaire.id = id;
                commentaire.evenementid = idEvent;
                dataProvider.UpdateComms(commentaire);


                evenement.listecommentaire.Remove(dataProvider.GetCommsById(commentaire.id).description);
                evenement.listecommentaire.Add("Commentaire id: " + commentaire.id.ToString() + " " + commentaire.description);
                dataProvider.UpdateEvent(evenement);
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
        [Authorize(Roles = Roles.Admin + "," + Roles.User)]
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
