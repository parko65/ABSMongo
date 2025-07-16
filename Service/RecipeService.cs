using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;
internal sealed class RecipeService : IRecipeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public RecipeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
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

        await _repository.Recipe.CreateRecipeAsync(recipe);
        await _repository.SaveAsync();

        var recipeDto = _mapper.Map<RecipeDto>(recipe);

        return recipeDto;
    }
}
