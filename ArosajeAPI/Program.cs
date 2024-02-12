using DataContext;
using DataContext.Repository;
using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ArosajeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            builder.Services.AddDbContext<DBContext>(options =>
            {
                string connect = builder.Configuration["ConnectionStrings:DBConnection"];
                options.UseMySql(connect, ServerVersion.AutoDetect(connect));
            });
            builder.Services.AddControllers();
            builder.Services.AddTransient<IJwtConnection, JwtLoginRepository>();
            builder.Services.AddTransient<IRepository<Annonce>, AnnonceRepository>();
            builder.Services.AddTransient<IRepository<Conversation>, ConversationRepository>();
            builder.Services.AddTransient<IRepository<Entretien>, EntretienRepository>();
            builder.Services.AddTransient<IRepository<Message>, MessageRepository>();
            builder.Services.AddTransient<IRepository<Suivi>, SuiviRepository>();
            builder.Services.AddTransient<IRepository<Utilisateur>, UtilisateurRepository>();
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

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
