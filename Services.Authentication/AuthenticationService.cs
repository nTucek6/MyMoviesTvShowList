using DatabaseContext;
using Entites;
using Entites.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyMoviesTvShowList.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtConfiguration jwtConfig;
        private readonly MyMoviesTvShowListContext database;

        public AuthenticationService(MyMoviesTvShowListContext _myMoviesListContext, IOptionsMonitor<JwtConfiguration> jwtConfiguration)
        {
            database = _myMoviesListContext;
            jwtConfig = jwtConfiguration.CurrentValue;
        }


        public async Task<string> Register(RegisterDTO user)
        {
            if ((await database.Users.Where(u => u.Email == user.Email).SingleOrDefaultAsync()) != null)
                throw new Exception("Email taken!");

            if ((await database.Users.Where(u => u.Username == user.Username).SingleOrDefaultAsync()) != null)
                throw new Exception("Username taken!");

            string passwordHash = GenerateHash(user.Password);


            await database.Users.AddAsync(
            new UsersEntity
            {
                Email = user.Email,
                Username = user.Username,
                PasswordHash = passwordHash,
                RoleId = UserRoleEnum.User,
                Joined = DateTime.Now.ToUniversalTime(),
            }); 

            await database.SaveChangesAsync();

            string token = await Login(
                new UserDTO
                {
                    Email = user.Email,
                    Password = user.Password
                });

            return token;
        }

        public async Task<string> Login(UserDTO user)
        {
            var userDb = await database.Users.Where(u => u.Email == user.Email).SingleOrDefaultAsync();

            if (userDb is null)
                throw new Exception("User not found");

            string passwordHash = GenerateHash(user.Password);

            if (userDb.PasswordHash != passwordHash)
                throw new Exception("Invalid password");

            var token = GenerateToken(new GenerateToken 
            { 
                Id = userDb.Id, 
                Username = userDb.Username, 
                Email = userDb.Email });
            return token;
        }

        public async Task<string> AdminLogin(AdminUserDTO user)
        {
            var userDb = await database.AdminUsers.Where(u => u.Username == user.Username).SingleOrDefaultAsync();

            if (userDb is null)
                throw new Exception("Invalid Login");

            string passwordHash = GenerateHash(user.Password);

            if (userDb.PasswordHash != passwordHash)
                throw new Exception("Invalid Login");

            var token = GenerateToken(new GenerateToken
            {
                Id = userDb.Id,
                Username = userDb.Username,
                Email = userDb.Email
            });
            return token;
        }



        public string GenerateToken(GenerateToken user)
        {
            // generate token that is valid for 15 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("Username", user.Username),
                    //new Claim("Role", user.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(jwtConfig.AccessTokenExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public string GenerateHash(string value)
        {
            string valueHash = "";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                StringBuilder builder = new();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                valueHash = builder.ToString();
            }

            return valueHash;
        }


    }
}