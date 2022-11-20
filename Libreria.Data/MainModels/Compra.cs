using System;
using System.Collections.Generic;

namespace Libreria.Data.MainModels
{
    public partial class Compra
    {
        public int IdCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public int? IdEmpleado { get; set; }

        public virtual Empleado? IdEmpleadoNavigation { get; set; }
    }
}
