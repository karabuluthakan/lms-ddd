namespace Application.Courses.Query;

public class GetCourseDetailQueryRequest : IRequest<IResponse>
{
    public Guid Id { get; }

    public GetCourseDetailQueryRequest(Guid id)
    {
        Id = Guard.Against.NullOrEmpty(id);
    }

    public class GetCourseDetailQueryHandler : IRequestHandler<GetCourseDetailQueryRequest, IResponse>
    {
        private readonly ICourseRepository _repository;
        private readonly IMapperAdapter _mapper;

        public GetCourseDetailQueryHandler(
            ICourseRepository repository,
            IMapperAdapter mapper)
        {
            _repository = Guard.Against.Null(repository);
            _mapper = Guard.Against.Null(mapper);
        }

        public async Task<IResponse> Handle(GetCourseDetailQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (data is null)
            {
                return Response.NotFound($"{request.Id} is not found");
            }

            var mappedData = _mapper.Map<DetailCourseDto>(data);
            return DataResponse.Get(mappedData);
        }
    }
}