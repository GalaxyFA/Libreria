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
    /// Lógica de interacción para Gestion_Venta.xaml
    /// </summary>
    public partial class Gestion_Venta : UserControl
    {
        public Gestion_Venta()
        {
            InitializeComponent();
        }

        private void cbTipo_Cliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbTipo_Cliente.SelectedIndex == 0)
            {
                SPCliente_Natural.Visibility = Visibility.Visible;
                SPCliente_Juridico.Visibility = Visibility.Collapsed;
            }
            else
            {
                SPCliente_Natural.Visibility = Visibility.Collapsed;
                SPCliente_Juridico.Visibility=Visibility.Visible;
            }
        }

        private void btnEliminar_de_lista_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnActualizar_Venta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCargar_datos_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
