using System;
using System.Collections.Generic;

#nullable disable

namespace FirstAPI.Models.DB
{
    public partial class Product
    {
        public Product()
        {
            OrdenCompras = new HashSet<OrdenCompra>();
        }

        public string CodigoProd { get; set; }
        public string TipoProd { get; set; }
        public decimal PrecioUnit { get; set; }

        public virtual ICollection<OrdenCompra> OrdenCompras { get; set; }
    }
}
