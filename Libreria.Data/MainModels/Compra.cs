using System;
using System.Collections.Generic;

namespace Libreria.Data.MainModels
{
    public partial class Compra
    {
        public Compra()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
        }

        public int IdCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public int? IdEmpleado { get; set; }

        public virtual Empleado? IdEmpleadoNavigation { get; set; }
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
    }
}
