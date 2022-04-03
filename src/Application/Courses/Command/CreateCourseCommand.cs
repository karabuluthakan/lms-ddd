#pragma warning disable CS8618
namespace Application.Courses.Command;

/// <summary>
/// 
/// </summary>
public class CreateCourseCommandRequest : IRequest<IResponse>
{
    /// <summary>
    /// 
    /// </summary>
    public UpsertCourseDto Dto { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    public CreateCourseCommandRequest(UpsertCourseDto dto)
    {
        Dto = Guard.Against.Null(dto);
    }

    public CreateCourseCommandRequest()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommandRequest, IResponse>
    {
        private readonly ICourseRepository _repository;
        private readonly IMapperAdapter _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public CreateCourseCommandHandler(
            ICourseRepository repository,
            IMapperAdapter mapper)
        {
            _repository = Guard.Against.Null(repository);
            _mapper = Guard.Against.Null(mapper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResponse> Handle(CreateCourseCommandRequest request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var data = Course.Create(dto.CoursePrefix, dto.Name);
            var addedData = await _repository.AddAsync(data, cancellationToken);
            var mappedData = _mapper.Map<DetailCourseDto>(addedData);
            return DataResponse.Get(mappedData);
        }
    }
}