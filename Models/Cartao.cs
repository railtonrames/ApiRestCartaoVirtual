using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCartaoVirtual.Models
{
    public class Cartao
    {
        public int Id { get; set; }
        public String Numero { get; set; }
        public Email Email { get; set; }
        public int EmailId { get; set; }
    }
}
