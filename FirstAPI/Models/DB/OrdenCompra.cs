using System;
using System.Collections.Generic;

#nullable disable

namespace FirstAPI.Models.DB
{
    public partial class OrdenCompra
    {
        public int NumPedido { get; set; }
        public DateTime FechaCompra { get; set; }
        public int IdUser { get; set; }
        public string Product { get; set; }
        public string Direccion { get; set; }
        public decimal MontoTotal { get; set; }

        public virtual UserW IdUserNavigation { get; set; }
        public virtual Product ProductNavigation { get; set; }
    }
}
