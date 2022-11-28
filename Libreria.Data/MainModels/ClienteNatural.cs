using System;
using System.Collections.Generic;

namespace Libreria.Data.MainModels
{
    public partial class ClienteNatural
    {
        public string PrimerNombre { get; set; } = null!;
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; } = null!;
        public string? SegundoApellido { get; set; }
        public int? IdCliente { get; set; }
        public int ClienteNaturalId { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
    }
}
