using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using static Tele2.Task.Extensions.LinqExtensions;

namespace Tele2.Task.Tests.Generic
{
    [TestFixture]
    class LinqExtensionsTests
    {
        [SetUp]
        public void SetEnumerable()
            => enumerable = Enumerable.Range(1, 10);

        IEnumerable<int> enumerable;

        /// <summary>
        /// Should skip enumeration if the canExecute is false
        /// </summary>
        [Test]
        public void IgnoresEnumeration()
        {
            var enumerated = enumerable.Find(
                ExtractCondition,
                () => false
            );
            Assert.AreEqual(enumerable, enumerated);
        }

        /// <summary>
        /// Should perform enumeration if the canExecute is true
        /// </summary>
        [Test]
        public void DoesNotIgnoreEnumeration()
        {
            var enumerated = enumerable.Find(
                ExtractCondition,
                () => true
            );
            Assert.AreEqual(5, enumerated.Count());
        }

        [Test]
        public void PaginationThrows()
        {
            Assert.Throws<ArgumentException>(delegate { enumerable.WithPagination(0, 10); });
        }

        [Test]
        public void SplitsEntireEnumeration()
        {
            var paginatedResult = enumerable.WithPagination(1, 5)
                                            .Concat(enumerable.WithPagination(2, 5));
            Assert.AreEqual(enumerable, paginatedResult);
        }

        [Test]
        public void PaginatesResult()
        {
            var paginatedResult = enumerable.WithPagination(2, 2);
            Assert.AreEqual(Enumerable.Range(1, 4).Skip(2), paginatedResult);
        }

        static bool ExtractCondition(int element)
            => element > 5;
    }
}
