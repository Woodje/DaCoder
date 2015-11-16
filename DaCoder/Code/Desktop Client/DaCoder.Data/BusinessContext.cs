using System;
using System.CodeDom;

namespace DaCoder.Data
{
    /// <summary>
    /// Encapsulation of the business rules when accessing the data layer.
    /// </summary>
    public class BusinessContext : IDisposable
    {
        private readonly DataContext dataContext;
        private bool disposed;

        public BusinessContext()
        {
            dataContext = new DataContext();
        }

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
        /// Static class used for checking common requirements.
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