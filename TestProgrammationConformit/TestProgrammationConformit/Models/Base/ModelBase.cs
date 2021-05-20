using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProgrammationConformit.Models.Base
{
    public class ModelBase
    {
        public int Id { get; set; }

        public ModelBase(int id)
        {
            this.Id = id;
        }
    }
}
