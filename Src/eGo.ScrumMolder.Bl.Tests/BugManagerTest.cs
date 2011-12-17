using System.Linq;
using eGo.ScrumMolder.Bl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using eGo.ScrumMolder.Dto;
using System.Collections.Generic;
using eGo.ScrumMolder.Dto.Bugs;

namespace eGo.ScrumMolder.Bl.Tests
{
    
    
    /// <summary>
    ///This is a test class for BugManagerTest and is intended
    ///to contain all BugManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BugManagerTest
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
        ///A test for GetAll
        ///</summary>
        [TestMethod()]
        public void GetAllTest()
        {
            BugManager target = new BugManager(); // TODO: Initialize to an appropriate value
            IEnumerable<Bug> expected = null; // TODO: Initialize to an appropriate value
            
            var actual = target.GetAll().FirstOrDefault();
            Assert.IsNotNull(actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {

            BugManager target = new BugManager(); // TODO: Initialize to an appropriate value
            Bug bug = new Bug { Name = "test", Id = 1, ImportedBugId = 2, Description = "", Category = null, State = Enums.State.Resolved, Project = null,
                CreatedBy = null,CreateDate = DateTime.Now,Priority = Enums.Priority.Critical, Reason = Enums.Reason.Deferred}; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            
            var actual = target.Save(bug,null);
            Assert.AreEqual(expected, actual);
        }
    }
}
