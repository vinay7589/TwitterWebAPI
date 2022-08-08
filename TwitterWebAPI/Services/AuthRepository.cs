using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TwitterWebAPI.Contracts;
using TwitterWebAPI.Data;
using TwitterWebAPI.Model;

namespace TwitterWebAPI.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext dataContext, IConfiguration configuration)
        {
            this._dataContext = dataContext;
            this._configuration = configuration;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password";
            }
            else
            {
                response.Data = CreateToken(user);
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            CreatePassswordHash(password, out byte[] paswordHash, out byte[] passwordSalt);
            if (await UserExists(user.Username))
            {
                response.Success = false;
                response.Message = "User already exists";
                return response;
            }
            user.PasswordHash = paswordHash;
            user.PasswordSalt = passwordSalt;

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            response.Data = user.Id;
            response.Message = "User registered successfully";
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _dataContext.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
                return true;
            return false;
        }

        private void CreatePassswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Appsettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

