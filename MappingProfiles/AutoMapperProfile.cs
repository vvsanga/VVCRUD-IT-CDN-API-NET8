using AutoMapper;
using VVCRUD_IT_CDN_API_NET8.Models.Dtos.ProfessionalDtos;
using VVCRUD_IT_CDN_API_NET8.Models.Entities;

namespace VVCRUD_IT_CDN_API_NET8.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Professional, ProfessionalViewDto>();
        }
    }
}
