using Libreria.Data;
using Libreria.Data.Empleados;
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
        Empleado registroEmpleado = new Empleado();
        int id;
        public Gestion_Empleado(LibreriaContext libreriaContext, IRepository<Empleado> repository)
        {
            InitializeComponent();
            _libreriaContext = libreriaContext;
            _repository = repository;
            MostrarLista();
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
            if(dgEmpleados.SelectedIndex != -1)
            {
                registroEmpleado = (Empleado)dgEmpleados.SelectedItem;
                _repository.Del(registroEmpleado.IdEmpleado);
                MostrarLista();
            }
        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            if(dgEmpleados.SelectedIndex != -1)
            {
                registroEmpleado = (Empleado)dgEmpleados.SelectedItem;
                txtId_empleado.Text = $"{registroEmpleado.IdEmpleado}";
                txtPrimer_nombre.Text = registroEmpleado.PrimerNombre;
                txtSegundo_nombre.Text = registroEmpleado.SegundoNombre;
                txtPrimer_apellido.Text = registroEmpleado.PrimerApellido;
                txtSegundo_apellido.Text = registroEmpleado.SegundoApellido;
                txtTelefono.Text = registroEmpleado.Telefono;
                txtDireccion.Text = registroEmpleado.Direccion;
                txtTitulo.Text=registroEmpleado.Titulo;
                btnEditar.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("No hay Datos", "Aviso");
            }
        }

        
            
      

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            registroEmpleado.IdEmpleado = Convert.ToInt32(txtId_empleado.Text);
            registroEmpleado.PrimerNombre= txtPrimer_nombre.Text;
            registroEmpleado.SegundoNombre = txtSegundo_nombre.Text;
            registroEmpleado.PrimerApellido = txtPrimer_apellido.Text;
            registroEmpleado.SegundoApellido = txtSegundo_apellido.Text;
            registroEmpleado.Titulo = txtTitulo.Text;
            registroEmpleado.Telefono = txtTelefono.Text;
            registroEmpleado.Direccion = txtDireccion.Text;
            _repository.Edit(registroEmpleado);
            Limpiar();
            btnEditar.IsEnabled = false;
            MostrarLista();

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            registroEmpleado.IdEmpleado = id;
            registroEmpleado.PrimerApellido = txtPrimer_apellido.Text;
            registroEmpleado.PrimerNombre = txtPrimer_nombre.Text;
            registroEmpleado.SegundoNombre = txtSegundo_nombre.Text;
            registroEmpleado.SegundoApellido = txtSegundo_apellido.Text;
            registroEmpleado.Telefono = txtTelefono.Text;
            registroEmpleado.Titulo = txtTitulo.Text;
            registroEmpleado.Direccion = txtDireccion.Text;
            _repository.Add(registroEmpleado);
            _repository.Savechange();
            Limpiar();
            
            MostrarLista();
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            MostrarLista();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            var res = from empleado in _libreriaContext.Empleados where 
                      empleado.PrimerNombre.Contains(txtBuscar_nombre.Text) orderby
                      empleado.PrimerNombre descending select empleado;
            dgEmpleados.ItemsSource= _repository.GetByQuery(res);

        }
        private void MostrarLista()
        {
            var empleados = _repository.GetAll();
            id = empleados.Count() + 1;
            dgEmpleados.ItemsSource= _repository.GetAll();
        }
        private void Limpiar()
        {
            txtBuscar_nombre.Clear();
            txtDireccion.Clear();
            txtId_empleado.Clear();
            txtPrimer_apellido.Clear();
            txtPrimer_nombre.Clear();
            txtSegundo_apellido.Clear();
            txtSegundo_nombre.Clear();
            txtTelefono.Clear();
            txtTitulo.Clear();
        }
    }
}
