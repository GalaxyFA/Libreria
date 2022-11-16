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
using System.Windows.Shapes;

namespace Libreria
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        BrushConverter bc = new BrushConverter();
        public Principal()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnCerrar_sesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void btnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;

            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnRegistrar_producto_Click(object sender, RoutedEventArgs e)
        {
            Desmarcar();
            GridPadre.Children.Clear();
            SPRegiSProducto.Background = bc.ConvertFrom("#FF2D379B") as Brush;
            Gestion_Producto gestion_Producto = new Gestion_Producto();
            GridPadre.Children.Add(gestion_Producto);   
        }

        private void btnRegistro_Usuario_Click(object sender, RoutedEventArgs e)
        {
            Desmarcar();
            GridPadre.Children.Clear();
            SPregiUsuario.Background = bc.ConvertFrom("#FF2D379B") as Brush;
        }

        private void btnRegistro_Empleados_Click(object sender, RoutedEventArgs e)
        {
            Desmarcar();
            GridPadre.Children.Clear();
            SPEmpleado.Background = bc.ConvertFrom("#FF2D379B") as Brush;
            Gestion_Empleado gestion_Empleado= new Gestion_Empleado();
            GridPadre.Children.Add(gestion_Empleado);
        }

        private void btnRegistro_Clientes_Click(object sender, RoutedEventArgs e)
        {
            Desmarcar();
            GridPadre.Children.Clear();
            SPCRegisCli.Background = bc.ConvertFrom("#FF2D379B") as Brush;
            Gestion_Cliente gestion_Cliente = new Gestion_Cliente();
            GridPadre.Children.Add(gestion_Cliente);
        }

        private void btnRegistro_Encargo_Click(object sender, RoutedEventArgs e)
        {
            Desmarcar();
            GridPadre.Children.Clear();
            SPRegisEncargo.Background = bc.ConvertFrom("#FF2D379B") as Brush;
            Gestion_Encargo gestion_Encargo = new Gestion_Encargo();
            GridPadre.Children.Add(gestion_Encargo);

        }

        private void btnRegistro_Ventas_Click(object sender, RoutedEventArgs e)
        {
            Desmarcar();
            GridPadre.Children.Clear();
            SPRegisVenta.Background = bc.ConvertFrom("#FF2D379B") as Brush;
            Gestion_Venta gestion_Venta = new Gestion_Venta();
            GridPadre.Children.Add(gestion_Venta);
        }

        private void btnEncargar_Click(object sender, RoutedEventArgs e)
        {
            Desmarcar();
            GridPadre.Children.Clear();
            SPEncargar.Background = bc.ConvertFrom("#FF2D379B") as Brush;
            Encargos encargo = new Encargos();
            GridPadre.Children.Add(encargo);
        }

        private void btnVender_Click(object sender, RoutedEventArgs e)
        {
            Desmarcar();
            GridPadre.Children.Clear();
            SPVender.Background = bc.ConvertFrom("#FF2D379B") as Brush;
             Venta venta = new Venta();
            GridPadre.Children.Add(venta);
        }

        private void btnCasa_Click(object sender, RoutedEventArgs e)
        {
            Desmarcar();
            GridPadre.Children.Clear();  
        }

        private void Desmarcar()
        {
           
            SPregiUsuario.Background = Brushes.Transparent;
            SPEncargar.Background = Brushes.Transparent;
            SPRegiSProducto.Background = Brushes.Transparent;
            SPVender.Background = Brushes.Transparent;
            SPCRegisCli.Background = Brushes.Transparent;
            SPRegisVenta.Background = Brushes.Transparent;
            SPRegisEncargo.Background = Brushes.Transparent;
            SPRegisVenta.Background = Brushes.Transparent;
            SPEmpleado.Background = Brushes.Transparent;
        }
    }
}
