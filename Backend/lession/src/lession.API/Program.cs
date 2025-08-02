using lession.API.Middleware;
using lession.Application.Mappings;
using lession.Application.Service.Implementation;
using lession.Application.Service.Interfaces;
using lession.Infrastructure.Data;
using lession.Infrastructure.Repositories.Implementation;
using lession.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

// Add FluentValidation
//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddFluentValidationClientsideAdapters();
//builder.Services.AddValidatorsFromAssemblyContaining<CreateKhachHangValidator>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));


// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add UnitOfWork and Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IKhachHangRepository, KhachHangRepository>();
builder.Services.AddScoped<ISanPhamRepository, SanPhamRepository>();
builder.Services.AddScoped<IDonHangRepository, DonHangRepository>();
builder.Services.AddScoped<IChiTietDonHangRepository, ChiTietDonHangRepository>();

// Add Services
builder.Services.AddScoped<ISanPhamService, SanPhamService>();
builder.Services.AddScoped<IKhachHangService, KhachHangService>();
builder.Services.AddScoped<IDonHangService, DonHangService>();

// Add Static JSON Generator Service
builder.Services.AddScoped<IStaticJsonGeneratorService, StaticJsonGeneratorService>();


// Configure Swagger/OpenAPI
// This is used to generate API documentation and provide a UI for testing the API endpoints.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Store Management API",
        Version = "v1",
        Description = "API for managing orders, customers, and products in a lesson application."
    });
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store Management API V1");
        //c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

// Initialize JSON files on startup
using (var scope = app.Services.CreateScope())
{
    var jsonGenerator = scope.ServiceProvider.GetRequiredService<IStaticJsonGeneratorService>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Generating initial static JSON files...");
        await jsonGenerator.GenerateAllJsonFilesAsync();
        logger.LogInformation("Initial static JSON files generated successfully");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error generating initial JSON files");
        // Don't fail startup if JSON generation fails
    }
}


// Cho phép truy cập file tĩnh
// wwwroot mặc định
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Set CORS headers for JSON files
        if (ctx.File.Name.EndsWith(".json"))
        {
            ctx.Context.Response.Headers?.Add("Access-Control-Allow-Origin", "*");
        }
    }
});



app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
