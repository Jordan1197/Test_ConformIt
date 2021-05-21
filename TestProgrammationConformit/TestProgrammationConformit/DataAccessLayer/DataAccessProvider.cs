using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammationConformit.Infrastructures;
using TestProgrammationConformit.Models;

namespace TestProgrammationConformit.DataAccessLayer
{
    public class DataAccessProvider
    {
        private readonly ConformitContext _context;

        public DataAccessProvider(ConformitContext context)
        {
            _context = context;
        }
        #region Evenements
        public List<Evenement> GetAllEvent()
        {
            return _context.evenement.ToList();
        }

        public Evenement GetEventById(int id)
        {
            var e = _context.evenement.Find(id);
            Evenement evenement = e;
            return e;
        }

        public void AddEvent(Evenement evenement)
        {
            _context.evenement.Add(evenement);
            _context.SaveChanges();
        }

        public void UpdateEvent(Evenement evenement)
        {
            //Evenement existingEvent = GetEventById(evenement.id);
            _context.evenement.Update(evenement);
            _context.SaveChanges();
            
        }

        public void DeleteEvent(int id)
        {
            _context.evenement.Remove(GetEventById(id));
            _context.SaveChanges();
        }
        #endregion
        #region Commentaires
        public List<Commentaire> GetAllComms()
        {
            return _context.commentaire.ToList();
        }

        public Commentaire GetCommsById(int id)
        {
            var c = _context.commentaire.Find(id);
            Commentaire comms = c;
            return comms;
        }

        public void AddComms(Commentaire commentaire)
        {
            _context.commentaire.Add(commentaire);
            _context.SaveChanges();
        }

        public void UpdateComms(Commentaire commentaire)
        {
            _context.commentaire.Update(commentaire);
            _context.SaveChanges();
        }

        public void DeleteComms(int id)
        {
            _context.commentaire.Remove(GetCommsById(id));
            _context.SaveChanges();
        }
        #endregion
    }
}
