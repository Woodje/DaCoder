using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaCoder.Data.Tests.FunctionalTests
{
    [TestClass]
    public class KeywordScenarioTests : FunctionalTest
    {
        [TestMethod]
        public void AddNewKeyWordIsPersisted()
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

                bool exists = businessContext.DataContext.Languages.Any(l => l.Id == keyword.Id);

                Assert.IsTrue(exists);
            }
        }
    }
}