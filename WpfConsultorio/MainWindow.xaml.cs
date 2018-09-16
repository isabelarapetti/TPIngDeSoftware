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
using System.Data.OleDb;
using System.Data;
using System.Configuration;
namespace WpfConsultorio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OleDbConnection con;
        public MainWindow()
        {
            InitializeComponent();
            NewConnection();
                    }

        private void NewConnection()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\Consultorio.accdb");
        }

        #region region navegacion
        private void Button_Click_PHoy(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageHoy();
        }

        private void Button_Click_Pturno(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageDarTurno();
        }
        private void Button_Click_PListaPacientes(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageListaPacientes();
        }

        private void Button_Click_PHistorialTurnos(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageHistorial();
        }
        #endregion


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageHoy();
            ////// Load data into the table Turnos. You can modify this code as needed.
            //ConsultorioDataSet consultorioDataSet = ((ConsultorioDataSet)(this.FindResource("consultorioDataSet")));
            ////ConsultorioDataSetTableAdapters.TurnosTableAdapter consultorioDataSetTurnosTableAdapter = new ConsultorioDataSetTableAdapters.TurnosTableAdapter();
            ////consultorioDataSetTurnosTableAdapter.Fill(consultorioDataSet.Turnos);
            ////CollectionViewSource turnosViewSource = ((CollectionViewSource)(this.FindResource("turnosViewSource")));
            ////turnosViewSource.View.MoveCurrentToFirst();

            //// Load data into the table Pacientes. You can modify this code as needed.
            //ConsultorioDataSetTableAdapters.PacientesTableAdapter consultorioDataSetPacientesTableAdapter = new ConsultorioDataSetTableAdapters.PacientesTableAdapter();
            //consultorioDataSetPacientesTableAdapter.Fill(consultorioDataSet.Pacientes);
            //System.Windows.Data.CollectionViewSource pacientesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pacientesViewSource")));
            //pacientesViewSource.View.MoveCurrentToFirst();

            //OleDbCommand cmd = new OleDbCommand();
            //if (con.State != ConnectionState.Open)
            //    con.Open();
            //cmd.Connection = con;
            //cmd.CommandText = "select * from Pacientes";
            //OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //pacientesDataGrid.ItemsSource = dt.AsDataView();

            //if (dt.Rows.Count > 0)
            //{

            //    pacientesDataGrid.Visibility = System.Windows.Visibility.Visible;
            //}
            //else
            //{

            //    pacientesDataGrid.Visibility = System.Windows.Visibility.Hidden;
            //}
            //crear objeto pacientes

        }
    }
}
