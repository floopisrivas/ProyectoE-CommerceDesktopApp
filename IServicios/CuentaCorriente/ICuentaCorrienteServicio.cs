using IServicios.CuentaCorriente.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServicios.CuentaCorriente
{
    public interface ICuentaCorrienteServicio
    {
        decimal ObtenerDeudaCliente(long clienteId);

        IEnumerable<CuentaCorrienteDto> Obtener(DateTime fechaDesde, DateTime fechaHasta, bool soloDeuda);
        
    }
}
