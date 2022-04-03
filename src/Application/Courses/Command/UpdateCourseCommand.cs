#pragma warning disable CS8618
namespace Application.Courses.Command;

/// <summary>
/// 
/// </summary>
public class UpdateCourseCommandRequest : IRequest<IResponse>
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// 
    /// </summary>
    public UpsertCourseDto Dto { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    public UpdateCourseCommandRequest(Guid id, UpsertCourseDto dto)
    {
        Id = Guard.Against.NullOrEmpty(id);
        Dto = Guard.Against.Null(dto);
    }

    public UpdateCourseCommandRequest()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommandRequest, IResponse>
    {
        private readonly ICourseRepository _repository;
        private readonly IMapperAdapter _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public UpdateCourseCommandHandler(
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
        public async Task<IResponse> Handle(UpdateCourseCommandRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (data is null)
            {
                return Response.NotFound($"{request.Id} is not found");
            }

            var mappedData = _mapper.Map(request.Dto, data);
            await _repository.UpdateAsync(mappedData, cancellationToken);
            return Response.NoContent();
        }
    }
}