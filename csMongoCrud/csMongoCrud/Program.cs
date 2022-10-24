using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace csMongoCrud;

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

        #region 查詢
        Console.WriteLine($"開始進行查詢 : Age=99");
        FilterDefinitionBuilder<MyCrudModel> builderFilter = Builders<MyCrudModel>.Filter;
        FilterDefinition<MyCrudModel> filter = 
            builderFilter.Eq(x => x.Age, 99);
        var retriveItem = collection.Find(filter).FirstOrDefault();
        Console.WriteLine($"查詢完成後的JSON物件:" +
            $"{JsonConvert.SerializeObject(retriveItem, Formatting.Indented)}");
        #endregion

        #region 更新
        Console.WriteLine($"開始進行查詢 : Age=168");
        FilterDefinition<MyCrudModel> filterByUpdate =
           builderFilter.Eq(x => x.Id, newItem.Id);
        var update = Builders<MyCrudModel>.Update.Set(x => x.Age, 168);
        collection.UpdateOne(filterByUpdate, update);
        #endregion

        #region 刪除
        Console.WriteLine($"開始進行刪除 : Age=168");
        FilterDefinition<MyCrudModel> filterByDelete =
           builderFilter.Eq(x => x.Id, newItem.Id);
        collection.DeleteOne(filterByDelete);
        #endregion
    }
}