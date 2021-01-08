using Dominio.UnidadDeTrabajo;
using IServicios.Comprobantes;
using IServicios.Comprobantes.DTOs;
using Servicios.Base;
using StructureMap;
using System;
using System.Collections.Generic;

namespace Servicios.Comprobantes
{
    public class ComprobanteServicio : IComprobanteServicio
    {
        protected readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private Dictionary<Type, string> _diccionario;

        public ComprobanteServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            
            _unidadDeTrabajo = unidadDeTrabajo;
            _diccionario = new Dictionary<Type, string>();
            InicializadorDiccionario();
        }

        private void InicializadorDiccionario()
        {
            _diccionario.Add(typeof(FacturaDto), "Servicios.Comprobantes.Factura");
        }

        public void AgregarOpcionDiccionario(Type type, string value)
        {
            _diccionario.Add(type, value);
        }

        public virtual long Insertar(ComprobanteDto dto)
        {
            var comprobante = GenericInstance<Comprobante>.InstanciarEntidad(dto,_diccionario);

            return comprobante.Insertar(dto);

        }
    }
}
