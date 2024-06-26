﻿using DataContext;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArosajeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtLoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IJwtConnection _context;

        public JwtLoginController(IConfiguration config, IJwtConnection context)
        {
            _config = config;
            _context = context;
        }

        private string GenerateToken(Utilisateur user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();

            if (user.EstBotaniste)
                claims.Add(new(ClaimTypes.Role, "botaniste"));
            if (user.EstModerateur)
                claims.Add(new(ClaimTypes.Role, "moderateur"));

            var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    expires: DateTime.Now.AddMinutes(15),
                    claims: claims,
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous, HttpPost("Login")]
        public async Task<ActionResult> Login(Utilisateur user)
        {
            ActionResult response = Unauthorized();
            var okUser = await _context.Login(user);
            if(okUser != null)
            {
                var token = GenerateToken(okUser);
                response = Ok(new { token = token });
            }

            return response;
        }

        [AllowAnonymous, HttpPost("Register")]
        public async Task<ActionResult> Register(Utilisateur user)
        {
            await _context.Register(user);
            var token = GenerateToken(user);
            return Ok(new { token = token });
        }
    }
}
