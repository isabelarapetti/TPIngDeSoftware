using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


namespace WpfConsultorio
{
    /// <summary>
    /// Interaction logic for PageListaPacientes.xaml
    /// </summary>
    public partial class PageListaPacientes : Page
    {
        OleDbConnection con;
        public PageListaPacientes()
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

        private void FillPacientesGrid()
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Pacientes";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            pacientes2DataGrid.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void Button_Click_CrearPaciente(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nombreTextBox.Text) && !string.IsNullOrWhiteSpace(apellidoTextBox.Text))
            {
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "Insert into Pacientes(nombre,apellido,dni,telefono,obra_social,nro_asociado)" +
                     " Values(@nombre,@apellido,@dni,@tel,@os,@nroAsoc)";
                cmd.Parameters.AddWithValue("@nombre", nombreTextBox.Text);
                cmd.Parameters.AddWithValue("@apellido", apellidoTextBox.Text);
                cmd.Parameters.AddWithValue("@dni", dniTextBox.Text);
                cmd.Parameters.AddWithValue("@tel", telefonoTextBox.Text);
                cmd.Parameters.AddWithValue("@os", obra_socialTextBox.Text);
                cmd.Parameters.AddWithValue("@nroAsoc", nro_asociadoTextBox.Text);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Paciente agregado", "Ok");
                con.Close();
                FillPacientesGrid();
            }
            else
            {
                MessageBox.Show("Complete todos los datos", "Oops");
            }
        }

        private void pacientesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = new DataGrid();
            dg = sender as DataGrid;
            DataRowView selectedRow = dg.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                iD_pacienteTextBox.Text = selectedRow["id_paciente"].ToString();
                nombreTextBox.Text = selectedRow["nombre"].ToString();
                apellidoTextBox.Text = selectedRow["apellido"].ToString();
                dniTextBox.Text = selectedRow["dni"].ToString();
                telefonoTextBox.Text = selectedRow["telefono"].ToString();
                obra_socialTextBox.Text = selectedRow["obra_social"].ToString();
                nro_asociadoTextBox.Text = selectedRow["nro_asociado"].ToString();
            }
        }

        private void Button_Click_EditarPaciente(object sender, RoutedEventArgs e)
        {
            int auxId = int.Parse(iD_pacienteTextBox.Text);
            if (auxId > 0)
            {

                if (auxId > 0)
                {
                    OleDbCommand cmd = new OleDbCommand();
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.CommandText = "UPDATE Pacientes SET nombre = @nombre, apellido = @apellido, dni = @dni, telefono = @tel, obra_social = @os, nro_asociado= @nroAsoc WHERE ID_paciente = @id";
                    cmd.Parameters.AddWithValue("@nombre", nombreTextBox.Text);
                    cmd.Parameters.AddWithValue("@apellido", apellidoTextBox.Text);
                    cmd.Parameters.AddWithValue("@dni", dniTextBox.Text);
                    cmd.Parameters.AddWithValue("@tel", telefonoTextBox.Text);
                    cmd.Parameters.AddWithValue("@os", obra_socialTextBox.Text);
                    cmd.Parameters.AddWithValue("@nroAsoc", nro_asociadoTextBox.Text);
                    cmd.Parameters.AddWithValue("@id", auxId);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Paciente editado ok", "Ok");
                    con.Close();
                    FillPacientesGrid();
                }
            }
            else
            {
                MessageBox.Show("Debe  seleccionar un paciente de la lista para editarlo o crear uno nuevo", "Oops");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (pacientes2DataGrid.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)pacientes2DataGrid.SelectedItem;
                int auxId = int.Parse(rowView["ID_paciente"].ToString());
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "delete from Turnos WHERE id_paciente = @id";
                cmd.Parameters.AddWithValue("@id", auxId);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "delete from Pacientes WHERE ID_paciente = @id";
                cmd.Parameters.AddWithValue("@id", auxId);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Paciente eliminado", "Ok");
                con.Close();
                FillPacientesGrid();
            }
        }
    }
}
