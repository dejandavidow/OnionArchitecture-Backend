using AutoMapper;
using Contracts.DTOs;
using Domain.Entities;

namespace Services.MappingProfile
{
    public class ServiceProfiles : Profile
    {
        public ServiceProfiles()
        {
            CreateMap<Client, ClientDTO>();
            CreateMap<Project, ProjectDTO>();
        }
    }
}
