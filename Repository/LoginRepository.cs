using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HomeManagement.Model;
using HomeManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HomeManagement.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly HomeManagementDbContext _context;
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        public LoginRepository(HomeManagementDbContext context, IConfiguration configuration)
        {
            _context = context;
            _secretKey = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
        }
        /// <summary>
        /// Authenticates a user based on the provided login model.
        /// <param name="loginModel">The login model containing the username and password.</param>
        /// <returns>The authenticated user if the credentials are valid; otherwise, null.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the user is not found.</exception>

        public async Task<string> LoginAsync(LoginModel loginModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginModel.Username);
            if (user == null || !user.VerifyPassword(loginModel.Password))
            {
                throw new InvalidOperationException("Invalid email or password");
            }
            var token = GenerateToken(user);
            return token;
        }

        
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}