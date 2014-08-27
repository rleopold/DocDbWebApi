using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocDbWebApi.Data
{
    public class PersonRepository :  DocumentDb
    {
        //each repo can specify it's own database and document collection
        public PersonRepository() : base("TestDb", "People") { }

        public Task<List<Person>> GetPeopleAsync()
        {
            return Task<List<Person>>.Run(() => 
                Client.CreateDocumentQuery<Person>(Collection.DocumentsLink)
                .ToList());
        }

        public Task<Person> GetPersonAsync(string id)
        {
            return Task<Person>.Run(() => 
                Client.CreateDocumentQuery<Person>(Collection.DocumentsLink)
                .Where(p => p.Id == id)
                .AsEnumerable()
                .FirstOrDefault());
        }

        public Task<ResourceResponse<Document>> CreatePerson(Person person)
        {
            return Client.CreateDocumentAsync(Collection.DocumentsLink, person);
        }

        public Task<ResourceResponse<Document>> UpdatePersonAsync(Person person)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == person.Id)
                .AsEnumerable() // why the heck do we need to do this??
                .FirstOrDefault();

            return Client.ReplaceDocumentAsync(doc.SelfLink, person);
        }

        public Task<ResourceResponse<Document>> DeletePersonAsync(string id)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == id)
                .AsEnumerable()
                .FirstOrDefault();

            return Client.DeleteDocumentAsync(doc.SelfLink);
        }

        public Task<List<Person>> GetPeopleByLastNameAsync(string lastName)
        {
            return Task.Run(() =>
                Client.CreateDocumentQuery<Person>(Collection.DocumentsLink)
                .Where(p => p.LastName == lastName)
                .ToList());
        }
    }
}