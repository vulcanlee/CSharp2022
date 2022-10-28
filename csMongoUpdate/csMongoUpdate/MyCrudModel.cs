using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csMongoUpdate;

[BsonIgnoreExtraElements]
public class MyCrudModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime Birthday { get; set; }
    public bool IsAudit { get; set; }
}

[BsonIgnoreExtraElements]
public class MyUser
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Account { get; set; }
    public string Password { get; set; }
}
