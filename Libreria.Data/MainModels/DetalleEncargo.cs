using System;
using System.Collections.Generic;

namespace Libreria.Data.MainModels
{
    public partial class DetalleEncargo
    {
        public int? IdEncago { get; set; }
        public int? IdProducto { get; set; }
        public int Cantidad { get; set; }

        public virtual Encargo? IdEncagoNavigation { get; set; }
        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
