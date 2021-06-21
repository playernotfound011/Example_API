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
    public class ProductController : ControllerBase
    {
        [HttpGet]

        public ActionResult Get()
        {
            try
            {
                using (Models.DB.WEBSITEContext db = new Models.DB.WEBSITEContext())
                {
                    var query = (from a in db.Products
                                 select new
                                 {
                                     a.CodigoProd,
                                     a.TipoProd,
                                     a.PrecioUnit
                                 }).ToList();

                    return Ok(query);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }
    }
}
