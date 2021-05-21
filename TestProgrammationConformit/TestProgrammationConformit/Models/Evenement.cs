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

        

        public List<string> listecommentaire { get; set; }
        

        //Constructeur vide
        public Evenement()
            : base(0)
        {
        }

        public Evenement(int id,string titre,string description,string personneResponsable,List<string> listecommentaires)
            :base(id)
        {
            this.titre = titre;
            this.description = description;
            this.personneresponsable = personneResponsable;
            
            this.listecommentaire = listecommentaires;
        }
    }
}
