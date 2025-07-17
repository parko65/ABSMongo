using AutoMapper;
using Contracts;
using Entities.ConfigurationOptions;
using Microsoft.Extensions.Options;
using Service.Contracts;

namespace Service;
public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IRecipeService> _recipeService;
    private readonly Lazy<IPLCService> _plcService;

    public ServiceManager(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IOptionsMonitor<PLCConfigurationOptions> plcOptions,
        IOptionsMonitor<StorageConfigurationOptions> storageOptions)
    {
        _recipeService = new Lazy<IRecipeService>(() => new RecipeService(repository, logger, mapper, storageOptions));
        _plcService = new Lazy<IPLCService>(() => new PLCService(plcOptions, logger));
    }
    public IRecipeService RecipeService => _recipeService.Value;
    public IPLCService PLCService => _plcService.Value;
}
