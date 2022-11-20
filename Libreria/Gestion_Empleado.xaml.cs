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
    /// Lógica de interacción para Gestion_Empleado.xaml
    /// </summary>
    public partial class Gestion_Empleado : UserControl
    {
        private readonly LibreriaContext _libreriaContext;
        private readonly IRepository<Empleado> _repository;

        public Gestion_Empleado(LibreriaContext libreriaContext, IRepository<Empleado> repository)
        {
            InitializeComponent();
            _libreriaContext = libreriaContext;
            _repository = repository;

            //

            //var empleados = _repository.GetAll();
            //Console.WriteLine(empleados.Count);

            //// ---- Para cuando se necesite flexibilidad

            //var res = from empleado in _libreriaContext.Empleados where empleado.PrimerNombre.Contains("M") orderby empleado.PrimerNombre descending select empleado;
            //// Esta parte no es necesaria, se va a implementar en Repository
            //var valores = res.ToList();
            //Console.WriteLine(valores.Count);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
