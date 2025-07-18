using AutoMapper;
using Contracts;
using Entities.ConfigurationOptions;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.Extensions.Options;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;
internal sealed class RecipeService : IRecipeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IOptionsMonitor<StorageConfigurationOptions> _storageConfigurationOptions;

    public RecipeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IOptionsMonitor<StorageConfigurationOptions> storageConfigurationOptions)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _storageConfigurationOptions = storageConfigurationOptions;
    }

    public async Task<IEnumerable<RecipeDto>> GetRecipesAsync()
    {
        var recipes = await _repository.Recipe.GetRecipesAsync();

        var recipeDtos = _mapper.Map<IEnumerable<RecipeDto>>(recipes);

        return recipeDtos;
    }

    public async Task<RecipeDto> GetRecipeByIdAsync(string id)
    {
        var recipe = await _repository.Recipe.GetRecipeByIdAsync(id);

        if (recipe is null)
            throw new RecipeNotFoundException(id);

        var recipeDto = _mapper.Map<RecipeDto>(recipe);

        return recipeDto;
    }

    public async Task<RecipeDto> CreateRecipeAsync(RecipeForCreationDto recipeForCreation)
    {
        var recipe = _mapper.Map<Recipe>(recipeForCreation);

        // Add hotbins to the recipe
        if (recipe.HotBins == null || recipe.HotBins.Length < _storageConfigurationOptions.CurrentValue.MaxBinCount)
        {
            recipe.HotBins = new HotAggregateBin[_storageConfigurationOptions.CurrentValue.MaxBinCount];
            for (int i = 0; i < _storageConfigurationOptions.CurrentValue.MaxBinCount; i++)
            {
                recipe.HotBins[i] = new HotAggregateBin() { Name = $"Bin {i + 1}", Take = 0 };
            }
        }

        if (recipe.BitumenTanks == null || recipe.BitumenTanks.Length < _storageConfigurationOptions.CurrentValue.MaxBinCount)
        {
            recipe.BitumenTanks = new BitumenTank[_storageConfigurationOptions.CurrentValue.MaxBinCount];
            for (int i = 0; i < _storageConfigurationOptions.CurrentValue.MaxBinCount; i++)
            {
                recipe.BitumenTanks[i] = new BitumenTank() { Name = $"Tank {i + 1}", Take = 0 };
            }
        }

        recipe.VersionNumber = 1; // Set initial version number

        await _repository.Recipe.CreateRecipeAsync(recipe);
        await _repository.SaveAsync();

        var recipeDto = _mapper.Map<RecipeDto>(recipe);

        return recipeDto;
    }
}
