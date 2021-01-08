using System;
using System.Windows.Forms;
using Aplicacion.Constantes;
using IServicio.Seguridad;
using PresentacionBase.Formularios;



namespace CommerceApp
{
    public partial class Login : FormBase
    {
        private readonly ISeguridadServicio _seguridadServicio;
        public bool ingresoSistema;
        public bool ejecutoCancelacion;
        public Login(ISeguridadServicio seguridadServicio)
        {
            InitializeComponent();
            _seguridadServicio = seguridadServicio;
            ingresoSistema = false;
            ejecutoCancelacion = false;
           
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtUsuario.Text)&& !string.IsNullOrEmpty(txtContraseña.Text))
            { 
                bool validacion = false;
                try
                { 
                var user = _seguridadServicio.ObtenerUsuarioLogin(txtUsuario.Text);
                validacion = _seguridadServicio.VerificarAcceso(txtUsuario.Text,txtContraseña.Text);
                
                if (validacion == true)
                {
                    ingresoSistema = true;
                        Identidad.EmpleadoId = user.EmpleadoId;
                        Identidad.Nombre = user.NombreEmpleado;
                        Identidad.Apellido = user.ApellidoEmpleado;
                        Identidad.Foto = user.FotoEmpleado;
                        Identidad.UsuarioId = user.Id;
                        Identidad.Usuario = user.NombreUsuario;
                        Close();
                }
                    else MessageBox.Show("Contraseña incorrecta");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message); ;
                }
            }
            else MessageBox.Show("Debe ingresar el usuario y contraseña");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de salir del sistema"
                 , "Atención"
                 , MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Question) == DialogResult.OK)
            {
                ejecutoCancelacion = true;
                ingresoSistema = false;
                Application.Exit();
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!ingresoSistema)
            {
                if (!ejecutoCancelacion)
                {
                    btnSalir.PerformClick(); 
                }
            }
        }












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
