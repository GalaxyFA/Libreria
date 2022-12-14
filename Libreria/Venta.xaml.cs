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
        private IRepository<Ventum> VenRepository;
        private IRepository<DetalleVentum> DetVenReposity;

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
            VenRepository = new Repository<Ventum>();
            DetVenReposity = new Repository<DetalleVentum>();
            MostrarLista();
            ObtenerIDEmpleado();
           
            //Console.WriteLine(client.Count());

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
                Limpiar();
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
                Limpiar();

            }
        }

        

        private void btnRealizar_Compra_Click(object sender, RoutedEventArgs e)
        {
            int indiceVenta = 0;
            if(dg_Carrito_Compras.ItemsSource!=null)

            if(cbTipo_Cliente.SelectedIndex==-1 || cbTipo_Cliente.SelectedIndex == 0)
            {   //Registar cliente
              if (txt_Primer_Nombre.Text != String.Empty && txt_Email.Text!= String.Empty && 
                        txt_Telefono.Text!= String.Empty && txt_Primer_Apellido.Text != String.Empty)
             {
                        Cliente cli = new Cliente();
                        cli.Email = txt_Email.Text;
                        cli.Telefono = txt_Telefono.Text;
                        cli.Estado = "Activo";
                        RegistrarCliente(cli);//Contiene las lineas de CliRepository add y savechange

                        ClienteNatural cliNat = new ClienteNatural();
                        cliNat.PrimerNombre = txt_Primer_Nombre.Text;
                        cliNat.SegundoNombre = txt_Segundo_Nombre.Text;
                        cliNat.PrimerApellido = txt_Primer_Apellido.Text;
                        cliNat.SegundoApellido = txt_Segundo_Apellido.Text;
                        indiceVenta = ObtenerIDCliente();
                        cliNat.IdCliente = indiceVenta;
                        //cliNat.IdClienteNavigation = cli;

                        Ventum v = RegistrarVenta(indiceVenta);

                        CliNatRepository.Add(cliNat);
                        CliNatRepository.Savechange();
                        RegistrarDetalleVenta();
                        ActualizarTablaProducto();
                        Limpiar();
                        ActualizarItem();
                        ActualizarMonto();
                        MostrarLista();
                        MessageBox.Show("Compra Realizada", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
             }
              else
              {
                        MessageBox.Show("No puede dejar vacios", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
              }
            }
            else if (cbTipo_Cliente.SelectedIndex == 1)
            {
              if (txtNombre_cliente.Text != String.Empty && txt_Email.Text != String.Empty &&
               txt_Telefono.Text != String.Empty && txtApellido_representante.Text != String.Empty
               && txtNombre_representante.Text != String.Empty && dp_Fecha_Consti.SelectedDate != null)
              {
                        Cliente cli = new Cliente();
                        cli.Email = txt_Email.Text;
                        cli.Telefono = txt_Telefono.Text;
                        cli.Estado = "Activo";
                        RegistrarCliente(cli);
                        indiceVenta = ObtenerIDCliente();
                        ClienteJuridico cliJur = new ClienteJuridico();
                        cliJur.NombreCliente = txtNombre_cliente.Text;
                        cliJur.NombreRepresentante = txtNombre_representante.Text;
                        cliJur.ApellidoRepresentante = txtApellido_representante.Text;
                        cliJur.FechaConstitucion = (DateTime)dp_Fecha_Consti.SelectedDate;
                        cliJur.IdCliente = indiceVenta;

                        Ventum v = RegistrarVenta(indiceVenta);

                        CliJurRepository.Add(cliJur);
                        CliJurRepository.Savechange();
                        RegistrarDetalleVenta();
                        ActualizarTablaProducto();
                        Limpiar();
                        ActualizarItem();
                        ActualizarMonto();
                        MostrarLista();
                        MessageBox.Show("Compra Realizada", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
              }
            else
             {
                        MessageBox.Show("No puede dejar vacios", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            }
        }

        private int ObtenerIDCliente()
        {
            int i = 0;
            List<Cliente> client = CliRepository.GetAll();
            List<string> res = (from d in client
                                orderby d.IdCliente descending
                       select (""+d.IdCliente )).Take(1).ToList();

            foreach (var la in res)
            {
                i = Convert.ToInt32(la);
                Console.WriteLine(la);

            }
            
            return i;
        }

        private void btnAddCarrito_Click(object sender, RoutedEventArgs e)
        {
            if(dg_Inventario.SelectedIndex!= -1)
            {
                try
                {
                    int can = 0;
                    var Selected = (Producto)dg_Inventario.SelectedItem;
                    can = Convert.ToInt32(txtCantidad.Text);
                    AddCarrito(Selected.IdProducto, Selected.NombreProducto, Selected.Precio, can, Selected.Upc);
                    txtCantidad.Clear();
                    ActualizarItem();
                    ActualizarMonto();
                }
                catch (FormatException fe){
                    MessageBox.Show(fe.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
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
                
               // can = carrito.AddOrUpadate(ProdRepository.GetAll(), Selected, can);
               
            }
        }

        private void AddCarrito(int idProducto,string nombre, decimal precio, int can, string upc)
        {
            if(carrito == null)
            {
                Producto pro = new();
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
                    can = ValidarCantidad(can);
                    Cambios(idProducto, can);
                }
                else
                {
                    can = ValidarCantidad(can);
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

        private int ValidarCantidad(int can)
        {
            var selected =(Producto) dg_Inventario.SelectedItem;
            if (selected.Cantidad <= can)
            {
                can = selected.Cantidad;
                MessageBox.Show("Cantidad excede el inventario", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            return can;
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

             decimal costo =0;
            total = 0;
            foreach(var item in carrito)
            {
                costo = item.Cantidad * item.Precio;
                total += costo;
            }

            text_Monto.Text = $"Monto total: C$ {total}";
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
        
        private void ObtenerIDEmpleado()
        {
            int id = 0;
            _liberiaContext = new();
            var res = from empleado in _liberiaContext.Empleados
                      where
                       empleado.Titulo.Contains("Vendedor")
                      orderby
                       empleado.Titulo descending
                      select empleado;
            var emple = EmpRepository.GetByQuery(res);

            foreach(var a in emple)
            {
                txt_IdEmpleado.Text = a.IdEmpleado.ToString();
            }
            
        }
        private Ventum RegistrarVenta(int id )
        {
            Ventum nuevaVenta = new();
            nuevaVenta.FechaVenta= DateTime.Now;
            nuevaVenta.TotalVenta = total;

            if (radEfectivo.IsChecked == true)
            {
                nuevaVenta.Pago = radEfectivo.Content.ToString();
            }
            else
            {
                nuevaVenta.Pago = radTarjeta.Content.ToString();
            }
            nuevaVenta.IdCliente= id;
            nuevaVenta.IdEmpleado = Convert.ToInt32(txt_IdEmpleado.Text);
            //nuevaVenta.IdClienteNavigation = cliente;

            VenRepository.Add(nuevaVenta);
            VenRepository.Savechange();
            return nuevaVenta;
        }
        private void RegistrarDetalleVenta()
        {
            foreach(Producto p in carrito)
            {
              DetalleVentum detalleVentum = new DetalleVentum();
              detalleVentum.IdVenta = ObtenerIdVenta();
              detalleVentum.IdProducto = p.IdProducto;
              detalleVentum.Cantidad= p.Cantidad;
              DetVenReposity.Add(detalleVentum);
            }
            DetVenReposity.Savechange();
        }
        private void ActualizarTablaProducto()
        {
            List<Producto> lista = new List<Producto>();
            lista = ProdRepository.GetAll();
            foreach (Producto producto in carrito)
            {
                ActualizarProducto(producto.IdProducto, producto.Cantidad,lista);
            }
        }

        private void ActualizarProducto(int idProducto, int cantidad, List<Producto> lista)
        {
            
            foreach(Producto producto in lista)
            {
                if(producto.IdProducto == idProducto)
                {
                    Console.WriteLine(producto.Cantidad + " " + cantidad);
                    producto.Cantidad= producto.Cantidad-cantidad;
                    
                    ProdRepository.Edit(producto);
                }   
            }
            ProdRepository.Savechange();
        }

        private void Limpiar()
        {
            txtApellido_representante.Clear();
            txtNombre_cliente.Clear();
            txtNombre_representante.Clear();
            txt_Email.Clear();
            txt_Primer_Apellido.Clear();
            txt_Segundo_Apellido.Clear();
            txt_Primer_Nombre.Clear();
            txt_Segundo_Nombre.Clear();
            txt_Telefono.Clear();
            dp_Fecha_Consti.SelectedDate = null;
            carrito.Clear();
            total = 0;
            
        }

        private int ObtenerIdVenta()
        {
            int i = 0;
            List<Ventum> vent = VenRepository.GetAll(); 
            List<string> res = (from d in vent
                                orderby d.IdCliente descending
                                select ("" + d.IdVenta)).Take(1).ToList();

            foreach (var la in res)
            {
                i = Convert.ToInt32(la);
                Console.WriteLine(la);

            }

            return i;

        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txt_Telefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            { string tel = txt_Telefono.Text;
                e.Handled = false;
                if (txt_Telefono.Text.Trim().Length >= 8)
                {
                   
                    MessageBox.Show("El Teléfono debe tener 8 dígitos");
                    //txt_Telefono.Text = tel;
                    
                }
            }
            else
                e.Handled = true;
               
            
            
        }
    }
}
