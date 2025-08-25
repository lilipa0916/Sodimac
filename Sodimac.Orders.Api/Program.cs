using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sodimac.Orders.Application.Interfaces;
using Sodimac.Orders.Application.Mappings;
using Sodimac.Orders.Infrastructure.Persistence;
using Sodimac.Orders.Infrastructure.Repository;
using System.Reflection;

namespace Sodimac.Orders.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Database Context
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("Sodimac.Orders.Infrastructure");
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                });
            });


            // Repositories
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // MediatR
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly));

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // FluentValidation
            builder.Services.AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly));

            // Swagger/OpenAPI

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sodimac Orders API",
                    Version = "v1",
                    Description = "API para gestión de pedidos Sodimac",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipo Desarrollo Sodimac",
                        Email = "dev@sodimac.com"
                    }
                });
            });

           

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAngular");
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
