using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammationConformit.Models.Base;

namespace TestProgrammationConformit.Models
{
    public class Commentaire  : ModelBase
    {
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int IdEvenement { get; set; }

        //constructeur vide
        public Commentaire()
            :base(0)
        {
        }

        public Commentaire(int id,string desc,DateTime date,int idEvenement)
            : base(id)
        {
            this.Description = desc;
            this.Date = date;
            this.Id = idEvenement;
        }
    }
}
