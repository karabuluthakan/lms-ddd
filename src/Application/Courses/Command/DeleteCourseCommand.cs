namespace Application.Courses.Command;

/// <summary>
/// 
/// </summary>
public class DeleteCourseCommandRequest : IRequest<IResponse>
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    public DeleteCourseCommandRequest(Guid id)
    {
        Id = Guard.Against.NullOrEmpty(id);
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommandRequest, IResponse>
    {
        private readonly ICourseRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public DeleteCourseCommandHandler(ICourseRepository repository)
        {
            _repository = Guard.Against.Null(repository);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResponse> Handle(DeleteCourseCommandRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (data is null)
            {
                return Response.NotFound($"{request.Id} is not found");
            }

            await _repository.DeleteAsync(data, cancellationToken);
            return Response.NotFound();
        }
    }
}