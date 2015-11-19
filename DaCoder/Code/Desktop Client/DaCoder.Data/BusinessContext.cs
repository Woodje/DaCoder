using System;

namespace DaCoder.Data
{
    /// <summary>
    /// Encapsulation of the business rules when accessing the data layer.
    /// </summary>
    public class BusinessContext : IDisposable
    {
        private readonly DataContext dataContext;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessContext"/> class.
        /// </summary>
        public BusinessContext()
        {
            dataContext = new DataContext();
        }

        /// <summary>
        /// Gets the underlying <see cref="DataContext"/>.
        /// </summary>
        public DataContext DataContext
        {
            get { return dataContext; }
        }

        /// <summary>
        /// Adds a new language.
        /// </summary>
        public void AddNewLanguage(Language language)
        {
            Check.Require(language.Name);

            dataContext.Languages.Add(language);
            dataContext.SaveChanges();
        }

        /// <summary>
        /// Updates an existing language.
        /// </summary>
        public void UpdateLanguage(Language language)
        {
            Check.Require(language.Name);

            Language existingLanguage = dataContext.Languages.Find(language.Id);

            if (existingLanguage == null)
                throw new ArgumentOutOfRangeException("language.Id", "Id must exist in the Languages-table.");

            existingLanguage = language;

            dataContext.SaveChanges();
        }

        /// <summary>
        /// Deletes an existing language.
        /// </summary>
        public void DeleteLanguage(Language language)
        {
            language = dataContext.Languages.Find(language.Id);

            if (language == null)
                throw new ArgumentOutOfRangeException("language.Id", "Id must exist in the Languages-table.");

            dataContext.Languages.Remove(language);
            dataContext.SaveChanges();
        }

        /// <summary>
        /// Adds a new keyword.
        /// </summary>
        public void AddNewKeyword(Keyword keyword)
        {
            Check.Require(keyword.Name);

            if (dataContext.Languages.Find(keyword.LanguageId) == null)
                throw new ArgumentOutOfRangeException("keyword.LanguageId", "LanguageId must exist in the Languages-table.");

            dataContext.Keywords.Add(keyword);
            dataContext.SaveChanges();
        }

        /// <summary>
        /// Updates an existing keyword.
        /// </summary>
        public void UpdateKeyword(Keyword keyword)
        {
            Check.Require(keyword.Name);

            Keyword existingKeyword = dataContext.Keywords.Find(keyword.Id);

            if (existingKeyword == null)
                throw new ArgumentOutOfRangeException("keyword.Id", "Id must exist in the Keywords-table.");

            existingKeyword = keyword;

            dataContext.SaveChanges();
        }

        /// <summary>
        /// Deletes an existing keyword.
        /// </summary>
        public void DeleteKeyword(Keyword keyword)
        {
            keyword = dataContext.Keywords.Find(keyword.Id);

            if (keyword == null)
                throw new ArgumentOutOfRangeException("keyword.Id", "Id must exist in the Keywords-table.");

            dataContext.Keywords.Remove(keyword);
            dataContext.SaveChanges();
        }

        /// <summary>
        /// Static class used for checking non-specific common requirements.
        /// </summary>
        static class Check
        {
            public static void Require(string value)
            {
                if (value == null)
                    throw new ArgumentNullException();
                else if (value.Trim().Length == 0)
                    throw new ArgumentException();
            }
        }

        #region IDisposable members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed || !disposing)
                return;

            if (dataContext != null)
                dataContext.Dispose();

            disposed = true;
        }

        #endregion

    }
}