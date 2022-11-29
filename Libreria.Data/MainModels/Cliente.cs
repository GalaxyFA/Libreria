using System;
using System.Collections.Generic;

namespace Libreria.Data.MainModels
{
    public partial class Cliente
    {
        public Cliente()
        {
            ClienteJuridicos = new HashSet<ClienteJuridico>();
            ClienteNaturals = new HashSet<ClienteNatural>();
            Encargos = new HashSet<Encargo>();
            Venta = new HashSet<Ventum>();
        }

        public int IdCliente { get; set; }
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Estado { get; set; } = null!;

        public virtual ICollection<ClienteJuridico> ClienteJuridicos { get; set; }
        public virtual ICollection<ClienteNatural> ClienteNaturals { get; set; }
        public virtual ICollection<Encargo> Encargos { get; set; }
        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
