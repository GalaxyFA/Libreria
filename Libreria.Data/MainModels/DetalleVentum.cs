using System;
using System.Collections.Generic;

namespace Libreria.Data.MainModels
{
    public partial class DetalleVentum
    {
        public int? IdVenta { get; set; }
        public int? IdProducto { get; set; }
        public int Cantidad { get; set; }

        public virtual Producto? IdProductoNavigation { get; set; }
        public virtual Ventum? IdVentaNavigation { get; set; }
    }
}
