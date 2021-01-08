using IServicio.BaseDto;
using System;
namespace IServicios.Precio.DTOs
{
    public class PrecioCrudDto : DtoBase
    {
        public long ArticuloId { get; set; }

        public long ListaPrecioId { get; set; }

        public decimal PrecioCosto { get; set; }

        public decimal PrecioPublico { get; set; }

        public DateTime FechaActualizacion { get; set; }

    }
}
