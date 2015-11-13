using System;

namespace DaCoder.Data
{
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

        public void AddNewLanguage(Language language)
        {
            Check.Require(language.Name);

            dataContext.Languages.Add(language);
            dataContext.SaveChanges();
        }

        static class Check
        {
            public static void Require(String value)
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