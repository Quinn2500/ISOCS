using System;
using System.Collections.Generic;
using System.Data;
using Business;
using DataModels;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private DatabaseCallsAdmin DatabaseCallsAdmin = new DatabaseCallsAdmin();

        private EmailLogic test = new EmailLogic();

        [TestMethod]
        public void TestMethod1()
        {
            ActionModel a = new ActionModel()
            {
                Name = "testa",
                Occurence = OccurenceEnum.Daily
            };

            ActionModel b = new ActionModel()
            {
                Name = "testb",
                Occurence = OccurenceEnum.Weekly
            };

            ActionModel c = new ActionModel()
            {
                Name = "testc",
                Occurence = OccurenceEnum.Monthly
            };

            ActionModel d = new ActionModel()
            {
                Name = "testd",
                Occurence = OccurenceEnum.Quarterly
            };

            ActionModel e = new ActionModel()
            {
                Name = "teste",
                Occurence = OccurenceEnum.Yearly
            };

            List<ActionModel> result = new List<ActionModel>();
            result.Add(a);
            result.Add(b);
            result.Add(c);
            result.Add(d);
            result.Add(e);
        }

    }
}
