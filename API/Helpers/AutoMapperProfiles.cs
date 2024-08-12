using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // d is for the destination the mapped object will be saved too, 
        // o is the object mapper that will perform the map,
        // s is the source object from the connected config instance "CreateMap(Photo)"
        // Then you grab the object that you wish to map to the destination
        // Add the ! so if an item isnt present auto mapper (MapFrom) will set the destination variable to null
        CreateMap<AppUser, MemberDTO>()
        .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge()))
        .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url));
        CreateMap<Photo, PhotoDto>();
        CreateMap<MemberUpdateDto, AppUser>();
        CreateMap<RegisterDto, AppUser>();
        CreateMap<string, DateOnly>().ConvertUsing(s => DateOnly.Parse(s));

    }
}
