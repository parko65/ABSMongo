using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace ABSMongo;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add your mapping configurations here
        // For example:
        // CreateMap<SourceClass, DestinationClass>();
        
        CreateMap<Recipe, RecipeDto>();
        CreateMap<RecipeForCreationDto, Recipe>();

        CreateMap<MaterialForCreationDto, Material>();

        CreateMap<HotAggregateBinForCreationDto, HotAggregateBin>();
    }
}
