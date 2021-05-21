using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProgrammationConformit.DataAccessLayer;
using TestProgrammationConformit.Infrastructures;

namespace TestProgrammationConformit_Test.DbHelpers
{
    class DatabaseHelper
    {
        //Il va manquer a changer de context pour la connection a la BD de test


        private readonly ConformitContext _context;
        DataAccessProvider dataProvider;
        public DatabaseHelper(ConformitContext context)
        {
            _context = context;
            dataProvider = new DataAccessProvider(context);
        }
        
        public void InitializeDb()
        {
            string connStrTest = "Host = LocalHost; Port = 5432; Username = postgres; Password = admin";
            var conn = new NpgsqlConnection(connStrTest);

            var createDb_Cmd = new NpgsqlCommand(@"
                CREATE DATABASE IF NOT EXISTS ConformitDbTest
                WITH OWNER = postgres
                ENCODING = 'UTF8'
                CONNECTION LIMIT = -1;
                ", conn);

            conn.Open();
            createDb_Cmd.ExecuteNonQuery();
            conn.Close();
        }
        #region Evenement Test
        public void CreateTestEvenementTable()
        {
            string connStr = "Host = LocalHost; Port = 5432; Database = ConformitDbTest; Username = postgres; Password = admin";
            var m_conn = new NpgsqlConnection(connStr);

            var m_Createtbl_cmd = new NpgsqlCommand(
                "CREATE TABLE evenementTest (id SERIAL PRIMARY KEY, titre VARCHAR(100), description VARCHAR(255), personneresponsable VARCHAR(50), listecommentaire test[][]);");
            m_conn.Open();
            m_Createtbl_cmd.ExecuteNonQuery();
            m_conn?.Close();
        }

        public void FillTestEvenementTable()
        {
            string connStr = "Host = LocalHost; Port = 5432; Database = ConformitDbTest; Username = postgres; Password = admin";
            var m_conn = new NpgsqlConnection(connStr);

            var m_addtbl_cmd = new NpgsqlCommand(
                "INSERT INTO `evenementTest` (`id`,`titre`,`description`,`personneresponsable`,`listecommentaire`) VALUES" +
                "(1,'test#1','description test #1','jordan','vide')," +
                "(2,'test#2','description test #2','jordan2','vide');");
            m_conn.Open();
            m_addtbl_cmd.ExecuteNonQuery();
            m_conn?.Close();
        }
        #endregion
        #region Commentaire Test
        public void CreateTestCommentaireTable()
        {
            string connStr = "Host = LocalHost; Port = 5432; Database = ConformitDbTest; Username = postgres; Password = admin";
            var m_conn = new NpgsqlConnection(connStr);

            var m_Createtbl_cmd = new NpgsqlCommand(
                "CREATE TABLE commentaireTest (id SERIAL PRIMARY KEY, evenementid INT, description VARCHAR(255), date DATE, FOREIGN KEY (evenementid) REFERENCES evenement(id));");
            m_conn.Open();
            m_Createtbl_cmd.ExecuteNonQuery();
            m_conn?.Close();
        }

        public void FillTestCommentaireTable()
        {
            string connStr = "Host = LocalHost; Port = 5432; Database = ConformitDbTest; Username = postgres; Password = admin";
            var m_conn = new NpgsqlConnection(connStr);

            var m_addtbl_cmd = new NpgsqlCommand(
                "INSERT INTO `commentaireTest` (`id`,`evenementid`,`description`,`date`) VALUES" +
                "(1,1,'description test #1','2021-05-21')," +
                "((2,1,'description test #2','2021-05-21');");
            m_conn.Open();
            m_addtbl_cmd.ExecuteNonQuery();
            m_conn?.Close();
        }
        #endregion
    }
}
