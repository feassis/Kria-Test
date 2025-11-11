
using MongoDB.Bson;
using MongoDB.Driver;

namespace DBProcessor.Database
{
    public class DatabaseWrapper
    {
        private string DBAddress;

        public DatabaseWrapper(string dBAddress)
        {
            DBAddress = dBAddress;
        }

        public async Task<List<T>> ConnectToDB<T>(string dbName, string collectionName)
        {
            var client = new MongoClient(DBAddress);
            var database = client.GetDatabase(dbName);

            var collection = database.GetCollection<T>(collectionName);

            var data = await collection.Find(_ => true).ToListAsync();

            Console.WriteLine($"Foram encontrados {data.Count} registros.");


            return data;
        }
        public async Task ConnectRaw()
        {
            var client = new MongoClient(DBAddress);
            var database = client.GetDatabase("Kria");
            var collection = database.GetCollection<BsonDocument>("Candidato");
            var docs = await collection.Find(_ => true).ToListAsync();

            Console.WriteLine($"Encontrados: {docs.Count}");
            Console.WriteLine(docs.FirstOrDefault()?.ToJson());
        }
    }
}