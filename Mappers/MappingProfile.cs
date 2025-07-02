using AutoMapper;
namespace CollegeManagementAPI.Data;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Student, StudentDTO>().ReverseMap();
    }
}