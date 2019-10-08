using System.Linq;
using AutoMapper;
using BoardGameDating.api.Helpers;
using BoardGameDating.api.Models;
using BoardGameDating.api.Models.DTOs;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForGetOneDto>()
                .ForMember(dest => dest.PhotoUrl, opt => 
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt =>
                {
                    opt.MapFrom(d => d.DOB.CalculateAge());
                });
            CreateMap<User, UsersForGetAllDto>()
                .ForMember(dest => dest.PhotoUrl, opt => 
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt =>
                {
                    opt.MapFrom(d => d.DOB.CalculateAge());
                });
            CreateMap<Photo, PhotosForDetailsDto>();
        }
    }
}