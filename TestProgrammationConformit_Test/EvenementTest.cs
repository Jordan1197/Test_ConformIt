using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using TestProgrammationConformit.Controllers;
using TestProgrammationConformit.DataAccessLayer;
using TestProgrammationConformit.Infrastructures;
using TestProgrammationConformit.Models;
using TestProgrammationConformit_Test.DbHelpers;

namespace TestProgrammationConformit_Test
{
    [TestClass]
    public class EvenementTest
    {
        private readonly ConformitContext _context;
        [TestInitialize]
        public void Initialize()
        {
            DatabaseHelper d = new DatabaseHelper(_context);
            d.InitializeDb();
            d.CreateTestEvenementTable();
            d.FillTestEvenementTable();
        }
        
        [TestMethod]
        [Description("Test la récupération complète des évènements")]
        public void Get_AllEvenement()
        {
            EvenementController controller = new EvenementController(_context);

            ActionResult<IEnumerable<Evenement>> evenements = controller.Get();

            evenements.Should().NotBeNull();
            evenements.Value.Should().HaveCount(2);
        }

        [TestMethod]
        [Description("Test la récupération d'un evenement à partir de son identifiant")]
        public void Get_EvenementById()
        {
            EvenementController controller = new EvenementController(_context);
            var evenement = controller.GetById(1);
            Assert.IsInstanceOfType(evenement, typeof(CreatedResult));
            CreatedResult cEvenement = (CreatedResult)evenement;

            Evenement e = (Evenement)cEvenement.Value;
            e.Should().NotBeNull();
            e.id.Should().Be(1);
        }

        [TestMethod]
        [Description("Test l'ajout d'un nouvelle evenement")]
        public void Post_NewEvenement()
        {
            DataAccessProvider dp = new DataAccessProvider(_context);
            List<string> liste = new List<string>();
            Evenement e = new Evenement(0, "test", "test", "test", liste);
            var evenementAdded = new EvenementController(_context).Post(e);
            Assert.IsInstanceOfType(evenementAdded, typeof(CreatedResult));
            CreatedResult cEvenementAdded = (CreatedResult)evenementAdded;

            Evenement eventAdded = (Evenement)cEvenementAdded.Value;

            int nbEvent = dp.GetAllEvent().Count;

            nbEvent.Should().Be(3);

            eventAdded.id.Should().Be(e.id);
            eventAdded.titre.Should().Be(e.titre);
            eventAdded.description.Should().Be(e.description);
        }

        [TestMethod]
        [Description("Test la modification d'un evenement existant")]
        public void Put_ExistingEvenement()
        {
            DataAccessProvider dp = new DataAccessProvider(_context);
            Evenement existingEvent = dp.GetEventById(1);
            existingEvent.titre += "modification du titre";

            var updatedEvent = new EvenementController(_context).Put(existingEvent.id, existingEvent);
            Assert.IsInstanceOfType(updatedEvent, typeof(OkResult));

            int nbEvent = dp.GetAllEvent().Count;
            Evenement dbEvent = dp.GetEventById(existingEvent.id);

            nbEvent.Should().Be(2);

            dbEvent.id.Should().Be(existingEvent.id);
            dbEvent.titre.Should().Be(existingEvent.titre);
            dbEvent.description.Should().Be(existingEvent.description);
            dbEvent.personneresponsable.Should().Be(existingEvent.personneresponsable);
            dbEvent.listecommentaire.Should().BeEquivalentTo(existingEvent.listecommentaire);
            
        }

        [TestMethod]
        [Description("Test la suppression d'un evenement existant")]
        public void Delete_ExistingEvenement()
        {
            DataAccessProvider dp = new DataAccessProvider(_context);

            Evenement existingEvent = dp.GetEventById(1);

            var deletedEvent = new EvenementController(_context).Delete(existingEvent.id);

            Assert.IsInstanceOfType(deletedEvent, typeof(OkResult));

            int nbEvent = dp.GetAllEvent().Count;
            nbEvent.Should().Be(1);

            Evenement nullEvent =dp.GetEventById(existingEvent.id);
            nullEvent.Should().BeNull();
        }
        
    }
}
