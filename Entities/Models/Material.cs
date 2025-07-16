using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models;
public class Material
{
    [BsonElement("materialNumber")]
    public int MaterialNumber { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; }
}