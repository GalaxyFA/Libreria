using System;
using System.Collections.Generic;

namespace Libreria.Data.MainModels
{
    public partial class ClienteJuridico
    {
        public string NombreCliente { get; set; } = null!;
        public string NombreRepresentante { get; set; } = null!;
        public string ApellidoRepresentante { get; set; } = null!;
        public DateTime FechaConstitucion { get; set; }
        public int? IdCliente { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
    }
}
