using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMongoDB
{
    [BsonIgnoreExtraElements]
    public class Todo
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completion { get; set; }
        public List<int> Tags { get; set; }
        public User User { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
    }
}
