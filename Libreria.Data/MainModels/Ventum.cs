using System;
using System.Collections.Generic;

namespace Libreria.Data.MainModels
{
    public partial class Ventum
    {
        public Ventum()
        {
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal TotalVenta { get; set; }
        public string Pago { get; set; } = null!;
        public int? IdCliente { get; set; }
        public int? IdEmpleado { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual Empleado? IdEmpleadoNavigation { get; set; }
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
