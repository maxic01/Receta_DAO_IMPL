using RecetasSLN.servicios.implementacion;
using RecetasSLN.servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.servicios
{
    internal class ImplementacionServicioF : AbstractFactoryServicio
    {
        public override IServicio crearServicio()
        {
            return new RecetaServicio();
        }
    }
}
