using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaCoder.Data.Tests
{
    public class FunctionalTest
    {
        [TestInitialize]
        public virtual void TestInitialize()
        {
            using (var dataContext = new DataContext())
            {
                if (dataContext.Database.Exists())
                    dataContext.Database.Delete();

                dataContext.Database.Create();
            }
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            using (var dataContext = new DataContext())
                if (dataContext.Database.Exists())
                    dataContext.Database.Delete();
        } 
    }
}