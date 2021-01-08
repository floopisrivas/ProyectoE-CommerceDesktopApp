using Aplicacion.Constantes;
using IServicios.Caja;
using IServicios.Caja.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Caja
{
    public partial class _00040_CierreCaja : FormBase
    {
        private readonly long _cajaId;

        private readonly ICajaServicio _cajaServicio;

        private CajaDto _caja;


        public _00040_CierreCaja(long cajaId)
        {
            InitializeComponent();
            _cajaId = cajaId;

            _cajaServicio = ObjectFactory.GetInstance<ICajaServicio>();

            CargarDatos(_cajaId);


        }

        private void CargarDatos(long cajaId)
        {
            _caja = _cajaServicio.Obtener(cajaId);

            if(_caja == null)
            {
                MessageBox.Show("Ocurrio un error al obtener la caja");
                Close();

            }

            txtCajaInicial.Text = _caja.MontoAperturaStr;

             var efectivo = _caja.Detalles.
                Where(x => x.TipoPago == TipoPago.Efectivo).Sum(x => x.Monto);

            var cheque = _caja.MontoAperturaStr;
            txtCheque.Text = _caja.Detalles.
                Where(x => x.TipoPago == TipoPago.Cheque).Sum(x => x.Monto).ToString("C");

            var tarjeta = _caja.MontoAperturaStr;
            txtTarjeta.Text = _caja.Detalles.
                Where(x => x.TipoPago == TipoPago.Tarjeta).Sum(x => x.Monto).ToString("C");


            var ctaCte = _caja.MontoAperturaStr;
            txtCheque.Text = _caja.Detalles.
                Where(x => x.TipoPago == TipoPago.CtaCte).Sum(x => x.Monto).ToString("C");

            nudTotalEfectivo.Value = efectivo;
            txtVentas.Text = efectivo.ToString("C");
            txtCheque.Text = cheque;
            txtTarjeta.Text = tarjeta;
            txtCtaCte.Text = ctaCte;



        }

        private void btnVerDetalleVenta_Click(object sender, EventArgs e)
        {
            var fVerComprobantes = new VerComprobantesCaja(_caja.Comprobantes);

            fVerComprobantes.ShowDialog();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            try
            {
                _cajaServicio.Cerrar(_cajaId, Identidad.UsuarioId, nudTotalEfectivo.Value );
                MessageBox.Show("La caja se cerro correctamente");
                Close();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
