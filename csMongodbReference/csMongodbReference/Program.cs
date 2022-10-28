using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace csMongodbReference;

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
        var myCollection = db.GetCollection<MyCrudModel>("MyCollection");
        var myUserCollection = db.GetCollection<MyUser>("MyUser");
        #endregion

        #region 新增
        Console.WriteLine($"開始進行新增操作");

        #region 新增使用者
        var newUser = new MyUser()
        {
            Account = "Vulcan",
            Password = "123",
        };
        myUserCollection.InsertOne(newUser);
        #endregion

        #region 新增 CRUD 紀錄
        int i = 99;
        var newItem = new MyCrudModel()
        {
            Name = $"Auto Name {i}",
            Age = i,
            Birthday = DateTime.Now.AddDays(i * -1),
            IsAudit = i % 3 == 0,
            User = newUser,
        };
        myCollection.InsertOne(newItem);

        Console.WriteLine($"完成後的JSON物件:" +
            $"{JsonConvert.SerializeObject(newItem, Formatting.Indented)}");
        #endregion

        #endregion

        #region 更新，MyUser 的物件值
        Console.WriteLine($"更新，MyUser 的物件值");
        newUser.Password = "168999";
        FilterDefinition<MyUser> builderMyUserFilter =
            Builders<MyUser>.Filter.Eq(x => x.Id, newUser.Id);
        myUserCollection.ReplaceOne(builderMyUserFilter, newUser, new ReplaceOptions { IsUpsert = true });
        Console.WriteLine($"完成後的JSON物件:" +
            $"{JsonConvert.SerializeObject(newUser, Formatting.Indented)}");
        #endregion
    }
}


#region 資料模型
[BsonIgnoreExtraElements]
public class MyCrudModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime Birthday { get; set; }
    public bool IsAudit { get; set; }
    public MyUser User { get; set; } = new MyUser();
}

[BsonIgnoreExtraElements]
public class MyUser
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Account { get; set; }
    public string Password { get; set; }
}
#endregion

