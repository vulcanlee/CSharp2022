using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMongoDB
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    [BsonIgnoreExtraElements]
    public class Movie
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string plot { get; set; }
        public List<string> genres { get; set; }
        public int runtime { get; set; }
        public int metacritic { get; set; }
        public string rated { get; set; }
        public List<string> cast { get; set; }
        public string poster { get; set; }
        public string title { get; set; }
        public string fullplot { get; set; }
        public List<string> languages { get; set; }
        public DateTime released { get; set; }
        public List<string> directors { get; set; }
        public List<string> writers { get; set; }
        public Awards awards { get; set; }
        public string lastupdated { get; set; }
        public int year { get; set; }
        public Imdb imdb { get; set; }
        public List<string> countries { get; set; }
        public string type { get; set; }
        public Tomatoes tomatoes { get; set; }
        public int num_mflix_comments { get; set; }
    }
    public class Awards
    {
        public int wins { get; set; }
        public int nominations { get; set; }
        public string text { get; set; }
    }

    public class Critic
    {
        public double rating { get; set; }
        public int numReviews { get; set; }
        public int meter { get; set; }
    }

    public class Imdb
    {
        public double rating { get; set; }
        public int votes { get; set; }
        public int id { get; set; }
    }


    public class Tomatoes
    {
        public string website { get; set; }
        public Viewer viewer { get; set; }
        public DateTime dvd { get; set; }
        public Critic critic { get; set; }
        public DateTime lastUpdated { get; set; }
        public string consensus { get; set; }
        public int rotten { get; set; }
        public string production { get; set; }
        public int fresh { get; set; }
    }

    public class Viewer
    {
        public double rating { get; set; }
        public int numReviews { get; set; }
        public int meter { get; set; }
    }

}
