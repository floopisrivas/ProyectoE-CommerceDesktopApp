using IServicios.Comprobante.DTOs;
using System.Collections.Generic;

namespace IServicios.Comprobantes
{
    public interface IFacturaServicio : IComprobanteServicio
    {
        IEnumerable<ComprobantePendienteDto> ObtenerPendientesPago();

    }
}
