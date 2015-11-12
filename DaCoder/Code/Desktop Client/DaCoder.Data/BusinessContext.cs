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

        public TestModel AddNewTestModel(String name)
        {
            if (name == null)
                throw new ArgumentNullException("name", "name must be not-null.");

            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("name must not be an empty string.", "name");

            var testModel = new TestModel
            {
                Name = name,
            };

            dataContext.TestModels.Add(testModel);
            dataContext.SaveChanges();

            return testModel;
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