using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tele2.Task.Interaction
{
    public abstract class DataManager<TData> : IRepository<TData>, IDisposable
    {
        bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }
                disposedValue = true;
            }
        }

        ~DataManager()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Initializes an object of type <see cref="DataManager{TData}"/>
        /// </summary>
        /// <remarks>To be called on startup</remarks>
        public abstract void Initialize();
        public abstract IEnumerable<TData> GetAll();
        public abstract TData Element(string id);
        public abstract bool Add(TData element);
    }
}
