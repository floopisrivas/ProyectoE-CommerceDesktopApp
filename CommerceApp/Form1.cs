using IServicio.Seguridad;
using Presentacion.Core.Articulo;
using Presentacion.Core.Cliente;
using Presentacion.Core.Comprobantes;
using Presentacion.Core.CondicionIva;
using Presentacion.Core.Configuracion;
using Presentacion.Core.Empleado;
using Presentacion.Core.Proveedor;
using Presentacion.Core.Usuario;
using Presentacion.Core.Caja;
using PresentacionBase.Formularios;
using StructureMap;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Aplicacion.Constantes;
using IServicios.Caja;
using Presentacion.Core.FormaPago;

namespace CommerceApp
{
    public partial class Form1 : FormBase
    {

        private readonly ICajaServicio _cajaServicio;


        public Form1(ICajaServicio cajaServicio)
        {
            InitializeComponent();
            _cajaServicio = cajaServicio;
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 250)
            {
                MenuVertical.Width = 70;
            }
            else
                MenuVertical.Width = 250;
        }



        private void iconcerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconmaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            iconrestaurar.Visible = true;
            iconmaximizar.Visible = false;
        }

        private void iconrestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            iconrestaurar.Visible = false;
            iconmaximizar.Visible = true;
        }

        private void iconminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //****************************************************************//
        private void btnprod_Click(object sender, EventArgs e)
        {
            if (SubMenuArticuloss.Visible == false)
                SubMenuArticuloss.Visible = true;
            else
                SubMenuArticuloss.Visible = false;



        }

        //****************************************************************//

        private void btnlogoInicio_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            var fLogin = new Login(ObjectFactory.GetInstance<ISeguridadServicio>());
            fLogin.ShowDialog();

            btnlogoInicio_Click(null, e);
        }



        //****************************************************************//
        private void btnConsultarUsuarios_Click(object sender, EventArgs e)
        {
            SubMenuUsuario.Visible = false;

            ObjectFactory.GetInstance<_00011_Usuario>().ShowDialog();
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {

            if (SubMenuUsuario.Visible == false)
                SubMenuUsuario.Visible = true;
            else
                SubMenuUsuario.Visible = false;
        }
        //*******************************************************************//
        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            if (SubMenuEmpleados.Visible == false)
                SubMenuEmpleados.Visible = true;
            else
                SubMenuEmpleados.Visible = false;
        }

        private void btnConsultarEmpleados_Click(object sender, EventArgs e)
        {
            SubMenuEmpleados.Visible = false;

            openChild(ObjectFactory.GetInstance<_00007_Empleado>());

            
        }
        //*******************************************************************//
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            if (SubMenuProv.Visible == false)
                SubMenuProv.Visible = true;
            else
                SubMenuProv.Visible = false;
        }

        private void btnConsultarProveedores_Click(object sender, EventArgs e)
        {
            SubMenuProv.Visible = false;

            openChild(ObjectFactory.GetInstance<_00015_Proveedor>());
            

        }
        //*******************************************************************//
        private void btnComprass_Click(object sender, EventArgs e)
        {
            if (SubMenuCompras.Visible == false)
                SubMenuCompras.Visible = true;
            else
                SubMenuCompras.Visible = false;
        }

        private void btnConsultarCompras_Click(object sender, EventArgs e)
        {
            SubMenuCompras.Visible = false;

            openChild(ObjectFactory.GetInstance<_00053_Compra>());
           
        }
        //*******************************************************************//
        private void btnClientes_Click(object sender, EventArgs e)
        {



            if (SubMenClientes.Visible == false)
                SubMenClientes.Visible = true;
            else
                SubMenClientes.Visible = false;

        }

        private void btnConsultarVentas_Click(object sender, EventArgs e)
        {


            SubMenuVentas.Visible = false;

            if(_cajaServicio.VerificarSiExisteCajaAbierta(Identidad.UsuarioId))
            {
                openChild(ObjectFactory.GetInstance<_00050_Venta>());
            }
            else
            {
               if (MessageBox.Show("La caja aun no esta abierta.Desea hacerlo ahora?", 
                    "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
               {
                    var formcaja = ObjectFactory.GetInstance<_00039_AperturaCaja>();
                    formcaja.ShowDialog();
                    if(formcaja.CajaABierta)
                    {
                        openChild(ObjectFactory.GetInstance<_00050_Venta>());
                        Show();
                    }

               }

            }
           

            
        }

        private void btnClientConsl_Click(object sender, EventArgs e)
        {
            SubMenClientes.Visible = false;

            openChild(ObjectFactory.GetInstance<_00009_Cliente>());
           

        }

        private void btnVentas_Click_1(object sender, EventArgs e)
        {
            if (SubMenuVentas.Visible == false)
                SubMenuVentas.Visible = true;
            else
                SubMenuVentas.Visible = false;
        }


        //*******************************************************************//

        private void btnPuestoTrabajoConsultar_Click(object sender, EventArgs e)
        {
            SubMenuVentas.Visible = false;

            openChild(ObjectFactory.GetInstance<_00051_PuestoTrabajo>());
            
        }



        //*******************************************************************//

        private void btnConsArtic_Click(object sender, EventArgs e)
        {
            SubMenuArticuloss.Visible = false;

            openChild(ObjectFactory.GetInstance<_00017_Articulo>());
           
        }

        private void btnConsArticLisPrec_Click(object sender, EventArgs e)
        {
            SubMenuArticuloss.Visible = false;

            openChild(ObjectFactory.GetInstance<_00032_ListaPrecio>());
            
        }

        private void btnConsArticMarca_Click(object sender, EventArgs e)
        {
            SubMenuArticuloss.Visible = false;

            openChild(ObjectFactory.GetInstance<_00021_Marca>());
           
        }

        private void btnConsArticRubro_Click(object sender, EventArgs e)
        {
            SubMenuArticuloss.Visible = false;

            openChild(ObjectFactory.GetInstance<_00019_Rubro>());
            
        }

        private void btnConsArticIva_Click(object sender, EventArgs e)
        {
            SubMenuArticuloss.Visible = false;

            openChild(ObjectFactory.GetInstance<_00025_Iva>());
           
        }

        private void btnConsArticBajaArtic_Click(object sender, EventArgs e)
        {
            SubMenuArticuloss.Visible = false;
            
            openChild(ObjectFactory.GetInstance<_00029_BajaDeArticulos>());
        }

        private void btnConsArticActPrec_Click(object sender, EventArgs e)
        {
            SubMenuArticuloss.Visible = false;
            
            openChild(ObjectFactory.GetInstance<_00031_ActualizarPrecios>());

        }

        private void btnConsArticDeposito_Click(object sender, EventArgs e)
        {
            SubMenuArticuloss.Visible = false;

            openChild(ObjectFactory.GetInstance<_00054_Deposito>());
        }

        private void ingresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            openChild(ObjectFactory.GetInstance<_00012_Configuracion>());
        }

        private void ingresarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            openChild(ObjectFactory.GetInstance<_00013_CondicionIva>());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblhora.Text = DateTime.Now.ToString("hh:mm:ss ");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        private void openChild(Form childForm)
        {
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(childForm);
            panelContenedor.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void ingresarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00038_Caja>().ShowDialog();
        }

        private void aperturaCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_cajaServicio.VerificarSiExisteCajaAbierta(Identidad.UsuarioId))
            {
                

                openChild(ObjectFactory.GetInstance<_00039_AperturaCaja>());
            }
            else
            {
                MessageBox.Show($"Se encuentra una caja habilitada para el usuario {Identidad.Apellido} {Identidad.Nombre}");
            }
}

        private void ingresarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //ObjectFactory.GetInstance<_00049_CobroDiferido>().ShowDialog();
            openChild(ObjectFactory.GetInstance<_00049_CobroDiferido>());
        }

     

        private void btnCtaCorrienteClientes_Click(object sender, EventArgs e)
        {
            openChild(ObjectFactory.GetInstance<_00034_ClienteCtaCte>());
            
        }
        //****************************************************************//




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
