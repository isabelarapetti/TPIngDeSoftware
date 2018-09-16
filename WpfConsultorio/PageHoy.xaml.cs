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
using WpfConsultorio.Model;

namespace WpfConsultorio
{
    /// <summary>
    /// Interaction logic for PageHoy.xaml
    /// </summary>
    public partial class PageHoy : Page
    {
        OleDbConnection con;
        public PageHoy()
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
            FillGrid();
        }
        private void FillGrid()
        {
            List<Paciente> PacientesList = getPacientes();
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != System.Data.ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Turnos ORDER BY Turnos.fecha DESC";
            OleDbDataReader reader = cmd.ExecuteReader();
            List<Turno> TurnosList = new List<Turno>();
            while (reader.Read())
            {
                Turno t = new Turno();
                t.IdTurno = Convert.ToInt32(reader["ID_turno"]);
                t.Fecha = Convert.ToString(reader["fecha"]);
                t.Hora = Convert.ToString(reader["hora"]);
                t.Estado = Convert.ToString(reader["estado"]);
                t.Observaciones = Convert.ToString(reader["observaciones"]);
                int idPaciente = Convert.ToInt32(reader["id_paciente"]);
                foreach (Paciente p in PacientesList)
                {
                    if (idPaciente == p.Id)
                    {
                        t.nombreCompleto = p.Nombre + " " + p.Apellido;
                    }
                }
                TurnosList.Add(t);
            }
            con.Close();
            turnosDataGrid.ItemsSource = TurnosList;
        }

        private List<Paciente> getPacientes()
        {
            List<Paciente> PacientesList = new List<Paciente>();
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != System.Data.ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT ID_paciente, nombre , apellido FROM Pacientes";
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Paciente p = new Paciente();
                p.Id = Convert.ToInt32(reader["ID_paciente"]);
                p.Nombre = Convert.ToString(reader["nombre"]);
                p.Apellido = Convert.ToString(reader["apellido"]);
                PacientesList.Add(p);
            }
            con.Close();
            return PacientesList;
        }
        private void BtnEstado_Click(object sender, RoutedEventArgs e)
        {
            if (turnosDataGrid.SelectedItem != null)
            {
                Turno row = (Turno)turnosDataGrid.SelectedItem;
                int auxId = row.IdTurno;
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "UPDATE Turnos SET estado = @estado WHERE ID_turno = @id";
                string content = (sender as Button).Content.ToString();
                if (content == "Atendido")
                {
                    cmd.Parameters.AddWithValue("@estado", "Atendido");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@estado", "Cancelado");
                }
                cmd.Parameters.AddWithValue("@id", auxId);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                FillGrid();
            }
        }
    }
}
