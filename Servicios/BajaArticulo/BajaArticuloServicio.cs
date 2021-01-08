using DocumentFormat.OpenXml.Drawing.Charts;
using Dominio.UnidadDeTrabajo;
using IServicio.Articulo.DTOs;
using IServicio.Articulo;
using IServicio.BaseDto;
using IServicios.BajaArticulo;
using IServicios.BajaArticulo.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Servicios.BajaArticulo
{
    public class BajaArticuloServicio : IBajaArticuloServicio

    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IArticuloServicio _articuloServicio;

        public BajaArticuloServicio(IUnidadDeTrabajo unidadDeTrabajo, IArticuloServicio articuloServicio)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _articuloServicio = articuloServicio;
        }

        public void Eliminar(long id)
        {
            _unidadDeTrabajo.BajaArticuloRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public void Insertar(DtoBase dtoEntidad)
        {

            using (var trans = new TransactionScope())
            {
                try
                {
                    var dto = (BajaArticuloDto)dtoEntidad;
                    var entidad = new Dominio.Entidades.BajaArticulo
                    {
                        ArticuloId = dto.ArticuloId,
                        MotivoBajaId = dto.MotivoBajaId,
                        Cantidad = dto.Cantidad,
                        Fecha = dto.Fecha,
                        Observacion = dto.Observacion,
                        EstaEliminado = false
                    };

                    _unidadDeTrabajo.BajaArticuloRepositorio.Insertar(entidad);

                    var articuloStock = (ArticuloDto)_articuloServicio.Obtener(dto.ArticuloId);
                    var stocks = articuloStock.Stocks.ToList();
                     foreach(var stock in stocks)
                    {

                        var entidadStock = _unidadDeTrabajo.StockRepositorio.Obtener(stock.Id);
                        if (entidadStock == null) throw new Exception("Ocurrio un error al obtener el Stock");
                        entidadStock.Cantidad = entidadStock.Cantidad - dto.Cantidad;

                          _unidadDeTrabajo.StockRepositorio.Modificar(entidadStock);
                    }

                    _unidadDeTrabajo.Commit();

                    trans.Complete();
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    throw new Exception(ex.Message);
                }

            }
        }

        public void Modificar(DtoBase dtoEntidad)
        {


            var dto = (BajaArticuloDto)dtoEntidad;

            var entidad = _unidadDeTrabajo.BajaArticuloRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("Ocurrio un error al dar la baja del articulo");
            {
                entidad.Id = dto.Id;
                entidad.ArticuloId = dto.ArticuloId;
                entidad.MotivoBajaId = dto.MotivoBajaId;
                entidad.Cantidad = dto.Cantidad;
                entidad.Observacion = dto.Observacion;


                _unidadDeTrabajo.BajaArticuloRepositorio.Modificar(entidad);

                _unidadDeTrabajo.Commit();


            }


        }

        public DtoBase Obtener(long id)
        {

            var entidad = _unidadDeTrabajo.BajaArticuloRepositorio.Obtener(id, "MotivoBaja,Articulo");
            return new BajaArticuloDto
            {
                Id = entidad.Id,
                Articulo = entidad.Articulo.Descripcion,
                ArticuloId = entidad.ArticuloId,
                MotivoBajaId = entidad.MotivoBajaId,
                MotivoBaja = entidad.MotivoBaja.Descripcion,
                Cantidad = entidad.Cantidad,
                Fecha = entidad.Fecha,
                Observacion = entidad.Observacion,
                Eliminado = entidad.EstaEliminado

            };



        }

        public IEnumerable<DtoBase> Obtener(string cadenaBuscar, bool mostrarTodos = true)
        {
            Expression<Func<Dominio.Entidades.BajaArticulo, bool>> filtro = x =>
            x.Observacion.Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);

            }

            return _unidadDeTrabajo.BajaArticuloRepositorio.Obtener(filtro, "MotivoBaja,Articulo"
                ).Select(x => new BajaArticuloDto
                {
                    Id = x.Id,

                    Articulo = x.Articulo.Descripcion,

                    ArticuloId = x.ArticuloId,
                   
                    Cantidad = x.Cantidad,

                    MotivoBaja = x.MotivoBaja.Descripcion,
                    
                    MotivoBajaId = x.MotivoBajaId,

                    Fecha = x.Fecha,

                    Observacion = x.Observacion,

                    Eliminado = x.EstaEliminado
                })
                .OrderBy(x => x.ArticuloId).ToList();


        }

        public bool VerificarSiExiste(string datoVerificar, long? entidadId = null)
        {
            return entidadId.HasValue
                ? _unidadDeTrabajo.BajaArticuloRepositorio.Obtener(x => !x.EstaEliminado
                                                                        && x.Id != entidadId.Value
                                                                        && x.Observacion.Equals(datoVerificar,
                                                                            StringComparison.CurrentCultureIgnoreCase))
                    .Any()
                : _unidadDeTrabajo.BajaArticuloRepositorio.Obtener(x => !x.EstaEliminado
                                                                        && x.Observacion.Equals(datoVerificar,
                                                                            StringComparison.CurrentCultureIgnoreCase))
                    .Any();
        }



    }
}
