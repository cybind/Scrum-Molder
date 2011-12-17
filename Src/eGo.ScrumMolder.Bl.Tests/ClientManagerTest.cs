using System.Linq;
using eGo.ScrumMolder.Bl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using eGo.ScrumMolder.Dto;
using System.Collections.Generic;

namespace eGo.ScrumMolder.Bl.Tests
{
    
    
    /// <summary>
    ///This is a test class for ClientManagerTest and is intended
    ///to contain all ClientManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ClientManagerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        public void GetTest()
        {
            var manager = new ClientManager(); 
            var client = new Client { Id = Guid.NewGuid(), Name = "Qwest", Projects = new List<Project> { new Project { Id = Guid.NewGuid(), Name = "Connect" } } };
            manager.Create(client);

            var clientCreated = manager.Get(client.Id);
            Assert.IsNotNull(clientCreated);
            Assert.AreEqual(clientCreated.Id, client.Id);
        }

        /// <summary>
        ///A test for GetAll
        ///</summary>
        [TestMethod()]
        public void GetAllTest()
        {
            var manager = new ClientManager();
            var client = new Client { Id = Guid.NewGuid(), Name = "Qwest", Projects = new List<Project> { new Project { Id = Guid.NewGuid(), Name = "Connect" } } };
            manager.Create(client);

            var clients = manager.GetAll();
            Assert.IsNotNull(clients);
            Assert.IsTrue(clients.Count() > 0);
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            ClientManager target = new ClientManager(); 
            Client client = new Client{ Id = Guid.NewGuid(), Name = "Qwest", Projects = new List<Project>{new Project{ Id = Guid.NewGuid(), Name = "Connect"}}};
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Create(client);
            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            var manager = new ClientManager();
            var client = new Client { Id = Guid.NewGuid(), Name = "Qwest", Projects = new List<Project> { new Project { Id = Guid.NewGuid(), Name = "Connect" } } };
            manager.Create(client);

            client.Projects.Add(new Project { Id = Guid.NewGuid(), Name = "test add" });
            client.Projects.FirstOrDefault().Name = "edited";
            client.Name = "edites";

            bool actual = manager.Update(client);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(client.Name, "edites");

        }
    }
}
