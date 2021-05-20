using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammationConformit.Models.Base;

namespace TestProgrammationConformit.Models
{
    public class Evenement :ModelBase
    {
        public string Titre { get; set; }

        public string Description { get; set; }

        public string PersonneResponsable { get; set; }

        public int CommentaireId { get; set; }

        //Constructeur vide
        public Evenement()
            : base(0)
        {
        }

        public Evenement(int id,string titre,string description,string personneResponsable,int commentaireId)
            :base(id)
        {
            this.Titre = titre;
            this.Description = description;
            this.PersonneResponsable = personneResponsable;
            this.CommentaireId = commentaireId;
        }
    }
}
