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
using System;

namespace TestProgrammationConformit_Test.DbHelpers
{
    [TestClass]
    class CommentaireTest
    {
        private readonly ConformitContext _context;
        [TestInitialize]
        public void Initialize()
        {
            DatabaseHelper d = new DatabaseHelper(_context);
            d.InitializeDb();

            d.CreateTestEvenementTable();
            d.FillTestEvenementTable();


            d.CreateTestCommentaireTable();
            d.FillTestCommentaireTable();
        }
        [TestMethod]
        [Description("Test la récupération complète des commentaires")]
        public void Get_AllEvenement()
        {
            CommentaireController controller = new CommentaireController(_context);

            ActionResult<IEnumerable<Commentaire>> commentaire = controller.Get();

            commentaire.Should().NotBeNull();
            commentaire.Value.Should().HaveCount(2);
        }

        [TestMethod]
        [Description("Test la récupération d'un commentaire à partir de son identifiant")]
        public void Get_EvenementById()
        {
            CommentaireController controller = new CommentaireController(_context);
            var commentaire = controller.GetById(1);
            Assert.IsInstanceOfType(commentaire, typeof(CreatedResult));
            CreatedResult cCommentaire= (CreatedResult)commentaire;

            Commentaire c = (Commentaire)cCommentaire.Value;
            c.Should().NotBeNull();
            c.id.Should().Be(1);
        }

        [TestMethod]
        [Description("Test l'ajout d'un nouveau commentaire")]
        public void Post_NewEvenement()
        {
            DataAccessProvider dp = new DataAccessProvider(_context);

            Commentaire c = new Commentaire(0, "test", DateTime.MinValue, 1);
            var commentaireAddedBeta = new CommentaireController(_context).Post(1,c);
            Assert.IsInstanceOfType(commentaireAddedBeta, typeof(CreatedResult));
            CreatedResult cCommentaireAdded = (CreatedResult)commentaireAddedBeta;

            Commentaire commentaireAdded = (Commentaire)cCommentaireAdded.Value;

            int nbcommentaire = dp.GetAllComms().Count;

            nbcommentaire.Should().Be(3);

            commentaireAdded.id.Should().Be(c.id);
            commentaireAdded.date.Should().Be(c.date);
            commentaireAdded.description.Should().Be(c.description);
            commentaireAdded.evenementid.Should().Be(c.evenementid);
        }

        [TestMethod]
        [Description("Test la modification d'un commentaire existant")]
        public void Put_ExistingEvenement()
        {
            DataAccessProvider dp = new DataAccessProvider(_context);
            Commentaire existingCommentaire = dp.GetCommsById(1);
            existingCommentaire.description+= "modification de la description";

            var updatedCommentaire = new CommentaireController(_context).Put(existingCommentaire.id,existingCommentaire.evenementid,existingCommentaire);
            Assert.IsInstanceOfType(updatedCommentaire, typeof(OkResult));

            int nbComms = dp.GetAllComms().Count;
            Commentaire dbCommentaire = dp.GetCommsById(existingCommentaire.id);

            nbComms.Should().Be(2);

            dbCommentaire.id.Should().Be(existingCommentaire.id);            
            dbCommentaire.description.Should().Be(existingCommentaire.description);
            dbCommentaire.date.Should().Be(existingCommentaire.date);
            dbCommentaire.evenementid.Should().Be(existingCommentaire.evenementid);

        }

        [TestMethod]
        [Description("Test la suppression d'un commentaire existant")]
        public void Delete_ExistingEvenement()
        {
            DataAccessProvider dp = new DataAccessProvider(_context);

            Commentaire existingComms = dp.GetCommsById(1);

            var deletedComms = new CommentaireController(_context).Delete(existingComms.id);

            Assert.IsInstanceOfType(deletedComms, typeof(OkResult));

            int nbComms = dp.GetAllComms().Count;
            nbComms.Should().Be(1);

            Commentaire nullComms = dp.GetCommsById(existingComms.id);
            nullComms.Should().BeNull();
        }
    }
}
