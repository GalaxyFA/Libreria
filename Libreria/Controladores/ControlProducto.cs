using Libreria.Data.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public void RetirarTodo()
        {
            productos.Clear();
        }
        public int AddOrUpadate(List<Producto> pros, Producto p, int cantidad)
        {
            int i_aux;
            if(SiExiste(p.IdProducto)== true)
            {
                i_aux = Validar_Cantidad(pros, p.IdProducto, cantidad);
                Actualizar(p.IdProducto, i_aux);
            }
            else
            {
                i_aux=Validar_Cantidad(pros,p.IdProducto, cantidad);    
                p.Cantidad=i_aux; 
                Agregar(p);
            }
            return i_aux;
            
        }

        private void Actualizar(int idProducto, int i_aux)
        {
           foreach(var item in productos)
            {
                if(item.IdProducto == idProducto)
                {
                    item.Cantidad = i_aux;
                }
            }
        }

        private int Validar_Cantidad(List<Producto> pros, int IdProducto, int cantidad)
        {
            int cant = 0;
            foreach(var producto in pros)
            {
                if(producto.IdProducto == IdProducto)
                {
                    int a = producto.Cantidad;
                    if (a <= cantidad)
                    {
                        cant = a;
                        MessageBox.Show("Cantidad excede el inventario", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        cant = cantidad;
                    }
                }
                
            } 
            return cant;
        }

        private bool SiExiste(int IDProducto)
        {
            bool exi = false;
            foreach(var producto in productos)
            {
                if(producto.IdProducto == IDProducto)
                {
                    exi = true;
                }
            }
            return exi;
        }
    }
}
