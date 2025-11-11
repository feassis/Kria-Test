
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

            var mongoUrl = new MongoUrl(DBAddress);
            var settings = MongoClientSettings.FromUrl(mongoUrl);


        }

        public async Task<List<T>> ConnectToDB<T>(string dbName, string collectionName)
        {
            var client = new MongoClient(DBAddress);
            var database = client.GetDatabase(dbName);

            var collection = database.GetCollection<T>(collectionName);

            var data = await collection.Find(_ => true).Limit(Secrets.Secrets.NUMBER_OF_TRANSACTIONS).ToListAsync();

            Console.WriteLine($"\n✅ Encontrados {data.Count} documentos:");
            


            return data;
        }
        public async Task ConnectRaw(string dbName, string collectionName)
        {
            try
            {
                Console.WriteLine("🔗 Conectando ao MongoDB Atlas...");
                var client = new MongoClient(DBAddress);

                // Testa a conexão listando os bancos disponíveis
                var dbs = await client.ListDatabaseNames().ToListAsync();
                Console.WriteLine("\n📚 Bancos disponíveis no cluster:");
                dbs.ForEach(Console.WriteLine);

                var database = client.GetDatabase(dbName);
                var colls = await database.ListCollectionNames().ToListAsync();
                Console.WriteLine($"\n📦 Coleções no banco '{dbName}':");
                colls.ForEach(Console.WriteLine);

                var collection = database.GetCollection<BsonDocument>(collectionName);
                var docs = await collection.Find(_ => true).Limit(3).ToListAsync();

                Console.WriteLine($"\n✅ Encontrados {docs.Count} documentos:");
                foreach (var doc in docs)
                {
                    Console.WriteLine(doc.ToJson());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Erro: {ex.Message}");
            }
        }
    }
}