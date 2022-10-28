using MongoDB.Driver;
using Newtonsoft.Json;

namespace csMongoUpdate;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        #region 連線的準備工作
        var settings = MongoClientSettings
        .FromConnectionString(
        "mongodb+srv://vulcan:P%40ssw0rd@vulcanmongo.hptf95d.mongodb.net/?retryWrites=true&w=majority");
        var client = new MongoClient(settings);

        client.DropDatabase("MyCrud");
        var db = client.GetDatabase("MyCrud");
        db.DropCollection("MyCollection");
        var collection = db.GetCollection<MyCrudModel>("MyCollection");
        #endregion

        #region 新增
        Console.WriteLine($"開始進行新增操作");
        int i = 99;
        var newItem = new MyCrudModel()
        {
            Name = $"Auto Name {i}",
            Age = i,
            Birthday = DateTime.Now.AddDays(i * -1),
            IsAudit = i % 3 == 0,
        };
        collection.InsertOne(newItem);
        Console.WriteLine($"完成後的JSON物件:" +
            $"{JsonConvert.SerializeObject(newItem, Formatting.Indented)}");
        #endregion

        #region 更新，直接更新整個物件，無須個別指定要更新的欄位
        Console.WriteLine($"開始進行查詢 : Age=168");
        newItem.Birthday = DateTime.Now.AddDays(7);
        newItem.Name = "Vulcan Lee";
        newItem.Age = 168;
        FilterDefinitionBuilder<MyCrudModel> builderFilter = Builders<MyCrudModel>.Filter;
        FilterDefinition<MyCrudModel> filterByUpdate =
           builderFilter.Eq(x => x.Id, newItem.Id);
        collection.ReplaceOne(filterByUpdate, newItem, new ReplaceOptions { IsUpsert = true });
        Console.WriteLine($"完成後的JSON物件:" +
            $"{JsonConvert.SerializeObject(newItem, Formatting.Indented)}");
        #endregion
    }
}