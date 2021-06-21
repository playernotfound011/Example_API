using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAPI.Models.Request
{
    public class UserRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string typeUser { get; set; }
    }
}
