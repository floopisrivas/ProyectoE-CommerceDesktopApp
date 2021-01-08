using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Constantes;
using IServicio.BaseDto;

namespace IServicios.Caja.DTOs
{
    public class CajaDetalleDto: DtoBase
    {
        public TipoPago TipoPago { get; set; }

        public decimal Monto { get; set; }
    }
}
