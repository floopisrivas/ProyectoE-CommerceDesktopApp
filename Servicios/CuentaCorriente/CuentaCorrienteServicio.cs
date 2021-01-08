using Aplicacion.Constantes;
using Dominio.Entidades;
using Dominio.UnidadDeTrabajo;
using IServicios.CuentaCorriente;
using IServicios.CuentaCorriente.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.CuentaCorriente
{
    public class CuentaCorrienteServicio : ICuentaCorrienteServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public CuentaCorrienteServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }


        public IEnumerable<CuentaCorrienteDto> Obtener(DateTime fechaDesde, DateTime fechaHasta, bool soloDeuda)
        {
            var _fechaDesde = new DateTime(fechaDesde.Year, fechaDesde.Month, fechaDesde.Day, 0, 0, 0);

            var _fechaHasta = new DateTime(fechaHasta.Year, fechaHasta.Month, fechaHasta.Day, 23, 59, 59);

            Expression<Func<MovimientoCuentaCorriente, bool>> filtro = x => true;

            filtro = filtro.And(x => x.Fecha >= _fechaDesde && x.Fecha <= _fechaHasta);

            if(soloDeuda)
            {
                filtro = filtro.And(x => x.TipoMovimiento == TipoMovimiento.Egreso);

            }

            return _unidadDeTrabajo.CuentaCorrienteRepositorio.Obtener(filtro).Select(x => new CuentaCorrienteDto
            {
                Fecha = x.Fecha,
                Descripcion = x.Descripcion,
                Monto = (x.Monto * (int)x.TipoMovimiento)

            }).OrderBy(x => x.Fecha)
            .ToList();
        }

        public decimal ObtenerDeudaCliente(long clienteId)
        {
            var movimientos = _unidadDeTrabajo.CuentaCorrienteRepositorio.Obtener(x => x.EstaEliminado && x.ClienteId == clienteId);

            return movimientos.Sum(x => x.Monto * (int)x.TipoMovimiento);


        }
    }
}
