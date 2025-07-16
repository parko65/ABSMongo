using Entities.Models;

namespace Contracts;
public interface IRecipeRepository
{
    // Define methods for the Recipe repository, e.g.:
    Task<IEnumerable<Recipe>> GetRecipesAsync();
    Task<Recipe> GetRecipeByIdAsync(string id);
    Task CreateRecipeAsync(Recipe recipe);
}
