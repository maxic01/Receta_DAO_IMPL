using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using RecetasSLN.dominio;

namespace RecetasSLN.datos
{
    internal class GestorDB : IRecetaDao
    {
        public DataTable GetIngredientes()
        {
            return RecetaDAO.ObtenerInstancia().listarIngredientes();
        }

        public int GetproximaReceta()
        {
            return RecetaDAO.ObtenerInstancia().proximaReceta();
        }

        public bool GetjecutarSP(Receta oReceta)
        {
            return RecetaDAO.ObtenerInstancia().ejecutarSP(oReceta);
        }
    }
}
