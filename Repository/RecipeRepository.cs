using Contracts;
using Entities.Models;

namespace Repository;
public class RecipeRepository : RepositoryBase<Recipe>, IRecipeRepository
{
    public RecipeRepository(IMongoContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Recipe>> GetRecipesAsync() =>
        // Example implementation for getting all recipes
        await FindAllAsync();

    public async Task<Recipe> GetRecipeByIdAsync(string id) =>
        await FindByIdAsync(id);

    public async Task CreateRecipeAsync(Recipe recipe)
    {
        await CreateAsync(recipe);
    }
}
