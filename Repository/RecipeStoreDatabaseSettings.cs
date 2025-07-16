using Contracts;

namespace Repository;

public class RecipeStoreDatabaseSettings : IRecipeStoreDatabaseSettings
{
    public string CollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}
