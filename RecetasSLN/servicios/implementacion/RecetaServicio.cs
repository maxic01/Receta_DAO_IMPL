using RecetasSLN.datos;
using RecetasSLN.dominio;
using RecetasSLN.servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.servicios.implementacion
{
    internal class RecetaServicio : IServicio
    {
        private IRecetaDao DAO;
        public RecetaServicio()
        {
            DAO = new GestorDB();
        }

        public bool ejecutarSP(Receta oReceta)
        {
            return DAO.GetjecutarSP(oReceta);
        }

        public DataTable listarIngredientes()
        {
            return DAO.GetIngredientes();
        }

        public int proximaReceta()
        {
            return DAO.GetproximaReceta();
        }
    }
}
