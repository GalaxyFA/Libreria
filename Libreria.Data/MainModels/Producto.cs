using System;
using System.Collections.Generic;

namespace Libreria.Data.MainModels
{
    public partial class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Upc { get; set; } = null!;
    }
}
