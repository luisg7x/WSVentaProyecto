using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WSVenta.Models;
using WSVenta.Models.Request;
using WSVenta.Models.Responses;
using WSVenta.Services;

namespace WSVenta.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class VentaController : ControllerBase
{
    private IVentaService _ventaService;

    public VentaController(IVentaService ventaService)
    {
        this._ventaService = ventaService;
    }

    [HttpPost]
    public IActionResult Add(VentaRequest oModel)
    {
        Respuesta oRespuesta = new Respuesta();
        try
        {
            _ventaService.Add(oModel);
            oRespuesta.Exito = 1;
           
        }
        catch (Exception ex)
        {
            oRespuesta.Mensaje = ex.Message;
        }

        return Ok(oRespuesta);

    }
}
