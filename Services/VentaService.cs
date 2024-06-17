
using WSVenta.Models;
using WSVenta.Models.Request;

namespace WSVenta.Services;
public class VentaService : IVentaService
{
    public void Add(VentaRequest oModel)
    {
            using (var db = new VentaRealContext())
            {
                //transacciones: si algo sale mal hace rollback... para ello ene el proceso bloquea la tabla (no ejecuta ningun query mientras este en la transaccion)
                //si el add de la tabla venta es exitoso pero el de tabla concepto no, pues esto ayuda a evitar eso. y que sea si y si en ambas
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var venta = new Ventum();
                        venta.Fecha = DateTime.Now;
                        venta.IdCliente = oModel.IdCliente;
                        venta.Total = oModel.Conceptos.Sum(d => d.Cantidad * d.PrecioUnitario);

                        db.Venta.Add(venta);
                        db.SaveChanges();

                        foreach (var modelConcepto in oModel.Conceptos)
                        {
                            var concepto = new Models.Concepto();

                            concepto.Cantidad = modelConcepto.Cantidad;
                            concepto.IdProducto = modelConcepto.IdProducto;
                            concepto.PrecioUnitario = modelConcepto.PrecioUnitario;
                            concepto.Importe = modelConcepto.Importe;
                            concepto.IdVenta = venta.Id;

                            db.Conceptos.Add(concepto);
                            db.SaveChanges();
                        }

                        transaction.Commit();

                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw new Exception("Ocurrio un error en la insercion");
                    }
                }
            }
       
    }
}
