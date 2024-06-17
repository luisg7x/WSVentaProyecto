using Microsoft.AspNetCore.Mvc;
using WSVenta.Models.Request;
using WSVenta.Models.Responses;
using WSVenta.Services;

namespace WSVenta.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    //why interface and not the class?; https://youtu.be/4nA4OFgxRtM?t=1553
    private IUserService _userServices;
    //injecting per class:
    public UserController(IUserService userService)
    {
        _userServices = userService;    
    }

    [HttpPost("login")] // = localhost:5421/api/login
    public IActionResult Autenticar([FromHeader] AuthRequest model)
    {
        Respuesta respuesta = new Respuesta();

        var userResponse = _userServices.Auth(model);

        if (userResponse == null)
        {
            respuesta.Mensaje = "Usuario o contrasena incorrecta";
            return BadRequest(respuesta);
        }
        respuesta.Exito = 1;
        respuesta.Data = userResponse;
        return Ok(respuesta);
    }
}
