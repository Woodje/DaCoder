using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaCoder.Data.Tests
{
    [TestClass]
    public class FunctionalTest
    {
        [TestInitialize][TestCleanup]
        public virtual void TestInitialize()
        {
            using (var dataContext = new DataContext())
            {
                var queryAllLanguages =
                    from language in dataContext.Languages
                    select language;

                if (queryAllLanguages.ToList().Count > 0)
                {
                    foreach (var language in queryAllLanguages)
                    {
                        dataContext.Languages.Remove(language);
                    }

                    dataContext.SaveChanges();
                }
            }
        }
    }
}