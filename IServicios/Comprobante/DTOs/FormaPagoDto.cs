using Aplicacion.Constantes;
using IServicio.BaseDto;

namespace IServicios.Comprobantes.DTOs
{
    public class FormaPagoDto : DtoBase
    {
        public decimal Monto { get; set; }
        public TipoPago TipoPago { get; set; }
        public bool Eliminado { get; set; }
    }
}
