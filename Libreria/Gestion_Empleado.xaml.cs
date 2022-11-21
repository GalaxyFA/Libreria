using Libreria.Data;
using Libreria.Data.MainModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Libreria
{
    /// <summary>
    /// Lógica de interacción para Gestion_Empleado.xaml
    /// </summary>
    public partial class Gestion_Empleado : UserControl
    {
        private LibreriaContext _libreriaContext;
        private IRepository<Empleado> _repository;
        public Gestion_Empleado()
        {
            InitializeComponent();
            _repository = new Repository<Empleado>();
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
                var eliminarEmpleado = (Empleado)dgEmpleados.SelectedItem;
                _repository.Del(eliminarEmpleado.IdEmpleado);
                _repository.Savechange();
                Limpiar();
                MostrarLista();
            }
        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            if(dgEmpleados.SelectedIndex != -1)
            {
                var cargarEmpleado = (Empleado)dgEmpleados.SelectedItem;
                txtId_empleado.Text = $"{ cargarEmpleado.IdEmpleado}";
                txtPrimer_nombre.Text =  cargarEmpleado.PrimerNombre;
                txtSegundo_nombre.Text =  cargarEmpleado.SegundoNombre;
                txtPrimer_apellido.Text =  cargarEmpleado.PrimerApellido;
                txtSegundo_apellido.Text = cargarEmpleado.SegundoApellido;
                txtTelefono.Text =  cargarEmpleado.Telefono;
                txtDireccion.Text =  cargarEmpleado.Direccion;
                txtTitulo.Text= cargarEmpleado.Titulo;
                btnEditar.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("No hay Datos", "Aviso");
            }
        }

        
            
      

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Empleado editarEmpleado = new()
            {
                IdEmpleado = Convert.ToInt32(txtId_empleado.Text),
                PrimerNombre= txtPrimer_nombre.Text,
                SegundoNombre = txtSegundo_nombre.Text,
                PrimerApellido = txtPrimer_apellido.Text,
                SegundoApellido = txtSegundo_apellido.Text,
                Titulo = txtTitulo.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text
            };
            _repository.Edit(editarEmpleado);
            _repository.Savechange();
            Limpiar();
            btnEditar.IsEnabled = false;
            MostrarLista();

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Empleado nuevoEmpleado = new();
            nuevoEmpleado.PrimerApellido = txtPrimer_apellido.Text;
            nuevoEmpleado.PrimerNombre = txtPrimer_nombre.Text;
            nuevoEmpleado.SegundoNombre = txtSegundo_nombre.Text;
            nuevoEmpleado.SegundoApellido = txtSegundo_apellido.Text;
            nuevoEmpleado.Telefono = txtTelefono.Text;
            nuevoEmpleado.Titulo = txtTitulo.Text;
            nuevoEmpleado.Direccion = txtDireccion.Text;
            _repository.Add(nuevoEmpleado);
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
            _libreriaContext = new LibreriaContext();
            var res = from empleado in _libreriaContext.Empleados where 
                      empleado.PrimerNombre.Contains(txtBuscar_nombre.Text) orderby
                      empleado.PrimerNombre descending select empleado;
            dgEmpleados.ItemsSource= _repository.GetByQuery(res);

        }
        private void MostrarLista()
        {
            var empleados = _repository.GetAll();
            //id = empleados.Count() + 1;
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
