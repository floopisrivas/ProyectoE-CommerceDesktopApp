using Dominio.Entidades;
using Dominio.Repositorio;
using Infraestructura.Repositorio;

namespace Infraestructura.UnidadDeTrabajo
{
    public partial class UnidadDeTrabajo
    {
        // ============================================================================================================ //

        private IRepositorio<Proveedor> proveedorRepositorio;

        public IRepositorio<Proveedor> ProveedorRepositorio => proveedorRepositorio
                                                               ?? (proveedorRepositorio =
                                                                   new Repositorio<Proveedor>(_context));

        // ============================================================================================================ //

        private IEmpleadoRepositorio empleadoRepositorio;

        public IEmpleadoRepositorio EmpleadoRepositorio => empleadoRepositorio
                                                           ?? (empleadoRepositorio = 
                                                               new EmpleadoRepositorio(_context));

        // ============================================================================================================ //

        private IClienteRepositorio clienteRepositorio;

        public IClienteRepositorio ClienteRepositorio => clienteRepositorio
                                                         ?? (clienteRepositorio =
                                                             new ClienteRepositorio(_context));

        // ============================================================================================================ //

        private IRepositorio<Configuracion> configuracionRepositorio;

        public IRepositorio<Configuracion> ConfiguracionRepositorio => configuracionRepositorio
                                                                       ?? (configuracionRepositorio =
                                                                           new Repositorio<Configuracion>(_context));

        // ============================================================================================================ //

        private IRepositorio<ListaPrecio> listaPrecioRepositorio;

        public IRepositorio<ListaPrecio> ListaPrecioRepositorio => listaPrecioRepositorio
                                                                   ?? (listaPrecioRepositorio =
                                                                       new Repositorio<ListaPrecio>(_context));

        // ============================================================================================================ //

        private IRepositorio<Articulo> articuloRepositorio;

        public IRepositorio<Articulo> ArticuloRepositorio => articuloRepositorio
                                                             ?? (articuloRepositorio =
                                                                 new Repositorio<Articulo>(_context));

        // ============================================================================================================ //

        //StockRepositorio

        private IRepositorio<Precio> precioRepositorio;

        public IRepositorio<Precio> PrecioRepositorio => precioRepositorio
                                                             ?? (precioRepositorio =
                                                                 new Repositorio<Precio>(_context));

        // ============================================================================================================ //


        private IRepositorio<Stock> stockRepositorio;

        public IRepositorio<Stock> StockRepositorio => stockRepositorio
                                                             ?? (stockRepositorio =
                                                                 new Repositorio<Stock>(_context));

        // ============================================================================================================ //


        private IRepositorio<BajaArticulo> bajaArticuloRepositorio;

        public IRepositorio<BajaArticulo> BajaArticuloRepositorio => bajaArticuloRepositorio
            ?? (bajaArticuloRepositorio = new Repositorio<BajaArticulo>(_context));

        // ============================================================================================================ //


        private IRepositorio<Contador> contadorRepositorio;

        public IRepositorio<Contador> ContadorRepositorio => contadorRepositorio
            ?? (contadorRepositorio = new Repositorio<Contador>(_context));

        // ============================================================================================================ //


        private IRepositorio<Caja> cajaRepositorio;

        public IRepositorio<Caja> CajaRepositorio => cajaRepositorio
            ?? (cajaRepositorio = new Repositorio<Caja>(_context));

        // ============================================================================================================ //

        private IRepositorio<DetalleCaja> detalleCajaRepositorio;

        public IRepositorio<DetalleCaja> DetalleCajaRepositorio => detalleCajaRepositorio
            ?? (detalleCajaRepositorio = new Repositorio<DetalleCaja>(_context));

        // ============================================================================================================ //
        
        private IRepositorio<ConceptoGasto> conceptoGastoRepositorio;

        public IRepositorio<ConceptoGasto> ConceptoGastoRepositorio => conceptoGastoRepositorio
            ?? (conceptoGastoRepositorio = new Repositorio<ConceptoGasto>(_context));

        // ============================================================================================================ //


        private IRepositorio<Factura> facturaRepositorio;

        public IRepositorio<Factura> FacturaRepositorio => facturaRepositorio
            ?? (facturaRepositorio = new Repositorio<Factura>(_context));

        // ============================================================================================================ //

        private IRepositorio<Comprobante> comprobanteRepositorio;

        public IRepositorio<Comprobante> ComprobanteRepositorio => comprobanteRepositorio
            ?? (comprobanteRepositorio = new Repositorio<Comprobante>(_context));

        // ============================================================================================================ //
        private IRepositorio<MovimientoCuentaCorriente> movimientoCuentaCorrienteRepositorio;

        public IRepositorio<MovimientoCuentaCorriente> MovimientoCuentaCorrienteRepositorio => movimientoCuentaCorrienteRepositorio
            ?? (movimientoCuentaCorrienteRepositorio = new Repositorio<MovimientoCuentaCorriente>(_context));

        // ============================================================================================================ //

        private ICuentaCorrienteRepositorio cuentaCorrienteRepositorio;

        public ICuentaCorrienteRepositorio CuentaCorrienteRepositorio => cuentaCorrienteRepositorio
                                                           ?? (cuentaCorrienteRepositorio =
                                                               new CuentaCorrienteRepositorio(_context));

        // ============================================================================================================ //

    }
}
