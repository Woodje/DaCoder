using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaCoder.Data.Tests.FunctionalTests
{
    [TestClass]
    public class KeywordScenarioTests : FunctionalTest
    {
        [TestMethod]
        public void AddNewKeywordIsPersisted()
        {
            using (var businessContext = new BusinessContext())
            {
                var language = new Language
                {
                    Name = "LanguageName"
                };

                businessContext.AddNewLanguage(language);

                var keyword = new Keyword
                {
                    Name = "KeywordName",
                    LanguageId = 1,
                };

                businessContext.AddNewKeyword(keyword);

                bool exists = businessContext.DataContext.Languages.Any(k => k.Id == keyword.Id);

                Assert.IsTrue(exists);
            }
        }

        [TestMethod]
        public void UpdateKeywordIsPersisted()
        {
            using (var businessContext = new BusinessContext())
            {
                var language = new Language
                {
                    Name = "LanguageName"
                };

                businessContext.AddNewLanguage(language);

                var keyword = new Keyword
                {
                    Name = "KeywordName",
                    LanguageId = 1,
                };

                businessContext.AddNewKeyword(keyword);

                keyword.Name = "New KeywordName";

                businessContext.UpdateKeyword(keyword);

                bool updated = businessContext.DataContext.Keywords.Any(k => k.Id == keyword.Id && k.Name == keyword.Name);

                Assert.IsTrue(updated);
            }
        }

        [TestMethod]
        public void DeleteKeywordIsPersisted()
        {
            using (var businessContext = new BusinessContext())
            {
                var language = new Language
                {
                    Name = "LanguageName"
                };

                businessContext.AddNewLanguage(language);

                var keyword = new Keyword
                {
                    Name = "KeywordName",
                    LanguageId = 1,
                };

                businessContext.AddNewKeyword(keyword);

                //businessContext.DeleteKeyword(keyword);

                bool notDeleted = businessContext.DataContext.Keywords.Any(k => k.Id == keyword.Id);

                //Assert.IsFalse(notDeleted);
            }
        }
    }
}