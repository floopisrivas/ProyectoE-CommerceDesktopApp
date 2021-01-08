using IServicio.BaseDto;

namespace IServicio.Articulo.DTOs
{
    public class StockDepositoDto:DtoBase
    {
        public decimal Cantidad { get; set; }

        public string Desposito { get; set; }
    }
}
