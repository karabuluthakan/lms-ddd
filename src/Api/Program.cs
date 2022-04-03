var builder = WebApplication.CreateBuilder(args);

var programAssembly = typeof(Program).Assembly;
var domainAssembly = AppDomain.CurrentDomain.Load("Domain");
var infrastructureAssembly = AppDomain.CurrentDomain.Load("Infrastructure");
var applicationAssembly = AppDomain.CurrentDomain.Load("Application");

var assemblies = new[] { programAssembly, domainAssembly, infrastructureAssembly, applicationAssembly };

builder.DependencyResolvers(assemblies);
builder.Services.AddMediatR(assemblies);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    })
    .AddFluentValidation(opt =>
    {
        opt.RegisterValidatorsFromAssemblyContaining<Program>();
        opt.ImplicitlyValidateChildProperties = true;
    })
    .AddMvcOptions(opt =>
    {
        opt.ModelMetadataDetailsProviders.Clear();
        opt.ModelValidatorProviders.Clear();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();