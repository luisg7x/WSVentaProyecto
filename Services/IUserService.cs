
using WSVenta.Models.Request;
using WSVenta.Models.Responses;

namespace WSVenta.Services;
public interface IUserService
{
    UserResponse Auth(AuthRequest model);
}
