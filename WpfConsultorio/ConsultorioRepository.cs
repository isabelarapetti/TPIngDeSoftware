using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfConsultorio.Model
{
    class ConsultorioRepository
    {
        OleDbConnection con;

        #region CRUD paciente
        private void GetPacientes()
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Pacientes";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //pacientesDataGrid.ItemsSource = dt.AsDataView();
            if (dt.Rows.Count > 0)
            {
                //pacientesDataGrid.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                //pacientesDataGrid.Visibility = System.Windows.Visibility.Hidden;
            }
            // List<Paciente> return 
        }
        public void AddPaciente(string nombre, string dni, string telefono, string apellido, string obra_social, string nro_asociado)
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "insert into Pacientes(nombre,dni,telefono,apellido,obra_social,nro_asociado) Values(" + nombre + ",'" + apellido + "','" + txtTelefono.Text + "'," + txtApellido.Text + ",'" + txtObraSocial.Text + "')";
            //INSERT INTO `Pacientes` (`nombre`, `dni`, `telefono`, `apellido`, `obra_social`, `nro_asociado`) VALUES (?, ?, ?, ?, ?, ?)
            cmd.ExecuteNonQuery();
        }
        public void UpdatePaciente()
        {
            //DataRowView row = (DataRowView)gvData.SelectedItems[0];
            //txtEmpId.Text = row["EmpId"].ToString();
            //txtEmpName.Text = row["EmpName"].ToString();
            //ddlGender.Text = row["Gender"].ToString();
            //txtContact.Text = row["Contact"].ToString();
            //txtAddress.Text = row["Address"].ToString();
            //txtEmpId.IsEnabled = false;
            //btnAdd.Content = "Update";
        }
        public void DeletePAciente()
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            //cmd.CommandText = "delete from Pacientes where EmpId=" + row["EmpId"].ToString();
            //cmd.ExecuteNonQuery();
            //MessageBox.Show("Employee Deleted Successfully...");
        }
        #endregion

        #region CRUD turno
        private void GetTurnos()
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Turnos";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //pacientesDataGrid.ItemsSource = dt.AsDataView();
            if (dt.Rows.Count > 0)
            {
                //pacientesDataGrid.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                //pacientesDataGrid.Visibility = System.Windows.Visibility.Hidden;
            }
            // List<Paciente> return 
        }
        private void GetTurnosforToday()
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Turnos where fecha = " + DateTime.Today;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //pacientesDataGrid.ItemsSource = dt.AsDataView();
            if (dt.Rows.Count > 0)
            {
                //pacientesDataGrid.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                //pacientesDataGrid.Visibility = System.Windows.Visibility.Hidden;
            }
            // List<Paciente> return 
        }
        public void AddTurno()
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            //cmd.CommandText = "insert into turnos(EmpId,EmpName,Gender,Contact,Address) Values(" + txtEmpId.Text + ",'" + txtEmpName.Text + "','" + ddlGender.Text + "'," + txtContact.Text + ",'" + txtAddress.Text + "')";
            cmd.ExecuteNonQuery();
        }
        public void DeleteTurno()
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            //cmd.CommandText = "delete from Turnos where EmpId=" + row["EmpId"].ToString();
            //cmd.ExecuteNonQuery();
            //MessageBox.Show("Employee Deleted Successfully...");
        }
        #endregion
    }
}
