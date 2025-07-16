namespace Entities.Exceptions;

public sealed class RecipeNotFoundException : NotFoundException
{
    public RecipeNotFoundException(string id)
        : base($"Recipe with id: {id} doesn't exist in the database.")
    {
    }
}