using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Tele2.Task.Models;

namespace Tele2.Task.Interaction
{
    public class DwellersManager : DataManager<Dweller>
    {
        public DwellersManager(IServiceScopeFactory scopeFactory, IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("PeopleGetter");
            sqlClient = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<DwellersContext>();
        }

        readonly HttpClient httpClient;
        readonly DwellersContext sqlClient;

        public override void Initialize()
        {
            List<Dweller> dwellers = httpClient.GetFromJsonAsync<List<Dweller>>("Task").Result;
            foreach (var dweller in dwellers)
            {
                var recieved = httpClient.GetFromJsonAsync<Dweller>("task/" + dweller.Id).Result;
                dweller.Age = recieved.Age;
                sqlClient.Dwellers.Add(dweller);
            }
            sqlClient.SaveChanges();
        }

        public override IEnumerable<Dweller> GetAll()
            => sqlClient.Dwellers;

        public override Dweller Element(string id)
            => sqlClient.Dwellers.Find(id);

        public override bool Add(Dweller element)
        {
            var entity = sqlClient.Dwellers.Add(element);
            return entity.State == EntityState.Added;
        }

        protected override void Dispose(bool disposing)
        {
            httpClient.Dispose();
            sqlClient.Dispose();
            base.Dispose(disposing);
        }
    }
}
