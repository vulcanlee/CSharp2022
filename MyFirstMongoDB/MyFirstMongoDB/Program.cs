using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;

namespace MyFirstMongoDB;

internal class Program
{
    static async Task Main(string[] args)
    {
        //QueryByBsonDocument();

        await QueryByLINQAsync();
    }

    static void QueryByBsonDocument()
    {
        var settings = MongoClientSettings
            .FromConnectionString("mongodb+srv://vulcan:pass123@vulcanmongo.hptf95d.mongodb.net/?retryWrites=true&w=majority");
        var client = new MongoClient(settings);
        var database = client.GetDatabase("sample_mflix");

        #region 使用 MongoDB 的用法來查詢
        var collection = database.GetCollection<BsonDocument>("movies");
        var result = collection.Find("{title:'The Princess Bride'}").FirstOrDefault();
        Console.WriteLine(result);
        Console.WriteLine();

        #region 將 BsonDocument 轉換成為 JSON，用於轉換成為 .NET Class
        var dotNetObj = BsonTypeMapper.MapToDotNetValue(result);
        var BsonToJson = JsonConvert.SerializeObject(dotNetObj);
        #endregion

        #endregion
    }

    static async Task QueryByLINQAsync()
    {
        var settings = MongoClientSettings
            .FromConnectionString("mongodb+srv://vulcan:pass123@vulcanmongo.hptf95d.mongodb.net/?retryWrites=true&w=majority");
        var client = new MongoClient(settings);
        var database = client.GetDatabase("sample_mflix");

        #region 使用 C# LINQ 的用法來查詢
        var collection = database.GetCollection<Movie>("movies");
        var result = await collection.AsQueryable()
            .Where(x => x.title == "The Princess Bride")
            .FirstOrDefaultAsync();
        Console.WriteLine(JsonConvert.SerializeObject(result));
        Console.WriteLine();

        #endregion
    }
}