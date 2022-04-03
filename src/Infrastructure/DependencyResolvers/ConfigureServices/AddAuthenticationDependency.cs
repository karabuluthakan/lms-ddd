namespace Infrastructure.DependencyResolvers.ConfigureServices;

public class AddAuthenticationDependency : IConfigureServiceModule
{
    public void Load(IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var tokenSettings = configuration.GetValue<TokenSettings>(nameof(TokenSettings));
        var systemUser = tokenSettings.SystemUser;
        var defaultUser = tokenSettings.DefaultUser;
        var securityKey = Encoding.UTF8.GetBytes(tokenSettings.Security);
        services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = tokenSettings.DefaultScheme;
                opt.DefaultChallengeScheme = tokenSettings.DefaultScheme;
            })
            .AddJwtBearer(systemUser.Scheme, jwtOptions =>
            {
                jwtOptions.RequireHttpsMetadata = false;
                jwtOptions.SaveToken = true;
                jwtOptions.MetadataAddress = systemUser.MetadataAddress;
                jwtOptions.Authority = systemUser.Authority;
                jwtOptions.Audience = systemUser.Audience;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                    ValidAudiences = defaultUser.ValidAudiences,
                    ValidIssuers = defaultUser.ValidIssuers,
                    ClockSkew = TimeSpan.Zero
                };
            })
            .AddJwtBearer(defaultUser.Scheme, jwtOptions =>
            {
                jwtOptions.RequireHttpsMetadata = false;
                jwtOptions.SaveToken = true;
                jwtOptions.MetadataAddress = defaultUser.MetadataAddress;
                jwtOptions.Authority = defaultUser.Authority;
                jwtOptions.Audience = defaultUser.Audience;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                    ValidAudiences = defaultUser.ValidAudiences,
                    ValidIssuers = defaultUser.ValidIssuers,
                    ClockSkew = TimeSpan.Zero
                };
            })
            .AddPolicyScheme(tokenSettings.DefaultScheme, tokenSettings.DefaultScheme, options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    string authorization = context.Request.Headers[HeaderNames.Authorization];
                    if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith(tokenSettings.TokenPrefix))
                    {
                        var token = authorization[tokenSettings.TokenPrefix.Length..].Trim();
                        var jwtHandler = new JwtSecurityTokenHandler();

                        return jwtHandler.CanReadToken(token) &&
                               jwtHandler.ReadJwtToken(token).Issuer.Equals(systemUser.Authority)
                            ? systemUser.Scheme
                            : defaultUser.Scheme;
                    }

                    return defaultUser.Scheme;
                };
            });
        services.AddAuthorization(opt =>
        {
            opt.AddPolicy(systemUser.Scheme, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AuthenticationSchemes.Add(systemUser.Scheme);
            });
            opt.AddPolicy(defaultUser.Scheme, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AuthenticationSchemes.Add(systemUser.Scheme);
                policy.AuthenticationSchemes.Add(defaultUser.Scheme);
            });
        });
    }
}