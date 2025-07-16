using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models;

public class Recipe
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; }    

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("materials")]
    public Material[]? Materials { get; set; }
}
