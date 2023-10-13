using TestWebApi.Extensions;
using TestWebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<DataContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("TestDatabase")));

//From ApplicationServiceExtensions
builder.Services.AddPersistenceDependencies();
builder.Services.AddServiceDependencies();

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cors moved to AddApplicationServices with CloudinarySettings
builder.Services.AddApplicationServices(builder.Configuration);
//builder.Services.AddCors();

var app = builder.Build();

//HTTP response interceptor
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200")) ;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
