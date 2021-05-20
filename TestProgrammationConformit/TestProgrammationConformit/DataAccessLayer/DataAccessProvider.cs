using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammationConformit.Models;
using TestProgrammationConformit.Infrastructures;

namespace TestProgrammationConformit.DataAccessLayer
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly ConformitContext _context;

        public DataAccessProvider(ConformitContext context)
        {
            _context = context;
        }

        public void AddEvenement(Evenement evenement)
        {
            _context.Evenements.Add(evenement);
            _context.SaveChanges();
        }

        public List<Evenement> GetAllEvenement()
        {
            return _context.Evenements.ToList();
        }
    }
}
