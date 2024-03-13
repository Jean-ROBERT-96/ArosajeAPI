using ArosajeAPI.Controllers;
using DataContext;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace UnitTest.ArosajeAPI
{
    public class JsonWebTokenTest
    {
        [Fact]
        public async void Use_ValidId_ReturnToken()
        {
            // Arrange
            var user = new Utilisateur { Mail = "test@outlook.fr", Password = "3DEF9A622CA7D92EF61B0E679FE0351FEC33CE45D42FF488A64C0CF19EBC1DC1" };
            var configMock = new Mock<IConfiguration>();
            configMock.SetupGet(c => c["Jwt:Key"]).Returns("58xZwYRdO4jXDMi0yL2ANaJ1LPb2iKl0");
            configMock.SetupGet(c => c["Jwt:Issuer"]).Returns("ArosajeAPI");
            configMock.SetupGet(c => c["Jwt:Audience"]).Returns("ArosajeAPI");

            var contextMock = new Mock<IJwtConnection>();
            contextMock.Setup(c => c.Login(It.IsAny<Utilisateur>())).ReturnsAsync(user);

            var controller = new JwtLoginController(configMock.Object, contextMock.Object);

            // Act
            var result = await controller.Login(user) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            var token = result.Value.GetType().GetProperty("token").GetValue(result.Value, null) as string;
            Assert.NotNull(token);
            Assert.True(!string.IsNullOrEmpty(token));
        }

        [Fact]
        public async void Use_InvalidId_ReturnError401()
        {
            // Arrange
            var user = new Utilisateur { Mail = "testing", Password = "maman42" };
            var configMock = new Mock<IConfiguration>();
            var contextMock = new Mock<IJwtConnection>();
            contextMock.Setup(c => c.Login(It.IsAny<Utilisateur>())).ReturnsAsync((Utilisateur)null); // Simuler le retour d'un utilisateur invalide

            var controller = new JwtLoginController(configMock.Object, contextMock.Object);

            // Act
            var result = await controller.Login(user);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Login_WithExpiredToken_ReturnsNewToken()
        {
            // Arrange
            var user = new Utilisateur { /* initialiser les propriétés de l'utilisateur */ };

            var jwtConfig = new Mock<IConfiguration>();
            jwtConfig.SetupGet(x => x["Jwt:Key"]).Returns("58xZwYRdO4jXDMi0yL2ANaJ1LPb2iKl0");
            jwtConfig.SetupGet(x => x["Jwt:Issuer"]).Returns("ArosajeAPI");
            jwtConfig.SetupGet(x => x["Jwt:Audience"]).Returns("ArosajeAPI");

            var expiredToken = GenerateExpiredToken(jwtConfig.Object);

            var contextMock = new Mock<IJwtConnection>();
            contextMock.Setup(c => c.Login(It.IsAny<Utilisateur>())).ReturnsAsync(user); // Simuler le retour d'un utilisateur valide

            var controller = new JwtLoginController(jwtConfig.Object, contextMock.Object);

            // Act
            var result = await controller.Login(user) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);

            var token = result.Value.GetType().GetProperty("token").GetValue(result.Value, null) as string;
            Assert.NotNull(token);

            // Vérifie si le nouveau token est valide
            Assert.True(ValidateToken(token, jwtConfig.Object));
        }

        private bool ValidateToken(string token, IConfiguration config)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = config["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero // Optionnel : permet de régler la marge d'erreur pour l'expiration du token
            };

            try
            {
                tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Méthode pour générer un token expiré
        private string GenerateExpiredToken(IConfiguration config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(-1), // Expired token
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
