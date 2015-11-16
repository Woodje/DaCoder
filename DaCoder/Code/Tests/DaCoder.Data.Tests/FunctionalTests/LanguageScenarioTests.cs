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

        [TestMethod]
        public void UpdateLanguageIsPersisted()
        {
            using (var businessContext = new BusinessContext())
            {
                var language = new Language
                {
                    Name = "LanguageName"
                };

                businessContext.AddNewLanguage(language);

                language.Name = "New LanguageName";

                businessContext.UpdateLanguage(language);

                bool updated = businessContext.DataContext.Languages.Any(l => l.Id == language.Id && l.Name == language.Name);

                Assert.IsTrue(updated);
            }
        }

        [TestMethod]
        public void DeleteLanguageIsPersisted()
        {
            using (var businessContext = new BusinessContext())
            {
                var language = new Language
                {
                    Name = "LanguageName"
                };

                businessContext.AddNewLanguage(language);

                businessContext.DeleteLanguage(language);

                bool notDeleted = businessContext.DataContext.Languages.Any(l => l.Id == language.Id);

                Assert.IsFalse(notDeleted);
            }
        }
    }
}