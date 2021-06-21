using System;
using System.Collections.Generic;

#nullable disable

namespace FirstAPI.Models.DB
{
    public partial class UserW
    {
        public UserW()
        {
            OrdenCompras = new HashSet<OrdenCompra>();
        }

        public int IdUser { get; set; }
        public string Username { get; set; }
        public string UserPass { get; set; }
        public string Email { get; set; }
        public string TypeUser { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<OrdenCompra> OrdenCompras { get; set; }
    }
}
