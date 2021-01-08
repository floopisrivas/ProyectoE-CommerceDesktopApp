using Dominio.UnidadDeTrabajo;
using IServicios.Comprobantes.DTOs;
using StructureMap;

namespace Servicios.Comprobantes
{
    public class Comprobante
    {
        protected readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public Comprobante()
        {
            _unidadDeTrabajo = ObjectFactory.GetInstance<IUnidadDeTrabajo>();
        }

        public virtual long Insertar(ComprobanteDto comprobante)
        {
            return 0;
        }
    }

}
