using Libreria.Data;
using Libreria.Data.MainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("--------------------------------------");
            //IRepository<Empleado> repo = new Repository<Empleado>();
            //var res = repo.GetEmpleados().Result;
            //Console.WriteLine(res.Count);
            //Console.WriteLine(res[0].PrimerNombre);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            //if (txtUsuario.Text == "Admin" && txtPasword.Password == "1234")
            //{
            //    lbInfo.Content = "Acceso permitido";
            //    lbInfo.Foreground = Brushes.Green;
            //    Llenar_Barra();
            //}
            //else
            //{
            //    lbInfo.Content = "Acceso Denegado";
            //    lbInfo.Foreground = Brushes.Red;
            //}
            Llenar_Barra();
        }

        private void pbProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (pbProgress.Value == 25)
            {
                lbCarga.Content = "Cargando";
            }
            else if (pbProgress.Value == 50)
            {
                lbCarga.Content = "Validando inicio de sesión";
            }
            else if (pbProgress.Value == 75)
            {
                lbCarga.Content = "Cargando sistema";
            }
            else if (pbProgress.Value == 100)
            {
                Principal principal = new Principal();
                principal.Show();
                this.Close();
            }
        }

        private void txtPasword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtUsuario.Text == "Admin" && txtPasword.Password == "1234")
                {
                    lbInfo.Content = "Acceso permitido";
                    lbInfo.Foreground = Brushes.Green;
                    Llenar_Barra();
                }
                else
                {
                    lbInfo.Content = "Acceso Denegado";
                    lbInfo.Foreground = Brushes.Red;
                }
            }
        }

        private void Llenar_Barra()
        {
            pbProgress.Visibility = Visibility.Visible;
            lbCarga.Visibility = Visibility.Visible;
            lbCarga.Content = "Cargando";
            pbProgress.Value = 0;
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(Worker_Do_work);
            worker.ProgressChanged += new ProgressChangedEventHandler(Worker_progress);
            worker.RunWorkerAsync();
        }

        private void Worker_Do_work(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i += 5)
            {
                (sender as BackgroundWorker).ReportProgress(i + 5);
                Thread.Sleep(250);
            }
        }

        private void Worker_progress(object sender, ProgressChangedEventArgs e)
        {
            pbProgress.Value = e.ProgressPercentage;
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtUsuario.Text == "Admin" && txtPasword.Password == "1234")
                {
                    lbInfo.Content = "Acceso permitido";
                    lbInfo.Foreground = Brushes.Green;
                    Llenar_Barra();
                }
                else
                {
                    lbInfo.Content = "Acceso Denegado";
                    lbInfo.Foreground = Brushes.Red;
                }
            }
        }

    }
}
