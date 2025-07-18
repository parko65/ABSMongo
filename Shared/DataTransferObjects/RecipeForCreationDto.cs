namespace Shared.DataTransferObjects;
public class RecipeForCreationDto
{
    public string? Name { get; set; }    
    public string? Description { get; set; }    
    public HotAggregateBinForCreationDto[]? HotBins { get; set; }
    public BitumenTankForCreationDto[]? BitumenTanks { get; set; }
}
