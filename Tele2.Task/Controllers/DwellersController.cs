using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tele2.Task.Interaction;
using Tele2.Task.Models;
using static Tele2.Task.Extensions.LinqExtensions;

namespace Tele2.Task.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DwellersController : ControllerBase
    {
        public DwellersController(DwellersManager dwellersManager)
        {
            dwellers = dwellersManager.GetAll();
        }

        IEnumerable<Dweller> dwellers;

        /// <summary>
        /// Gets the entire retrieved data about all city dwellers
        /// </summary>
        /// <returns>A list of dwellers</returns>
        [HttpGet]
        public IEnumerable<Dweller> GetDwellersInfo()
            => dwellers.ToList();

        /// <summary>
        /// Gets certain person by his/her unique id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("single")]
        public Dweller GetDwellerInfo(string id)
            => dwellers.Where(d => d.Id == id).FirstOrDefault();

        /// <summary>
        /// Finds people satisfying the desired criteria and splits the results into pages.
        /// </summary>
        /// <remarks>Won't apply filtering if parameters are intentionally or accidentally wrong</remarks>
        /// <param name="page">1-based page number</param>
        /// <param name="entries">number of entries to display per page</param>
        /// <param name="sex">aplies to filtering only if contains "male" or "female" <see cref="string"/>s</param>
        /// <param name="youngest">the lowest age of people to display</param>
        /// <param name="oldest">the greatest age to display</param>
        /// <returns>An enumeration of dwellers</returns>
        [HttpGet("filter")]
        public IEnumerable<Dweller> GetFilteredDwellersInfo(int page, int entries, string sex = "any", int youngest = 1, int oldest = 100)
            => dwellers.Find(d => d.Sex == sex,
                            () => sex == "male" | sex == "female")
                       .Find(d => d.Age < oldest && d.Age > youngest,
                            () => youngest > 0 && oldest > 0 && oldest > youngest)
                       .WithPagination(page, entries);
    }
}
