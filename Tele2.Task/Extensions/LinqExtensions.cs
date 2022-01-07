using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tele2.Task.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Finds element in an enumeration using predicate <paramref name="execute"/> if <paramref name="canExecute"/> returns true, otherwise returns the enumeration untouched.
        /// </summary>
        /// <remarks>This method enumerates the sequence</remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        /// <returns></returns>
        public static IEnumerable<T> Find<T>(this IEnumerable<T> enumerable, Predicate<T> execute, Func<bool> canExecute)
        {
            if (!canExecute())
            {
                return enumerable;
            }
            return enumerable.WithPredicate(execute);
        }

        public static IEnumerable<T> WithPredicate<T>(this IEnumerable<T> enumerable, Predicate<T> execute)
        {
            foreach (T match in enumerable)
            {
                if (execute(match))
                {
                    yield return match;
                }
            }
        }

        /// <summary>
        /// Applies pagination for an enumeration of <typeparamref name="T"/> objects
        /// </summary>
        /// <remarks>Designed for 1-based page number</remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="page"></param>
        /// <param name="entries"></param>
        /// <returns></returns>
        public static IEnumerable<T> WithPagination<T>(this IEnumerable<T> enumerable, int page, int entries)
        {
            if (page < 1) throw new ArgumentException("Page number should be 1 or greater");

            return enumerable.Skip(entries * (page - 1))
                             .Take(entries);
        }
    }
}
