using IServicio.Seguridad;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Comprobantes.Clases
{
    public partial class AutorizacionListaPrecio : Form
    {
        private readonly ISeguridadServicio _seguridad;

        private bool _tienePermiso;
        public bool PermisoAutorizado => _tienePermiso;

        public AutorizacionListaPrecio(ISeguridadServicio seguridad)
        {
            InitializeComponent();

            _seguridad = seguridad;
            _tienePermiso = false;
        }

    
        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            try
            {
                _tienePermiso = _seguridad.VerificarAcceso(txtUsuario.Text, txtPassword.Text);

                if (_tienePermiso)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El usuario o el Password son Icorrectos");
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            _tienePermiso = false;
            Close();
        }

        private void btnVerPassword_MouseDown_1(object sender, MouseEventArgs e)
        {
            txtPassword.PasswordChar = Char.MinValue;
        }

        private void btnVerPassword_MouseUp_1(object sender, MouseEventArgs e)
        {
            txtPassword.PasswordChar = Char.Parse("*");
        }
    }
}
