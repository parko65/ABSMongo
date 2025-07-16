namespace Contracts;
public interface IRepositoryManager
{
    IRecipeRepository Recipe { get; }
    Task SaveAsync();
}