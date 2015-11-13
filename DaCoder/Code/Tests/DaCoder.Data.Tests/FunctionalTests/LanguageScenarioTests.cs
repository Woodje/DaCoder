using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaCoder.Data.Tests.FunctionalTests
{
    [TestClass]
    public class LanguageScenarioTests : FunctionalTest
    {
        [TestMethod]
        public void AddNewLanguageIsPersisted()
        {
            using (var businessContext = new BusinessContext())
            {
                var language = new Language
                {
                    Name = "LanguageName"
                };

                businessContext.AddNewLanguage(language);

                bool exists = businessContext.DataContext.Languages.Any(l => l.Id == language.Id);

                Assert.IsTrue(exists);
            }
        }
         
    }
}