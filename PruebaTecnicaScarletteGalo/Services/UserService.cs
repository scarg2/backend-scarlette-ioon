using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PruebaTecnicaScarletteGalo.Data;
using PruebaTecnicaScarletteGalo.Models;


namespace PruebaTecnicaScarletteGalo.Services
{
    public class UserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public UserService(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        public User Register(string username, string password, string role)
        {
            if (_appDbContext.Users.Any(u => u.Username == username))
            {
                throw new InvalidOperationException("User is already taken");
            }

            var activateState = _appDbContext.States.FirstOrDefault(s => s.StateName == "Active");

            if (activateState == null)
            {
                throw new InvalidOperationException("Activate state is not configured in the database");
            }

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Role = role,
                StateId = activateState.StateId
            };

            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();

            return user;
        }
        public (User user, string token) Authenticate(string username, string password)
        {
            var user = _appDbContext.Users.Include(u => u.State)
                .FirstOrDefault(u => u.Username == username);

            if ((user == null || user.State.StateName != "Active" ) && !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials or inactive user");
            }

            var token = GenerateJwtToken(user);
            return (user, token);

        }

        public User GetUserByID (Guid userId)
        {
            return _appDbContext.Users.Include(u => u.State).FirstOrDefault(u => u.UserId == userId);
        }

        public IQueryable<User> GetUsers() 
        {
            return _appDbContext.Users.Include(u => u.State);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
