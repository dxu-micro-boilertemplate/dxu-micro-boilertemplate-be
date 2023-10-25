using API.Application.Models.ViewModels;
using API.Data.Models;
using AutoMapper;

namespace API.Application.DataMapping;

/// <inheritdoc />
public class MappingProfile : Profile
{
    /// <inheritdoc />
    public MappingProfile()
    {
        UserProfile();
    }

    private void UserProfile()
    {
        this.CreateMap<User, UserResponse>().ReverseMap();
    }
}