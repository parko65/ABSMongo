using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models;
public class HotAggregateBin
{
    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("take")]
    public int Take { get; set; }
}