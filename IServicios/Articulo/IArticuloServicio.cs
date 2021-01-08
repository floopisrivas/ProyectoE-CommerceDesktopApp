using IServicio.BaseDto;
using IServicios.Articulo.DTOs;
using System;
using System.Collections.Generic;

namespace IServicio.Articulo
{
    public interface IArticuloServicio : Base.IServicio
    {
        int ObtenerSiguienteNroCodigo();
        bool VerificarSiExiste(string datoVerificar, long? entidadId = null);

        IEnumerable<ArticuloVentaDto> ObtenerLookUp(string cadenaBuscar, long listaPrecioId);

        ArticuloVentaDto ObtenerPorCodigo(string codigo, long listaPrecioId, long depositoId);

       
    }
}
