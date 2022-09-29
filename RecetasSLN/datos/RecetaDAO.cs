using RecetasSLN.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace RecetasSLN.datos
{
    internal class RecetaDAO : acceso
    {
        private static RecetaDAO instancia;
        
        public static RecetaDAO ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new RecetaDAO();    
            }
            return instancia;
        }
                       
        public bool ejecutarSP(Receta oReceta)
        {
            bool ok = true;
            SqlTransaction t = null;
            try
            {
                comando.Parameters.Clear();
                conectar();
                t = conexion.BeginTransaction();
                comando.Transaction = t;
                comando.CommandText = "SP_INSERTAR_RECETA";
                comando.Parameters.AddWithValue("@id_receta", oReceta.RecetaNro);
                comando.Parameters.AddWithValue("@tipo_receta", oReceta.TipoReceta);
                comando.Parameters.AddWithValue("@nombre", oReceta.Nombre);
                if (oReceta.Chef != null)
                {
                    comando.Parameters.AddWithValue("@cheff", oReceta.Chef);
                }
                else
                {
                    comando.Parameters.AddWithValue("@cheff", DBNull.Value);
                }                   
                comando.ExecuteNonQuery();
                comando.Parameters.Clear();
                int count = 1;
                foreach (DetalleReceta detalle in oReceta.DetalleRecetas)
                {
                    comando.CommandText = "SP_INSERTAR_DETALLES";
                    comando.Parameters.AddWithValue("@id_receta", oReceta.RecetaNro);
                    comando.Parameters.AddWithValue("@id_ingrediente", detalle.Ingrediente.IngredienteID);
                    comando.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    count++;
                    comando.ExecuteNonQuery();
                    comando.Parameters.Clear();

                }
                t.Commit();
            }
            catch (Exception)
            {
                t.Rollback();
                ok = false;                
            }
            finally
            {
                desconectar();
            }
            return ok;
        }

        public DataTable listarIngredientes()
        {
            comando.Parameters.Clear();
            conectar();
            comando.CommandText = "SP_CONSULTAR_INGREDIENTES";
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());           
            desconectar();
            return tabla;
        }

        public int proximaReceta()
        {
            SqlParameter param = new SqlParameter("@prox", SqlDbType.Int);
            try
            {
                conectar();
                comando.Parameters.Clear();
                comando.CommandText = "ProximaReceta";
                param.Direction = ParameterDirection.Output;
                comando.Parameters.Add(param);
                comando.ExecuteNonQuery();
                return (int)param.Value;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                desconectar();
            }

        }
    }
}
