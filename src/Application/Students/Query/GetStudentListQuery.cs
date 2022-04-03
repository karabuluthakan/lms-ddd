namespace Application.Students.Query;

public class GetStudentListQueryRequest : IRequest<IResponse>
{
    public PaginationQuery Query { get; }

    public GetStudentListQueryRequest(PaginationQuery query)
    {
        Query = Guard.Against.Null(query);
    }

    public class GetStudentListQueryHandler : IRequestHandler<GetStudentListQueryRequest, IResponse>
    {
        private readonly IStudentRepository _repository;
        private readonly IMapperAdapter _mapper;
        private readonly IPaginationUriProvider _uriProvider;

        public GetStudentListQueryHandler(
            IStudentRepository repository,
            IMapperAdapter mapper,
            IPaginationUriProvider uriProvider)
        {
            _repository = Guard.Against.Null(repository);
            _mapper = Guard.Against.Null(mapper);
            _uriProvider = Guard.Against.Null(uriProvider);
        }

        public async Task<IResponse> Handle(GetStudentListQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetAllAsync(cancellationToken);
            var mappedData = _mapper.Map<IReadOnlyList<DetailStudentDto>>(data);
            return PaginationDataResponse<DetailStudentDto>.OK(mappedData, request.Query, data.Count, _uriProvider);
        }
    }
}