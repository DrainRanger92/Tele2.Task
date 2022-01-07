using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tele2.Task.Interaction
{
    interface IRepository<T>
    {
        /// <summary>
        /// Method gets all entries of a type <typeparamref name="T"/>
        /// </summary>
        /// <returns>All existing entries in <see cref="IRepository{T}"/></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Finds a certain element in repository
        /// </summary>
        /// <param name="id"><see cref="string"/> as an identity</param>
        /// <returns>Element of a type <typeparamref name="T"/></returns>
        T Element(string id);

        /// <summary>
        /// Adds an element to a repository
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool Add(T element);
    }
}
