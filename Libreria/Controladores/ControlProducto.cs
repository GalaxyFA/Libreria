using Libreria.Data.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Controladores
{
    internal class ControlProducto
    {
        List<Producto> productos = new();

        public void Agregar(Producto p)
        {
            productos.Add(p);
        }
        public void Eliminar( int id)
        {
            foreach (var item in productos)
            {
                if (item.IdProducto == id) ;
                break;
            }
        }
        public List<Producto> Cargar()
        {
            return productos;
        }
    }
}
