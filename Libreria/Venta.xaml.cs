using Libreria.Controladores;
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
        //ControlProducto carrito= new();
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
            if(cbTipo_Cliente.SelectedIndex==-1 || cbTipo_Cliente.SelectedIndex == 0)
            {
                Cliente cli = new Cliente();
                cli.Email = txt_Email.Text;
                cli.Telefono = txt_Telefono.Text;
                cli.Estado = "Activo";
                RegistrarCliente(cli);
                ClienteNatural cliNat = new ClienteNatural();
                cliNat.PrimerNombre = txt_Primer_Nombre.Text;
                cliNat.SegundoNombre= txt_Segundo_Nombre.Text;
                cliNat.PrimerApellido=txt_Primer_Apellido.Text;
                cliNat.SegundoApellido= txt_Segundo_Apellido.Text; 
                //cliNat.IdCliente
            }
            else if (cbTipo_Cliente.SelectedIndex == 1)
            {
                Cliente cli = new Cliente();
                cli.Email = txt_Email.Text;
                cli.Telefono = txt_Telefono.Text;
                cli.Estado = "Activo";
                RegistrarCliente(cli);
            }
        }

        private void btnAddCarrito_Click(object sender, RoutedEventArgs e)
        {
            if(dg_Inventario.SelectedIndex!= -1)
            {
                int can = 0;
                var Selected = (Producto)dg_Inventario.SelectedItem;
                //var Cantidad =  Convert.ToInt32(txtCantidad.Text);

                //if (carrito == null)
                //{
                //Esto agrega de la tabla inventario a 
                //CarriProd.Cantidad = Convert.ToInt32(txtCantidad.Text);
                //carrito.Add(CarriProd);
                //}
                //else
                //{
                //AddOrUpadate(carrito, CarriProd);
                //}
                can = Convert.ToInt32(txtCantidad.Text);
               // can = carrito.AddOrUpadate(ProdRepository.GetAll(), Selected, can);
                AddCarrito(Selected.IdProducto, Selected.NombreProducto,Selected.Precio, can, Selected.Upc);
                txtCantidad.Clear();
                   
                ActualizarItem();
                ActualizarMonto();
               
            }
        }

        private void AddCarrito(int idProducto,string nombre, decimal precio, int can, string upc)
        {
            if(carrito == null)
            {
                Producto pro = new Producto();
                pro.IdProducto = idProducto;
                pro.NombreProducto = nombre;
                pro.Cantidad = can;
                pro.Precio = precio; 
                pro.Upc=upc;
                carrito.Add(pro);
                
            }
            else
            {
                if (Existe(idProducto) == true)
                {
                    Cambios(idProducto, can);
                }
                else
                {
                    Producto pro = new Producto();
                    pro.IdProducto = idProducto;
                    pro.NombreProducto = nombre;
                    pro.Cantidad = can;
                    pro.Precio = precio;
                    pro.Upc = upc;
                    carrito.Add(pro);
                }
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
            _liberiaContext = new LibreriaContext();
            var search = from producto in _liberiaContext.Productos
                         where
                         producto.NombreProducto.Contains(txtBuscar_producto.Text)
                         orderby
                         producto.NombreProducto descending
                         select producto;
            dg_Inventario.ItemsSource = ProdRepository.GetByQuery(search);
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            MostrarLista();
        }
        private void ActualizarItem() {
            dg_Carrito_Compras.ItemsSource = null;
            dg_Carrito_Compras.ItemsSource = carrito;

        }
        private void ActualizarMonto()
        {
            decimal total = 0, costo =0;
            foreach(var item in carrito)
            {
                costo = item.Cantidad * item.Precio;
                total += costo;
            }

            text_Monto.Text = "Monto total: " + $"{total}";
        }
        private bool Existe(int id)
        {
            foreach(var item in carrito)
            {
                if (item.IdProducto == id)
                {
                    return true;
                }
            }
            return false;
        }
        private void Cambios(int id, int canti)
        {
            foreach(var item in carrito){
                if(item.IdProducto == id)
                {
                    item.Cantidad = canti;
                }
            }
        }
        private void RegistrarCliente(Cliente cliente)
        {
            CliRepository.Add(cliente);
            CliRepository.Savechange();
        }
        
        
    }
}
