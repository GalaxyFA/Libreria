using System;
using System.Collections.Generic;

namespace Libreria.Data.MainModels
{
    public partial class Encargo
    {
        public Encargo()
        {
            DetalleEncargos = new HashSet<DetalleEncargo>();
        }

        public int IdEncargo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Abono { get; set; }
        public decimal MontoTotal { get; set; }
        public string Pago { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public int? IdCliente { get; set; }
        public int? IdEmpleado { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual Empleado? IdEmpleadoNavigation { get; set; }
        public virtual ICollection<DetalleEncargo> DetalleEncargos { get; set; }
    }
}
