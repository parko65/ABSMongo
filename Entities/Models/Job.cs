using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models;
public class Job
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public int JobNumber { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<string> MaterialIds { get; set; } = new List<string>();
    public List<string> RecipeIds { get; set; } = new List<string>();
}
