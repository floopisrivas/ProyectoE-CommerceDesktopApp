using IServicio.BaseDto;
using System;
using System.Collections.Generic;

namespace IServicios.Precio
{
    public interface IPrecioServicio 
    {

        void Actualizar( bool marca, bool rubro, bool articulo,
             bool listaPrecio, decimal valor, bool porcentaje, long marcaId, long rubroId, long codigoDesde,
             long codigoHasta, long listaPrecioId);

    }
}
