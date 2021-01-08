using IServicio.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServicios.Comprobantes.DTOs
{
    public class DetalleComprobanteDto : DtoBase
    {
        public decimal Cantidad { get; set; }
        public decimal Iva { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public long ArticuloId { get; set; }
        public string Codigo { get; set; }
        public decimal SubTotal { get; set; }
        public bool Eliminado { get; set; }
    }
}
