using System;
using System.Linq;
using DataBaseLog;
using DataBaseLoger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using ViewModel;
using ViewWPF;

namespace DataBaseLoggerTest
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void Logger_IsExistARow()
        {
            DBLogger logger = new DBLogger();
            DatabaseContextLog DBL = new DatabaseContextLog();

            int temp = DBL.Logs.Count();
            string testString = "Test";
            logger.Log(testString);
            Assert.AreEqual(temp + 1, DBL.Logs.Count());

            Assert.AreEqual(DBL.Logs.OrderByDescending(p => p.Time).FirstOrDefault().Message, testString);


        }
    }
}
