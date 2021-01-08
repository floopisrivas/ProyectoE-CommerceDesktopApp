using IServicio.Persona;
using IServicio.Persona.DTOs;
using PresentacionBase.Formularios;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Cliente
{
    public partial class ClienteLookUp : FormLookUp
    {
        private readonly IClienteServicio _clienteServicio;

        public ClienteLookUp(IClienteServicio clienteServicio)
        {
            InitializeComponent();
            _clienteServicio = clienteServicio;
            EntidadSeleccionada = null;

        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {

            var clientes = (List<ClienteDto>)_clienteServicio
                 .Obtener(typeof(ClienteDto), !string.IsNullOrEmpty(cadenaBuscar)
                 ? cadenaBuscar : string.Empty, false);

            dgv.DataSource = clientes.Where(x => x.Dni != Aplicacion.Constantes.Cliente.ConsumidorFinal).ToList();

            FormatearGrilla(dgv);

        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["ApyNom"].Visible = true;
            dgv.Columns["ApyNom"].Width = 150;
            dgv.Columns["ApyNom"].HeaderText = "Apellido y Nombre ";
            dgv.Columns["ApyNom"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["ApyNom"].DisplayIndex = 0;

            dgv.Columns["Dni"].Visible = true;
            dgv.Columns["Dni"].Width = 150;
            dgv.Columns["Dni"].HeaderText = "Dni";
            dgv.Columns["Dni"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Dni"].DisplayIndex = 1;

            dgv.Columns["Telefono"].Visible = true;
            dgv.Columns["Telefono"].HeaderText = @"Teléfono";
            dgv.Columns["Telefono"].Width = 150;
            dgv.Columns["Telefono"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Telefono"].DisplayIndex = 2;

            dgv.Columns["CtaCteStr"].Visible = true;
            dgv.Columns["CtaCteStr"].Width = 150;
            dgv.Columns["CtaCteStr"].HeaderText = "Cuenta corriente";
            dgv.Columns["CtaCteStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["CtaCteStr"].DisplayIndex = 4;


            dgv.Columns["LimiteCompraStr"].Visible = true;
            dgv.Columns["LimiteCompraStr"].Width = 90;
            dgv.Columns["LimiteCompraStr"].HeaderText = "Tiene Limite";
            dgv.Columns["LimiteCompraStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["LimiteCompraStr"].DisplayIndex = 5;

            dgv.Columns["MontoMaximoCtaCteStr"].Visible = true;
            dgv.Columns["MontoMaximoCtaCteStr"].HeaderText = @"Monto limite";
            dgv.Columns["MontoMaximoCtaCteStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["MontoMaximoCtaCteStr"].Width = 150;
            dgv.Columns["MontoMaximoCtaCteStr"].DisplayIndex =6;



        }

    }
}
