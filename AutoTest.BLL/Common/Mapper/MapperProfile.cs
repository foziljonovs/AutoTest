using AutoMapper;
using AutoTest.BLL.DTOs.Tests.Option;
using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.DTOs.Tests.Topic;
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
        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.Tests, opt => opt.Ignore());
        CreateMap<LoginDto, User>()
            .ForMember(dest => dest.Tests, opt => opt.Ignore());
        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Tests, opt => opt.Ignore());
        CreateMap<UserChangePasswordDto, User>()
            .ForMember(dest => dest.Tests, opt => opt.Ignore());

        /*-----------------------------------Test-----------------------------------*/
        CreateMap<TestDto, Test>()
            .ForMember(dest => dest.User, opt => opt.Ignore());
        CreateMap<Test, TestDto>();
        CreateMap<CreateTestDto, Test>()
            .ForMember(dest => dest.User, opt => opt.Ignore());
        CreateMap<UpdateTestDto, Test>()
            .ForMember(dest => dest.User, opt => opt.Ignore());

        /*-----------------------------------Question-----------------------------------*/
        CreateMap<QuestionDto, Question>();
        CreateMap<Question, QuestionDto>();
        CreateMap<CreateQuestionDto, Question>();
        CreateMap<UpdateQuestionDto, Question>();

        /*-----------------------------------Option-----------------------------------*/
        CreateMap<OptionDto, Option>();
        CreateMap<Option, OptionDto>();
        CreateMap<CreateOptionDto, Option>();
        CreateMap<UpdateOptionDto, Option>();

        /*-----------------------------------Topic-----------------------------------*/
        CreateMap<TopicDto, Topic>();
        CreateMap<Topic, TopicDto>();
        CreateMap<CreateTopicDto, Topic>();
        CreateMap<UpdateTopicDto, Topic>();
    }
}
