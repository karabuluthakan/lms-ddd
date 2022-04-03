namespace Application.Students.Query;

public class GetStudentDetailQueryRequest : IRequest<IResponse>
{
    public Guid Id { get; }

    public GetStudentDetailQueryRequest(Guid id)
    {
        Id = Guard.Against.NullOrEmpty(id);
    }

    public class GetStudentDetailQueryHandler : IRequestHandler<GetStudentDetailQueryRequest, IResponse>
    {
        private readonly IStudentRepository _repository;
        private readonly IMapperAdapter _mapper;

        public GetStudentDetailQueryHandler(
            IStudentRepository repository,
            IMapperAdapter mapper)
        {
            _repository = Guard.Against.Null(repository);
            _mapper = Guard.Against.Null(mapper);
        }

        public async Task<IResponse> Handle(GetStudentDetailQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (data is null)
            {
                return Response.NotFound($"{request.Id} is not found");
            }

            var mappedData = _mapper.Map<DetailStudentDto>(data);
            return DataResponse.Get(mappedData);
        }
    }
}