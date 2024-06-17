using Microsoft.AspNetCore.Mvc;
using WSVenta.Models;
using WSVenta.Models.Responses;
using WSVenta.Models.Request;
using Microsoft.AspNetCore.Authorization;

//this was created as api class type
namespace MVCInjectionDependecy.Controllers;
[Route("api/[controller]")]
[ApiController]
//beacuse we now have a token: you need a token for access to this controller
[Authorize]
public class ClienteController : ControllerBase
{

    [HttpGet]
    public IActionResult Get()
    {
        Respuesta oRespuesta = new Respuesta();
        //unnecery, wea creted a contructor on respuesta class to inicialize it
        //oRespuesta.Exito = 0;
        try
        {
            using (var db = new VentaRealContext())
            {
                var clients = db.Clientes.OrderByDescending(d => d.Id).ToList();
                oRespuesta.Exito = 1;
                //Ok = converting the list to json.........
                //return Ok(clients);
                oRespuesta.Data = clients;
            }
        }
        catch (Exception ex)
        {
            oRespuesta.Mensaje = ex.Message;
        }

        return Ok(oRespuesta);

    }
    //viewmodel or request folder = una clase que sirve para formularios....
    //https://www.michalbialecki.com/2020/09/28/read-request-headers-as-an-object-in-asp-net-core-api/ for multiple header or for use the ClienteRequestClass... and not string Nombre
    /* [HttpPost]
     public IActionResult Add([FromHeader] string Nombre)
     {
         Respuesta oRespuesta = new Respuesta();
         try
         {
             using (var db = new VentaRealContext())
             {
                 var client = new Cliente();
                 client.Nombre = Nombre;

                 db.Clientes.Add(client);
                 db.SaveChanges();

                 oRespuesta.Exito = 1;
                 oRespuesta.Data = client;
             }
         }
         catch (Exception ex)
         {
             oRespuesta.Mensaje = ex.Message;
         }

         return Ok(oRespuesta);

     }*/

    //Both commented need recieve a json body to work
    [HttpPost]
    public IActionResult Add(ClienteRequest oModel)
    {
        Respuesta oRespuesta = new Respuesta();
        try
        {
            using (var db = new VentaRealContext())
            {
                var client = new Cliente();
                client.Nombre = oModel.Nombre;

                db.Clientes.Add(client);
                db.SaveChanges();

                oRespuesta.Exito = 1;
                oRespuesta.Data = client;
            }
        }
        catch (Exception ex)
        {
            oRespuesta.Mensaje = ex.Message;
        }

        return Ok(oRespuesta);

    }

    /*[HttpPut]
    public IActionResult Edit(ClienteRequest oModel)
    {
        Respuesta oRespuesta = new Respuesta();
        try
        {
            using (var db = new VentaRealContext())
            {
                var client = db.Clientes.Find(oModel.Id);
                client.Nombre = oModel.Nombre;

                db.Clientes.Update(client);
                db.SaveChanges();
   
                oRespuesta.Exito = 1;
                oRespuesta.Data = client;
            }
        }
        catch (Exception ex)
        {
            oRespuesta.Mensaje = ex.Message;
        }

        return Ok(oRespuesta);

    }*/

    [HttpPut]
    public IActionResult Edit([FromQuery] ClienteRequest oModel)
    {
        Respuesta oRespuesta = new Respuesta();
        try
        {
            using (var db = new VentaRealContext())
            {
                var client = db.Clientes.Find(oModel.Id);
                client.Nombre = oModel.Nombre;

                db.Clientes.Update(client);
                db.SaveChanges();

                oRespuesta.Exito = 1;
                oRespuesta.Data = client;
            }
        }
        catch (Exception ex)
        {
            oRespuesta.Mensaje = ex.Message;
        }

        return Ok(oRespuesta);

    }

    //reciving parameter on the url: https://localhost:5001/api/Client/2  the 2 is the id
    //[HttpDelete("{Id}")]

    //[fromquery] = https://localhost:5001/api/Cliente?Id=2
    [HttpDelete]
    public IActionResult Delete([FromQuery] int Id)
    {
        Respuesta oRespuesta = new Respuesta();
        try
        {
            using (var db = new VentaRealContext())
            {
                var client = db.Clientes.Find(Id);

                db.Clientes.Remove(client);
                db.SaveChanges();

                oRespuesta.Exito = 1;
            }
        }
        catch (Exception ex)
        {
            oRespuesta.Mensaje = ex.Message;
        }

        return Ok(oRespuesta);

    }
}
