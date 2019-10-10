using System;
using System.Data;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private DatabaseCallsAdmin DatabaseCallsAdmin = new DatabaseCallsAdmin();
        [TestMethod]
        public void TestMethod1()
        {
            DataTable test = DatabaseCallsAdmin.GetAllUsers("test");
            int number = test.Columns.Count;
        }
    }
}
