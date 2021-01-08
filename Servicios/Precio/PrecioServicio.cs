using Dominio.UnidadDeTrabajo;
using IServicio.BaseDto;
using IServicios.Articulo.DTOs;
using IServicios.Precio;
using IServicios.Precio.DTOs;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace Servicios.Precio
{
    public class PrecioServicio : IPrecioServicio
    {
        private readonly IUnidadDeTrabajo _unidaDeTrabajo;


        public PrecioServicio(IUnidadDeTrabajo unidaDeTrabajo)
        {
            _unidaDeTrabajo = unidaDeTrabajo;
        }


        public void Actualizar(bool marca, bool rubro, bool articulo, bool listaPrecio, 
            decimal valor, bool porcentaje, long marcaId, long rubroId, long codigoDesde, long codigoHasta, long listaPrecioId)
        {
            using (var transaccion = new TransactionScope())
            {
                try
                {
                    Expression<Func<Dominio.Entidades.Articulo, bool>> filtro = x => true;


                   

                    if (marca)
                    {
                        filtro = filtro.And(x => x.MarcaId == marcaId);
                    }
                    if (rubro)
                    {
                        filtro = filtro.And(x => x.RubroId == rubroId);
                    }
                    if (articulo)
                    {
                        filtro = filtro.And(x => (x.Id >= codigoDesde && x.Id <= codigoHasta));
                    }
                   
                    var resultado = _unidaDeTrabajo.ArticuloRepositorio.Obtener(filtro, "Precios");
                    var listaDePrecio = _unidaDeTrabajo.ListaPrecioRepositorio.Obtener();
                    var fechaActual = DateTime.Now;

                    foreach (var producto in resultado)

                    {
                        if (listaPrecioId != 0)
                        {
                            var precios = producto.Precios.FirstOrDefault(x => x.ListaPrecioId == listaPrecioId
                            && x.FechaActualizacion <= fechaActual
                            && x.FechaActualizacion == producto.Precios.Where(p => p.ListaPrecioId == listaPrecioId
                            && x.FechaActualizacion <= fechaActual).Max(f => f.FechaActualizacion));


                            var precioCosto = porcentaje ? precios.PrecioCosto + (precios.PrecioCosto * (valor / 100m))
                                : precios.PrecioCosto + valor;
                            var listaSeleccionada = listaDePrecio.FirstOrDefault(x => x.Id == listaPrecioId);
                            var precioPublico = precioCosto + (precioCosto * (listaSeleccionada.PorcentajeGanancia / 100m));

                            var nuevoPrecio = new Dominio.Entidades.Precio
                            {
                                ArticuloId = producto.Id,
                                ListaPrecioId = listaPrecioId,
                                FechaActualizacion = fechaActual,
                                PrecioCosto = precioCosto,
                                PrecioPublico = precioPublico,
                                EstaEliminado = false

                            };

                            _unidaDeTrabajo.PrecioRepositorio.Insertar(nuevoPrecio);
                        }
                        else
                        {
                            foreach (var listas in listaDePrecio)
                            {
                                var precios = producto.Precios.FirstOrDefault(x => x.ListaPrecioId == listas.Id
                                && x.FechaActualizacion <= fechaActual
                                && x.FechaActualizacion == producto.Precios.Where(p => p.ListaPrecioId == listas.Id
                                && x.FechaActualizacion <= fechaActual).Max(f => f.FechaActualizacion));


                                var precioCosto = porcentaje ? precios.PrecioCosto + (precios.PrecioCosto * (valor / 100m))
                                    : precios.PrecioCosto + valor;

                                var precioPublico = precioCosto + (precioCosto * (listas.PorcentajeGanancia / 100m));

                                var nuevoPrecio = new Dominio.Entidades.Precio
                                {
                                    ArticuloId = producto.Id,
                                    ListaPrecioId = listas.Id,
                                    FechaActualizacion = fechaActual,
                                    PrecioCosto = precioCosto,
                                    PrecioPublico = precioPublico,
                                    EstaEliminado = false

                                };

                                _unidaDeTrabajo.PrecioRepositorio.Insertar(nuevoPrecio);
                            }

                        }


                    }

                    _unidaDeTrabajo.Commit();
                    transaccion.Complete();
                }
                catch (Exception e)
                {
                    transaccion.Dispose();
                    throw new Exception(e.Message);
                }


            }

        }

       
    }
}
