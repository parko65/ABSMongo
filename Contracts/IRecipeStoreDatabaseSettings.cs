namespace Contracts;
public interface IRecipeStoreDatabaseSettings
{
    string CollectionName { get; set; }
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
}
