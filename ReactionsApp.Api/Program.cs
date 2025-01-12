using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReactionsApp.Business;
using ReactionsApp.Business.Services;
using ReactionsApp.Data;
using ReactionsApp.Data.Repositories;
using System.Text;

namespace ReactionsApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
                };
            });

            builder.Services.AddDbContext<ReactionsAppDbContext>(options =>
                options.UseSqlServer("YourConnectionStringHere"));

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<StartingLightsGameResultService>();
            builder.Services.AddScoped<StartingLightsGameResultRepository>();
            builder.Services.AddScoped<RandomPointsGameResultService>();
            builder.Services.AddScoped<RandomPointsGameResultRepository>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
