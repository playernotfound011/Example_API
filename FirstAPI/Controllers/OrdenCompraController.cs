using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenCompraController : ControllerBase
    {
        [HttpGet("{num_order}")]

        public ActionResult Get(int num_order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Models.DB.WEBSITEContext db = new Models.DB.WEBSITEContext())
                    {
                        var query = (from a in db.OrdenCompras
                                     join at in db.UserWs
                                     on a.IdUser equals at.IdUser
                                     join ar in db.Products
                                     on a.Product equals ar.CodigoProd
                                     where a.NumPedido.Equals(num_order)
                                     select new
                                     {
                                         a.NumPedido,
                                         a.IdUser,
                                         UserName = at.Username,
                                         Email = at.Email,
                                         Type_User = at.TypeUser,
                                         a.Product,
                                         ar.TipoProd,
                                         ar.PrecioUnit,
                                         a.Direccion,
                                         a.FechaCompra,
                                         a.MontoTotal
                                     }).FirstOrDefault();

                        return Ok(query);
                    }
                }
                else
                {
                    return ValidationProblem();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }                   

        [HttpPost]

        public ActionResult Post([FromBody] Models.Request.OrdenCompraRequest model)
        {
            if (ModelState.IsValid)
            {
                using (Models.DB.WEBSITEContext db = new Models.DB.WEBSITEContext())
                {
                    Models.DB.OrdenCompra insOrden = new Models.DB.OrdenCompra();
                    insOrden.FechaCompra = DateTime.Now;
                    insOrden.IdUser = model.Id_User;
                    insOrden.Product = model.Product;
                    insOrden.Direccion = model.Direccion;
                    insOrden.MontoTotal = (decimal)model.Monto_Total;

                    db.OrdenCompras.Add(insOrden);
                    db.SaveChanges();

                    return Ok(new { Message = "Order Created! " +insOrden.NumPedido });
                }
            }
            else
            {
                return ValidationProblem();
            }
        }
    }
}
