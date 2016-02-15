using System;
using KnockKnock.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KnockKnock.Server;

namespace Tests
{
    [TestClass]
    public class FeedsController
    {
        public IDataRepository Repository { get; set; }
        [TestMethod]
        public void TestMethod1()
        {
            Repository.GetFeedsAsync()
        }
    }
}
