using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammationConformit.Models.Base;

namespace TestProgrammationConformit.Models
{
    public class Evenement : ModelBase
    {
        [StringLength(100)]
        public string titre { get; set; }

        public string description { get; set; }

        public string personneresponsable { get; set; }

        public int idcommentaire { get; set; }

        //public List<Commentaire> listecommentaires { get; set; } = new List<Commentaire>();
        

        //Constructeur vide
        public Evenement()
            : base(0)
        {
        }

        public Evenement(int id,string titre,string description,string personneResponsable,int commentaireId,List<Commentaire> listecommentaires)
            :base(id)
        {
            this.titre = titre;
            this.description = description;
            this.personneresponsable = personneResponsable;
            this.idcommentaire = commentaireId;
            //this.listecommentaires = listecommentaires;
        }
    }
}
