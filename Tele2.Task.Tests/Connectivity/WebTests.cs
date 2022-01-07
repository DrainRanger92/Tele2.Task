using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Tele2.Task.Models;

namespace Tele2.Task.Tests.Connectivity
{
    [TestFixture]
    class WebTests
    {
        [SetUp]
        public void Configure()
        {
            client = new();
        }

        const string url = "http://testlodtask20172.azurewebsites.net/task/";
        HttpClient client;

        [Test]
        public void RemoteReachable()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 15000;
            request.Method = "GET";
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                }
            }
            catch (WebException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void CanPullData()
        {
            var collection = client.GetFromJsonAsync<List<Dweller>>(url).Result;
            Assert.NotNull(collection);
            var recieved = client.GetFromJsonAsync<Dweller>(url + collection.First().Id).Result;
            Assert.NotNull(recieved);
        }
    }
}
