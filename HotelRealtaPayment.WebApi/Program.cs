using HotelRealtaPayment.Services.Abstraction;
using HotelRealtaPayment.WebAPI.Extensions;
using Northwind.Services;

namespace HotelRealtaPayment.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ILoggerManager logger = new LoggerManager();

            // Add services to the container.
            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureDbContext(builder.Configuration);
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureLoggerService();

            builder.Services.AddControllers();
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

            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();

            // add custome
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}