using AutoMapper;
using AutoTest.BLL.DTOs.User;
using AutoTest.Domain.Entities.Tests;
using AutoTest.Domain.Entities.Users;

namespace AutoTest.BLL.Common.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        /*-----------------------------------User-----------------------------------*/
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Tests, opt => opt.Ignore());

        CreateMap<User, UserDto>();
        CreateMap<User, RegisterDto>();
        CreateMap<User, LoginDto>();
        CreateMap<User, UpdateUserDto>();
        CreateMap<User, UserChangePasswordDto>();
    }
}
