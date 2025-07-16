namespace Service.Contracts;
public interface IServiceManager
{
    IRecipeService RecipeService { get; }
    IPLCService PLCService { get; }
}
