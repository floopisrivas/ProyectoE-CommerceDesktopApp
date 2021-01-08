using Aplicacion.Constantes;
using IServicios.Caja;
using IServicios.Caja.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Caja
{
    public partial class _00038_Caja : FormBase
    {
        private readonly ICajaServicio _cajaServicio;
        private CajaDto _cajaSeleccionada;

        public _00038_Caja(ICajaServicio cajaServicio)
        {
            InitializeComponent();
            _cajaServicio = cajaServicio;
            _cajaSeleccionada = null;

        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            if(!_cajaServicio.VerificarSiExisteCajaAbierta(Identidad.UsuarioId))
            {
                var fAbrirCaja = ObjectFactory.GetInstance<_00039_AperturaCaja>();
                fAbrirCaja.ShowDialog();

                ActualizarDatos(string.Empty, false, DateTime.Today, DateTime.Today);
            }
            else
            {
                MessageBox.Show($"Se encuentra una caja habilitada para el usuario {Identidad.Apellido} {Identidad.Nombre}");
            }
        }

        private void ActualizarDatos(String cadenaBuscar, bool filtroPorfecha, DateTime fechaDesde, DateTime fechaHasta)
        {
            dgvGrilla.DataSource = _cajaServicio.Obtener(cadenaBuscar, filtroPorfecha, fechaDesde, fechaHasta);

            FormatearGrilla(dgvGrilla);
        }

        private void chkRangoFecha_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaDesde.Enabled = chkRangoFecha.Checked;
            dtpFechaHasta.Enabled = chkRangoFecha.Checked;

            if (!chkRangoFecha.Checked) return;
            {
                dtpFechaDesde.Value = DateTime.Now;
                dtpFechaHasta.Value = DateTime.Now;
            }
        }

        private void dtpFechaDesde_ValueChanged(object sender, EventArgs e)
        {
            dtpFechaHasta.Value = dtpFechaDesde.Value;
            dtpFechaHasta.Value = dtpFechaDesde.Value;

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(!string.IsNullOrEmpty(txtBuscar.Text) ? txtBuscar.Text : string.Empty, 
                chkRangoFecha.Checked, dtpFechaDesde.Value,dtpFechaHasta.Value);
        }

        private void _00038_Caja_Load(object sender, EventArgs e)
        {
            dtpFechaDesde.Value = DateTime.Today;
            dtpFechaHasta.Value = DateTime.Today;


            ActualizarDatos(!string.IsNullOrEmpty(txtBuscar.Text) ? txtBuscar.Text : string.Empty,
                chkRangoFecha.Checked, dtpFechaDesde.Value, dtpFechaHasta.Value);

        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["UsuarioApertura"].Visible = true;
            dgv.Columns["UsuarioApertura"].HeaderText = @"Usuario Apertura";
            dgv.Columns["UsuarioApertura"].Width = 150;
            dgv.Columns["UsuarioApertura"].DisplayIndex = 0;

            dgv.Columns["FechaAperturaStr"].Visible = true;
            dgv.Columns["FechaAperturaStr"].HeaderText = @"Fecha Apertura";
            dgv.Columns["FechaAperturaStr"].Width = 70;
            dgv.Columns["FechaAperturaStr"].DisplayIndex = 1;

            dgv.Columns["MontoAperturaStr"].Visible = true;
            dgv.Columns["MontoAperturaStr"].Width = 70;
            dgv.Columns["MontoAperturaStr"].HeaderText = @"Monto Apertura";
            dgv.Columns["MontoAperturaStr"].DisplayIndex = 2;

            dgv.Columns["UsuarioCierre"].Visible = true;
            dgv.Columns["UsuarioCierre"].Width = 150;
            dgv.Columns["UsuarioCierre"].HeaderText = @"Usuario Cierre";
            dgv.Columns["UsuarioCierre"].DisplayIndex = 3;

            dgv.Columns["FechaCierreStr"].Visible = true;
            dgv.Columns["FechaCierreStr"].Width = 70;
            dgv.Columns["FechaCierreStr"].HeaderText = @"Fecha Cierre";
            dgv.Columns["FechaCierreStr"].DisplayIndex = 4;

            dgv.Columns["MontoCierreStr"].Visible = true;
            dgv.Columns["MontoCierreStr"].Width = 70;
            dgv.Columns["MontoCierreStr"].HeaderText = @"Monto Cierre ";
            dgv.Columns["MontoCierreStr"].DisplayIndex = 5;

            //*****************************************//

            dgv.Columns["TotalEntradaEfectivoStr"].Visible = true;
            dgv.Columns["TotalEntradaEfectivoStr"].Width = 70;
            dgv.Columns["TotalEntradaEfectivoStr"].HeaderText = @"Entrada Efectivo";
            dgv.Columns["TotalEntradaEfectivoStr"].DisplayIndex = 6;

            dgv.Columns["TotalEntradaCtaCteStr"].Visible = true;
            dgv.Columns["TotalEntradaCtaCteStr"].Width = 70;
            dgv.Columns["TotalEntradaCtaCteStr"].HeaderText = @"Entrada CtaCte";
            dgv.Columns["TotalEntradaCtaCteStr"].DisplayIndex = 7;

            dgv.Columns["TotalEntradaTarjetaStr"].Visible = true;
            dgv.Columns["TotalEntradaTarjetaStr"].Width = 70;
            dgv.Columns["TotalEntradaTarjetaStr"].HeaderText = @"Entrada Tarjeta";
            dgv.Columns["TotalEntradaTarjetaStr"].DisplayIndex = 8;


            dgv.Columns["TotalEntradaChequeStr"].Visible = true;
            dgv.Columns["TotalEntradaChequeStr"].Width = 70;
            dgv.Columns["TotalEntradaChequeStr"].HeaderText = @"Entrada Cheque";
            dgv.Columns["TotalEntradaChequeStr"].DisplayIndex = 8;


            //***********************************************//



            dgv.Columns["TotalSalidaEfectivoStr"].Visible = true;
            dgv.Columns["TotalSalidaEfectivoStr"].Width = 70;
            dgv.Columns["TotalSalidaEfectivoStr"].HeaderText = @"Salida Efectivo";
            dgv.Columns["TotalSalidaEfectivoStr"].DisplayIndex = 9;


            dgv.Columns["TotalSalidaCtaCteStr"].Visible = true;
            dgv.Columns["TotalSalidaCtaCteStr"].Width = 70;
            dgv.Columns["TotalSalidaCtaCteStr"].HeaderText = @"Salida CtaCte";
            dgv.Columns["TotalSalidaCtaCteStr"].DisplayIndex = 10;


            dgv.Columns["TotalSalidaTarjetaStr"].Visible = true;
            dgv.Columns["TotalSalidaTarjetaStr"].Width = 70;
            dgv.Columns["TotalSalidaTarjetaStr"].HeaderText = @"Salida Tarjeta ";
            dgv.Columns["TotalSalidaTarjetaStr"].DisplayIndex = 11;


            dgv.Columns["TotalSalidaChequeStr"].Visible = true;
            dgv.Columns["TotalSalidaChequeStr"].Width = 70;
            dgv.Columns["TotalSalidaChequeStr"].HeaderText = @"Salida Cheque";
            dgv.Columns["TotalSalidaChequeStr"].DisplayIndex = 12;
            //*************************************************//


        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCierreCaja_Click(object sender, EventArgs e)
        {
            var fCierreCaja = new _00040_CierreCaja(_cajaSeleccionada.Id);
            fCierreCaja.ShowDialog();
            ActualizarDatos(string.Empty, chkRangoFecha.Checked,dtpFechaDesde.Value,dtpFechaHasta.Value);
        }

        private void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrilla.RowCount <= 0)
            {
                _cajaSeleccionada = null;
                return;
            }
            _cajaSeleccionada = (CajaDto)dgvGrilla.Rows[e.RowIndex].DataBoundItem;

        }
    }
}
