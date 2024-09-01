using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VVCRUD_IT_CDN_API_NET8.Data;
using VVCRUD_IT_CDN_API_NET8.Middlewares;
using VVCRUD_IT_CDN_API_NET8.Services;
using VVCRUD_IT_CDN_API_NET8.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add Controllers
builder.Services.AddControllers(options =>
{
    //Creating Custom Cache Profiles
    options.CacheProfiles.Add("CacheForSeconds10", new CacheProfile()
    {
        Duration = 10,
        Location = ResponseCacheLocation.Any
    });
    options.CacheProfiles.Add("NoCache", new CacheProfile()
    {
        Location = ResponseCacheLocation.None,
        NoStore = true
    });
});
//Add Response Caching Service
builder.Services.AddResponseCaching();
//
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCORSPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://vvcrud-it-cdn-ui-ang16.onrender.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// Add Professional Servise
builder.Services.AddScoped<IProfessionalService, ProfessionalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Add middleware for global Exception handelling
    app.UseMiddleware<ExceptionHandlingMiddleware>();
}

// Add middleware for request logging
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseCors("AllowCORSPolicy");

app.UseResponseCaching();

app.MapControllers();

app.Run();

