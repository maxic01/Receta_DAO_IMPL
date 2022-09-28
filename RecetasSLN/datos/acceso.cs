using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RecetasSLN.datos
{
    internal class acceso
    {
        protected SqlConnection conexion = new SqlConnection(Properties.Resources.conexionString);
        protected SqlCommand comando = new SqlCommand();
        protected SqlParameter parametro = new SqlParameter();

        protected void conectar()
        {
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
        }
        protected void desconectar()
        {
            conexion.Close();
        }
    }
}
