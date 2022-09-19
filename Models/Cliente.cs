using System;
using System.Collections.Generic;

namespace apiSorteio.Models
{
    public  class Cliente
    {
        public int ClienteID { get; }
        public string? ClienteNome { get; set; }
        public string? ClienteCPF { get; set; }
        public string? ClienteEmail { get; set; }
        public string? ClienteTelefone { get; set; }
        public int ClienteNumSorteado { get; set; }
       
       

    }
}
