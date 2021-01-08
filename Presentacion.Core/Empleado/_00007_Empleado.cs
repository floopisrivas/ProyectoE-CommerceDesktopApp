using IServicio.Persona;
using IServicio.Persona.DTOs;
using PresentacionBase.Formularios;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Presentacion.Core.Empleado
{
    public partial class _00007_Empleado : FormConsulta
    {

        private readonly IEmpleadoServicio _empleadoServicio;

        public _00007_Empleado(IEmpleadoServicio empleadoServicio)
        {
            InitializeComponent();
            _empleadoServicio = empleadoServicio;


        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
           

            dgv.DataSource = (List<EmpleadoDto>)_empleadoServicio
                .Obtener(typeof(EmpleadoDto), !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            FormatearGrilla(dgv);


        }


        public override bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {
            var form = new _00008_Abm_Empleado(tipoOperacion, id);
            form.ShowDialog();
            return form.RealizoAlgunaOperacion;
            
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Id"].Visible = false;
            dgv.Columns["Id"].Width = 40;
            dgv.Columns["Id"].HeaderText = "Id";
            dgv.Columns["Id"].DisplayIndex = 0;


            dgv.Columns["Apellido"].Visible = true;
            dgv.Columns["Apellido"].Width = 150;
            dgv.Columns["Apellido"].HeaderText = "Apellido ";
            dgv.Columns["Apellido"].DisplayIndex = 1;


            dgv.Columns["Nombre"].Visible = true;
            dgv.Columns["Nombre"].HeaderText = @"Nombre";
            dgv.Columns["Nombre"].Width = 150;
            dgv.Columns["Nombre"].DisplayIndex = 2;

            dgv.Columns["Dni"].Visible = true;
            dgv.Columns["Dni"].Width = 120;
            dgv.Columns["Dni"].HeaderText = "Dni";
            dgv.Columns["Dni"].DisplayIndex = 3;

            dgv.Columns["Direccion"].Visible = true;
            dgv.Columns["Direccion"].HeaderText = @"Dirección";
            dgv.Columns["Direccion"].Width = 120;
            dgv.Columns["Direccion"].DisplayIndex = 4;

            dgv.Columns["Telefono"].Visible = true;
            dgv.Columns["Telefono"].HeaderText = @"Teléfono";
            dgv.Columns["Telefono"].Width = 120;
            dgv.Columns["Telefono"].DisplayIndex = 5;

            dgv.Columns["Mail"].Visible = true;
            dgv.Columns["Mail"].HeaderText = @"Email";
            dgv.Columns["Mail"].Width = 170;
            dgv.Columns["Mail"].DisplayIndex = 6;

            dgv.Columns["Localidad"].Visible = true;
            dgv.Columns["Localidad"].HeaderText = @"Localidad";
            dgv.Columns["Localidad"].Width = 140;
            dgv.Columns["Localidad"].DisplayIndex = 7;

            dgv.Columns["EliminadoStr"].Visible = true;
            dgv.Columns["EliminadoStr"].HeaderText = @"Eliminado";
            dgv.Columns["EliminadoStr"].Width = 80;
           




        }




    }
}
