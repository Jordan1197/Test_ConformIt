using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProgrammationConformit.Models.Base
{
    public class ModelBase
    {
        
        public int id { get; set; }

        public ModelBase(int id)
        {
            this.id = id;
        }
    }
}
