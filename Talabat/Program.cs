
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.IRepositories;
using Talabat.Errors;
using Talabat.Extensions;
using Talabat.Helper;
using Talabat.Middlewares;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat
{
    public class Program
    {
        public static async Task  Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSwaggerServicesExtension();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(S => {


                var connection = builder.Configuration.GetConnectionString("CSRedis");

                return ConnectionMultiplexer.Connect(connection);

            });

            builder.Services.AddApplicationServices();

                var app = builder.Build();

            using var scop = app.Services.CreateScope();

            var services = scop.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var dbContext = services.GetRequiredService<StoreContext>();

              await   dbContext.Database.MigrateAsync();

             await StoreContextSeed.SeedAsync(dbContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex, ex.Message);

            }

            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }
            app.UseStatusCodePagesWithReExecute("Error/{0}");
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();
    

            app.Run();
        }
    }
}
