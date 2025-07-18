using Entities.ConfigurationOptions;
using Microsoft.Extensions.Options;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ABSMongo.Components.RecipePages;

public partial class RecipeEditor
{
    private readonly IOptionsMonitor<StorageConfigurationOptions> _storageOptions;
    private readonly IServiceManager _serviceManager;

    public RecipeEditor(IOptionsMonitor<StorageConfigurationOptions> storageOptions, IServiceManager serviceManager)
    {
        _storageOptions = storageOptions;
        _serviceManager = serviceManager;
    }

    private int MaxBinCount => _storageOptions.CurrentValue.MaxBinCount;
    private RecipeForCreationDto RecipeForCreation { get; set; } = new RecipeForCreationDto();
    private List<RecipeDto> Recipes { get; set; } = [];
    private HotAggregateBinForCreationDto HotBin { get; set; } = new HotAggregateBinForCreationDto();
    private List<HotAggregateBinForCreationDto> HotBins { get; set; } = [];

    private bool _recipeCreated = false;

    private async Task LoadRecipesAsync()
    {
        var recipes = await _serviceManager.RecipeService.GetRecipesAsync();
        if (recipes is not null)
        {
            Recipes = recipes.ToList();
        }
    }

    private async Task CreateRecipeAsync()
    {
        var createdRecipe = await _serviceManager.RecipeService.CreateRecipeAsync(RecipeForCreation);
        // check if the recipe was created successfully and set the flag to disable the button
        if (createdRecipe is not null)
        {
            _recipeCreated = true;

            foreach (var hotbin in createdRecipe.HotBins!)
            {
                HotBins.Add(new HotAggregateBinForCreationDto
                {
                    Name = hotbin.Name,
                    Take = hotbin.Take
                });
            }
        }

        await LoadRecipesAsync();
    }
}
