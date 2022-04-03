namespace Application.Authentication;

public class GenerateAccessTokenRequest : IRequest<IResponse>
{
    public AccessTokenRequestDto Dto { get; }

    public GenerateAccessTokenRequest(AccessTokenRequestDto dto)
    {
        Dto = Guard.Against.Null(dto);
    }

    public class GenerateAccessTokenQueryHandler : IRequestHandler<GenerateAccessTokenRequest, IResponse>
    {
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICacheProvider _cache;

        public GenerateAccessTokenQueryHandler(
            ISystemUserRepository systemUserRepository,
            IStudentRepository studentRepository,
            ICacheProvider cache)
        {
            _systemUserRepository = Guard.Against.Null(systemUserRepository);
            _studentRepository = Guard.Against.Null(studentRepository);
            _cache = Guard.Against.Null(cache);
        }

        public async Task<IResponse> Handle(GenerateAccessTokenRequest request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var key = dto.Key.Trim().ToLowerInvariant();

            string GetCacheKey()
            {
                return $"RefreshToken-{key}";
            }

            switch (dto.Type)
            {
                case GrantType.DefaultUser:
                {
                    var data = (await _studentRepository.GetAsync(x =>
                        x.Email.Equals(key), cancellationToken)).SingleOrDefault();

                    if (data is null)
                    {
                        return Response.NotFound();
                    }


                    break;
                }
                case GrantType.SystemUser:
                {
                    var data = (await _systemUserRepository.GetAsync(x =>
                        x.Email.Equals(key), cancellationToken)).SingleOrDefault();

                    if (data is null)
                    {
                        return Response.NotFound();
                    }

                    break;
                }
                case GrantType.RefreshToken:
                {
                    var data = await _cache.GetAsync<string>(GetCacheKey());
                    if (string.IsNullOrEmpty(data))
                    {
                        return Response.NotFound();
                    }

                    if (!string.Equals(data, dto.Secret))
                    {
                        return Response.BadRequest();
                    }

                    break;
                }
                default:
                    throw new IndexOutOfRangeException();
            }

            throw new NotImplementedException();
        }
    }
}