using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Task_Manager_Api.Middleware;
using Task_Manager_Api.Repositories;
using Task_Manager_Api.Services;

namespace Task_Manager_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.
                AddDbContext<MainDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<ILogService, LogService>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMyCustomRequestResponseMiddleware();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}