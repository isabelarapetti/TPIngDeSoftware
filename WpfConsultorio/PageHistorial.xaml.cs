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
    /// Interaction logic for PageHistorial.xaml
    /// </summary>
    public partial class PageHistorial : Page
    {
        OleDbConnection con;
        public PageHistorial()
        {
            InitializeComponent();
        }
        private void NewConnection()
        {
            con = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\Consultorio.accdb");
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (turnosDataGrid.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)turnosDataGrid.SelectedItem;
                int auxId = int.Parse(rowView["ID_turno"].ToString());
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "DELETE from Turnos WHERE ID_turno = @id";
                cmd.Parameters.AddWithValue("@id", auxId);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Turno eliminado", "Ok");
                con.Close();
                FillTurnosGrid();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NewConnection();
            FillTurnosGrid();
        }
        private void FillTurnosGrid()
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Turnos ORDER BY Turnos.fecha DESC";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            turnosDataGrid.ItemsSource = dt.DefaultView;
            con.Close();
        }
    }
}
