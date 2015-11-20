using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DocDbWebApi.Data;
using System.Threading.Tasks;

namespace DocDbWebApi.Controllers
{
    [Authorize]
    public class PeopleController : ApiController
    {
        private PersonRepository _repo;

        public PeopleController()
        {
            Initilization = InitializeAsync();
        }

        public Task Initilization { get; private set; }

        private async Task InitializeAsync()
        {
            _repo = new PersonRepository();
            await _repo.Initilization;
        }

        //api/people/
        public async Task<IHttpActionResult> Get()
        {
            await Initilization;
            var people = await _repo.GetPeopleAsync();
            return Ok(people);
        }

        //api/people/{id}
        public async Task<IHttpActionResult> Get(string id)
        {
            await Initilization;
            var person = await _repo.GetPersonAsync(id);
            if (person != null)
                return Ok(person);
            return NotFound();
        }

        //api/people/?lastName=
        public async Task<IHttpActionResult> GetByLastName(string lastName)
        {
            await Initilization;
            var people = await _repo.GetPeopleByLastNameAsync(lastName);
            return Ok(people);
        }

        //api/people
        public async Task<IHttpActionResult> Post(Person person)
        {
            await Initilization;
            var response = await _repo.CreatePerson(person);
            return Ok(response.Resource);
        }

        //api/people
        public async Task<IHttpActionResult> Put(Person person)
        {
            await Initilization;
            var response = await _repo.UpdatePersonAsync(person);
            return Ok(response.Resource);
        }

        //api/people
        public async Task<IHttpActionResult> Delete(string id)
        {
            await Initilization;
            var response = await _repo.DeletePersonAsync(id);
            return Ok();
        }
    }
}
