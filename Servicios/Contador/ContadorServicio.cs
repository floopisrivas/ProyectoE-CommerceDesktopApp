using Aplicacion.Constantes;
using Dominio.UnidadDeTrabajo;
using IServicios.Contador;
using System;
using System.Linq;

namespace Servicios.Contador
{
    public class ContadorServicio : IContadorServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public ContadorServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public int ObtenerSiguienteNumero(TipoComprobante tipoComprobante)
        {
           var result = _unidadDeTrabajo.ContadorRepositorio.Obtener(x => !x.EstaEliminado && x.TipoComprobante == tipoComprobante);

            if (!result.Any()) throw new Exception("No se encontro el Contador para el tipo de comprobante");

            var entidad = result.FirstOrDefault();

            entidad.Valor += 1;

            _unidadDeTrabajo.ContadorRepositorio.Modificar(entidad);

            _unidadDeTrabajo.Commit();
            return entidad.Valor;

        }
    }
}
