using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAPI.Models.Request
{
    public class OrdenCompraRequest
    {
        public int Id_User { get; set; }
        public string Product { get; set; }
        public string Direccion { get; set; }
        public float Monto_Total { get; set; }
    }
}
