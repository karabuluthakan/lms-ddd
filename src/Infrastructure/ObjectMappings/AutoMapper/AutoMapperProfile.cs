namespace Infrastructure.ObjectMappings.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UpsertCourseDto, Course>().ReverseMap();
    }
}