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
    /// Lógica de interacción para Encargos.xaml
    /// </summary>
    public partial class Encargos : UserControl
    {
        private LibreriaContext _liberiaContext;
        private IRepository<Producto> ProdRepository;
        private IRepository<Empleado> EmpRepository;
        private IRepository<Cliente> CliRepository;
        private IRepository<ClienteJuridico> CliJurRepository;
        private IRepository<ClienteNatural> CliNatRepository;
        private IRepository<Encargo> EncarRepository;
        private IRepository<DetalleEncargo> DetEncarRepository;
        List<Producto> carrito = new();
        decimal total = 0;

        public Encargos()
        {
            InitializeComponent();
            ProdRepository = new Repository<Producto>();
            EmpRepository = new Repository<Empleado>();
            CliRepository = new Repository<Cliente>();
            CliJurRepository = new Repository<ClienteJuridico>();
            CliNatRepository = new Repository<ClienteNatural>();
            EncarRepository = new Repository<Encargo>();
            DetEncarRepository = new Repository<DetalleEncargo>();
            MostrarLista();
            ObtenerIDEmpleado();
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

            foreach (var a in emple)
            {
                txt_IdEmpleado.Text = a.IdEmpleado.ToString();
            }
        }

        private void MostrarLista()
        {
            dg_Inventario.ItemsSource = ProdRepository.GetAll();
        }

        private void btnCancelar_encargo_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnSacar_producto_Click(object sender, RoutedEventArgs e)
        {
            if (dg_Carrito_Compra.SelectedIndex != -1)
            {
                Producto SacarProd = (Producto)dg_Carrito_Compra.SelectedItem;
                foreach (var ite in carrito)
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

        private void AddCarrito(int idProducto, string nombreProducto, decimal precio, int can, string upc)
        {
            if (carrito == null)
            {
                Producto pro = new();
                pro.IdProducto = idProducto;
                pro.NombreProducto = nombreProducto;
                pro.Cantidad = can;
                pro.Precio = precio;
                pro.Upc = upc;
                carrito.Add(pro);
            }
            else
            {
                if (Exite(idProducto) == true)
                {

                    can = ValidarCantidad(can);
                    Cambios(idProducto, can);
                }
                else
                {
                    can = ValidarCantidad(can);
                    Producto pro = new Producto();
                    pro.IdProducto = idProducto;
                    pro.NombreProducto = nombreProducto;
                    pro.Cantidad = can;
                    pro.Precio = precio;
                    pro.Upc = upc;
                    carrito.Add(pro);
                }
            }
        }

        private int ValidarCantidad(int can)
        {

            var selected = (Producto)dg_Inventario.SelectedItem;
            if (selected.Cantidad <= can)
            {
                can = selected.Cantidad;
                MessageBox.Show("Cantidad excede el inventario", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            return can;
        }

        private void Cambios(int idProducto, int can)
        {
            foreach (var item in carrito)
            {
                if (item.IdProducto == idProducto)
                {
                    item.Cantidad = can;
                }
            }
            }

        private bool Exite(int idProducto)
        {
            foreach (var item in carrito)
            {
                if (item.IdProducto == idProducto)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnAnadir_producto_Click(object sender, RoutedEventArgs e)
        {
            if (dg_Inventario.SelectedIndex != -1)
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
                catch (FormatException fe)
                {
                    MessageBox.Show(fe.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        private void btnRealizar_Encargo_Click(object sender, RoutedEventArgs e)
        {
            int indiceEncargo=0;
            if(dg_Carrito_Compra.ItemsSource != null)
                if(cbTipo_Cliente.SelectedIndex==-1 || cbTipo_Cliente.SelectedIndex == 0)
                {
                    if(txt_Primer_Nombre.Text != String.Empty && txt_Email.Text != String.Empty &&
                        txt_Telefono.Text != String.Empty && txt_Primer_Apellido.Text != String.Empty && txtAbono.Text !=String.Empty)
                    {
                        Cliente cli = new Cliente();
                        cli.Email = txt_Email.Text;
                        cli.Telefono = txt_Telefono.Text;
                        cli.Estado = "Activo";
                        RegistrarCliente(cli);
                        ClienteNatural cliNat = new ClienteNatural();
                        cliNat.PrimerNombre = txt_Primer_Nombre.Text;
                        cliNat.SegundoNombre = txt_Segundo_Nombre.Text;
                        cliNat.PrimerApellido = txt_Primer_Apellido.Text;
                        cliNat.SegundoApellido = txt_Segundo_Apellido.Text;
                        indiceEncargo = ObtenerIDCliente();
                        cliNat.IdCliente = indiceEncargo;

                        Encargo en = RegistrarEncargo(indiceEncargo);

                        CliNatRepository.Add(cliNat);
                        CliNatRepository.Savechange();
                        RegistrarDetalleEncargo();
                        Limpiar();
                        ActualizarItem();
                        ActualizarMonto();

                        MessageBox.Show("Encargo Registrado", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("No puede dejar vacios", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }else if (cbTipo_Cliente.SelectedIndex == 1)
                {
                    if(txtNombre_cliente.Text != String.Empty && txt_Email.Text != String.Empty && txtAbono.Text !=String.Empty &&
                         txt_Telefono.Text != String.Empty && txtApellido_representante.Text != String.Empty
                        && txtNombre_representante.Text != String.Empty && dp_Fecha_Consti.SelectedDate != null)
                    {
                        Cliente cli = new Cliente();
                        cli.Email = txt_Email.Text;
                        cli.Telefono = txt_Telefono.Text;
                        cli.Estado = "Activo";
                        RegistrarCliente(cli);
                        indiceEncargo = ObtenerIDCliente();
                        ClienteJuridico cliJur = new ClienteJuridico();
                        cliJur.NombreCliente = txtNombre_cliente.Text;
                        cliJur.NombreRepresentante = txtNombre_representante.Text;
                        cliJur.ApellidoRepresentante = txtApellido_representante.Text;
                        cliJur.FechaConstitucion = (DateTime)dp_Fecha_Consti.SelectedDate;
                        cliJur.IdCliente = indiceEncargo;

                        CliJurRepository.Add(cliJur);
                        CliJurRepository.Savechange();
                        RegistrarDetalleEncargo();
                        Limpiar();
                        ActualizarItem();
                        ActualizarMonto();

                        MessageBox.Show("Encargo Registrado", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else
                    {
                        MessageBox.Show("No puede dejar vacios", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }


                }
        }

        private void ActualizarMonto()
        {
            decimal costo = 0;
            total = 0;
            foreach (var item in carrito)
            {
                costo = item.Cantidad * item.Precio;
                total += costo;
            }

            text_Monto_.Text = $"Monto total: C$ {total}";
        }

        private void ActualizarItem()
        {
            dg_Carrito_Compra.ItemsSource = null;
            dg_Carrito_Compra.ItemsSource = carrito;
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

        private void RegistrarDetalleEncargo()
        {
          foreach(Producto p in carrito)
            {
                DetalleEncargo detalleEncargo = new DetalleEncargo();
                detalleEncargo.IdEncago = ObtenerIdEncargo();
                detalleEncargo.Cantidad = p.Cantidad;
                detalleEncargo.IdProducto = p.IdProducto;
                DetEncarRepository.Add(detalleEncargo);
            }
            DetEncarRepository.Savechange();
        }

        private int? ObtenerIdEncargo()
        {
            int i = 0;
            List<Encargo> enc = EncarRepository.GetAll();
            List <string> res= (from d in enc 
                                orderby d.IdEncargo descending
                                select(""+d.IdEncargo)).Take(1).ToList();
            foreach(var la in res)
            {
                i = Convert.ToInt32(la);
            }
            return i;
        }

        private Encargo RegistrarEncargo(int indiceEncargo)
        {
            Encargo nuevoEncargo = new();
            nuevoEncargo.Fecha = DateTime.Now;
            nuevoEncargo.MontoTotal = total;
            nuevoEncargo.Estado = "Activo";
            nuevoEncargo.Abono= Convert.ToDecimal(txtAbono.Text);
            if (radEfectivo.IsChecked == true)
            {
                nuevoEncargo.Pago = radEfectivo.Content.ToString();
            }
            else
            {
                nuevoEncargo.Pago = radTarjeta.Content.ToString();
            }
            nuevoEncargo.IdCliente = indiceEncargo;
            nuevoEncargo.IdEmpleado = Convert.ToInt32(txt_IdEmpleado.Text);
            EncarRepository.Add(nuevoEncargo);
            EncarRepository.Savechange();
            return nuevoEncargo;
        }

        private int ObtenerIDCliente()
        {
            int i = 0;
            List<Cliente> client = CliRepository.GetAll();
            List<string> res = (from d in client
                                orderby d.IdCliente descending
                                select ("" + d.IdCliente)).Take(1).ToList();

            foreach (var la in res)
            {
                i = Convert.ToInt32(la);
                Console.WriteLine(la);

            }

            return i;
        }

        private void RegistrarCliente(Cliente cli)
        {
            CliRepository.Add(cli);
            CliRepository.Savechange();
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

        private void cbTipo_Cliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTipo_Cliente.SelectedIndex == 0)
            {
                txtNombre_cliente.Visibility = Visibility.Collapsed;
                txtNombre_representante.Visibility = Visibility.Collapsed;
                txtApellido_representante.Visibility = Visibility.Collapsed;
                dp_Fecha_Consti.Visibility = Visibility.Collapsed;

                txt_Primer_Apellido.Visibility = Visibility.Visible;
                txt_Primer_Nombre.Visibility = Visibility.Visible;
                txt_Segundo_Apellido.Visibility = Visibility.Visible;
                txt_Segundo_Nombre.Visibility = Visibility.Visible;
            }
            else if (cbTipo_Cliente.SelectedIndex == 1)
            {
                txt_Primer_Apellido.Visibility = Visibility.Collapsed;
                txt_Primer_Nombre.Visibility = Visibility.Collapsed;
                txt_Segundo_Apellido.Visibility = Visibility.Collapsed;
                txt_Segundo_Nombre.Visibility = Visibility.Collapsed;

                txtApellido_representante.Visibility = Visibility.Visible;
                txtNombre_representante.Visibility = Visibility.Visible;
                txtNombre_cliente.Visibility = Visibility.Visible;
                dp_Fecha_Consti.Visibility = Visibility.Visible;

            }
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtAbono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txt_Telefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                string tel = txt_Telefono.Text;
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
