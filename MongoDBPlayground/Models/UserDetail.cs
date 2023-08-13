using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace MongoDBPlayground.Models
{
    public class UserDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("_id")]
        public string? Id { get; set; }

        [BsonElement("DiplayName")]
        [JsonPropertyName("DiplayName")]
        public string Name { get; set; } = string.Empty;

        public JobItem? Job { get; set; }

        [BsonElement("Programming Languages")]
        [JsonPropertyName("Programming Language")]
        public List<string> Languages { get; set; } = new List<string>();

        public string UserName { get; set; } = string.Empty;
    }

    public class JobItem
    {
        public string Title { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        [BsonElement("isManager")]
        public bool IsManager { get; set; }
    }
}

/*
[
  {
    "_id": {"$oid": "64bddea9488fff3053a96812"},
    "DiplayName": "Pinal Dave",
    "Job": {
      "Title": "DBA",
      "Area": "Database Performance Tuning",
      "isManager": false
    },
    "Programming Languages": ["T-SQL", "JS", "HTML"],
    "UserName": "pinaldave"
  }
]

*/