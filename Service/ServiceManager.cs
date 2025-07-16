using AutoMapper;
using Contracts;
using Service.Contracts;

namespace Service;
public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IRecipeService> _recipeService;

    public ServiceManager(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _recipeService = new Lazy<IRecipeService>(() => new RecipeService(repository, logger, mapper));
    }
    public IRecipeService RecipeService => _recipeService.Value;
}
