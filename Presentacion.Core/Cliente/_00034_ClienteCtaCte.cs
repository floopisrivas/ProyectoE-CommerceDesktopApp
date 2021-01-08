using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IServicio.Persona.DTOs;
using IServicios.CuentaCorriente;
using IServicios.CuentaCorriente.DTOs;
using PresentacionBase.Formularios;
using StructureMap;

namespace Presentacion.Core.Cliente
{
    public partial class _00034_ClienteCtaCte : FormBase
    {
        private ClienteDto _clienteSeleccionado;
        private ICuentaCorrienteServicio _cuentaCorrienteServicio;

        public _00034_ClienteCtaCte(ICuentaCorrienteServicio cuentaCorrienteServicio)
        {
            InitializeComponent();
            _cuentaCorrienteServicio = cuentaCorrienteServicio;

            dgvGrilla.DataSource = new List<CuentaCorrienteDto>();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            var fClienteUp = ObjectFactory.GetInstance<ClienteLookUp>();
            fClienteUp.ShowDialog();

            if (fClienteUp.EntidadSeleccionada != null)
            { 
                _clienteSeleccionado = (ClienteDto) fClienteUp.EntidadSeleccionada;

                txtApyNom.Text = _clienteSeleccionado.ApyNom;
                txtCelular.Text = _clienteSeleccionado.Telefono;
                txtDni.Text = _clienteSeleccionado.Dni;

                CargarDatos();


            }
            else
            {
                txtCelular.Clear();
                txtApyNom.Clear();
                txtDni.Clear();

                _clienteSeleccionado = null;

                dgvGrilla.DataSource = new List<CuentaCorrienteDto>();

            }

        }

        private void CargarDatos()
        {

            dgvGrilla.DataSource = _cuentaCorrienteServicio.Obtener(dtpfechaDesde.Value, dtpfechaHasta.Value, rdbDeuda.Checked);
            FormatearGrilla(dgvGrilla);

        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].HeaderText = @"Descripción";


            dgv.Columns["FechaStr"].Visible = true;
            dgv.Columns["FechaStr"].Width = 100;
            dgv.Columns["FechaStr"].HeaderText = @"Fecha";

            dgv.Columns["HoraStr"].Visible = true;
            dgv.Columns["HoraStr"].Width = 100;
            dgv.Columns["HoraStr"].HeaderText = @"Hora";

            dgv.Columns["MontoStr"].Visible = true;
            dgv.Columns["MontoStr"].Width = 100;
            dgv.Columns["MontoStr"].HeaderText = @"Monto";


        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();

        }

        private void dtpfechaDesde_ValueChanged(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void dtpfechaHasta_ValueChanged(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void rdbDeuda_CheckedChanged(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
}
