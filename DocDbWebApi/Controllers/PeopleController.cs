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
    public class PeopleController : ApiController
    {
        private PersonRepository _repo;

        public PeopleController()
        {
            _repo = new PersonRepository(); //we would want to DI this in real life
        }

        //api/people/
        public async Task<IHttpActionResult> Get()
        {
            var people = await _repo.GetPeopleAsync();
            return Ok(people);
        }

        //api/people/{id}
        public async Task<IHttpActionResult> Get(string id)
        {
            var person = await _repo.GetPersonAsync(id);
            if (person != null)
                return Ok(person);
            return NotFound();
        }

        //api/people/?lastName=
        public async Task<IHttpActionResult> GetByLastName(string lastName)
        {
            var people = await _repo.GetPeopleByLastNameAsync(lastName);
            return Ok(people);
        }

        //api/people
        public async Task<IHttpActionResult> Post(Person person)
        {
            var response = await _repo.CreatePerson(person);
            return Ok(response.Resource);
        }

        //api/people
        public async Task<IHttpActionResult> Put(Person person)
        {
            var response = await _repo.UpdatePersonAsync(person);
            return Ok(response.Resource);
        }

        //api/people
        public async Task<IHttpActionResult> Delete(string id)
        {
            var response = await _repo.DeletePersonAsync(id);
            return Ok();
        }
    }
}
