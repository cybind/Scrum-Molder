using System.Linq;
using eGo.ScrumMolder.Bl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using eGo.ScrumMolder.Dto;
using System.Collections.Generic;

namespace eGo.ScrumMolder.Bl.Tests
{
    
    
    /// <summary>
    ///This is a test class for ProjectManagerTest and is intended
    ///to contain all ProjectManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProjectManagerTest
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
            var manger = new ProjectManager();
            var project = new Project { Id = Guid.NewGuid(), Name = "test" }; 
          
            var actual = manger.Save(project);
            Assert.IsTrue(actual);

        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            var manger = new ProjectManager();
            var project = new Project { Id = Guid.NewGuid(), Name = "test" };
            manger.Save(project);

            project.Name = "edited";
            var actual = manger.Save(project);

            Assert.IsTrue(actual);

        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        public void GetTest()
        {
            var manger = new ProjectManager();
            var project = new Project { Id = Guid.NewGuid(), Name = "test" };
            manger.Save(project);

            var proj = manger.Get(project.Id);
            Assert.IsNotNull(proj);
        }

        /// <summary>
        ///A test for GetAll
        ///</summary>
        [TestMethod()]
        public void GetAllTest()
        {
            var manger = new ProjectManager();
            var project = new Project { Id = Guid.NewGuid(), Name = "test" };
            manger.Save(project);

            var projects = manger.GetAll();

            Assert.IsNotNull(projects);
            Assert.IsTrue(projects.Count() > 0);
        }

    }
}
