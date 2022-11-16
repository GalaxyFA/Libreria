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
        public Encargos()
        {
            InitializeComponent();
        }

        private void btnCancelar_encargo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSacar_producto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAnadir_producto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRealizar_Encargo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
