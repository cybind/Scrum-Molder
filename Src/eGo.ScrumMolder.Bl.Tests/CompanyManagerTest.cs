using System.Linq;
using eGo.ScrumMolder.Bl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using eGo.ScrumMolder.Dto;
using System.Collections.Generic;

namespace eGo.ScrumMolder.Bl.Tests
{
    
    
    /// <summary>
    ///This is a test class for CompanyManagerTest and is intended
    ///to contain all CompanyManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CompanyManagerTest
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
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void CreateTest()
        {
            var manager = new CompanyManager();
            var company = new Company{ Id = Guid.NewGuid(), Clients = new List<Client>(), Name = "test name", ChildCompanies = new List<Company> {new Company{ Id = Guid.NewGuid(), Name="test child"}}}; 
            var actual = manager.Create(company);
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        public void GetTest()
        {
            var manager = new CompanyManager();
            var company = new Company { Id = Guid.NewGuid(), Clients = new List<Client>(), Name = "test name" };
            manager.Create(company);

            var retutnCompany = manager.Get(company.Id);
            Assert.IsNotNull(retutnCompany);
        }

        /// <summary>
        ///A test for GetAll
        ///</summary>
        [TestMethod()]
        public void GetAllTest()
        {
            var manager = new CompanyManager();
            var company = new Company { Id = Guid.NewGuid(), Clients = new List<Client>(), Name = "test name" };
            manager.Create(company);

            var actual = manager.GetAll();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count() > 0);

        }
    }
}
