using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ArosajeAPI.Middlewares
{
    public class JwtExpiration
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtExpiration(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length).Trim();

                var secretKey = _configuration["Jwt:Key"];

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"]
                };

                try
                {
                    tokenHandler.ValidateToken(token, validationParameters, out _);
                }
                catch (SecurityTokenExpiredException)
                {
                    var expiredToken = tokenHandler.ReadJwtToken(token);
                    if (expiredToken.ValidTo <= DateTime.UtcNow.AddDays(-2))
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Le jeton d'accès doit être renouvellé.");
                        return;
                    }

                    this.ExtendTokenExpiration(token);
                    return;
                }
                catch (Exception)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Une erreur s'est produite lors de la validation du jeton d'accès.");
                    return;
                }
            }

            await _next(context);
        }

        public string ExtendTokenExpiration(string expiredToken)
        {
            var secretKey = _configuration["Jwt:Key"];
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var token = tokenHandler.ReadJwtToken(expiredToken);

                var newTokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = token.Issuer,
                    Audience = _configuration["Jwt:Audience"],
                    Expires = DateTime.Now.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                        SecurityAlgorithms.HmacSha256)
                };

                var newToken = tokenHandler.CreateToken(newTokenDescriptor);

                return tokenHandler.WriteToken(newToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la prolongation de la durée de vie du jeton : {ex.Message}");
                throw;
            }
        }
    }
}
