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
    /// Lógica de interacción para Venta.xaml
    /// </summary>
    public partial class Venta : UserControl
    {
        private LibreriaContext _liberiaContext;
        private IRepository<Producto> ProdRepository;
        private IRepository<Empleado> EmpRepository;
        private IRepository<Cliente> CliRepository;
        private IRepository<ClienteJuridico> CliJurRepository;
        private IRepository<ClienteNatural> CliNatRepository;
        List<Producto> carrito = new();
        decimal total  = 0;
        public Venta()
        {
            InitializeComponent();
            ProdRepository = new Repository<Producto>();
            EmpRepository = new Repository<Empleado>();
            CliRepository = new Repository<Cliente>();
            CliJurRepository = new Repository<ClienteJuridico>();
            CliNatRepository = new Repository<ClienteNatural>();
            MostrarLista();
        }

        private void MostrarLista()
        {
            dg_Inventario.ItemsSource = ProdRepository.GetAll();
        }


        private void cbTipo_Cliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTipo_Cliente.SelectedIndex == 0)
            {
                txtNombre_cliente.Visibility= Visibility.Collapsed;
                txtNombre_representante.Visibility = Visibility.Collapsed;
                txtApellido_representante.Visibility = Visibility.Collapsed;
                dp_Fecha_Consti.Visibility= Visibility.Collapsed;

                txt_Primer_Apellido.Visibility=Visibility.Visible;
                txt_Primer_Nombre.Visibility= Visibility.Visible;
                txt_Segundo_Apellido.Visibility= Visibility.Visible;
                txt_Segundo_Nombre.Visibility = Visibility.Visible;
            }
            else if (cbTipo_Cliente.SelectedIndex == 1)
            {
                txt_Primer_Apellido.Visibility= Visibility.Collapsed;
                txt_Primer_Nombre.Visibility=Visibility.Collapsed;
                txt_Segundo_Apellido.Visibility = Visibility.Collapsed;
                txt_Segundo_Nombre.Visibility = Visibility.Collapsed;

                txtApellido_representante.Visibility= Visibility.Visible;
                txtNombre_representante.Visibility=Visibility.Visible;
                txtNombre_cliente.Visibility = Visibility.Visible;
                dp_Fecha_Consti.Visibility = Visibility.Visible;

            }
        }

        

        private void btnRealizar_Compra_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddCarrito_Click(object sender, RoutedEventArgs e)
        {
            if(dg_Inventario.SelectedIndex!= -1)
            {
                var CarriProd = (Producto)dg_Inventario.SelectedItem;
                //var Cantidad =  Convert.ToInt32(txtCantidad.Text);

                //if (carrito == null)
                //{
                //Esto agrega de la tabla inventario a 
                CarriProd.Cantidad = Convert.ToInt32(txtCantidad.Text);
                carrito.Add(CarriProd);
                //}
                //else
                //{
                //AddOrUpadate(carrito, CarriProd);
                //}
                txtCantidad.Clear();
                   
                ActualizarItem();
                total= ActualizarMonto();
                text_Monto.Text = "Monto total: " + $"{total}";
            }
        }

        private void btnSacar_Compra_Click(object sender, RoutedEventArgs e)
        {
            if(dg_Carrito_Compras.SelectedIndex!= -1)
            {
                Producto SacarProd = (Producto)dg_Carrito_Compras.SelectedItem;
                foreach (var ite in carrito )
                {
                    if (ite.NombreProducto == SacarProd.NombreProducto)
                    {
                        carrito.Remove(ite);
                        break;
                    }
                }
                ActualizarMonto();
                ActualizarItem();
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ActualizarItem() {
            dg_Carrito_Compras.ItemsSource = null;
            dg_Carrito_Compras.ItemsSource = carrito;

        }
        private decimal ActualizarMonto()
        {
            decimal total = 0, costo =0;
            foreach(var item in carrito)
            {
                costo = item.Cantidad * item.Precio;
                total += costo;
            }
            
            return total;
        }
        
        private void AddOrUpadate(List<Producto> pros, Producto p)
        {
            
            foreach(Producto prod in pros)
            {
                if(prod.IdProducto == p.IdProducto)
                {
                    prod.Cantidad = Convert.ToInt32(txtCantidad.Text);
                    break;
                }
                else
                {
                    p.Cantidad = Convert.ToInt32(txtCantidad.Text);
                    pros.Add(p);
                    break;
                }
            } 
        }
    }
}
