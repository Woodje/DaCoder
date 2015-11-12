using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaCoder.Data.Tests.FunctionalTests
{
    [TestClass]
    public class TestModelScenarioTests
    {
        [TestMethod]
        public void AddNewTestModelIsPersisted()
        {
            using (var businessContext = new BusinessContext())
            {
                TestModel entity = businessContext.AddNewTestModel("TestModelName");

                bool exists = businessContext.DataContext.TestModels.Any(customer => customer.Id == entity.Id);

                Assert.IsTrue(exists);
            }
        }
         
    }
}