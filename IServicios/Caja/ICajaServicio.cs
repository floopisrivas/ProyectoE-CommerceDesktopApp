using IServicios.Caja.DTOs;
using System;
using System.Collections.Generic;

namespace IServicios.Caja
{
    public interface ICajaServicio
    {
        bool VerificarSiExisteCajaAbierta(long usuarioId);

        decimal ObtenerMontoCajaAnterior(long usuarioId);
        IEnumerable<CajaDto> Obtener(string cadenaBuscar, bool filtroPorFecha, DateTime fechaDesde, DateTime fechaHasta);

        void Abrir(long usuarioId, decimal monto, DateTime fecha);

        void Cerrar(long cajaId, long usuarioId, decimal monto);

        CajaDto Obtener(long cajaId);

    }
}
