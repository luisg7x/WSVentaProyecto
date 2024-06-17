
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WSVenta.Models;
using WSVenta.Models.Common;
using WSVenta.Models.Request;
using WSVenta.Models.Responses;
using WSVenta.Tools;

namespace WSVenta.Services;

//services folder = to make injection dependecies
public class UserService : IUserService
{
    private readonly AppSettings _appSettings;

    public UserService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }


    public UserResponse Auth(AuthRequest model)
    {
        UserResponse userResponse = new UserResponse();

        using (var db = new VentaRealContext())
        {
            string spawword = Encrypt.GetSHA256(model.Password);

            var usuario = db.Usuarios.Where(d => d.Email == model.Email && d.Password == spawword).FirstOrDefault();

            if (usuario == null) return null;

            userResponse.Email = usuario.Email;
            userResponse.Token = GetToken(usuario);
        }

        return userResponse;
    }

    private string GetToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secreto);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, usuario.Email.ToString())
                    }
                ),
            Expires = DateTime.UtcNow.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
