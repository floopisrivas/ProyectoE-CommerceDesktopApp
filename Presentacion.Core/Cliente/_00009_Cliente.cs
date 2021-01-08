using IServicio.Departamento;
using IServicio.Persona;
using IServicio.Persona.DTOs;
using PresentacionBase.Formularios;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Cliente
{
    public partial class _00009_Cliente : FormConsulta
    {
        private readonly IClienteServicio _clienteServicio;
       
        public _00009_Cliente(IClienteServicio clienteServicio)
        {
            InitializeComponent();
            _clienteServicio = clienteServicio;
            
        }


        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {

            var resultado = (List<ClienteDto>)_clienteServicio
                 .Obtener(typeof(ClienteDto), !string.IsNullOrEmpty(cadenaBuscar)
                 ? cadenaBuscar : string.Empty);

            dgv.DataSource = resultado.Where(x => x.Dni != Aplicacion.Constantes.Cliente.ConsumidorFinal).ToList();


            base.ActualizarDatos(dgv, cadenaBuscar);
        }

        public override bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {
            var formulario = new _00010_Abm_Cliente(tipoOperacion, id);
            formulario.ShowDialog();
            return base.EjecutarComando(tipoOperacion, id);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

           
           
            dgv.Columns["Apellido"].Visible = true;
            dgv.Columns["Apellido"].Width = 140;
            dgv.Columns["Apellido"].HeaderText = "Apellido ";
            dgv.Columns["Apellido"].DisplayIndex = 0;

            dgv.Columns["Nombre"].Visible = true;
            dgv.Columns["Nombre"].HeaderText = @"Nombre";
            dgv.Columns["Nombre"].Width = 140;
            dgv.Columns["Nombre"].DisplayIndex = 1;

            dgv.Columns["Dni"].Visible = true;
            dgv.Columns["Dni"].Width = 80;
            dgv.Columns["Dni"].HeaderText = "DNI";
            dgv.Columns["Dni"].DisplayIndex = 2;

            dgv.Columns["Direccion"].Visible = true;
            dgv.Columns["Direccion"].HeaderText = @"Dirección";
            dgv.Columns["Direccion"].Width = 120;
            dgv.Columns["Direccion"].DisplayIndex = 3;

            dgv.Columns["Telefono"].Visible = true;
            dgv.Columns["Telefono"].HeaderText = @"Teléfono";
            dgv.Columns["Telefono"].Width = 80;
            dgv.Columns["Telefono"].DisplayIndex = 4;


            dgv.Columns["Mail"].Visible = true;
            dgv.Columns["Mail"].HeaderText = @"Email";
            dgv.Columns["Mail"].Width = 100;
            dgv.Columns["Mail"].DisplayIndex = 5;


            dgv.Columns["Localidad"].Visible = true;
            dgv.Columns["Localidad"].HeaderText = @"Localidad";
            dgv.Columns["Localidad"].Width = 80;
            dgv.Columns["Localidad"].DisplayIndex = 6;


            dgv.Columns["CondicionIva"].Visible = true;
            dgv.Columns["CondicionIva"].Width = 100;
            dgv.Columns["CondicionIva"].HeaderText = "Condicion Iva";
            dgv.Columns["CondicionIva"].DisplayIndex = 7;


            dgv.Columns["ActivarCtaCte"].Visible = true;
            dgv.Columns["ActivarCtaCte"].Width = 60;
            dgv.Columns["ActivarCtaCte"].HeaderText = "Activar cuenta corriente";
            dgv.Columns["ActivarCtaCte"].DisplayIndex = 8;


            dgv.Columns["TieneLimiteCompra"].Visible = true;
            dgv.Columns["TieneLimiteCompra"].Width = 60;
            dgv.Columns["TieneLimiteCompra"].HeaderText = "Tiene Limite Compra";
            dgv.Columns["TieneLimiteCompra"].DisplayIndex = 9;


            dgv.Columns["MontoMaximoCtaCte"].Visible = true;
            dgv.Columns["MontoMaximoCtaCte"].HeaderText = @"Monto Maximo CtaCte";
            dgv.Columns["MontoMaximoCtaCte"].Width = 60;
            dgv.Columns["MontoMaximoCtaCte"].DisplayIndex = 10;


            dgv.Columns["EliminadoStr"].Visible = true;
            dgv.Columns["EliminadoStr"].HeaderText = @"Eliminado";
            dgv.Columns["EliminadoStr"].Width = 50;



        }


    }
}
