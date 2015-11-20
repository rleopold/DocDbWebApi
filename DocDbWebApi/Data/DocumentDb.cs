using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace DocDbWebApi.Data
{
    public class DocumentDb
    {
        private static string _databaseId;
        private static string _collectionId;
        private static Database _database;
        private static DocumentCollection _collection;
        private static DocumentClient _client;

        public DocumentDb(string database, string collection)
        {
            _databaseId = database;
            _collectionId = collection;
            Initilization = InitializeAsync();
        }

        public Task Initilization { get; private set; }

        private async Task InitializeAsync()
        {
            await ReadOrCreateDatabase();
            await ReadOrCreateCollection(_database.SelfLink);
        }

        protected static DocumentClient Client
        {
            get
            {
                if (_client == null)
                {
                    string endpoint = ConfigurationManager.AppSettings["endpoint"];
                    string authKey = ConfigurationManager.AppSettings["authKey"];

                    Uri endpointUri = new Uri(endpoint);
                    _client = new DocumentClient(endpointUri, authKey);
                }
                return _client;
            }
        }

        protected static DocumentCollection Collection
        {
            get { return _collection; }
        }

        private static async Task ReadOrCreateCollection(string databaseLink)
        {
            var collections = Client.CreateDocumentCollectionQuery(databaseLink)
                              .Where(col => col.Id == _collectionId).ToArray();

            if (collections.Any())
            {
                _collection = collections.First();
            }
            else
            {
                _collection = await Client.CreateDocumentCollectionAsync(databaseLink,
                    new DocumentCollection { Id = _collectionId });
            }
        }

        private static async Task ReadOrCreateDatabase()
        {
            var query = Client.CreateDatabaseQuery()
                            .Where(db => db.Id == _databaseId);

            var databases = query.ToArray();
            if (databases.Any())
            {
                _database = databases.First();
            }
            else
            {
                _database = await Client.CreateDatabaseAsync(new Database { Id = _databaseId });
            }
        }

    }
}