using Serilog;

namespace DockerDemo
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console()
                        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                        .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day,
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
                            .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("DbConnecton");

            builder.Services.AddSerilog();
            builder.Services.AddControllers();
            builder.Services.AddSingleton<ConnectionManager>(args => new ConnectionManager(connectionString));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
