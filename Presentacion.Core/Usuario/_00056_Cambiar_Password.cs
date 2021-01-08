using IServicio.Usuario;
using PresentacionBase.Formularios;
using StructureMap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Usuario
{
    public partial class _00056_Cambiar_Password : FormBase
    {
        
        private readonly IUsuarioServicio _usuarioServicio;
        long id;
        public _00056_Cambiar_Password(long entidadId)
        {
            InitializeComponent();
            _usuarioServicio = ObjectFactory.GetInstance<IUsuarioServicio>();
            id = entidadId;
        }

        public bool VerificarDatos()
        {
            if (string.IsNullOrEmpty(txtConfirmar.Text)) return false;
            if (string.IsNullOrEmpty(txtPassword.Text)) return false;
            return true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmar.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden");
            }
            else
            {
                if (VerificarDatos())
                { 
                    _usuarioServicio.CambiarPassword(id, txtPassword.Text);
                     MessageBox.Show("La contraseña se cambió correctamente");
                    this.Close();
                }
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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
