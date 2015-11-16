using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaCoder.Data.Tests.UnitTests
{
    [TestClass]
    public class BusinessContextTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewLanguage_ThrowsException_WhenNameIsNull()
        {
            using (var businessContext = new BusinessContext())
            {
                var language = new Language
                {
                    Name = null
                };

                businessContext.AddNewLanguage(language);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewLanguage_ThrowsException_WhenNameIsEmpty()
        {
            using (var businessContext = new BusinessContext())
            {
                var language = new Language
                {
                    Name = ""
                };

                businessContext.AddNewLanguage(language);
            }
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void AddNewKeyword_ThrowsException_WhenLanguageIdDoesNotExist()
        {
            using (var businessContext = new BusinessContext())
            {
                var keyword = new Keyword
                {
                    Name = "KeywordName",
                    LanguageId = 0
                };

                businessContext.AddNewKeyword(keyword);
            }
        }
    }
}