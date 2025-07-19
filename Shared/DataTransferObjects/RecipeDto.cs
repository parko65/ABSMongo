namespace Shared.DataTransferObjects;
public record RecipeDto
{    
    public string? Id { get; init; }    
    public string? Name { get; init; }    
    public string? Description { get; init; }
    public int VersionNumber { get; init; }
    public DateTime Created { get; init; }
    public HotAggregateBinDto[]? HotBins { get; init; }
    public BitumenTankDto[]? BitumenTanks { get; init; }
}
