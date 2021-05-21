using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammationConformit.Models.Base;

namespace TestProgrammationConformit.Models
{
    public class Commentaire  :ModelBase
    {
       

        public string description { get; set; }

        public DateTime date { get; set; }

        public int evenementid { get; set; }

        
        //constructeur vide
        public Commentaire()
            :base(0)
        {
        }

        public Commentaire(int id,string desc,DateTime date,int idevenement)
            : base(id)
        {
            this.description = desc;
            this.date = date;
            this.evenementid = idevenement;
        }

    }
}
