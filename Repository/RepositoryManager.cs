using Contracts;

namespace Repository;
public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IRecipeRepository> _recipeRepository;

    public RepositoryManager(IMongoContext context)
    {
        _recipeRepository = new Lazy<IRecipeRepository>(() => new RecipeRepository(context));
    }

    public IRecipeRepository Recipe => _recipeRepository.Value;

    public async Task SaveAsync()
    {
        // In MongoDB, changes are saved immediately, so this method can be empty or used for logging
        await Task.CompletedTask;
    }
}