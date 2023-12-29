using Microsoft.IdentityModel.Tokens;
using Movies.Business.users;
using Movies.Interface;
using Movies.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Movies.Security
{
    public class JWTConfig : JWTGenerator
    {
        private readonly JWTSetting _jwtsetting;
        private readonly IUserRepository _userRepository;

        public JWTConfig(JWTSetting jwtsetting, IUserRepository userRepository)
        {
            _jwtsetting = jwtsetting;
            _userRepository = userRepository;
        }

        public JWTConfig(IUserRepository userRepository)
        {
            _jwtsetting = new JWTSetting();
            _userRepository = userRepository;
        }

        public string? GenerateToken(UserDTO userDTO)
        {
            var user = _userRepository.GetUser(userDTO.UserName);
            if (user == null)
            {
                return "Error! Unauthorized.";
            }
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(_jwtsetting.SecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("UserName", user.Username),
                        new Claim("Role", user.Role)
                    }
                ),
                Expires = DateTime.Now.AddMinutes((double) _jwtsetting.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            string finaltoken = tokenhandler.WriteToken(token);

            return finaltoken;
        }
    }
}
