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

        CreateMap<Test, TestDto>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => src.Topics))
            .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question));

        CreateMap<CreateTestDto, Test>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Topics, opt => opt.Ignore());

        CreateMap<TestDto, CreateTestDto>();

        CreateMap<UpdateTestDto, Test>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Topics, opt => opt.Ignore());

        /*-----------------------------------Question-----------------------------------*/
        CreateMap<Question, QuestionDto>();
        CreateMap<QuestionDto, Question>();
        CreateMap<CreateQuestionDto, Domain.Entities.Tests.Question>();
        CreateMap<UpdateQuestionDto, Domain.Entities.Tests.Question>();

        /*-----------------------------------Option-----------------------------------*/
        CreateMap<OptionDto, Option>();
        CreateMap<Option, OptionDto>();
        CreateMap<CreateOptionDto, Option>();
        CreateMap<UpdateOptionDto, Option>();

        /*-----------------------------------Topic-----------------------------------*/
        CreateMap<Topic, TopicDto>();

        CreateMap<TopicDto, Topic>()
            .ForMember(dest => dest.Tests, opt => opt.Ignore());

        CreateMap<CreateTopicDto, Domain.Entities.Tests.Topic>()
            .ForMember(dest => dest.Tests, opt => opt.Ignore());

        CreateMap<UpdateTopicDto, Domain.Entities.Tests.Topic>()
            .ForMember(dest => dest.Tests, opt => opt.Ignore()); ;
    }
}
