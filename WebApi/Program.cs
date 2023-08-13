using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var ConnectionString = builder.Configuration.GetSection("DBConnectionString").Value;
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));

            builder.Services.AddScoped<IOperationService, OperationService>();
            builder.Services.AddScoped<IOperationRepository, OperationRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}