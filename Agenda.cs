using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace my_agenda
{
    class Agenda
    {
        private SQLiteConnection cn = null; //para la conexion
        private SQLiteCommand cmd = null; //para ejecutar comandos sqlite
        private SQLiteDataReader reader = null; //para almacenar los datos
        private DataTable table = null; // para organizar la informacion recibida

        //metodo para insertar en la base de datos
        public bool insertar(string nombre, string telefono)
        {
            try
            {
                string query = "INSERT INTO directorio(nombre, telefono)VALUES('" + telefono + "','" + nombre + "')";
                cn = Conexion.conectar();
                cn.Open();
                cmd = new SQLiteCommand(query, cn);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return false;
        }
        //metodo para consultar
        public DataTable consultar()
        {
            try
            {
                nombresColumnas();
                string query = "SELECT * FROM directorio";
                cn = Conexion.conectar();
                cn.Open();
                cmd = new SQLiteCommand(query, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add(new object[] { reader["id"], reader["Nombre"], reader["Telefono"] });
                }
                reader.Close();
                cn.Close();
                return table;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return table;
        }
        //metodo para eliminar
        public bool eliminar(int id)
        {
            try
            {
                string query = "DELETE FROM directorio WHERE id= '" + id + "'";
                cn = Conexion.conectar();
                cmd = new SQLiteCommand(query, cn);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    cn.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,"Ocurrio un Error en el proceso");
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return false;
        }
        //metodo para actualizar
        public bool actualizar(int id, string nombre, string telefono)
        {
            try
            {
                string query = "UPDATE directorio SET nombre ='"+ nombre +"', telefono ='" + telefono + "' WHERE id ='" + id.ToString() + "'";
                System.Windows.Forms.MessageBox.Show(query);
                cn = Conexion.conectar();
                cmd = new SQLiteCommand(query, cn);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    cn.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Ocurrio un Error en el proceso");
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return false;
        }
        //metodo para darle nombre a las columnas
        private void nombresColumnas()
        {
            table = new DataTable();
            table.Columns.Add("id");
            table.Columns.Add("Nombre");
            table.Columns.Add("Telefono");
        }
    }
}
