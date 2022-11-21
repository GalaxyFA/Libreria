using Libreria.Data;
using Libreria.Data.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Libreria
{
    /// <summary>
    /// Lógica de interacción para Gestion_Producto.xaml
    /// </summary>
    public partial class Gestion_Producto : UserControl
    {
        private LibreriaContext _liberiaContext;
        private IRepository<Producto> _repository;
        public Gestion_Producto()
        {
            InitializeComponent();
                _repository = new Repository<Producto>();
            MostrarLista();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            _liberiaContext = new LibreriaContext();
            var buscar = from producto in _liberiaContext.Productos
                      where
                      producto.NombreProducto.Contains(txtBuscar_nombre.Text)
                      orderby
                      producto.NombreProducto descending
                      select producto;
            dgProductos.ItemsSource = _repository.GetByQuery(buscar);
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            MostrarLista();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Producto NuevoProducto = new();
            NuevoProducto.NombreProducto = txtNombre_producto.Text;
            NuevoProducto.Cantidad = Convert.ToInt32(txtCantidad.Text);
            NuevoProducto.Precio = Convert.ToDecimal(txtPrecio.Text);
            NuevoProducto.Upc= AutocompletarUPC(txtCodigo.Text);
            _repository.Add(NuevoProducto);
            _repository.Savechange();

            Limpiar();
            MostrarLista();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Producto editarProducto = new() {
                IdProducto = Convert.ToInt32(txtId_producto.Text),
                NombreProducto= txtNombre_producto.Text,
                Cantidad= Convert.ToInt32(txtCantidad.Text),
                Precio= Convert.ToDecimal(txtPrecio.Text),
                Upc= txtCodigo.Text
            };
            _repository.Edit(editarProducto);
            _repository.Savechange();

            Limpiar();
            btnEditar.IsEnabled = false;
            MostrarLista();  
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedIndex != -1)
            {
                var eliminarProducto = (Producto)dgProductos.SelectedItem;
                _repository.Del(eliminarProducto.IdProducto);
                _repository.Savechange();
                Limpiar();
                MostrarLista();
            }
        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            if(dgProductos.SelectedIndex != -1)
            {
                var cargarProducto = (Producto)dgProductos.SelectedItem;
                txtId_producto.Text = $"{cargarProducto.IdProducto}";
                txtNombre_producto.Text = cargarProducto.NombreProducto;
                txtCantidad.Text = $"{cargarProducto.Cantidad}";
                txtPrecio.Text = $"{cargarProducto.Precio}";
                txtCodigo.Text = cargarProducto.Upc;
                btnEditar.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("No hay Datos", "Aviso");
            }

        }
        private void MostrarLista()
        {
            var prod = _repository.GetAll();
           dgProductos.ItemsSource= _repository.GetAll();
        }
        private void Limpiar()
        {
            txtBuscar_nombre.Clear();
            txtCantidad.Clear();
            txtCodigo.Clear();
            txtId_producto.Clear();
            txtNombre_producto.Clear();
            txtPrecio.Clear();
        }
        private string AutocompletarUPC( string e)
        {
            if(e.Count() <= 14)
            {
                int dif= 14- e.Count();
                for(int i=0; i < dif; i++)
                {
                    e = "0" + e;
                }
            }
            return e;
        }
    }
}
