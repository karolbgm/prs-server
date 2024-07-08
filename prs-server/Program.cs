using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using prs_server.Data;
namespace prs_server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<PrsDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PrsDbContext") ?? throw new InvalidOperationException("Connection string 'PrsDbContext' not found.")));

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddCors();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
