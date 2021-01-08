using IServicio.Persona;
using IServicio.Persona.DTOs;
using PresentacionBase.Formularios;
using System.Windows.Forms;

namespace Presentacion.Core.Empleado
{
    public partial class EmpleadoLookUp : FormLookUp
    {
        private readonly IEmpleadoServicio _empleadoServicio;

        public EmpleadoLookUp(IEmpleadoServicio empleadoServicio)
        {
            InitializeComponent();
            _empleadoServicio = empleadoServicio;
            EntidadSeleccionada = null;
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = _empleadoServicio.Obtener(typeof(EmpleadoDto), cadenaBuscar);

            base.ActualizarDatos(dgv, cadenaBuscar);
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

          

        }



    }
}
