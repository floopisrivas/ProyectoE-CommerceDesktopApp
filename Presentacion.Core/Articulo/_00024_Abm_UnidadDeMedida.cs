using IServicio.UnidadMedida;
using IServicio.UnidadMedida.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using System.Windows.Forms;

namespace Presentacion.Core.Articulo
{
    public partial class _00024_Abm_UnidadDeMedida : FormAbm
    {
        private readonly IUnidadMedidaServicio _unidadMedidaServicio;

        public _00024_Abm_UnidadDeMedida(TipoOperacion tipoOperacion, long? entidadId = null, IUnidadMedidaServicio unidadMedidaServicio = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _unidadMedidaServicio = ObjectFactory.GetInstance<IUnidadMedidaServicio>();
            
        }

        public override void CargarDatos(long? entidadId)
        {
            base.CargarDatos(entidadId);

            if (entidadId.HasValue)
            {
                var resultado = (UnidadMedidaDto)_unidadMedidaServicio.Obtener(entidadId.Value);

                if (resultado == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");
                    Close();
                }

                txtDescripcion.Text = resultado.Descripcion;

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
            var nuevoRegistro = new UnidadMedidaDto();
            nuevoRegistro.Descripcion = txtDescripcion.Text;
            nuevoRegistro.Eliminado = false;

            _unidadMedidaServicio.Insertar(nuevoRegistro);
        }

        public override void EjecutarComandoModificar()
        {
            var modificarRegistro = new UnidadMedidaDto();
            modificarRegistro.Id = EntidadId.Value;
            modificarRegistro.Descripcion = txtDescripcion.Text;
            modificarRegistro.Eliminado = false;

            _unidadMedidaServicio.Modificar(modificarRegistro);
        }

        public override void EjecutarComandoEliminar()
        {
            _unidadMedidaServicio.Eliminar(EntidadId.Value);

        }

        public override void LimpiarControles(Form formulario)
        {

            base.LimpiarControles(formulario);

            txtDescripcion.Focus();
        }
    }
}
