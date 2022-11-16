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
    /// Lógica de interacción para Gestion_Encargo.xaml
    /// </summary>
    public partial class Gestion_Encargo : UserControl
    {
        public Gestion_Encargo()
        {
            InitializeComponent();
        }

        private void btnActualizar_Encargo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCargar_datos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelar_Encargo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbTipo_Cliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTipo_Cliente.SelectedIndex == 0)
            {
                SPCliente_Natural.Visibility = Visibility.Visible;
                SPCliente_Juridico.Visibility = Visibility.Collapsed;
            }
            else
            {
                SPCliente_Natural.Visibility = Visibility.Collapsed;
                SPCliente_Juridico.Visibility = Visibility.Visible;
            }
        }
    }
}
