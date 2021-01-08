using IServicios.Comprobantes;
using Dominio.UnidadDeTrabajo;
using System.Collections.Generic;
using IServicios.Comprobante.DTOs;
using Aplicacion.Constantes;
using System.Linq;

namespace Servicios.Comprobantes
{
    public class FacturaServicio : ComprobanteServicio, IFacturaServicio
    {

        public FacturaServicio(IUnidadDeTrabajo unidadDeTrabajo)
        : base(unidadDeTrabajo)
        {


        }

        public IEnumerable<ComprobantePendienteDto>ObtenerPendientesPago()
        {
            return _unidadDeTrabajo.FacturaRepositorio.Obtener(x => x.EstaEliminado
            && x.Estado == Estado.Pendiente, "Cliente, DetalleComprobantes").Select(x => new ComprobantePendienteDto
            {
                Id = x.Id,
                Cliente = $"{x.Cliente.Apellido} {x.Cliente.Nombre}",
                Direccion = x.Cliente.Direccion,
                Dni = x.Cliente.Dni,
                Telefono = x.Cliente.Telefono,
                Fecha = x.Fecha,
                MontoPagar = x.Total,
                Numero = x.Numero,
                Eliminado = x.EstaEliminado,
                Items = x.DetalleComprobantes.Select(d => new DetallePendienteDto 
                {
                    Id = d.Id,
                    Descripcion = d.Descripcion,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio,
                    SubTotal = d.SubTotal,
                    Eliminado = d.EstaEliminado,

                }).ToList()

        })
        .OrderByDescending(x => x.Fecha)
            .ToList();
 
        }
    }

}
