using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Forms;
using IServicios.Comprobante.DTOs;
using IServicios.Comprobantes;
using PresentacionBase.Formularios;

namespace Presentacion.Core.FormaPago
{
    public partial class _00049_CobroDiferido : FormBase
    {

        private readonly IFacturaServicio _facturaServicio;

        private ComprobantePendienteDto comprobanteSeleccionado;
        public _00049_CobroDiferido(IFacturaServicio facturaServicio)
        {
            InitializeComponent();
            _facturaServicio = facturaServicio;


            comprobanteSeleccionado = null;
            //dgvGrillaPedientePago.DataSource = new List<ComprobantePendienteDto>();
            //FormatearGrilla(dgvGrillaPedientePago);

            //dgvGrillaDetalleComprobante.DataSource = new List<DetallePendienteDto>();
            //FormatearGrillaDetalle(dgvGrillaDetalleComprobante);

            CargarDatos();
            // Libreria para que refresque cada 60 seg la grilla
            // con las facturas que estan pendientes de pago.

            Observable.Interval(TimeSpan.FromSeconds(30))
                .ObserveOn(DispatcherScheduler.Current)
                .Subscribe(_ => 
                {

                    CargarDatos();
                });

        }


        private void CargarDatos()
        {
            dgvGrillaPedientePago.DataSource = null;
            dgvGrillaPedientePago.DataSource = _facturaServicio.ObtenerPendientesPago();

            FormatearGrilla(dgvGrillaPedientePago);
        }
        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);


            dgv.Columns["Numero"].Visible = true;
            dgv.Columns["Numero"].Width = 100;
            dgv.Columns["Numero"].HeaderText = "Numero";
            dgv.Columns["Numero"].DisplayIndex = 0;

            dgv.Columns["Cliente"].Visible = true;
            dgv.Columns["Cliente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Cliente"].HeaderText = "Cliente";
            dgv.Columns["Cliente"].DisplayIndex = 1;

            dgv.Columns["MontoPagarStr"].Visible = true;
            dgv.Columns["MontoPagarStr"].Width = 150;
            dgv.Columns["MontoPagarStr"].HeaderText = "Monto Pagar";
            dgv.Columns["MontoPagarStr"].DisplayIndex = 0;


        }

        private void dgvGrillaPedientePago_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrillaPedientePago.RowCount > 0)
            {
                comprobanteSeleccionado = null;

                return;
            } 
            

           comprobanteSeleccionado = (ComprobantePendienteDto)dgvGrillaPedientePago.Rows[e.RowIndex].DataBoundItem;

            if (comprobanteSeleccionado != null) return;
                
            nudTotal.Value = comprobanteSeleccionado.MontoPagar;

            dgvGrillaDetalleComprobante.DataSource = null;


            dgvGrillaDetalleComprobante.DataSource = comprobanteSeleccionado.Items.ToList();

             FormatearGrillaDetalle(dgvGrillaDetalleComprobante);
                
            
        }

        private void FormatearGrillaDetalle(DataGridView dgv)
        {
            for(int i = 0; i < dgv.ColumnCount; i++)
            {

                dgv.Columns[i].Visible = false;

                dgv.Columns["Descripcion"].Visible = true;
                dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns["Descripcion"].HeaderText = @"Articulo";
                dgv.Columns["Descripcion"].DisplayIndex = 0;

                dgv.Columns["PrecioStr"].Visible = true;
                dgv.Columns["PrecioStr"].Width = 100;
                dgv.Columns["PrecioStr"].HeaderText = @"Precio";
                dgv.Columns["PrecioStr"].DisplayIndex = 1;

               

                dgv.Columns["Cantidad"].Visible = true;
                dgv.Columns["Cantidad"].Width = 150;
                dgv.Columns["Cantidad"].HeaderText = @"Cantidad";
                dgv.Columns["Cantidad"].DisplayIndex = 0;




            }
        }

        private void dgvGrillaPedientePago_DoubleClick(object sender, EventArgs e)
        {
            var fFormaDePago = new _00044_FormaPago(comprobanteSeleccionado);
            fFormaDePago.ShowDialog();

            if(fFormaDePago.RealizoVenta)
            {
                MessageBox.Show("Los datos se grabaron correctamente");

            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if(comprobanteSeleccionado != null)
            {
                var fFormaDePago = new _00044_FormaPago(comprobanteSeleccionado);
                fFormaDePago.ShowDialog();
                if(fFormaDePago.RealizoVenta)
                {
                    CargarDatos();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un comprobante");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
