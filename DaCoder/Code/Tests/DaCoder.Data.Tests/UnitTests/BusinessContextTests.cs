using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaCoder.Data.Tests.UnitTests
{
    [TestClass]
    public class BusinessContextTests : FunctionalTest
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
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateLanguage_ThrowsException_WhenNameIsEmpty()
        {
            using (var businessContext = new BusinessContext())
            {
                var language = new Language
                {
                    Name = ""
                };

                businessContext.UpdateLanguage(language);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateLanguage_ThrowsException_WhenLanguageIdDoesNotExist()
        {
            using (var businessContext = new BusinessContext())
            {
                var language = new Language
                {
                    Name = "LanguageName"
                };

                businessContext.UpdateLanguage(language);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DeleteLanguage_ThrowsException_WhenLanguageIdDoesNotExist()
        {
            using (var businessContext = new BusinessContext())
            {
                var language = new Language
                {
                    Name = "LanguageName"
                };

                businessContext.DeleteLanguage(language);
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewKeyword_ThrowsException_WhenNameIsNull()
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
                    Name = null,
                    LanguageId = 1
                };

                businessContext.AddNewKeyword(keyword);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewKeyword_ThrowsException_WhenNameIsEmpty()
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
                    Name = "",
                    LanguageId = 1
                };

                businessContext.AddNewKeyword(keyword);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateKeyword_ThrowsException_WhenKeywordIdDoesNotExist()
        {
            using (var businessContext = new BusinessContext())
            {
                var keyword = new Keyword
                {
                    Name = "KeywordName"
                };

                businessContext.UpdateKeyword(keyword);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DeleteKeyword_ThrowsException_WhenKeywordIdDoesNotExist()
        {
            using (var businessContext = new BusinessContext())
            {
                var keyword = new Keyword
                {
                    Name = "KeywordName"
                };

                businessContext.DeleteKeyword(keyword);
            }
        }
    }
}