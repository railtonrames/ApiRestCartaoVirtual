using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCartaoVirtual.Models
{
    public class Email
    {
        public int Id { get; set; }
        public String Endereco { get; set; }
        public List<Cartao> Cartoes { get; set; }
    }
}
