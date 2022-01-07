using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Tele2.Task.Controllers;
using Tele2.Task.Interaction;
using Tele2.Task.Models;

namespace Tele2.Task.Tests.Controllers
{
    [TestFixture]
    class DwellersControllerTests
    {
        [SetUp]
        public void ConfigureController()
        {
            //controller = new(new List<Dweller>() { new() { Name = "Big Dick", Age = 12, Id = "yes", Sex = "a lot" } });
        }

        DwellersController controller;

        [Test]
        public void GetResultNotNull()
        {
            Assert.NotNull(controller.GetDwellerInfo("yes"));
        }
    }
}
