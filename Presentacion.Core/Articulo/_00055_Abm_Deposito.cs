using IServicio.Deposito;
using IServicio.Deposito.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using System.Windows.Forms;

namespace Presentacion.Core.Articulo
{
    public partial class _00055_Abm_Deposito : FormAbm
    {
        private readonly IDepositoSevicio _depositoServicio;

        public _00055_Abm_Deposito(TipoOperacion tipoOperacion, long? entidadId = null, IDepositoSevicio depositoServicio = null  )
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();


            _depositoServicio = ObjectFactory.GetInstance<IDepositoSevicio>();


        }

        public override void CargarDatos(long? entidadId)
        {
            base.CargarDatos(entidadId);

            if(entidadId.HasValue)
            {
                var depositoResultado = (DepositoDto)_depositoServicio.Obtener(entidadId.Value);

                if(depositoResultado == null)
                {
                    MessageBox.Show("Ocurrio un error al seleccionar el deposito seleccionado");
                    Close();
                }

                txtDescripcion.Text = depositoResultado.Descripcion;
                txtUbicacion.Text = depositoResultado.Ubicacion;

                if (TipoOperacion == TipoOperacion.Eliminar)
                    DesactivarControles(this);
            }
            else
            {
                btnEjecutar.Text = "Nuevo";
            }

        }

        public override bool VerificarDatosObligatorios()
        {
            return !string.IsNullOrEmpty(txtDescripcion.Text);
            
        }

        public override void EjecutarComandoNuevo()
        {
            var nuevoDeposito = new DepositoDto();
            nuevoDeposito.Descripcion = txtDescripcion.Text;
            nuevoDeposito.Ubicacion = txtUbicacion.Text;
            nuevoDeposito.Eliminado = false;
            
            _depositoServicio.Insertar(nuevoDeposito);
        }


        public override void EjecutarComandoModificar()
        {
            var modificarDeposito = new DepositoDto();
            modificarDeposito.Id = EntidadId.Value;
            modificarDeposito.Descripcion = txtDescripcion.Text;
            modificarDeposito.Ubicacion = txtUbicacion.Text;
            modificarDeposito.Eliminado = false;

            _depositoServicio.Modificar(modificarDeposito);
        }

        public override void EjecutarComandoEliminar()
        {
            _depositoServicio.Eliminar(EntidadId.Value);
        }

        public override void LimpiarControles(Form formulario)
        {
            base.LimpiarControles(formulario);

            txtDescripcion.Focus();
            txtUbicacion.Focus();

        }



    }
}
