using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using RecetasSLN.dominio;

namespace RecetasSLN.servicios.Interfaz
{
    internal interface IServicio
    {
        DataTable listarIngredientes();
        int proximaReceta();
        bool ejecutarSP(Receta oReceta);
    }
}
