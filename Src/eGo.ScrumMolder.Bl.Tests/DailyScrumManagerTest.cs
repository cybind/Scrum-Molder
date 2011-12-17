using System.Collections.Generic;
using eGo.ScrumMolder.Bl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using eGo.ScrumMolder.Data;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Bl.Tests
{
    
    
    /// <summary>
    ///This is a test class for DailyScrumManagerTest and is intended
    ///to contain all DailyScrumManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DailyScrumManagerTest
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
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            DailyScrumManager target = new DailyScrumManager(new ClientManager(), new ProjectManager()); // TODO: Initialize to an appropriate value
            //ProjectManager targetForProject = new ProjectManager();
            //var project = targetForProject.GetAll().[0];
            DailyScrum dailyScrum = new DailyScrum
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                User = new User{ Id = Guid.NewGuid()},
                DailyProjectScrums = new List<DailyProjectScrum>{ 
                    new DailyProjectScrum{ Id = Guid.NewGuid(), Project = new Project(){Id = new Guid("B06F5C08-7C05-4FD3-89A7-4BDD563CB705")}, SpentTime = new TimeSpan(0,8,0,0),
                        UpdateDate = null, WhatDoneLastTime = "test Repository What Done Last Time", WhatProblems = "What Problems", WhatToDoNext = " What To Do Next" }  }
            };
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            //var s = target.GetAdUserEmail();
            actual = target.Save(dailyScrum);
            Assert.AreEqual(expected, actual);
            
        }
    }
}
