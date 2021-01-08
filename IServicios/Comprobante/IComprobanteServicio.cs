using IServicios.Comprobantes.DTOs;


namespace IServicios.Comprobantes
{
    public interface IComprobanteServicio 
    {
        long Insertar(ComprobanteDto dto);

    }
}
