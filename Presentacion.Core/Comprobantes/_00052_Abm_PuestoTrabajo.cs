using IServicios.PuestoTrabajo;
using IServicios.PuestoTrabajo.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Presentacion.Core.Comprobantes
{
    public partial class _00052_Abm_PuestoTrabajo : FormAbm
    {
        private readonly IPuestoTrabajoServicio _puestoTrabajoServicio;

        public _00052_Abm_PuestoTrabajo(TipoOperacion tipoOperacion, long? entidadId = null, IPuestoTrabajoServicio puestoTrabajoServicio = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();
            _puestoTrabajoServicio = ObjectFactory.GetInstance<IPuestoTrabajoServicio>();

        }

        public override void CargarDatos(long? entidadId)
        {
            base.CargarDatos(entidadId);



            if(entidadId.HasValue)
            {


                var resultados = (PuestoTrabajoDto)_puestoTrabajoServicio.Obtener(entidadId.Value);

                if (resultados == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");
                    Close();
                }
                txtCodigo.Text = resultados.Codigo.ToString();
                txtDescripcion.Text = resultados.Descripcion;
                


                if (TipoOperacion == TipoOperacion.Eliminar)
                    DesactivarControles(this);


            }
            else
            {
                btnEjecutar.Text = "Nuevo";

                LimpiarControles(this, false);
            }
        }


        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtCodigo.Text)) return false;
            return !string.IsNullOrEmpty(txtDescripcion.Text);
            
        }


        public override void EjecutarComandoNuevo()
        {
            var nuevoRegistro = new PuestoTrabajoDto
            {
                
                Codigo = int.Parse(txtCodigo.Text),
                Descripcion = txtDescripcion.Text,
                Eliminado = false
            };
            
            _puestoTrabajoServicio.Insertar(nuevoRegistro);
        }


        public override void EjecutarComandoModificar()
        {
            var modificarRegistro = new PuestoTrabajoDto
            {
                Id = EntidadId.Value,
                Codigo = int.Parse(txtCodigo.Text),
                Descripcion = txtDescripcion.Text,
                Eliminado = false
            };

            _puestoTrabajoServicio.Modificar(modificarRegistro);
        }



        protected override void LimpiarControles(object obj, bool tieneValorAsociado = false)
        {

            base.LimpiarControles(obj, tieneValorAsociado);

            txtCodigo.Text = _puestoTrabajoServicio.ObtenerSiguienteCodigo().ToString();

            txtDescripcion.Focus();

        }

        public override void EjecutarComandoEliminar()
        {
            _puestoTrabajoServicio.Eliminar(EntidadId.Value);
        }


    }
}
