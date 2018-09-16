using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

namespace WpfConsultorio
{
    /// <summary>
    /// Interaction logic for PageDarTurno.xaml
    /// </summary>
    public partial class PageDarTurno : Page
    {
        OleDbConnection con;
        public PageDarTurno()
        {
            InitializeComponent();

        }
        private void NewConnection()
        {
            con = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\Consultorio.accdb");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NewConnection();
            FillPacientesGrid();
        }
        public void FillPacientesGrid()
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Pacientes";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            pacientesDataGrid.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void pacientesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (dg.SelectedItem is DataRowView SelectedRow)
            {
                id_pacienteTextBox.Text = SelectedRow["id_paciente"].ToString();
            }
        }

        private void Button_ClickSaveTurno(object sender, RoutedEventArgs e)
        {
            if (id_pacienteTextBox.Text != null && fechaDatePicker.Text != null)
            {
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "INSERT INTO Turnos(fecha, id_paciente , observaciones , estado, hora)" +
                     "Values(@fecha ,@id, @hora, @obs , @estado)";
                string auxFecha = fechaDatePicker.Text.Substring(0, 2) + "/" + fechaDatePicker.Text.Substring(2, 2) + "/" + fechaDatePicker.Text.Substring(4, 4);
                string auxHora = horaDatePicker.Text.Substring(0, 2) + ":" + horaDatePicker.Text.Substring(2, 2);
                cmd.Parameters.AddWithValue("@fecha", auxFecha);
                cmd.Parameters.AddWithValue("@id", int.Parse(id_pacienteTextBox.Text));
                cmd.Parameters.AddWithValue("@obs", observacionesTextBox.Text);
                cmd.Parameters.AddWithValue("@estado", "En espera");
                cmd.Parameters.AddWithValue("@hora", auxHora);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                                MessageBox.Show("Turno guardado para el dia " + auxFecha + "  a las  " + auxHora, "Ok!");
                con.Close();
            }
            else
            {
                MessageBox.Show("Complete todos los datos", "Oops");
            }
        }
    }
}
