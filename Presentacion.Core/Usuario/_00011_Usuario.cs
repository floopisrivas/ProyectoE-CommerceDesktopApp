using IServicio.Usuario;
using IServicio.Usuario.DTOs;
using Presentacion.Core.Provincia;
using PresentacionBase.Formularios;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Usuario
{
    public partial class _00011_Usuario : FormBase
    {

        private readonly IUsuarioServicio _usuarioServicio;
        private UsuarioDto _usuario;

        public _00011_Usuario(IUsuarioServicio usuarioServicio)
        {
            InitializeComponent();
            _usuarioServicio = usuarioServicio;
            
        }
           
        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Id"].Visible = false;
            dgv.Columns["Id"].Width = 50;
            dgv.Columns["Id"].HeaderText = "Id";
            dgv.Columns["Id"].DisplayIndex = 0;

            dgv.Columns["NombreUsuario"].Visible = true;
            dgv.Columns["NombreUsuario"].HeaderText = @"Nombre de Usuario";
            dgv.Columns["NombreUsuario"].Width = 350;
            dgv.Columns["NombreUsuario"].DisplayIndex = 1;

            dgv.Columns["ApyNomEmpleado"].Visible = true;
            dgv.Columns["ApyNomEmpleado"].HeaderText = @"Empleado";
            dgv.Columns["ApyNomEmpleado"].Width = 300;
            dgv.Columns["ApyNomEmpleado"].DisplayIndex = 2;

            dgv.Columns["Password"].Visible = false;
            dgv.Columns["Password"].HeaderText = @"Password";
            dgv.Columns["Password"].Width = 80;
            dgv.Columns["Password"].DisplayIndex = 3;
         
            dgv.Columns["EstaBloqueadoStr"].Visible = true;
            dgv.Columns["EstaBloqueadoStr"].HeaderText = @"Bloqueado";
            dgv.Columns["EstaBloqueadoStr"].Width = 150;
            dgv.Columns["EstaBloqueadoStr"].DisplayIndex = 4;

            dgv.Columns["ApellidoEmpleado"].Visible = false;
            dgv.Columns["ApellidoEmpleado"].HeaderText = @"Apellido";
            dgv.Columns["ApellidoEmpleado"].Width = 50;
            dgv.Columns["ApellidoEmpleado"].DisplayIndex = 5;

            dgv.Columns["NombreEmpleado"].Visible = false;
            dgv.Columns["NombreEmpleado"].HeaderText = @"Nombre";
            dgv.Columns["NombreEmpleado"].Width = 50;
            dgv.Columns["NombreEmpleado"].DisplayIndex = 6;

            dgv.Columns["EmpleadoId"].Visible = false;
            dgv.Columns["EmpleadoId"].HeaderText = @"Empleado Id";
            dgv.Columns["EmpleadoId"].Width = 50;
            dgv.Columns["EmpleadoId"].DisplayIndex = 7;
        }

        private void btnNuevo_Click(object sender, System.EventArgs e)
        {
            if (dgvGrilla.RowCount > 0)
            {
                if (_usuario.Id == 0) // Pregunto si tiene un valor
                {
                    _usuarioServicio.Crear(_usuario.EmpleadoId,_usuario.ApellidoEmpleado,_usuario.NombreEmpleado);
                    MessageBox.Show("El usuario fue creado de forma exitosa");
                    ActualizarDatos(dgvGrilla, string.Empty);
                }
                else
                {
                    MessageBox.Show("Ya existe un usuario para este empleado");
                }
            }
            else
            {
                MessageBox.Show("No hay registros Cargados");
            }
        }

        public virtual void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = _usuarioServicio.Obtener(cadenaBuscar, true);
            FormatearGrilla(dgv);
        }
        private void _00011_Usuario_Load(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, string.Empty);
        }

        public virtual void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrilla.RowCount <= 0) return;

            _usuario = (UsuarioDto)dgvGrilla.Rows[e.RowIndex].DataBoundItem;

            if(_usuario.Id == 0)
            {
                btnBloquear.Enabled = false;
                btnReset.Enabled = false;
                btnNuevo.Enabled = true;
            }
            else
            {
                btnBloquear.Enabled = true;
                btnReset.Enabled = true;
                btnNuevo.Enabled = false;
            }
        }

        private void btnBloquear_Click(object sender, EventArgs e)
        {
            if (dgvGrilla.RowCount > 0)
            {
                if (_usuario.Id != 0)
                {
                    _usuarioServicio.Bloquear(_usuario.Id);
                    
                    MessageBox.Show("El usuario fue bloqueado");

                    ActualizarDatos(dgvGrilla, string.Empty);
                }
                else
                {
                    MessageBox.Show("No existe usuario para bloquear");
                }
            }


        }

        private void btnReset_Click(object sender, EventArgs e)
        {

            if (dgvGrilla.RowCount > 0)
            {
                if(_usuario.Id != 0)
                {
                    _usuarioServicio.ResetPassword(_usuario.Id);

                    MessageBox.Show("La contraseña fue reseteada");

                    ActualizarDatos(dgvGrilla, string.Empty);
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un usuario a resetear");
                }

            }

        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, string.Empty);
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, txtBuscar.Text);
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {

            if (dgvGrilla.RowCount > 0)
            {
                if (_usuario.Id != 0)
                {
                    var formulario = new _00056_Cambiar_Password(_usuario.Id);
                    formulario.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un usuario a resetear");
                }

            }

        }


        //**************************************//























        /*
           Universidad Tecnologica Nacional
           Facultad Regional Tucuman
           Tecnicatura Universitaria en Programacion

           Programacion I 2020

           RIVAS, FLORENCIA ANABELA 
           DNI 36040430

           Comision 4
           */

    }
}
