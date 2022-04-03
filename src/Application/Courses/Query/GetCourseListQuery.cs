namespace Application.Courses.Query;

public class GetCourseListQueryRequest : IRequest<IResponse>
{
    public PaginationQuery Query { get; }

    public GetCourseListQueryRequest(PaginationQuery query)
    {
        Query = Guard.Against.Null(query);
    }

    public class GetCourseListQueryHandler : IRequestHandler<GetCourseListQueryRequest, IResponse>
    {
        private readonly ICourseRepository _repository;
        private readonly IMapperAdapter _mapper;
        private readonly IPaginationUriProvider _uriProvider;

        public GetCourseListQueryHandler(
            ICourseRepository repository,
            IMapperAdapter mapper,
            IPaginationUriProvider uriProvider)
        {
            _repository = Guard.Against.Null(repository);
            _mapper = Guard.Against.Null(mapper);
            _uriProvider = Guard.Against.Null(uriProvider);
        }

        public async Task<IResponse> Handle(GetCourseListQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetAllAsync(cancellationToken);
            var mappedData = _mapper.Map<IReadOnlyList<DetailCourseDto>>(data);
            return PaginationDataResponse<DetailCourseDto>.OK(mappedData, request.Query, data.Count, _uriProvider);
        }
    }
}