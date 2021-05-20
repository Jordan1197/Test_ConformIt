using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammationConformit.Models;

namespace TestProgrammationConformit.DataAccessLayer
{
    public interface IDataAccessProvider
    {
        void AddEvenement(Evenement evenement);
        List<Evenement> GetAllEvenement();
    }
}
