using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaCoder.Data.Tests.UnitTests
{
    [TestClass]
    public class BusinessContextTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewTestModel_ThrowsException_WhenNameIsNull()
        {
            using (var businessContext = new BusinessContext())
            {
                businessContext.AddNewTestModel(null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewTestModel_ThrowsException_WhenNameIsEmpty()
        {
            using (var businessContext = new BusinessContext())
            {
                businessContext.AddNewTestModel("");
            }
        }
    }
}