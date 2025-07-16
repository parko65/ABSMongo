using Shared.DataTransferObjects;

namespace Service.Contracts;
public interface IRecipeService
{
    Task<IEnumerable<RecipeDto>> GetRecipesAsync();
    Task<RecipeDto> GetRecipeByIdAsync(string id);
    Task<RecipeDto> CreateRecipeAsync(RecipeForCreationDto recipeForCreation);
}
