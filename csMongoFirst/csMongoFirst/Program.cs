using MongoDB.Bson;
using MongoDB.Driver;

namespace csMongoFirst;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var settings = MongoClientSettings
            .FromConnectionString(
            "mongodb+srv://vulcan:P%40ssw0rd@vulcanmongo.hptf95d.mongodb.net/?retryWrites=true&w=majority");
        var client = new MongoClient(settings);
        var database = client.GetDatabase("sample_mflix");
        var collection = database.GetCollection<BsonDocument>("movies");
        var result = collection.Find("{title:'The Princess Bride'}").FirstOrDefault();
        Console.WriteLine(result);
    }
}